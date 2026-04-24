const express = require("express");
const cors = require("cors");

const app = express();

// =====================
// MIDDLEWARE
// =====================
app.use(cors());
app.use(express.json());

// =====================
// ROUTES
// =====================

/**
 * Health Check
 */
app.get("/", (req, res) => {
  res.json({
    status: "running",
    service: "Camelot Fair Backend",
    version: "1.0.0",
    timestamp: new Date().toISOString()
  });
});

/**
 * Get Active Events
 */
app.get("/events", (req, res) => {
  res.json({
    currentEvent: "Camelot Grand Fair",
    weeklyTournament: {
      active: true,
      name: "Weekly Jousting Tournament",
      startDate: "2026-04-20T00:00:00Z",
      endDate: "2026-04-27T23:59:59Z",
      rewardPool: 10500,
      maxPlayers: 10000
    },
    specialEvents: [
      {
        id: "dragon-hunt-festival",
        name: "Dragon Hunt Festival",
        active: true,
        startDate: "2026-04-21T00:00:00Z",
        endDate: "2026-04-28T23:59:59Z",
        multiplier: 2.0
      }
    ]
  });
});

/**
 * Get Leaderboard
 */
app.get("/leaderboard", (req, res) => {
  const limit = Math.min(parseInt(req.query.limit) || 10, 100);
  
  const leaderboard = [
    { rank: 1, playerName: "Arthur", score: 12500, tier: "Platinum" },
    { rank: 2, playerName: "Lancelot", score: 11200, tier: "Gold" },
    { rank: 3, playerName: "Guinevere", score: 9800, tier: "Gold" },
    { rank: 4, playerName: "Merlin", score: 8600, tier: "Silver" },
    { rank: 5, playerName: "Galahad", score: 7400, tier: "Silver" },
    { rank: 6, playerName: "Bedivere", score: 6200, tier: "Bronze" },
    { rank: 7, playerName: "Mordred", score: 5100, tier: "Bronze" },
    { rank: 8, playerName: "Elaine", score: 4800, tier: "Bronze" },
    { rank: 9, playerName: "Gawain", score: 4500, tier: "Bronze" },
    { rank: 10, playerName: "Bors", score: 4200, tier: "Bronze" }
  ];

  res.json({
    tournament: "Weekly Jousting",
    week: "2026-04-20 to 2026-04-27",
    totalPlayers: 8432,
    leaderboard: leaderboard.slice(0, limit)
  });
});

/**
 * Submit Game Score
 */
app.post("/submit-score", (req, res) => {
  const { playerId, gameType, score, difficulty } = req.body;

  // Validation
  if (!playerId || !gameType || typeof score !== "number" || !difficulty) {
    return res.status(400).json({
      error: "Missing required fields: playerId, gameType, score, difficulty"
    });
  }

  // Server-side validation
  const validGameTypes = ["jousting", "dragon-hunt"];
  const validDifficulties = ["easy", "normal", "hard", "insane"];

  if (!validGameTypes.includes(gameType)) {
    return res.status(400).json({ error: "Invalid game type" });
  }

  if (!validDifficulties.includes(difficulty)) {
    return res.status(400).json({ error: "Invalid difficulty" });
  }

  // Cheat detection
  if (score > 5000 && difficulty === "easy") {
    return res.status(403).json({
      error: "Suspicious score detected",
      reason: "Score too high for difficulty level"
    });
  }

  // Apply difficulty multiplier
  const multipliers = {
    "easy": 1.0,
    "normal": 1.5,
    "hard": 2.0,
    "insane": 2.5
  };

  const adjustedScore = Math.floor(score * multipliers[difficulty]);

  res.status(201).json({
    success: true,
    playerId,
    gameType,
    difficulty,
    baseScore: score,
    adjustedScore,
    timestamp: new Date().toISOString(),
    message: "Score submitted successfully"
  });
});

/**
 * Get Player Profile
 */
app.get("/player/:playerId", (req, res) => {
  const { playerId } = req.params;

  res.json({
    playerId,
    playerName: "Player_" + playerId.slice(0, 8),
    level: 42,
    totalCoins: 15600,
    currentRank: Math.floor(Math.random() * 10000),
    gamesPlayed: 156,
    averageScore: 2340,
    badges: ["First Blood", "Dragon Slayer", "Tournament Winner"],
    joinDate: "2026-01-15T12:00:00Z"
  });
});

/**
 * Error Handling
 */
app.use((err, req, res, next) => {
  console.error("Error:", err);
  res.status(500).json({
    error: "Internal server error",
    message: err.message
  });
});

// =====================
// SERVER START
// =====================
const PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
  console.log(`
╔════════════════════════════════════╗
║   🏰 Camelot Fair Backend         ║
║   ✅ Running on port ${PORT}        ║
║   📊 Events: /events              ║
║   🏆 Leaderboard: /leaderboard    ║
║   📝 Submit Score: /submit-score  ║
╚════════════════════════════════════╝
  `);
});

module.exports = app;
