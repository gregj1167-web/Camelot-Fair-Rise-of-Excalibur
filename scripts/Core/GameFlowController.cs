using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public JoustingGame joustingGame;
    public EggSpawner eggSpawner;

    void Start()
    {
        SetupSystems();
    }

    void SetupSystems()
    {
        Debug.Log("Initializing Camelot Event Systems...");

        // Start gameplay systems only when event is active
        InvokeRepeating(nameof(CheckEventState), 1f, 1f);
    }

    void CheckEventState()
    {
        if (EventManager.Instance.IsEventActive())
        {
            if (!joustingGame.enabled)
                joustingGame.enabled = true;

            if (!eggSpawner.enabled)
                eggSpawner.enabled = true;
        }
        else
        {
            joustingGame.enabled = false;
            eggSpawner.enabled = false;
        }
    }
}
