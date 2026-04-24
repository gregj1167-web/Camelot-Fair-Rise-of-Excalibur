using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem Instance;

    public int combo = 0;
    public float comboResetTime = 2f;

    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (combo > 0)
        {
            timer += Time.deltaTime;

            if (timer > comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    public void AddHit()
    {
        combo++;
        timer = 0f;

        int bonus = combo * 2;

        GameManager.Instance.AddPoints(bonus);

        Debug.Log("Combo: " + combo);
    }

    void ResetCombo()
    {
        combo = 0;
        timer = 0f;
    }
}
