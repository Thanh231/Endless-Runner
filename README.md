# Endless Runner 3D Game 🎮

A 3D endless runner prototype developed in Unity. The player automatically runs forward and can dodge or destroy obstacles using a gun system.

---

## 📌 Features

- ✅ Auto-forward movement with lane-based side movement (left/right).
- ✅ Side-scrolling 3D camera view.
- ✅ Jumping and shooting mechanics.
- ✅ Limited ammo system with manual reload.
- ✅ Obstacles with variable health.
- ✅ Difficulty scaling over time (increased speed).
- ❌ Game over condition with ragdoll effect. - ✅ Game over condition with fall animation.
- ✅ UI updates for health and ammo.
- ✅ Clean code architecture using design patterns:
  - Singleton (GameManager, UIManager, etc.)
  - Observer pattern via `EventManager`

---

## 🚀 How to Play

- **A / Left Arrow** – Move Left
- **D / Right Arrow** – Move Right
- **Space** – Jump
- **F** – Fire
- **R** – Reload
- Game ends when health reaches 0.

---

## 📁 Project Structure

├── Scripts/ # C# scripts (Gameplay, UI, Events, Systems)
├── Art/ # Models, prefabs, animations,Font
├── Scenes/ # Main game scene
└── ExternalAssets/ # Downloaded Unity Asset Store packages

---

## 🧱 Assets Used

All assets used are free and available in the Unity Asset Store or open sources:

- [Free Timmy Character Controller Pack](https://mixamo.com)
- [Simple Gun Pack](https://assetstore.unity.com/packages/3d/props/guns/weapons-pack-guns-310355)
- [Stylized Nature Textures](https://assetstore.unity.com/packages/2d/textures-materiais/hand-painted-textures-mega-pack-vol-1-238389)

- Custom UI designed using TextMeshPro

## 🛠️ How to Run

1. Clone or download this file.
2. Open the project in Unity **2023.2.5f1 or later**.
3. Open `Scenes/MainScene.unity`.
4. Press **Play**.

---

## 🧩 Design Patterns

- **Singleton Pattern**: Used for managing global managers like `GameManager`, `UIManager`.
- **Observer Pattern**: Implemented using `EventManager` for communication between player, UI, and game systems.

---

## 👨‍💻 Author

- Le Nguyen Cong Thanh
