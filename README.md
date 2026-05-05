# The Travelling Merchant

> Pay off your wife's 1,000,000₩ tuition bill before she loses patience. Buy low, drive your truck, haggle hard, sell high.

A solo-developed first-person trading simulation built in Unity (C#).

---

## 🎮 Gameplay Video

- **Trailer:** https://youtu.be/NzFSncqKxOA

---

## 🧭 The Loop

1. **Buy** goods from the Storehouse (starting cash: 500,000₩).
2. **Load** them onto your Truck.
3. **Drive** to customers and sell through a real-time negotiation UI.
4. **Pay** your wife to fill the tuition gauge.
5. **Win** when tuition reaches 1,000,000₩.

---

## 👤 My Role

**Solo Developer** — programming, game design, UI/UX, animation, and production.
3D models and audio were outsourced. 

---

## 🛠️ Tech Stack

- **Engine:** Unity 6000.3.5f2
- **Language:** C#
- **Key Packages:** Cinemachine, New Input System, TextMeshPro
- **Architecture:** ScriptableObject-driven content, modular gameplay systems

---

## ⚙️ Key Systems Implemented

| System | Description | Script |
|---|---|---|
| **First-Person Controller** | Movement and camera built on Cinemachine + a custom `PlayerInputHandler`. | [`FirstPersonController.cs`](Assets/Mine/Scripts/FirstPersonController.cs) |
| **Item Catalog** | 20+ tradeable items defined as `ScriptableObject` assets for designer-friendly authoring. | [`ItemData.cs`](Assets/Mine/Scripts/ItemData.cs) |
| **NPC Spawn Manager** | Spawns customers at 5 fixed points with randomized goods demand and speech bubbles. | [`NPCSpawnManager.cs`](Assets/Mine/Scripts/NPCSpawnManager.cs) |
| **Negotiation UI** | Real-time price haggling between player and NPC, with offer/counter-offer logic. | [`TradingUI.cs`](Assets/Mine/Scripts/TradingUI.cs) |
| **HUD** | Tuition gauge, currency display, and held-item slot. | [`HUDController.cs`](Assets/Mine/Scripts/HUDController.cs) |


---

## 🚀 How to Run

1. Clone the repo:
   ```bash
   git clone https://github.com/[your-username]/travelling-merchant.git
   ```
2. Open the project in **Unity Hub** with version **[6000.3.5f2]**.
3. Open the `StartMenu` scene at `Assets/Scenes/StartMenu.unity`.
4. Press **Play**.

---

## 📋 Known Issues / Roadmap

- [ ] Polish NPC pathfinding around obstacles
- [ ] Balance starting cash and item prices for difficulty curve
- [ ] Add audio integration (currently outsourced)
- [ ] Save/load testing across multiple sessions

---

## 📬 Contact

[Hwang Jeong U] — [green021129@gmail.com] — [https://www.linkedin.com/in/jeongu-hwang-2824a5210/?locale=en]
