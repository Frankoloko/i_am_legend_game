// HOW TO USE THIS SCRIPT
    // Create a game object (NOT INSIDE ANY OTHER OBJECT, HAS TO BE UNDER ROOT). Throw this script, a Mesh Filter and a Mesh Renderer onto it
    // The mesh will only be visible once you hit play. Try and hit pause and search for it if you don't see it.
    // You need to put fieldOfView.SetAimDirection(aimDirection); inside the Aim script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Francois.Utilities2D;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private GameObject rotatingObject; // The object whose Z rotation distances the view angle
    [SerializeField] private LayerMask viewBlockLayer; // The layers that will block the view/light

    private Mesh mesh;
    private float FOV;
    private float aimDirection;
    private Vector3 origin;
    private float viewDistance;

    void Start() {
        // Create a new mesh object
        mesh = new Mesh();
        // Get the mesh component on this game object
        GetComponent<MeshFilter>().mesh = mesh;
        FOV = 90f;
        viewDistance = 5f;
        origin = Vector3.zero;
    }

    void LateUpdate() {
        // IMPORTANT: I think this entire function about field of view can be removed for the enemies
        // Because the logic that checks what was hit doesn't use this field of view at all
        // This field of view is literally just there to create a cone mesh object to put a material on
        // If anything we should move this to the init so that it only creates the cone on startup

        aimDirection = FrancoisUtilities2D.GetAngleFromVector3(Vector3.zero) + FOV / 2f;
        // FrancoisUtilities2D.DebugDrawTowardsAngle(rotatingObject, aimDirection); // Leave here for debug purposes

        // Settings
        int rayCount = 50; // Increase for smoother cone edge

        // Variables
        float angleIncrease = FOV / rayCount;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        // Here we shoot out the rays from the player into the cone direction
        // Then we use the end of those rays as a way to draw the mesh
        for (int i = 0; i <= rayCount; i++) {
            // Convert aimDirection into vector3
            float angleRad = aimDirection * ( Mathf.PI / 180f );
            Vector3 vectorFromAngle = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            // Check if the light ray hits anything
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, vectorFromAngle, viewDistance, viewBlockLayer);
            if (raycastHit2D.collider == null) {
                // Didn't hit anything, set vertex at max distance
                
                vertex = origin + vectorFromAngle * viewDistance;
            } else {
                // Hit object
                vertex = raycastHit2D.point;
            }
            Debug.DrawLine(origin, vertex);
            
            vertices[vertexIndex] = vertex;

            // Our cone is actually a lot of triangles against each other
            if (i > 0) {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            aimDirection -= angleIncrease; // + or - would go clockwise or counter clockwise
        }

        // Upload data to mesh
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    float GetAngleFromVectorFloat(Vector3 direction) {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public bool CheckIfWithinView(GameObject searchObject) {
        // Get the distance from this object to the ser
        float thisObjectToPlayerDistance = Vector3.Distance(rotatingObject.transform.position, searchObject.transform.position);
        
        // Check if the distance above is within the view distance
        if (thisObjectToPlayerDistance > viewDistance) {
            // Not within view distance
            return false;
        }

        // Check if this object's view direction is within the searcObject's view direction
        Vector3 directionToSearchObject = (searchObject.transform.position - rotatingObject.transform.position).normalized;
        if (Vector3.Angle(rotatingObject.transform.right, directionToSearchObject) >= FOV / 2f) {
            return false;
        }

        // Cast a ray from this position towards the searcObject's direction
        string[] ignoreNamesContaining = new string[]{"Enemy", "BearTrap"};
        GameObject firstObjectHit = FrancoisUtilities2D.GetFirstObjectInView(rotatingObject, directionToSearchObject, viewDistance, ignoreNamesContaining);

        // Check if anything was hit at all
        if (firstObjectHit == null) {
            // Nothing was hit
            return false;
        }

        // Check if something other than the searchObject was hit
        if (firstObjectHit != searchObject) {
            // Something else was hit, but it's not the search object
            return false;
        }

        return true;
    }
}
