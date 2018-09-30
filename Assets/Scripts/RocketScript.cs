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
    [SerializeField] Light flameLight;

    [SerializeField] float levelLoadDelay = 2f;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    int currentScene;
    int maxScenes;
    int firstScene;
    bool collisionsAreEnabled = true;

    void Start () {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    void Update () {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            Rotate();
            if (Debug.isDebugBuild)
            {
                RespondToDebugKeys();
            }
            
        }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !collisionsAreEnabled) { return; }
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

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsAreEnabled = !collisionsAreEnabled;
        }
    }

    void StartSuccessSequence()
    {
        print("Landing...");
        state = State.Transcending;
        Invoke("LoadNextScene", levelLoadDelay);
        rocketAudio.PlayOneShot(success);
        successParticles.Play();
    }

    void StartDeathSequence()
    {
        state = State.Dying;
        print("Dead.");
        Invoke("Death", levelLoadDelay);
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(deathAudio);
        deathAudioParticles.Play();
    }

    void LoadNextScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        maxScenes = SceneManager.sceneCount;
        firstScene = 0;

        if (currentScene >= firstScene && currentScene != maxScenes)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            SceneManager.LoadScene(firstScene); // Add menu
        }
        
    }

    void Death()
    {
        SceneManager.LoadScene(currentScene);
    }

    void RespondToThrustInput()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) // Must repeatedly press space to stay up
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
            flameLight.enabled = false;
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
        flameLight.enabled = true;
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
