using UnityEngine;

public class JoustingGame : MonoBehaviour
{
    public GameObject targetPrefab;

    public float spawnInterval = 1.2f;
    public float spawnXRange = 4f;
    public float spawnY = 6f;

    public float gameTime = 30f;

    private float timer;
    private float gameClock;
    private bool running = true;

    void Start()
    {
        gameClock = gameTime;
    }

    void Update()
    {
        if (!running) return;

        gameClock -= Time.deltaTime;

        if (gameClock <= 0)
        {
            EndGame();
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTarget();
            timer = 0f;
        }
    }

    void SpawnTarget()
    {
        float x = Random.Range(-spawnXRange, spawnXRange);
        Vector3 pos = new Vector3(x, spawnY, 0);

        Instantiate(targetPrefab, pos, Quaternion.identity);
    }

    void EndGame()
    {
        running = false;
        Debug.Log("Joust Finished!");
    }
}
