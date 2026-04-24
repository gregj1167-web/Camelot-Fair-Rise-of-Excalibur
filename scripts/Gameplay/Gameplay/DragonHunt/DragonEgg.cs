using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public int points = 25;
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnMouseDown()
    {
        Collect();
    }

    void Collect()
    {
        GameManager.Instance.AddPoints(points);

        DragonHuntManager.Instance.OnEggCollected(this);

        Destroy(gameObject);
    }
}
