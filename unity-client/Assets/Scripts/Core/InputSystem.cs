using UnityEngine;

/// <summary>
/// InputSystem - Handles mobile and desktop input
/// 
/// Responsibilities:
/// - Swipe detection (left, center, right)
/// - Tap detection
/// - Hold detection
/// - Mobile touch input + keyboard input (for testing)
/// 
/// Usage: Attach to Canvas or persistent GameObject
/// </summary>
public class InputSystem : MonoBehaviour
{
    // =====================
    // SINGLETON PATTERN
    // =====================
    public static InputSystem Instance { get; private set; }

    // =====================
    // INPUT TYPES
    // =====================
    public enum InputAction
    {
        None,
        SwipeLeft,
        SwipeCenter,
        SwipeRight,
        Tap,
        Hold,
        Drag
    }

    public enum InputSource
    {
        Touch,
        Mouse,
        Keyboard
    }

    // =====================
    // INPUT STATE
    // =====================
    private InputAction lastInputAction = InputAction.None;
    private InputSource currentInputSource = InputSource.Keyboard;

    private Vector2 touchStartPos = Vector2.zero;
    private Vector2 touchEndPos = Vector2.zero;
    private float touchStartTime = 0f;
    private bool isTouching = false;

    // =====================
    // CONFIGURATION
    // =====================
    [SerializeField] private float swipeThreshold = 100f; // Minimum swipe distance
    [SerializeField] private float swipeTimeThreshold = 0.5f; // Maximum time for swipe
    [SerializeField] private float holdTimeThreshold = 0.3f; // Minimum time for hold
    [SerializeField] private float screenWidth = 1080f; // For swipe detection zones
    [SerializeField] private bool debugMode = true;

    // =====================
    // EVENTS
    // =====================
    public delegate void InputDetectedDelegate(InputAction action, Vector2 position);
    public event InputDetectedDelegate OnInputDetected;

    public delegate void SwipeDetectedDelegate(InputAction swipeDirection, Vector2 startPos, Vector2 endPos);
    public event SwipeDetectedDelegate OnSwipeDetected;

    public delegate void TapDetectedDelegate(Vector2 position);
    public event TapDetectedDelegate OnTapDetected;

    public delegate void HoldDetectedDelegate(Vector2 position, float duration);
    public event HoldDetectedDelegate OnHoldDetected;

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

