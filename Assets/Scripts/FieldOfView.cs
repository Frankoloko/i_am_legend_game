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
    [SerializeField] public GameObject rotatingObject; // The object whose Z rotation distances the view angle

    [SerializeField] private LayerMask viewBlockLayer; // The layers that will block the view/light
    [SerializeField] private LayerMask seeThroughLayer; // A layer that will not be caught in the "Is in view" ray
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
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(rotatingObject.transform.position, directionToSearchObject, viewDistance);

        // // Get the first object hit that is not a part of this object's parents
        // List<GameObject> allParents = FrancoisUtilities2D.GetAllParentsUpwards(this.gameObject);
        // RaycastHit2D firstObjectHit = new RaycastHit2D();
        // foreach(RaycastHit2D rayCast in raycastHit2D) {
        //     if (rayCast.collider.transform.parent) {
        //         GameObject hitObject = rayCast.collider.transform.parent.gameObject;
        //         Debug.Log(hitObject);
        //         if (!allParents.Contains(hitObject)) {
        //             firstObjectHit = rayCast;
        //         }
        //     }
        // }
        // Debug.Break();

        // RaycastHit2D firstObjectHit = new RaycastHit2D();
        // foreach(RaycastHit2D rayCast in raycastHit2D) {
        //     if (rayCast.collider.transform.parent) {
        //         GameObject hitObject = rayCast.collider.transform.parent.gameObject;
        //         Debug.Log(hitObject.layer);
        //     }
        // }
        // Debug.Break();

        // CONTINUE HERE:
        // Ek dink ek moet uitvind hoe om multiple layer op iets te sit. Want die enemies kort n "Enemies" layer, maar hulle kort ook n "SeeThrough" layer
        // Want ek moet basies kry dat enemies nie mekaar raak raycast nie
        // Unless ek net n "ignore list" hier in sit met die actual object names "Enemy" of so iets

        // Check if anything was hit at all
        if (firstObjectHit.collider == null) {
            // Nothing was hit
            return false;
        }

        // Check if something other than the searchObject was hit
        if (firstObjectHit.collider.gameObject != searchObject) {
            // Something else was hit, but it's not the search object
            return false;
        }

        return true;
    }
}
