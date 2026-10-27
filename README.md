# Kitchen Chaos

A cooking game built with Unity and C#, where players prepare ingredients, assemble dishes, and complete customer orders. I developed this project while following the Kitchen Chaos course to practice object-oriented programming, gameplay algorithms, and Unity workflows.

## Features

- Pick up ingredients, carry them, and place them on counters.
- Chop, cook, and burn ingredients using ScriptableObject recipes.
- Assemble dishes on plates, with ingredient models and UI icons.
- Track orders by unique IDs and update individual order UI entries through a Dictionary.
- Match dishes against recipes, count successful deliveries, and award bonus time.
- Tutorial screen, start countdown, game-over screen, main menu, and pause menu.
- Audio settings and keyboard rebinding with PlayerPrefs persistence.
- Movement particles, burning warnings, animations, and delivery result notifications.

## Technologies and Concepts

- Unity **6.6 / 6000.6.4f1**
- C#, Universal Render Pipeline, Input System, Cinemachine, and TextMeshPro
- Inheritance, polymorphism, interfaces, events, callbacks, and state machines
- Prefabs and ScriptableObject data assets

## Getting Started

1. Clone this repository.
2. Add the project folder in Unity Hub and open it with Unity **6000.6.4f1**.
3. Wait for Unity to restore packages and generate the Library folder.
4. Open `Assets/Scenes/MainMenuScene.unity` and enter Play mode.
5. Select Play in the main menu, then press the interaction key on the tutorial screen to start the round.

## Default Controls

| Action | Key |
| --- | --- |
| Move | W A S D / arrow keys |
| Interact / start the round | E |
| Alternate interaction / chop | F |
| Pause | Escape |

Controls can be remapped through **Options → Key Bindings** in the pause menu. Press Escape to cancel an active rebinding operation. This version focuses on keyboard and mouse input.

## Project Structure

- `Assets/Scripts`: Gameplay logic, counters, kitchen objects, UI, and audio management.
- `Assets/Prefabs`: Gameplay prefabs and data assets.
- `Assets/Scenes`: MainMenuScene, LoadingScene, and GameScene.
- `Assets/Animations`: UI and gameplay animations.
- `Packages` and `ProjectSettings`: Unity dependencies and project configuration.

Generated Unity folders such as Library, Temp, and Logs, along with IDE files and local settings, are excluded from version control. Unity `.meta` files are tracked alongside their assets to preserve references.

## Credits

This is a learning project based on Code Monkey's Kitchen Chaos course and the visual and audio assets supplied with it. Third-party assets remain the property of their respective owners.
