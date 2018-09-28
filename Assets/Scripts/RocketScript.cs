using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RocketScript : MonoBehaviour {

    Rigidbody rb;
    bool rocketPlay;
    AudioSource rocketAudio;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip deathAudio;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathAudioParticles;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    int currentScene;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            Rotate();	
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } // ignoring collisions when dead
        switch (collision.gameObject.tag)
        {
            case "Landing":
                StartSuccessSequence();
                break;
            case "Fuel":
                print("Fueling up..."); // todo add fuel
                break;
            default:
                StartDeathSequence();
                break;
        }
    }


    void StartSuccessSequence()
    {
        print("Landing...");
        state = State.Transcending;
        Invoke("LoadNextScene", 3f);
        rocketAudio.PlayOneShot(success);
        successParticles.Play();
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        print("Dead.");
        Invoke("Death", 2f);
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(deathAudio);
        deathAudioParticles.Play();
    }

    void LoadNextScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene >= 0)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            SceneManager.LoadScene(0); // add menu and put here
        }
        
    }

    void Death()
    {
        SceneManager.LoadScene(currentScene);
    }

    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else if (rocketAudio.isPlaying)
        {
            rocketAudio.Stop();
        }
        else
        {
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float forceThisFrame = mainThrust * Time.deltaTime;
        rb.AddRelativeForce(Vector3.up * forceThisFrame, ForceMode.Force);

        if (!rocketAudio.isPlaying)
        {
            rocketAudio.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
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
