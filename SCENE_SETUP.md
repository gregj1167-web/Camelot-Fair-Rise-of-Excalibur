# 🎮 Scene Setup Guide: Jousting Mini-Game

This guide shows how to set up a playable Jousting scene in Unity.

---

## 📋 Prerequisites

✅ GameManager.cs
✅ EventManager.cs
✅ InputSystem.cs
✅ JoustingGame.cs
✅ JoustingTarget.cs
✅ JoustingUIController.cs

All scripts should be in `Assets/Scripts/`

---

## 🎬 Scene Setup Steps

### **Step 1: Create a New Scene**

1. In Unity, create a new scene: `File → New Scene`
2. Save as: `Assets/Scenes/JoustingDemo.unity`

---

### **Step 2: Create Core GameObjects**

In the Hierarchy, create these GameObjects:

| GameObject | Components | Purpose |
|------------|------------|---------|
| GameManager | GameManager.cs | Player progression |
| EventManager | EventManager.cs | Leaderboard sync |
| InputSystem | InputSystem.cs | Input detection |
| JoustingGame | JoustingGame.cs | Game controller |
| Canvas | Canvas, JoustingUIController.cs | UI overlay |

---

### **Step 3: Detailed Setup**

#### **GameManager Setup**
1. Right-click in Hierarchy → Create Empty → Rename to "GameManager"
2. Add Component → Search "GameManager.cs" → Add
3. Inspector settings:
   - Debug Mode: ✅ (checked)
   - Coin Reward Per Game: 100
   - Essence Reward Per Game: 10

#### **EventManager Setup**
1. Right-click → Create Empty → Rename to "EventManager"
2. Add Component → "EventManager.cs"
3. Inspector settings:
   - Backend URL: http://localhost:3000
   - Debug Mode: ✅
   - Use Mock Data: ✅ (for testing offline)

#### **InputSystem Setup**
1. Right-click → Create Empty → Rename to "InputSystem"
2. Add Component → "InputSystem.cs"
3. Inspector settings:
   - Swipe Threshold: 100
   - Debug Mode: ✅

#### **JoustingGame Setup**
1. Right-click → Create Empty → Rename to "JoustingGame"
2. Add Component → "JoustingGame.cs"
3. Inspector settings:
   - Easy Settings → (Leave defaults)
   - Normal Settings → (Leave defaults)
   - Hard Settings → (Leave defaults)
   - Insane Settings → (Leave defaults)
   - Debug Mode: ✅
   - Target Prefab: (Leave empty for testing, we'll add later)

#### **Canvas Setup**
1. Right-click → UI → Canvas → Rename to "Canvas"
2. Add Component → "JoustingUIController.cs"
3. In Canvas, create UI elements:

**Create Score Text:**
- Right-click Canvas → UI → Legacy → Text
- Rename to "ScoreText"
- Position: Top-Left
- Text: "0"
- Font Size: 40
- Color: White

**Create Combo Text:**
- Right-click Canvas → UI → Legacy → Text
- Rename to "ComboText"
- Position: Top-Right
- Text: "x0"
- Font Size: 50
- Color: Yellow

**Create Timer Text:**
- Right-click Canvas → UI → Legacy → Text
- Rename to "TimerText"
- Position: Top-Center
- Text: "01:00"
- Font Size: 60
- Color: White

**Create Difficulty Text:**
- Right-click Canvas → UI → Legacy → Text
- Rename to "DifficultyText"
- Position: Bottom-Left
- Text: "Difficulty: Normal"
- Font Size: 30
- Color: White

**Assign UI Elements to JoustingUIController:**
1. Select Canvas in Hierarchy
2. In Inspector, find JoustingUIController component
3. Drag ScoreText → Score Text field
4. Drag ComboText → Combo Text field
5. Drag TimerText → Timer Text field
6. Drag DifficultyText → Difficulty Text field

---

### **Step 4: Create Target Prefab**

For the jousting game to spawn enemies:

1. Create a 3D GameObject: Right-click → 3D Object → Cube
2. Rename to "JoustingTarget"
3. Add Component → "JoustingTarget.cs"
4. Add Component → "SpriteRenderer" (or keep Cube for placeholder)
5. Scale: Set to (0.5, 0.5, 1) to make it smaller
6. Color: Make it red (represent enemy knight)

**Drag to Prefabs:**
1. Create folder: `Assets/Prefabs/`
2. Drag "JoustingTarget" from Hierarchy → `Assets/Prefabs/`
3. Delete from scene (we'll spawn it)

**Assign Prefab to JoustingGame:**
1. Select JoustingGame in Hierarchy
2. In Inspector, find JoustingGame component
3. Drag prefab to "Target Prefab" field

---

### **Step 5: Scene Hierarchy (Complete)**

Your final hierarchy should look like:

```
JoustingDemo Scene
├── GameManager
├── EventManager
├── InputSystem
├── JoustingGame
└── Canvas
    ├── ScoreText
    ├── ComboText
    ├── TimerText
    └── DifficultyText
```

---

## ▶️ Testing the Scene

### **Play Button in Unity**

1. Press **Play** button in Unity
2. Watch Console for initialization logs:

```
[GameManager] GameManager initialized with player: Knight_5432
[EventManager] EventManager initialized with PlayerId: player_default
[InputSystem] InputSystem initialized
[JoustingGame] JoustingGame initialized
```

### **Start a Game (Console Command)**

In the Scene, add a test script or use the Console to call:

```csharp
JoustingGame.Instance.StartGame("normal");
```

### **Keyboard Controls**

- **A** = Swipe Left
- **S** = Swipe Center
- **D** = Swipe Right
- **Space** = Tap
- **ESC** = Pause

### **Expected Behavior**

1. Press Play
2. System initialize
3. Press **A** (swipe left)
   - Console: `[InputSystem] Swipe detected: SwipeLeft`
   - UI: Score updates if target defeated
4. Targets spawn every 1-2 seconds
5. Hit targets to gain points
6. Combo increases with consecutive hits
7. Time runs out → Game Over

---

## 🎨 Visual Feedback (Optional)

To make it feel more like a game:

### **Add Animations**

1. Right-click → Create Folder → "Animations"
2. Create "Knight" animation with movement frames
3. Assign to JoustingTarget

### **Add Particle Effects**

1. Create particle effect for target defeat
2. Instantiate in `JoustingTarget.Defeat()`

### **Add Sound Effects**

1. Create folder: `Assets/Audio/`
2. Add SFX for: hit, defeat, combo
3. Play in respective methods

---

## 🐛 Troubleshooting

| Problem | Solution |
|---------|----------|
| "JoustingGame instance is null" | Make sure script finds/creates the singleton |
| "UI Text not updating" | Check that UI elements are assigned in inspector |
| "No input detected" | Make sure InputSystem is in scene, test with keyboard keys |
| "Targets not spawning" | Check Target Prefab is assigned in JoustingGame inspector |
| "Game doesn't end" | Check timer logic in JoustingGame.Update() |

---

## 📊 Next Steps

1. **Add Graphics**: Create knight sprite, background, UI buttons
2. **Add Sounds**: Background music, hit effects, combo alerts
3. **Add Menus**: Start screen, pause screen, game over screen
4. **Add Polish**: Animations, particles, screen shake
5. **Test on Mobile**: Build to iOS/Android and test touch input

---

## 🎯 Scene is Ready!

Your Jousting mini-game is now fully playable and connected to:
- ✅ Player progression (GameManager)
- ✅ Leaderboards (EventManager)
- ✅ Mobile input (InputSystem)
- ✅ Real-time UI updates

**Press Play and start jousting! 🐎⚔️**
