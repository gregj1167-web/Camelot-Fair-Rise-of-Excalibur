# 🏆 Event System: Camelot Fair

## Overview

The event system drives engagement through **weekly tournaments**, **seasonal progressions**, and **special events**. All events are anti-cheat enabled and fairly distributed.

---

## 1. Event Hierarchy

```
┌─────────────────────────────────────┐
│   SPECIAL EVENTS (Limited Time)     │
│   - Festival Events (1-2 week)      │
│   - Holiday Events (3-7 day)        │
│   - Collaboration Events            │
└─────────────────────────────────────┘
           ↑
      [Monthly]
           ↑
┌─────────────────────────────────────┐
│  SEASONAL TOURNAMENTS (30 days)     │
│  - Cumulative score leaderboard     │
│  - Ranked tiers (Bronze→Platinum)   │
│  - Final reward payout on day 30    │
└─────────────────────────────────────┘
           ↑
      [Weekly]
           ↑
┌─────────────────────────────────────┐
│  WEEKLY TOURNAMENTS (7 days)        │
│  - Sunday 00:00 UTC → Saturday 23:59│
│  - Score uploads in real-time       │
│  - Live leaderboard updates         │
│  - Reward payout Sunday 00:00 UTC   │
└─────────────────────────────────────┘
           ↑
      [Daily]
           ↑
┌─────────────────────────────────────┐
│    DAILY QUESTS & CHALLENGES        │
│    - Refresh at 00:00 UTC           │
│    - Independent of tournaments     │
│    - Bonus coins for completions    │
└─────────────────────────────────────┘
```

---

## 2. Weekly Tournament System

### Schedule & Timing

**Cycle**: Sunday 00:00 UTC → Saturday 23:59:59 UTC

```
Timeline:
Sun 00:00 ─ Previous week payouts
          ─ New tournament begins
          ─ Leaderboard resets
          ─ Players can join (auto-join on first play)
          
Wed 12:00 ─ Midweek snapshot (milestone rewards)

Sat 23:00 ─ 1-hour warning banner

Sat 23:59 ─ Tournament closes
          ─ Final scores locked

Sun 00:00 ─ Rewards distributed
          ─ New tournament begins
```

### Entry & Qualification

**Entry Method**: Automatic on first mini-game play

**Cost**: Free (no entry fee)

**Requirements**:
- Account created ≥24 hours ago
- Email verified
- No active ban

**Maximum Participants**: 10,000 (auto-cap, waitlist after)

### Leaderboard Tiers

```
Tier          | Rank    | Coins   | Item Reward
──────────────|---------|---------|-──────────────────
🥔 Platinum   | 1-10    | 5,000   | Legendary Weapon
🥈 Gold       | 11-50   | 2,000   | Epic Armor
🥉 Silver     | 51-200  | 1,000   | Rare Accessory
🧈 Bronze     | 201+    | 250     | Common Potion
```

### Scoring Mechanics

**Base Scoring**:
```
Mini-Game Score = (Game Points × Difficulty Multiplier)

Difficulty Multipliers:
- Easy:   1.0x
- Normal: 1.5x
- Hard:   2.0x
- Insane: 2.5x
```

**Weekly Total**:
```
Weekly Score = SUM(All Game Scores)
             = Game1 + Game2 + Game3 + ...
             
Example:
Jousting Hard (500 pts): 500 × 2.0 = 1,000
Dragon Hunt Normal (300 pts): 300 × 1.5 = 450
Weekly Total: 1,450 points
```

**Leaderboard Position**:
```
RANK = Rank by descending Weekly Score
TIES = Resolved by earliest submission time
```

### Real-Time Updates

```
Every Game Completion:
1. Score submitted to server
2. Anti-cheat validation (2-5 second)
3. Leaderboard updated (+2 second delay for UI)
4. Player sees ranking immediately
5. Top 50 players notified of rank changes

Update Frequency: Near real-time (5-10 second refresh)
Leaderboard Cache: 30 second TTL on client
```

---

## 3. Monthly Seasonal Tournament

### Schedule & Structure

**Duration**: 1st of Month → Last day of Month

```
Timeline:
Day 1:  Seasonal tournament opens
        Leaderboard resets
        
Day 15: Midpoint - Season emblem earned (cosmetic)
        Bonus multiplier +10% activated
        
Day 28: Final week begins
        "Last chance" notification

Day 30/31: Season closes
           Final scores locked
           24-hour reward processing
           
Day 1 (Next Month):
           Rewards distributed
           New season begins
```

### Scoring System

