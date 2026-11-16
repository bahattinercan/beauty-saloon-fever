# Beauty Saloon Fever

Beauty Saloon Fever is a 3D mobile arcade idle salon simulation game prototype developed in Unity. Players control a salon manager using a virtual joystick to gather and stack hair salon supplies (scissors and hair dye) produced by automated dispensers (`scissorMaker` and `hairDyeMaker`), carry stacked items to service stations (`hairDresser` and `hairDyer`), collect stacks of cash generated as customers are serviced, and reinvest earned money into expanding the salon with new stations, hiring autonomous AI workers, and upgrading player and station attributes. This repository is a personal prototype project, not a maintained product, with the last substantial work completed in 2022 (`2022-09-22 14:23:28 +0300`).

## Gameplay

- **Player Movement & Direction**: Touch-based virtual joystick controls that steer the player and rotate the character model toward movement direction (`Assets/Packages/Joystick Pack/Examples/JoystickPlayerExample.cs` and `Assets/Scripts/Base/VirtualDPad.cs`).
- **Smooth Follow Camera**: Third-person perspective camera tracking the player using Lerp smoothing (`Assets/Scripts/Base/CameraFollow.cs`).
- **Locomotion & Carrying Animations**: Mecanim animation state updates toggling `isWalking` and `isCarrying` flags based on movement velocity and stacked inventory count (`Assets/Scripts/AnimationControllers/PlayerAnimController.cs` and `Assets/Scripts/AnimationControllers/WorkerAnimController.cs`).
- **Physical Item Stacking**: Vertical local stacking and capacity management for items carried by the player or AI workers (`Assets/Scripts/Stack/StackManager.cs` and `Assets/Scripts/Stack/StackTrigger.cs`).
- **Resource Dispensing & Spawner Cycles**: Timed procedural production of stackable scissors and hair dye items arranged in spatial grid stacks (`Assets/Scripts/Stack/StackSpawner.cs` and `Assets/Scripts/Stack/StackGiver.cs`).
- **Service Station Resource Ingestion**: Proximity detection that transfers required supplies from carriers into processing stations (`Assets/Scripts/Stack/StackGetter.cs`).
- **Money Production & Cash Collection**: Timed conversion of supplied materials into physical stacks of dollar bills and continuous proximity payout collection (`Assets/Scripts/Base/MoneyMaker.cs` and `Assets/Scripts/Base/MoneyGiver.cs`).
- **Station Construction Pads**: Proximity-activated purchase trigger areas with dynamic visual progress bars that consume cash to instantiate new salon buildings (`Assets/Scripts/Base/PayForBuild.cs`).
- **Player & Machine Upgrades**: Multi-tier cash upgrades that increase player stack capacity, reduce item pickup delay, and accelerate worker/machine processing intervals (`Assets/Scripts/Upgrade/Manager.cs` and `Assets/Scripts/Classes/Upgrade.cs`).
- **Autonomous Helper AI**: NavMeshAgent-driven helpers that locate supply spawners needing clearance, pick up items, and haul them to workstations needing stock (`Assets/Scripts/AI/WorkerAI.cs`, `Assets/Scripts/AI/WorkerController.cs`, and `Assets/Scripts/AI/WorkPoint.cs`).
- **Helper Staff Management & Upgrades**: Staff hiring triggers and upgrades for helper movement speed and carrying capacity (`Assets/Scripts/Upgrade/HelperManager.cs`).
- **User Interface & Currency Formatting**: Cash counter displaying abbreviated values (e.g. `$1k`, `$10k`), trigger zones opening modal upgrade panels, and dynamic button state validation (`Assets/Scripts/UI/UIManager.cs`, `Assets/Scripts/UI/UIManagerPanel.cs`, `Assets/Scripts/UI/UIHelperManagerPanel.cs`, and `Assets/Scripts/UI/TriggerUI.cs`).
- **Global Game Coordination**: Centralized singleton governing economy state, 60 FPS target frame rate, prefab lookups, and upgrade dispatch events (`Assets/Scripts/Base/GameManager.cs`).

## Tech Stack

- **Engine**: Unity `2020.3.20f1` (declared in `ProjectSettings/ProjectVersion.txt`)
- **Scripting Runtime**: C# (.NET Standard 2.0 / Unity Mono)
- **Notable Packages & Dependencies** (`Packages/manifest.json`):
  - `com.unity.textmeshpro` (`3.0.6`) — UI and world-space text rendering
  - `com.unity.modules.ai` (`1.0.0`) — NavMesh pathfinding and navigation for AI workers
  - `com.unity.device-simulator` (`3.0.3-preview`) — In-editor mobile device resolution simulation
  - `com.unity.modules.physics` (`1.0.0`) — 3D physics collisions and trigger volumes
  - `com.unity.modules.animation` (`1.0.0`) — Mecanim character animation controllers
  - `com.unity.ugui` (`1.0.0`) — Unity UI Canvas, buttons, and layouts

## Project Structure

