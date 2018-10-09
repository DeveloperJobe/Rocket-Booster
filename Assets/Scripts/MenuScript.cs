using UnityEngine;

namespace RocketBooster
{
    public class MenuScript : MonoBehaviour
    {

        // Grab menu, score display and powerup text prefabs
        [SerializeField] GameObject menuObject, scoreDisplayer, powerupDisplayer;


        void Start()
        {
            PauseOnMenu();
        }

        // Pauses game at the start or restart
        void PauseOnMenu()
        {
            if (menuObject.activeSelf)
            {
                Time.timeScale = 0f;
                scoreDisplayer.SetActive(false);
                powerupDisplayer.SetActive(false);
            }
            else
            {
                scoreDisplayer.SetActive(true);
                powerupDisplayer.SetActive(true);
            }
        }

        // Function for play button
        public void PlayIsPressed()
        {
            powerupDisplayer.SetActive(true);
            scoreDisplayer.SetActive(true);
            menuObject.SetActive(false);
            Time.timeScale = 1f;
        }

        // Function for exit button
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
