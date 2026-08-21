# Simple Car Physics

Small prototype of the first **Drive Mad** level. Main focus is car physics that feels familiar — not graphics or polish.

## How to run

1. Open this project in **Unity 6000.4.10f1** (Unity 6).
2. Open `Assets/Scenes/Level_01.unity`.
3. Press Play.

The scene is already in Build Settings (Editor / WebGL / mobile).

## Controls

- Forward: `D` / Right Arrow, or Gas button on screen
- Reverse: `A` / Left Arrow, or Reverse button on screen
- Restart: `R` / Space, or Restart button on screen

## What’s in the prototype

- Car with drive, inertia, center of mass, wheel spin, flips
- Level 1 style track: start, climb, finish (no obstacles)
- Win at finish, lose on flip or fall, restart reloads the level
- Keyboard + touch UI
- Cinemachine side camera

## Architecture

Kept simple on purpose.

**Zenject** wires things together.  
**R3** is for game events (win, lose, restart) so UI and gameplay don’t poke each other directly.

Rough split:

- **Core** — game rules: playing / won / lost, when the level ends
- **Presentation** — car, camera, buttons, result screen
- **Infrastructure** — input, spawning levels, settings
- **Installers** — Zenject setup for the project and the current level

Levels made as **prefabs**. One gameplay scene stays loaded (car, camera, UI, game session). To start or restart a level we spawn its prefab; it includes the track, finish, fail zones, and a **spawn point** for the car. Next level = despawn current prefab, spawn the next one, put the car on the new spawn point.

Flow: play → win/lose → restart same prefab, or load the next level prefab.

Right now the prototype is still one baked scene (`Level_01`), but the intended shape is the prefab approach above. Scripts today:

```
Assets/Scripts/
  Car/        — driving and flip checks
  Gameplay/   — win / lose / restart
  Input/      — keyboard + touch
  UI/         — HUD and result screen
```

Prefabs are under `Assets/Prefabs/`.

## Folder overview

```
Assets/
  Scenes/     — Level_01 (gameplay scene for now)
  Prefabs/    — car, track, UI, cameras (levels would live here too)
  Models/     — provided meshes
  Scripts/    — code above
  Settings/   — URP
```