**Cumulative Tracking**:
```
Monthly Total = SUM(Weekly Scores from Weeks 1-4)

Example:
Week 1: 5,000 points
Week 2: 7,200 points (has midpoint +10% bonus = actual 7,920)
Week 3: 6,500 points
Week 4: 8,300 points
─────────────────────────
TOTAL:  27,000 points (approximately)
```

### Final Tier Rewards

```
Rank      | Coins    | Item          | Badge/Title
──────────┼──────────┼───────────────┼──────────────────
1         | 50,000   | Excalibur     | Season Champion
2-3       | 25,000   | Dragon Crest  | Season Runner-up
4-10      | 10,000   | Royal Crown   | Season Elite
11-50     | 2,000    | Knight's Seal | Season Notable
51-100    | 1,000    | —             | —
101-500   | 500      | —             | —
501+      | 100      | —             | —
```

### Seasonal Prestige

**At Season End**: Players earn Prestige Points

```
Prestige Calculation:
- Rank 1: +10 prestige
- Rank 2-10: +8 prestige
- Rank 11-50: +5 prestige
- Rank 51-200: +2 prestige
- Rank 201+: +1 prestige

Prestige Rewards (Cosmetics):
- 10 Prestige: Gold banner frame
- 25 Prestige: Dragon companion
- 50 Prestige: "Legendary" title
```

---

## 4. Daily Quests & Challenges

### Daily Rotation

**Reset**: Every day at 00:00 UTC

**Quest Pool**:
```
Total Slots: 3 daily quests
─────────────────────────────
Type 1: Play 3 games of Jousting
        Reward: 150 coins

Type 2: Achieve 2,000+ total points
        Reward: 200 coins

Type 3: Reach Top 100 in leaderboard
        Reward: 500 coins
```

### Difficulty Scaling

```
Quests auto-scale based on player level:

Player Level 1-10:
- Play 2 games → 100 coins
- Score 500+ → 75 coins

Player Level 50+:
- Play 5 games → 300 coins
- Score 5,000+ → 400 coins
```

### Weekly Quest Modifier

**Bonus Multiplier**: 1.5x on weekends (Sat-Sun)

```
Example:
Quest Base Reward: 200 coins
Weekend Multiplier: 200 × 1.5 = 300 coins
```

### Quest Completion Status

```
Player Dashboard:
┌─────────────────────────────────────┐
│ TODAY'S QUESTS                      │
├─────────────────────────────────────┤
│ ☑ Play 3 Jousting games (3/3)     │ +150
│ ☐ Score 2,000+ points (1,200/2,000)│ +200
│ ☐ Reach Top 100 (Rank 245)         │ +500
├─────────────────────────────────────┤
│ Total Available: 850 coins          │
└─────────────────────────────────────┘
```

---

## 5. Special Events & Festivals

### Festival Events (1-2 week limited)

**Example: "Dragon Slayer Festival"**

```
Duration: Friday → Next Thursday (10 days)

Featured Game: Dragon Egg Hunt (exclusive variant)
- 6x6 grid (harder)
- 2x coin multiplier
- Dragon eggs have special powers

Special Leaderboard:
- Top 100 only (ultra-competitive)
- Rank rewards: 500-20,000 coins

New Currency: Dragon Scales
- Earn 1-5 scales per game
- Trade scales for cosmetics
- Special Dragon Armor (cosmetic)
```

### Holiday Events (3-7 day intensive)

**Example: "Christmas Carnival"**

```
Duration: Dec 25 - Dec 31

Mechanics:
- All games 2x coins
- Gift box drops randomly (bonus +500 coins)
- Wear Christmas outfit (free temporary cosmetic)

Leaderboard: "Holiday Leaderboard"
- Separate from main leaderboard
- Top 50 earn Xmas-themed items

Special Reward: "Season's Greetings" badge
```

### Collaboration Events (Variable)

**Example: "Merlin's Magical Challenge"**

```
Partner: Game studio or brand
Duration: 2 weeks

Feature:
- New mini-game: Spell Casting
- Collaborate with Merlin character
- Unlock Merlin permanent in-game

Rewards:
- Excalibur Coins (standard)
- Partner game crossover cosmetic
- "Archmage" title (cosmetic)
```

---

## 6. Anti-Cheat System

### Real-Time Score Validation

```python
def validate_score(player_id, score, game_type, difficulty):
    """Server-side validation of submitted score"""
    
    checks = []
    
    # Check 1: Impossible scores for difficulty
    if game_type == "jousting":
        max_possible = 500 * difficulty_factor[difficulty]
        if score > max_possible:
            checks.append(("impossible_score", True))
    
    # Check 2: Session duration
    if session_duration < 10 seconds:
        score = score * 0.5  # Penalize
        checks.append(("short_session", True))
    
    # Check 3: Input frequency
    if input_frequency > 100_per_second:
        checks.append(("bot_detection", True))
        return INVALID
    
    # Check 4: Perfect scores every game
    if all_scores == 500 and game_count > 10:
        checks.append(("perfect_pattern", True))
        flag_for_review(player_id)
    
    return VALID if len(checks) < 2 else REVIEW
```

