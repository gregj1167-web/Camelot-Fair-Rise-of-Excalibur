using UnityEngine;

public class DragonHuntManager : MonoBehaviour
{
    public static DragonHuntManager Instance;

    public int eggsCollected = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected(DragonEgg egg)
    {
        eggsCollected++;

        Debug.Log("Eggs Collected: " + eggsCollected);

        TriggerMagicEffect();
    }

    void TriggerMagicEffect()
    {
        Debug.Log("✨ Magic energy pulses through Camelot...");
        // Later: particle system + Merlin system unlock
    }
}
