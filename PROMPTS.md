# InteractionSystem_BurakArslan

## Prompt 1: Core System Generation

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.30

**Prompt:**
> Hi, I'm working on a Unity Interaction System for a technical case and I need to strictly follow specific coding conventions. Could you help me generate the core scripts?
>
> I need two scripts:
> 1. An interface named "IInteractable" (in namespace InteractionSystem.Runtime.Core).
> 2. A player script named "InteractionDetector" (in namespace InteractionSystem.Runtime.Player).
>
> Here are the strict requirements (Ludu Arts Standards):
> - Naming: All private fields must have an "m_" prefix. Serialized fields must be private.
> - Interface: void Interact(GameObject interactor) method and string InteractionPrompt property.
> - Conventions: Use #region blocks. Use "== null" for Unity Object checks.
> - Documentation: Add XML summary comments.

**Alınan Cevap (Özet):**
> IInteractable interface'i ve InteractionDetector scripti Ludu Arts standartlarına (m_ prefix, explicit interface vb.) uygun şekilde oluşturuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Temel sistemin iskeletini oluşturmak için kullandım. Kodlar standartlara uygundu.


## Prompt 2: Interactable Objects Logic

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.40

**Prompt:**
> Great, the core system is ready. Now I need the "Interactable Objects" implementation.
> Please generate 3 scripts in the namespace "InteractionSystem.Runtime.Interactables":
> 1. "InteractableBase" (Abstract Class): Inherits from MonoBehaviour, implements IInteractable explicitly.
> 2. "SimpleInteractable" (Class): Inherits from Base, destroys on interact.
> 3. "SwitchInteractable" (Class): Inherits from Base, toggles bool state.
> STRICT REMINDERS: Use "m_" prefix, #region blocks, and XML Documentation.

**Alınan Cevap (Özet):**
> 3 adet script (Base, Simple, Switch) oluşturuldu. Base class interface implementasyonunu yönetirken, türetilen sınıflar sadece OnInteract() metodunu override ediyor. Standartlara uygun.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim


## Prompt 3: Simple FPS Controller

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.57

**Prompt:**
> The interaction system is implemented, but I need a way to move the player to test it.
> Please generate a "SimpleFPSController" script in namespace "InteractionSystem.Runtime.Player".
> REQUIREMENTS: Inherit from MonoBehaviour, Implement WASD/Mouse Look, Strict Ludu Arts conventions (m_ prefix, #regions), Hide cursor.

**Alınan Cevap (Özet):**
> CharacterController kullanan, Ludu Arts standartlarına (m_ prefix, private serialized fields) uygun basit bir FPS kontrolcüsü oluşturuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

## Prompt 4: Complex Interactable Objects

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.05

**Prompt:**
> The basic interactions work perfectly. Now I need the complex interactable objects.
> Please generate 2 scripts in "InteractionSystem.Runtime.Interactables" and update the Base class.
> 1. UPDATE "InteractableBase.cs": Add virtual GetHoldDuration().
> 2. GENERATE "DoorInteractable.cs": Locked/Unlocked logic, requires KeyID.
> 3. GENERATE "ChestInteractable.cs": Hold logic, changes state/color on open.
> STRICT CONVENTIONS: m_ prefix, #regions, XML Documentation.

**Alınan Cevap (Özet):**
> InteractableBase sınıfına GetHoldDuration metodu eklendi. DoorInteractable kilit mekanizmasıyla, ChestInteractable ise görsel geri bildirim (renk değişimi) ile oluşturuldu.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim
