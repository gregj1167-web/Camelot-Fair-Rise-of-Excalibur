using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 10;
    public float speed = 4f;

    public float xWobble = 1.5f;
    private float baseX;

    void Start()
    {
        baseX = transform.position.x;
    }

    void Update()
    {
        // Forward joust motion
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Slight lane wobble (makes it feel alive)
        float wobble = Mathf.Sin(Time.time * 2f) * xWobble;
        transform.position = new Vector3(baseX + wobble, transform.position.y, 0);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        GameManager.Instance.AddPoints(points);

        ComboSystem.Instance.AddHit();

        HitFeedback.Play(transform.position);

        Destroy(gameObject);
    }
}