### Flagging System

**Severity Levels**:

```
LEVEL 1: Suspicious (auto-accept, flag for review)
- Short sessions (< 20 sec)
- Unusually high scores (top 0.1%)
- Pattern: Always highest difficulty

LEVEL 2: Likely Cheating (hold score 7 days)
- Multiple red flags combined
- Input frequency anomalies
- Device fingerprint matches banned account

LEVEL 3: Confirmed Cheating (score rejected)
- Bot detection
- Impossible scores
- Account farming ring detected
```

### Punishment System

```
Offense 1:
- Score invalidated
- Coins not granted
- Account flagged (7-day cooldown on tournament)
- Warning sent to player

Offense 2:
- Repeat offense = 30-day tournament ban
- Account flagged in system
- Previous scores reviewed

Offense 3:
- Permanent ban from tournaments
- All coins from flagged period removed
- Appeal process available
```

### Device Fingerprinting

**Detection Method**:
```
Fingerprint Components:
- OS type & version
- Device model
- Screen resolution
- Browser/game engine version
- IP address
- WiFi MAC address (mobile)

Alert: Multiple accounts from same fingerprint
Action: Flag accounts for linking check
```

---

## 7. Fairness & Transparency

### Leaderboard Integrity

**Weekly Audits**:
```
Every Tuesday 03:00 UTC:
1. Run anti-cheat validation on ALL scores
2. Remove flagged scores
3. Recalculate rankings
4. Notify affected players
5. Update rewards (if rank changes)
```

**Public Verification**:
- Player can view their own score calculation
- Timestamps visible (with precision to second)
- Game replay available for top 100 scores

### Appeal Process

**For Suspected False Positive**:

```
1. Player files appeal (in-game form)
2. System emails confirmation
3. Human review within 48 hours (level 3 appeals only)
4. Decision sent to player with explanation
5. If upheld: Permanent record kept
6. If overturned: Coins restored + apologetic item
```

---

## 8. Event Communication

### In-Game Notifications

```
Priority 1 (Always show):
- Tournament starting now
- You reached new rank
- Quest completed

Priority 2 (Show once):
- New event available
- Tournament ending in 1 hour
- You earned a badge

Priority 3 (Show in feed):
- Friend beat your score
- New cosmetic available
```

### Email Notifications (Optional)

**Subject**: "You reached Top 100! 🏆"
**Content**:
- Current rank
- Coins earned this week
- Next reward tier requirement
- Link to leaderboard

**Frequency**: Weekly (Sunday after payouts)

---

## 9. Event Metrics & Analytics

### Success Criteria

```
KPI                      | Target    | Current
─────────────────────────│───────────│─────────
Weekly Tournament Entry  | 80%+ DAU  | 75,000/100k
Tournament Completion    | 60%+ of entries
Average Score per Player | 2,500+    | 2,180
Top 100 Engagement       | 20+ games/week
Daily Quest Completion   | 40%+      | 38%
Special Event Uptake     | 50%+ DAU  | 45%
Leaderboard Refresh Rate | <10 sec   | 5.2 sec
```

### Monthly Review Process

```
Every 1st of Month:
1. Analyze previous month's event metrics
2. Identify top performers (reward stability)
3. Check for fraud patterns (adjust validation)
4. Player feedback collection
5. Adjust next month's events based on data
```

---

## 10. Future Event Ideas (Roadmap)

### Guild Tournaments (Q3)
```
Feature: Guilds compete against other guilds
Scoring: Team total points
Reward: Shared guild treasury
```

### Seasonal Battle Pass (Q4)
```
Feature: Time-limited progression pass
Cost: 1,000 coins or $4.99
Rewards: Exclusive cosmetics + bonus coins
```

### PvP Challenges (Q2 2027)
```
Feature: 1v1 real-time duels
Format: Best-of-3 mini-games
Reward: Glory points + cosmetic titles
```

---

## Event System Checklist

- ✅ Weekly tournaments (auto-join, real-time leaderboard)
- ✅ Monthly seasonal (cumulative, prestige system)
- ✅ Daily quests (auto-scale, weekend bonuses)
- ✅ Special events (limited-time, unique mechanics)
- ✅ Anti-cheat validation (server-side, fingerprinting)
- ✅ Punishment system (escalating consequences)
- ✅ Appeal process (human review available)
- ✅ Transparency (players see their scores)
- ✅ Analytics tracking (monthly reviews)
- ✅ Communication (in-game + email alerts)

