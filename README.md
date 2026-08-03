# Space Shooter - C# WinForms

A 2D arcade-style space shooter built entirely from scratch using C# and the Windows Forms framework. This project demonstrates core object-oriented programming principles, event-driven architecture, and real-time UI manipulation without the use of a dedicated game engine.

---

## Technical Highlights

This repository is designed to showcase fundamental software engineering concepts to technical recruiters and peers:

* **Event-Driven Game Loop:** Utilizes asynchronous `Timer` controls to manage the core game loop, handling background rendering, entity movement, and frame updates simultaneously without freezing the main UI thread.
* **Collision Detection Engine:** Implements bounding-box collision logic (`Bounds.IntersectsWith`) to accurately calculate real-time hits between player munitions, enemy ships, and enemy projectiles.
* **Efficient Object Management:** Uses array-based object pooling for entities (enemies, stars, munitions). Instead of constantly instantiating and destroying UI controls, the game toggles visibility and repositions existing elements, ensuring strict memory efficiency and preventing memory leaks.
* **State Management & Scaling Difficulty:** Features a robust state machine managing gameplay transitions. This includes active pausing, Game Over/Replay states, and dynamic difficulty scaling that adjusts enemy velocities and attack frequencies automatically as the player levels up.
* **Multimedia Integration:** Integrates the `WMPLib` (Windows Media Player API) for concurrent audio streaming, handling looped background music alongside overlapping, asynchronous sound effects.

---

## Gameplay Overview

* **Objective:** Survive incoming waves of enemy ships and projectiles while racking up points to advance through 10 progressively difficult levels.
* **Controls:** 
  * Use the **Arrow Keys** to navigate the ship across the screen.
  * Press the **Spacebar** to instantly pause or resume the game.
* **Progression:** Every 20 enemies destroyed automatically advances the player to the next level, resetting the score and increasing the challenge. Reaching Level 10 triggers the victory state.

---

## Technology Stack

* **Language:** C#
* **UI Framework:** Windows Forms (.NET)
* **Libraries:** `System.Drawing` (rendering), `WMPLib` (audio playback)
* **Architecture:** Event-Driven Desktop Application

---

## Quick Start (How to Run Locally)

1. Clone the repository.
2. Open the `Space_Game.sln` file using **Visual Studio**.
3. Ensure the `.NET Framework` workload is installed in your Visual Studio environment.
4. Press `F5` or click **Start** at the top of the editor to build and launch the game.
