using TMPro;
using UnityEngine;

namespace RocketBooster {
    [RequireComponent(typeof(Collider))]

    public class ScoreCounter : MonoBehaviour {

        [SerializeField] GameObject textAsset;
        public int score;

        private void OnTriggerExit(Collider collider)
        {
            if (collider.tag == "Score")
            {
                score++;
                textAsset.GetComponent<TextMeshProUGUI>().text = "Score: " + score;

            }


        }
    }
}
