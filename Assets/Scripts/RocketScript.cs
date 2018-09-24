using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RocketScript : MonoBehaviour {

    Rigidbody rb;
    bool rocketPlay;
    AudioSource rocketAudio;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();	
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Landing":
                print("Landing..."); //todo remove
                break;
            case "Fuel":
                print("Fueling up..."); //todo remove
                break;
            default:
                print("Dead."); //todo kill player
                break;
        }
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!rocketAudio.isPlaying)
            {
                rocketAudio.Play();
            }
            float forceThisFrame = mainThrust * Time.deltaTime;
            rb.AddRelativeForce(Vector3.up * forceThisFrame, ForceMode.Force);
        }
        else if (rocketAudio.isPlaying)
        {
            rocketAudio.Stop();
        }
    }

    void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rb.freezeRotation = true; // Manual control

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame, Space.Self);
        }
        rb.freezeRotation = false; // Resume physics
    }
}
