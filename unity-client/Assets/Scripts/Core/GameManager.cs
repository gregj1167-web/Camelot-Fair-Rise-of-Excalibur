using UnityEngine;

/// <summary>
/// GameManager - Main game controller with state machine
/// 
/// Responsibilities:
/// - Manage game states (Menu, Playing, Paused, GameOver)
/// - Coordinate between systems (EventManager, InputSystem)
/// - Handle game flow and transitions
/// - Manage player progress and currency
/// 
/// Usage: Create GameObject "GameManager" → Add this component
/// </summary>
public class GameManager : MonoBehaviour
{
    // =====================
    // SINGLETON PATTERN
    // =====================
    public static GameManager Instance { get; private set; }

    // =====================
    // GAME STATE ENUM
    // =====================
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        Loading
    }

    // =====================
    // PLAYER DATA
    // =====================
    [System.Serializable]
    public class PlayerData
    {
        public string playerId;
        public string playerName;
        public int level = 1;
        public int coins = 0;
        public int essence = 0;
        public int totalGamesPlayed = 0;
        public int totalCoinsEarned = 0;
    }

    // =====================
    // STATE
    // =====================
    private GameState currentState = GameState.Menu;
    private PlayerData playerData = new PlayerData();
    private float gameStartTime = 0f;
    private bool isPaused = false;

    // =====================
    // CONFIGURATION
    // =====================
    [SerializeField] private bool debugMode = true;
    [SerializeField] private int coinRewardPerGame = 100;
    [SerializeField] private int essenceRewardPerGame = 10;

    // =====================
    // EVENTS
    // =====================
    public delegate void GameStateChangedDelegate(GameState newState);
    public event GameStateChangedDelegate OnGameStateChanged;

    public delegate void PlayerDataUpdatedDelegate(PlayerData data);
    public event PlayerDataUpdatedDelegate OnPlayerDataUpdated;

    public delegate void CurrencyAddedDelegate(int coins, int essence);
    public event CurrencyAddedDelegate OnCurrencyAdded;

    // =====================
    // LIFECYCLE
    // =====================
    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeGameManager();
    }

    private void Start()
    {
        Log("Game initialized. Current state: Menu");
        
        // Load player data from PlayerPrefs
        LoadPlayerData();
        
        // Set initial state to Menu
        SetGameState(GameState.Menu);
    }

    private void Update()
    {
        // Handle pause input (ESC key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                PauseGame();
            else if (currentState == GameState.Paused)
                ResumeGame();
        }
    }

    private void InitializeGameManager()
    {
        // Initialize player data
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerData.playerName = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerData.playerName = "Knight_" + Random.Range(1000, 9999);
            PlayerPrefs.SetString("PlayerName", playerData.playerName);
        }

        playerData.playerId = PlayerPrefs.GetString("PlayerId", "player_default");
        playerData.coins = PlayerPrefs.GetInt("Coins", 0);
        playerData.essence = PlayerPrefs.GetInt("Essence", 0);
        playerData.level = PlayerPrefs.GetInt("PlayerLevel", 1);
        playerData.totalGamesPlayed = PlayerPrefs.GetInt("TotalGamesPlayed", 0);
        playerData.totalCoinsEarned = PlayerPrefs.GetInt("TotalCoinsEarned", 0);

        Log($"GameManager initialized with player: {playerData.playerName}");
    }

    // =====================
    // GAME STATE MANAGEMENT
    // =====================
    public void SetGameState(GameState newState)
    {
        if (currentState == newState)
            return;

        GameState previousState = currentState;
        currentState = newState;

        Log($"Game state changed: {previousState} → {newState}");

        OnGameStateChanged?.Invoke(newState);

        // Handle state-specific logic
        switch (newState)
        {
            case GameState.Menu:
                OnMenuState();
                break;
            case GameState.Playing:
                OnPlayingState();
                break;
            case GameState.Paused:
                OnPausedState();
                break;
            case GameState.GameOver:
                OnGameOverState();
                break;
        }
    }

    private void OnMenuState()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void OnPlayingState()
    {
        gameStartTime = Time.time;
        Time.timeScale = 1f;
        isPaused = false;
        
        // Track game play
        playerData.totalGamesPlayed++;
        SavePlayerData();
    }

    private void OnPausedState()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void OnGameOverState()
    {
        Time.timeScale = 1f;
    }

    // =====================
    // GAME FLOW CONTROL
    // =====================
    public void StartGame()
    {
        SetGameState(GameState.Playing);
        Log("Game started");
    }

    public void PauseGame()
    {
        if (currentState != GameState.Playing)
            return;

        SetGameState(GameState.Paused);
        Log("Game paused");
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
            return;

        SetGameState(GameState.Playing);
        Log("Game resumed");
    }

    public void EndGame(int gameScore)
    {
        SetGameState(GameState.GameOver);
        
        float gameDuration = Time.time - gameStartTime;
        Log($"Game ended. Score: {gameScore}, Duration: {gameDuration.ToString("F2")}s");

        // Award rewards
        AwardGameRewards(gameScore);
    }

    public void ReturnToMenu()
    {
        SetGameState(GameState.Menu);
        Log("Returned to menu");
    }

    // =====================
    // REWARDS & CURRENCY
    // =====================
    public void AwardGameRewards(int gameScore)
    {
        int coinsReward = coinRewardPerGame;
        int essenceReward = essenceRewardPerGame;

        // Calculate bonus based on score
        if (gameScore > 1000)
            coinsReward = Mathf.FloorToInt(coinsReward * 1.5f);
        else if (gameScore > 500)
            coinsReward = Mathf.FloorToInt(coinsReward * 1.2f);

        AddCurrency(coinsReward, essenceReward);
        Log($"Rewards awarded: +{coinsReward} coins, +{essenceReward} essence");
    }

    public void AddCurrency(int coins, int essence)
    {
        playerData.coins += coins;
        playerData.essence += essence;
        playerData.totalCoinsEarned += coins;

        SavePlayerData();
        OnCurrencyAdded?.Invoke(coins, essence);
        OnPlayerDataUpdated?.Invoke(playerData);
    }

    public void SpendCoins(int amount)
    {
        if (playerData.coins < amount)
        {
            Log($"Insufficient coins. Required: {amount}, Available: {playerData.coins}");
            return;
        }

        playerData.coins -= amount;
        SavePlayerData();
        OnPlayerDataUpdated?.Invoke(playerData);
    }

    // =====================
    // PLAYER PROGRESSION
    // =====================
    public void LevelUp()
    {
        playerData.level++;
        SavePlayerData();
        OnPlayerDataUpdated?.Invoke(playerData);
        Log($"Player leveled up to {playerData.level}");
    }

    public void AddExperience(int xp)
    {
        // Placeholder for XP system
        Log($"Added {xp} experience");
    }

    // =====================
    // DATA PERSISTENCE
    // =====================
    private void LoadPlayerData()
    {
        playerData.coins = PlayerPrefs.GetInt("Coins", 0);
        playerData.essence = PlayerPrefs.GetInt("Essence", 0);
        playerData.level = PlayerPrefs.GetInt("PlayerLevel", 1);
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Coins", playerData.coins);
        PlayerPrefs.SetInt("Essence", playerData.essence);
        PlayerPrefs.SetInt("PlayerLevel", playerData.level);
        PlayerPrefs.SetInt("TotalGamesPlayed", playerData.totalGamesPlayed);
        PlayerPrefs.SetInt("TotalCoinsEarned", playerData.totalCoinsEarned);
        PlayerPrefs.Save();
    }

    // =====================
    // PUBLIC INTERFACE
    // =====================
    public GameState GetGameState()
    {
        return currentState;
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public int GetCoins()
    {
        return playerData.coins;
    }

    public int GetEssence()
    {
        return playerData.essence;
    }

    public int GetPlayerLevel()
    {
        return playerData.level;
    }

    public string GetPlayerName()
    {
        return playerData.playerName;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // =====================
    // UTILITY
    // =====================
    private void Log(string message)
    {
        if (debugMode)
            Debug.Log($"[GameManager] {message}");
    }
}
