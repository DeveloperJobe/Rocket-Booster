using UnityEngine;

namespace RocketBooster
{
    public class TreePool : MonoBehaviour
    {

        [SerializeField] GameObject tree1, tree2, rocket;
        int treePoolSize = 10;
        float spawnRate = 3f;
        float treeSizeMin = .8f;
        float treeSizeMax = 1.5f;

        GameObject[] trees;
        int currentTree = 0;

        Vector3 objectPoolPosition = new Vector3(20f, 0f, -100f);
        float spawnXPosition, spawnYPosition, spawnZPosition;

        float timeSinceLastSpawned;

        // Use this for initialization
        void Start()
        {
            LoopThroughTrees();
            spawnYPosition = 0f;

        }

        // Update is called once per frame
        void Update()
        {
            RocketPosition();
            TreeSpawning();
        }

        void RocketPosition()
        {
            spawnXPosition = rocket.transform.position.x + 100;
        }

        void LoopThroughTrees()
        {
            trees = new GameObject[treePoolSize];

            for (int i = 0; i < treePoolSize; i++)
            {
                trees[i] = (GameObject)Instantiate(tree1, objectPoolPosition, Quaternion.identity);
                trees[i] = (GameObject)Instantiate(tree2, objectPoolPosition, Quaternion.identity);
            }
        }

        void TreeSpawning()
        {
            spawnZPosition = Random.Range(10f, 250f);
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;
                trees[currentTree].transform.position = new Vector3(spawnXPosition, spawnYPosition, spawnZPosition);
                currentTree++;
                if (currentTree >= treePoolSize)
                {
                    currentTree = 0;
                }
            }
        }
    }
}