        Log("InputSystem initialized");
    }

    private void Update()
    {
        // Detect current input source
        DetectInputSource();

        // Process input
        if (currentInputSource == InputSource.Touch)
            ProcessTouchInput();
        else if (currentInputSource == InputSource.Keyboard)
            ProcessKeyboardInput();
    }

    // =====================
    // INPUT DETECTION
    // =====================
    private void DetectInputSource()
    {
        // Check for touch input (mobile)
        if (Input.touchCount > 0)
        {
            currentInputSource = InputSource.Touch;
            return;
        }

        // Check for mouse input
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            currentInputSource = InputSource.Mouse;
            return;
        }

        // Default to keyboard
        currentInputSource = InputSource.Keyboard;
    }

    // =====================
    // TOUCH INPUT
    // =====================
    private void ProcessTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                HandleTouchBegan(touch.position);
                break;

            case TouchPhase.Moved:
                HandleTouchMoved(touch.position);
                break;

            case TouchPhase.Ended:
                HandleTouchEnded(touch.position);
                break;

            case TouchPhase.Canceled:
                HandleTouchCanceled();
                break;
        }
    }

    private void HandleTouchBegan(Vector2 position)
    {
        touchStartPos = position;
        touchStartTime = Time.time;
        isTouching = true;

        Log($"Touch began at {position}");
    }

    private void HandleTouchMoved(Vector2 position)
    {
        touchEndPos = position;
    }

    private void HandleTouchEnded(Vector2 position)
    {
        if (!isTouching)
            return;

        touchEndPos = position;
        float touchDuration = Time.time - touchStartTime;

        // Determine if swipe or tap
        float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

        if (swipeDistance > swipeThreshold && touchDuration < swipeTimeThreshold)
        {
            // Swipe detected
            ProcessSwipe(touchStartPos, touchEndPos);
        }
        else if (touchDuration < holdTimeThreshold && swipeDistance < swipeThreshold)
        {
            // Tap detected
            ProcessTap(touchEndPos);
        }
        else if (touchDuration >= holdTimeThreshold)
        {
            // Hold detected
            ProcessHold(touchEndPos, touchDuration);
        }

        isTouching = false;
    }

    private void HandleTouchCanceled()
    {
        isTouching = false;
        Log("Touch canceled");
    }

    // =====================
    // KEYBOARD INPUT (FOR TESTING)
    // =====================
    private void ProcessKeyboardInput()
    {
        // A key for swipe left
        if (Input.GetKeyDown(KeyCode.A))
        {
            ProcessSwipe(
                new Vector2(Screen.width * 0.7f, Screen.height / 2),
                new Vector2(Screen.width * 0.3f, Screen.height / 2)
            );
        }

        // S key for swipe center (up)
        if (Input.GetKeyDown(KeyCode.S))
        {
            ProcessSwipe(
                new Vector2(Screen.width / 2, Screen.height * 0.3f),
                new Vector2(Screen.width / 2, Screen.height * 0.7f)
            );
        }

        // D key for swipe right
        if (Input.GetKeyDown(KeyCode.D))
        {
            ProcessSwipe(
                new Vector2(Screen.width * 0.3f, Screen.height / 2),
                new Vector2(Screen.width * 0.7f, Screen.height / 2)
            );
        }

        // Space for tap
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProcessTap(new Vector2(Screen.width / 2, Screen.height / 2));
        }
    }

    // =====================
    // INPUT PROCESSING
    // =====================
    private void ProcessSwipe(Vector2 startPos, Vector2 endPos)
    {
        Vector2 swipeVector = endPos - startPos;
        float horizontalDistance = Mathf.Abs(swipeVector.x);
        float verticalDistance = Mathf.Abs(swipeVector.y);

        InputAction swipeAction = InputAction.None;

        // Determine swipe direction based on angle
        if (horizontalDistance > verticalDistance)
        {
            // Horizontal swipe
            if (swipeVector.x < 0)
                swipeAction = InputAction.SwipeLeft;
            else
                swipeAction = InputAction.SwipeRight;
        }
        else
        {
            // Vertical swipe (up)
            swipeAction = InputAction.SwipeCenter;
        }

        lastInputAction = swipeAction;

        Log($"Swipe detected: {swipeAction}");

        OnSwipeDetected?.Invoke(swipeAction, startPos, endPos);
        OnInputDetected?.Invoke(swipeAction, endPos);
    }

    private void ProcessTap(Vector2 position)
    {
        lastInputAction = InputAction.Tap;

        Log($"Tap detected at {position}");

        OnTapDetected?.Invoke(position);
        OnInputDetected?.Invoke(InputAction.Tap, position);
    }

    private void ProcessHold(Vector2 position, float duration)
    {
        lastInputAction = InputAction.Hold;

        Log($"Hold detected at {position} for {duration.ToString("F2")} seconds");

        OnHoldDetected?.Invoke(position, duration);
        OnInputDetected?.Invoke(InputAction.Hold, position);
    }

    // =====================
    // PUBLIC INTERFACE
    // =====================
    public InputAction GetLastInputAction()
    {
        return lastInputAction;
    }

    public InputSource GetCurrentInputSource()
    {
        return currentInputSource;
    }

    public Vector2 GetScreenCenter()
    {
        return new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public Vector2 ScreenToWorldPosition(Vector2 screenPos)
    {
        return Camera.main.ScreenToWorldPoint(screenPos);
    }

    // =====================
    // UTILITY
    // =====================
    private void Log(string message)
    {
        if (debugMode)
            Debug.Log($"[InputSystem] {message}");
    }
}
