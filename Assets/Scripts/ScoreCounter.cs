using TMPro;
using UnityEngine;

namespace RocketBooster {
    [RequireComponent(typeof(Collider))]

    public class ScoreCounter : MonoBehaviour {

        [SerializeField] GameObject textAsset;
        public int score = 0;

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
