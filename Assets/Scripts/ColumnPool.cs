using UnityEngine;

namespace RocketBooster
{
    public class ColumnPool : MonoBehaviour
    {

        [SerializeField] GameObject rocket;
        public GameObject columnPrefab;                                 // The column game object.
        public int columnPoolSize = 5;                                  // How many columns to keep on standby.
        public float spawnRate = 3f;                                    // How quickly columns spawn.
        public float columnMin = -1f;                                   // Minimum y value of the column position.
        public float columnMax = 3.5f;

        private GameObject[] columns;                                   // Collection of pooled columns.
        private int currentColumn = 0;

        private Vector3 objectPoolPosition = new Vector3(20f, 0f, -25f);     // A holding position for our unused columns.
        private float spawnXPosition;
        private float spawnZPosition;

        private float timeSinceLastSpawned;

        void Start()
        {
            LoopThroughArray();
        }

        void Update()
        {
            GetRocketPosition();
            ColumnSpawning();
        }

        void GetRocketPosition()
        {
            spawnXPosition = rocket.transform.position.x + 100;
        }

        void LoopThroughArray()
        {
            //Initialize the columns collection.
            columns = new GameObject[columnPoolSize];
            //Loop through the collection... 
            for (int i = 0; i < columnPoolSize; i++)
            {
                //...and create the individual columns.
                columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);

            }
        }

        void ColumnSpawning()
        {
            timeSinceLastSpawned += Time.deltaTime;

            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPosition = Random.Range(columnMin, columnMax);

                //...then set the current column to that position.
                columns[currentColumn].transform.position = new Vector3(spawnXPosition, spawnYPosition, spawnZPosition);

                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                currentColumn++;

                if (currentColumn >= columnPoolSize)
                {
                    currentColumn = 0;
                }
            }
        }
    }
}
