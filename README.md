# InteractionSystem_BurakArslan


# Unity Interaction System

A modular, scalable, and optimized Interaction System developed in Unity. This project demonstrates modern C# practices, SOLID principles, and asynchronous programming patterns.

## 🎮 Key Features

- **Raycast-Based Detection:** Precise object detection from the player's camera.
- **Multiple Interaction Types:**
  - **Instant:** Switches, Levers, Simple Collectibles.
  - **Hold:** Chests (Long press logic with visual feedback).
  - **Locked:** Doors requiring specific keys from the inventory.
- **Inventory System:** Logic to collect items and use them to unlock gated content.
- **Visual Feedback (UI):** Smooth HUD updates using **DOTween** (Fade In/Out, Progress Bars, Shake Effects).

## 🏗️ Technical Architecture & Design Patterns

### 1. SOLID Principles
- **Single Responsibility Principle (SRP):** - `InteractionDetector` handles only input and detection.
  - `InteractionUI` handles only visualization.
  - Interactable objects handle only their internal state.
- **Open/Closed Principle (OCP):** New interactable types (e.g., *Lever*, *NPC*) can be added by extending `InteractableBase` without modifying the core system.
- **Interface Segregation:** The system relies on `IInteractable` interface, decoupling the detector from concrete implementations.

### 2. Design Patterns
- **Observer Pattern:** The UI system is completely decoupled from the logic. `InteractionUI` listens to events (`OnInteractableFound`, `OnInteractionFeedback`) from the Detector.
- **Dependency Injection (Method Injection):** The interacting actor (`interactor`) is passed to the `OnInteract` method, allowing objects to access the player's systems (Inventory, Detector) without heavy `FindObjectOfType` calls.

### 3. Optimization & Performance
- **UniTask Integration:** Used `UniTask` instead of Coroutines for asynchronous operations (e.g., pickup delays, backend sync simulation) to prevent Garbage Collection (GC) allocation.
- **Caching:** Components like `Renderer` or `Inventory` are cached or accessed directly via reference to avoid expensive calls like `GetComponent` or `FindObjectOfType` during runtime.
- **Event-Based Updates:** UI updates are event-driven, not polled in `Update()`.

## 📦 Third-Party Libraries

- **DOTween:** Used for high-performance UI animations (Fade, Shake, Punch).
- **UniTask:** Used for zero-allocation async/await operations.

## 🚀 How to Test

1. Clone the repository.
2. Open the **TestScene**.
3. Press **Play**.
   - Look at the **Door** -> Try to open (Locked).
   - Find the **Red Key** -> Pick it up (E).
   - Go back to **Door** -> Unlock and Open.
   - Go to **Chest** -> Hold 'E' for 3 seconds to open.

---
*Developed by Burak Arslan as a showcase of Interaction System Architecture.*


## 🔮 Future Improvements & Roadmap

While the current system is robust for functionality demonstration, the following architectural improvements are planned for a production-ready release:

1.  **ScriptableObject Integration for Item Data:**
    * *Current:* Keys and Doors interact via string-based IDs (e.g., "RedKey").
    * *Planned:* Replace strings with `ItemDataSO` (ScriptableObjects). This would eliminate typo risks and allow attaching metadata (Icons, Descriptions) to items directly in the Inspector.

2.  **Object Pooling:**
    * Instead of `Destroy(gameObject)` when picking up keys, an Object Pool system could be implemented to reuse objects, further optimizing memory allocation in scenes with hundreds of collectables.

3.  **Input System (New):**
    * Migrate from the legacy `Input.GetKeyDown` to Unity's new **Input System Package** for better cross-platform support (Gamepad/Console).
