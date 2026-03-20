# Match3

# Match-3 Architecture Prototype (Unity)

## Overview
This project is a **modular Match-3 game architecture prototype** built in **Unity** with a strong focus on **clean code, separation of concerns, and scalable gameplay systems**.

The goal of the project is **not just to create a Match-3 game**, but to demonstrate the ability to design:

- maintainable gameplay logic  
- deterministic board simulation  
- extensible game systems  
- clear runtime flow from input → simulation → presentation  

This repository can be used as a **portfolio project** to showcase junior level gameplay programming skills.

<p align="center">
  <img src="gif/2026-03-2015-12-12-ezgif.com-video-to-gif-converter.gif" width="600"/>
  <img src="gif/2026-03-2015-13-44-ezgif.com-video-to-gif-converter.gif" width="600"/>
  <img src="gif/2026-03-2015-13-59-ezgif.com-video-to-gif-converter.gif" width="600"/>
</p>

---

## Key Features

### Core Gameplay
- Grid-based Match-3 board
- Tile swapping validation
- Match detection (horizontal & vertical)
- Cascading system (gravity → refill → new matches)
- Shuffle system when no moves are available
- Deterministic RNG support

### Game Flow
- Move processing pipeline
- Cascade resolution loop
- Board state tracking
- Move availability checking

### Presentation Layer
- Board rendering system
- Tile visual database
- HUD controller
- Move playback controller (visual sequence handling)

### Input System
- Swap input controller
- Decoupled gameplay interaction logic

### Configuration
- Level settings (board size, rules)
- Animation settings
- Tile visual registry

---

## Architecture

The project follows a **layered architecture approach**:

Controller Layer
↓
Runtime / Application Layer
↓
Domain / Simulation Layer
↓
Data Layer


### 1. Controller Layer
Handles high-level game orchestration.

Example:
- `GameController`

Responsibilities:
- Session lifecycle
- Level loading
- Communication between UI and simulation

---

### 2. Runtime Layer (Application Logic)

Contains gameplay orchestration components:

- Bootstrap systems  
- Input handling  
- Playback / animation coordination  
- UI management  

Example modules:
- `GameBootstraper`
- `LevelLoader`
- `SwapInputController`
- `MovePlaybackController`
- `HudController`

Responsibilities:
- Convert player input into game commands
- Trigger simulation updates
- Control visual feedback

---

### 3. Simulation Layer (Domain Logic)

This is the **core of the project**.

Pure gameplay logic without Unity dependencies.

Board Systems:
- `BoardGenerator`
- `SwapValidator`
- `SwapExecutor`
- `MatchFinder`
- `ClearSystem`
- `GravitySystem`
- `RefillSystem`
- `ShuffleSystem`
- `MoveAvailabilityChecker`

Game Systems:
- `MoveProcessor`
- `CascadeResolver`
- `GameSession`

Responsibilities:
- Deterministic board updates
- Match calculation
- Cascade resolution
- Game state transitions

---

### 4. Data Layer

Contains pure data structures:

- Tile types
- Positions
- Match groups
- Move results
- Clear / spawn / shuffle results
- Game state enums

Utility:
- Custom RNG implementation for repeatable gameplay simulations

---

## Project Goals

This project demonstrates:

- Ability to design **gameplay architecture**
- Understanding of **game loop pipelines**
- Implementation of **deterministic simulation**
- Writing **testable and decoupled systems**
- Separation between **logic and visuals**
- Building scalable systems suitable for:
  - power-ups  
  - boosters  
  - level goals  
  - special tiles  
  - meta progression  

---

## Possible Future Improvements

- Special tiles (bombs, rockets, color matchers)
- Level objectives system
- Score calculation and combo multipliers
- Save / load progression
- Object pooling for tile visuals
- Editor tools for level creation
- Automated tests for board simulation
- AI solver / hint system

---

## Tech Stack

- Unity (URP compatible)
- C#
- ScriptableObjects for configuration
- Modular gameplay systems

---

## How to Run

1. Open the project in Unity Hub.
2. Load the main gameplay scene.
3. Press **Play**.
4. Interact with tiles using mouse / touch input.

---

## Author

Gameplay architecture and implementation created as a **portfolio project focused on Match-3 core mechanics and clean game system design**.

