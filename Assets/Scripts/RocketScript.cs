using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace RocketBooster
{
    [RequireComponent(typeof(AudioSource))]

    public class RocketScript : MonoBehaviour
    {

        Rigidbody rb;
        bool rocketPlay;
        AudioSource rocketAudio;

        [SerializeField] float mainThrust = 100f;
        [SerializeField] AudioClip mainEngine;
        [SerializeField] AudioClip success;
        [SerializeField] AudioClip deathAudio;

        [SerializeField] ParticleSystem mainEngineParticles;
        [SerializeField] ParticleSystem successParticles;
        [SerializeField] ParticleSystem deathAudioParticles;
        [SerializeField] Light flameLight;

        [SerializeField] float levelLoadDelay = 2f;
        [SerializeField] GameObject powerup;
        [SerializeField] GameObject textAsset;
        string powerupText;
        int currentScore;



        enum State { Alive, Dying, Transcending }
        State state = State.Alive;
        int currentScene;
        int maxScenes;
        int firstScene;
        public bool collisionsAreEnabled = true;
        bool powerupActivated = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rocketAudio = GetComponent<AudioSource>();
            powerupText = textAsset.GetComponent<TextMeshProUGUI>().text;
        }

        void Update()
        {
            if (state == State.Alive)
            {
                RespondToThrustInput();
                if (Debug.isDebugBuild)
                {
                    RespondToDebugKeys();
                }

            }
            if (powerupActivated)
            {
                SlowDownTime();
            }

        }

        void SlowDownTime()
        {
            float startTime = Time.time;
            if (startTime < 5f)
            {
                Time.timeScale = .5f;
                powerupText = powerupText + "Time Slowed for 5 seconds";

            }
            else if (startTime >= 5f)
            {
                Time.timeScale = 1f;
                powerupActivated = false;
            }


        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Powerup")
            {
                powerup.SetActive(false);
                powerupActivated = true;

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
                    print("Fueling up..."); // todo add fuel pickup
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
                SceneManager.LoadScene(firstScene); // todo Add menu

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
            if (transform.rotation.z < 40)
            {
                transform.Rotate(Vector3.forward);
            }
            else if (transform.rotation.z > 20)
            {
                transform.Rotate(-Vector3.forward);
            }


            if (!rocketAudio.isPlaying)
            {
                rocketAudio.PlayOneShot(mainEngine);
            }

            mainEngineParticles.Play();
            flameLight.enabled = true;
        }


    }
}