| Path | Description | Type |
| --- | --- | --- |
| `Assets/Scripts/AI/` | AI worker behavior, NavMesh movement, and workstation coordination (`WorkerAI.cs`, `WorkerController.cs`, `WorkPoint.cs`) | Own code |
| `Assets/Scripts/AnimationControllers/` | Animation state controllers for player and worker characters (`PlayerAnimController.cs`, `WorkerAnimController.cs`) | Own code |
| `Assets/Scripts/Base/` | Core game management, camera tracking, cash spawning, building purchase pads, and input helpers (`GameManager.cs`, `CameraFollow.cs`, `MoneyMaker.cs`, `MoneyGiver.cs`, `PayForBuild.cs`, `VirtualDPad.cs`) | Own code |
| `Assets/Scripts/Classes/` | Data classes including serializable upgrade configuration (`Upgrade.cs`) | Own code |
| `Assets/Scripts/Enums/` | Enumerations defining building types and stackable item types (`EBuilding.cs`, `EStack.cs`) | Own code |
| `Assets/Scripts/Stack/` | Stacking mechanics, item spawners, pickup/delivery triggers, and stack containers (`StackManager.cs`, `StackSpawner.cs`, `StackGiver.cs`, `StackGetter.cs`, `StackTrigger.cs`) | Own code |
| `Assets/Scripts/UI/` | HUD currency displays, modal upgrade panels, and UI trigger zones (`UIManager.cs`, `UIManagerPanel.cs`, `UIHelperManagerPanel.cs`, `TriggerUI.cs`) | Own code |
| `Assets/Scripts/Upgrade/` | Upgrade purchase managers for player abilities and AI helper stats (`Manager.cs`, `HelperManager.cs`) | Own code |
| `Assets/Scenes/` | Main scene `Gameplay.unity`, baked NavMesh data, and scene lighting settings | Own scene / Data |
| `Assets/Prefabs/` | Prefabs for player, AI worker, salon workstations, dispensers, money stacks, and construction pads | Own prefabs / Data |
| `Assets/Models/` | 3D FBX models and meshes for characters and salon decor | Assets / 3D Models |
| `Assets/Materials/` | Render materials and color definitions for environment objects and props | Own materials / Data |
| `Assets/Physics/` | 3D physic materials | Data |
| `Assets/Sounds/` | Background music track `bensound-hey-no-copyright-music.mp3` | Third-party audio |
| `Assets/Sprites/` | UI upgrade icons, logos, and currency icons | Assets / Sprites |
| `Assets/Packages/Joystick Pack/` | On-screen floating and variable joystick touch controls with example scene and scripts | Third-party asset |
| `Assets/Packages/JMO Assets/Toony Colors/` | Toony Colors Free v2.0 cel shaders and materials by Jean Moreno | Third-party asset |
| `Assets/Packages/TextMesh Pro/` | TextMesh Pro font assets, default resources, shaders, and emoji atlas | Third-party asset |
| `Assets/Samples/Input System/` | Unity Input System sample assets | Third-party sample |
| `Packages/` | Unity Package Manager manifest (`manifest.json`) | Project configuration |
| `ProjectSettings/` | Unity project configurations including `ProjectVersion.txt` and `EditorBuildSettings.asset` | Project configuration |

## Running / Building

### Opening in Unity Editor

1. Install Unity **2020.3.20f1** via the Unity Hub.
2. In Unity Hub, click **Add** and select the repository root directory (`beauty-saloon-fever`).
3. Open the project using Unity Editor version `2020.3.20f1`.
4. Once packages finish resolving, navigate to and open the primary entry scene:
   - `Assets/Scenes/Gameplay.unity` (configured as scene index `0` in `ProjectSettings/EditorBuildSettings.asset`).
5. Press the **Play** button in the editor toolbar to run the game in the Game view or Device Simulator.

### Building Standalone Release

1. In the Unity Editor, select **File > Build Settings...**.
2. Ensure `Assets/Scenes/Gameplay.unity` is checked under **Scenes In Build**.
3. Select your target platform (e.g., **Android** or **PC, Mac & Linux Standalone**).
4. Click **Build** and specify the destination folder.

## Repository Notes

- **Automated Tests**: None present (no unit or integration test suites exist in the repository).
- **Continuous Integration**: None configured (no GitHub Actions workflows or automated build pipelines).
- **Commit History**: Minimal 2-commit history from September 2022 (`Initial commit` and `1`).
- **Prototype Nature**: This repository is a learning and gameplay prototype created to explore arcade idle mechanics in Unity. It was developed as a proof-of-concept and is not an actively maintained product.

## Third-Party Assets

| Folder Path | Description / Asset Name | Licence Status in Repository |
| --- | --- | --- |
| `Assets/Packages/JMO Assets/Toony Colors` | Toony Colors Free v2.0 cel shaders and textures by Jean Moreno | Contains `!ToonyColors Readme.txt` with copyright notice (`© 2019, Jean Moreno`); no separate license file. |
| `Assets/Packages/Joystick Pack` | Mobile variable and dynamic joystick components by Fenerax Studios | Contains `Documentaion.pdf`; no license file present in the repository. |
| `Assets/Packages/TextMesh Pro` | TextMesh Pro font assets and EmojiOne sprite pack | Contains `EmojiOne Attribution.txt` (EmojiOne licensed under CC-BY 4.0); no standalone license file. |
| `Assets/Sounds` | `bensound-hey-no-copyright-music.mp3` royalty-free background audio | Bensound royalty-free audio; no license file present in the repository. |
| `Assets/Sprites/UI` | `icons8-cash-64.png` icon asset | Icons8 icon asset; no license file present in the repository. |
