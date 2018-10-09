using UnityEngine;

namespace RocketBooster
{
    public class RotateScript : MonoBehaviour
    {

        [SerializeField] float rcsThrust = 100f;
        Rigidbody rb;
        RocketScript rs;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rs = GetComponent<RocketScript>();

            
        }

        // Update is called once per frame
        void Update()
        {
            Rotate();
        }

        void Rotate()
        {
            float rotationThisFrame = rcsThrust * Time.deltaTime;
            

            if (Input.GetKey(KeyCode.A) && rs.collisionsAreEnabled)
            {
                rb.freezeRotation = true; // Manual control
                transform.Rotate(Vector3.forward * rotationThisFrame, Space.Self);
            }
            else if (Input.GetKey(KeyCode.D) && rs.collisionsAreEnabled)
            {
                rb.freezeRotation = true; // Manual control
                transform.Rotate(-Vector3.forward * rotationThisFrame, Space.Self);
            }
            rb.freezeRotation = false; // Resume physics
        }
    }
}
