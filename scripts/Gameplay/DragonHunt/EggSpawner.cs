using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;

    public float spawnInterval = 3f;
    public float spawnRangeX = 6f;
    public float spawnY = 4f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEgg();
            timer = 0f;
        }
    }

    void SpawnEgg()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 pos = new Vector3(x, spawnY, 0);

        Instantiate(eggPrefab, pos, Quaternion.identity);
    }
}
