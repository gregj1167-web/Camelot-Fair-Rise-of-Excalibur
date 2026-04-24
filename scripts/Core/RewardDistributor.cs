using UnityEngine;

public class RewardDistributor : MonoBehaviour
{
    public static RewardDistributor Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DistributeRewards()
    {
        int score = GameManager.Instance.eventPoints;

        int gold = score / 10;
        int essence = score / 25;

        Debug.Log("💰 Rewards Granted:");
        Debug.Log("Gold: " + gold);
        Debug.Log("Essence: " + essence);

        TriggerMerlinCheck(score);
    }

    void TriggerMerlinCheck(int score)
    {
        if (score > 200)
        {
            Debug.Log("🧙 Merlin awakens...");
            // future unlock hook
        }
    }
}
