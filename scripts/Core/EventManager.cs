using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public EventPhase currentPhase = EventPhase.Lobby;

    public float eventDuration = 60f;
    private float timer;
    private bool running;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartEvent();
    }

    void Update()
    {
        if (!running) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            EndEvent();
        }
    }

    public void StartEvent()
    {
        currentPhase = EventPhase.Active;
        timer = eventDuration;
        running = true;

        Debug.Log("🏰 Camelot Fair has begun!");
    }

    public void EndEvent()
    {
        currentPhase = EventPhase.Results;
        running = false;

        Debug.Log("🏁 Event ended. Calculating rewards...");

        RewardDistributor.Instance.DistributeRewards();
    }

    public bool IsEventActive()
    {
        return currentPhase == EventPhase.Active;
    }
}
