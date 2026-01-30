```markdown
# LLM Kullanım Dokümantasyonu

> Bu dosya, Ludu Arts Unity Intern Case süresince kullanılan Gemini (Google) etkileşimlerini belgelemektedir.

## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | 7 Ana Prompt (+ Refactoring) |
| Kullanılan araçlar | Gemini |
| En çok yardım alınan konular | Boilerplate Code, DOTween Syntax, Unity Event System |
| Tahmini LLM ile kazanılan süre | ~3-4 Saat |

---

## Prompt 1: Core System Generation (Temel Sistem)

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.30

**Prompt:**
```text
Hi, I'm working on a Unity Interaction System for a technical case and I need to strictly follow specific coding conventions. Could you help me generate the core scripts?

I need two scripts:
1. An interface named "IInteractable" (in namespace InteractionSystem.Runtime.Core).
2. A player script named "InteractionDetector" (in namespace InteractionSystem.Runtime.Player).

Here are the strict requirements (Ludu Arts Standards):
- Naming: All private fields must have an "m_" prefix. Serialized fields must be private.
- Interface: void Interact(GameObject interactor) method and string InteractionPrompt property.
- Conventions: Use #region blocks. Use "== null" for Unity Object checks.
- Documentation: Add XML summary comments.

```

**Alınan Cevap (Özet):**
IInteractable arayüzü ve InteractionDetector sınıfı, belirtilen kod standartlarına (naming conventions, regions vb.) uygun olarak oluşturuldu.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

**Açıklama:**
Projenin temel iskeletini (Interface ve Detector) hızlıca kurmak ve kod standartlarına baştan itibaren sadık kalmak için kullandım.

---

## Prompt 2: Interactable Objects Logic (Etkileşim Objeleri)

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.40

**Prompt:**

```text
Great, the core system is ready. Now I need the "Interactable Objects" implementation.
Please generate 3 scripts in the namespace "InteractionSystem.Runtime.Interactables":
1. "InteractableBase" (Abstract Class): Inherits from MonoBehaviour, implements IInteractable explicitly.
2. "SimpleInteractable" (Class): Inherits from Base, destroys on interact.
3. "SwitchInteractable" (Class): Inherits from Base, toggles bool state.
STRICT REMINDERS: Use "m_" prefix, #region blocks, and XML Documentation.

```

**Alınan Cevap (Özet):**
Soyut (Abstract) bir temel sınıf ve bundan türeyen iki farklı etkileşim sınıfı oluşturuldu. Explicit Interface Implementation mantığı doğru kuruldu.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

**Açıklama:**
Polimorfik yapıyı kurmak ve kod tekrarını önlemek adına Base Class mantığını yapay zekaya hazırlattım.

---

## Prompt 3: Simple FPS Controller

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 15.57

**Prompt:**

```text
The interaction system is implemented, but I need a way to move the player to test it.
Please generate a "SimpleFPSController" script in namespace "InteractionSystem.Runtime.Player".
REQUIREMENTS: Inherit from MonoBehaviour, Implement WASD/Mouse Look, Strict Ludu Arts conventions (m_ prefix, #regions), Hide cursor.

```

**Alınan Cevap (Özet):**
Test sahnesinde gezinebilmek için CharacterController tabanlı, basit bir FPS kontrolcüsü sağlandı.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

---

## Prompt 4: Complex Interactable Objects (Karmaşık Objeler)

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 16.15

**Prompt:**

```text
The basic interactions work perfectly. Now I need the complex interactable objects.
Please generate 2 scripts in "InteractionSystem.Runtime.Interactables" and update the Base class.
1. UPDATE "InteractableBase.cs": Add virtual GetHoldDuration().
2. GENERATE "DoorInteractable.cs": Locked/Unlocked logic, requires KeyID.
3. GENERATE "ChestInteractable.cs": Hold logic, changes state/color on open.
STRICT CONVENTIONS: m_ prefix, #regions, XML Documentation.

```

**Alınan Cevap (Özet):**
Base sınıfa "Hold" süresi eklendi. Kilitli kapı ve süreli açılan sandık mantıkları kodlandı.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

---

## Prompt 5: Interaction Detector Upgrade (Hold Logic & Events)

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 17.26

**Prompt:**

```text
The interactable objects are ready, but the "InteractionDetector" currently only supports instant interaction. I need to upgrade it to support "Hold" interactions and Event-based UI updates.
Please REWRITE the "InteractionDetector" script completely.
REQUIREMENTS:
1. Logic Updates: Check GetHoldDuration(), handle Input.GetKey for hold logic, fire OnHoldProgress events.
2. Events: OnInteractableFound, OnHoldProgress actions.
3. Strict Conventions: m_ prefix, #region blocks.

```

**Alınan Cevap (Özet):**
Event tabanlı ve "Basılı Tutma" (Hold) mantığını içeren InteractionDetector scripti oluşturuldu. Ancak AI, interface tanımını güncellemeyi atladı.

**Nasıl Kullandım:**

* [ ] Direkt kullandım
* [x] Adapte ettim
* [ ] Reddettim

**Yapılan Değişiklikler:**
Yapay zeka `GetHoldDuration` metodunu Base class'a ekledi ancak `IInteractable` arayüzüne (interface) eklemeyi unuttu. Bu durum derleme hatasına yol açtığı için `IInteractable.cs` dosyasını manuel güncelleyerek `float GetHoldDuration();` imzasını ekledim.

---

## Prompt 6: UI Feedback System (DOTween)

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 17.42

**Prompt:**

```text
The logic is solid. Now I need the UI Feedback system.
Please generate a "InteractionUI" script in "InteractionSystem.Runtime.UI".
REQUIREMENTS:
1. References: TextMeshProUGUI, Image (ProgressBar), CanvasGroup.
2. Setup: Listen to InteractionDetector events.
3. DOTween Integration: Fade CanvasGroup in/out.
4. Logic: Show/Hide UI based on events, update fillAmount.
STRICT CONVENTIONS: m_ prefix, Explicit null checks.

```

**Alınan Cevap (Özet):**
Observer Pattern kullanarak dedektörü dinleyen ve DOTween kütüphanesi ile animasyonlu (Fade In/Out) geri bildirim veren UI scripti oluşturuldu.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

---

## Prompt 7: Inventory System

**Araç:** Gemini
**Tarih/Saat:** 2026-01-30 18.10

**Prompt:**

```text
The UI and interactions are great. Now I need a simple Inventory System to handle Keys and unlocking Doors.
Please generate/update the following scripts:
1. GENERATE "Inventory.cs" (List<string> keys).
2. GENERATE "KeyInteractable.cs" (Adds key to inventory, destroys self).
3. UPDATE "DoorInteractable.cs" (Check inventory for KeyID before opening).
STRICT CONVENTIONS: m_ prefix, Null checks, XML Docs.

```

**Alınan Cevap (Özet):**
Basit bir envanter sistemi (String ID tabanlı) kuruldu ve kapı/anahtar etkileşimleri bu sisteme bağlandı.

**Nasıl Kullandım:**

* [x] Direkt kullandım
* [ ] Adapte ettim
* [ ] Reddettim

---

## 🛠️ Faz 2: Manuel Refactoring & Optimizasyon (Mühendislik Dokunuşu)

> Yapay zeka temel mantığı ve hızlı prototiplemeyi sağlasa da, kod tabanını modern mimari standartlarına (SOLID) uyumlu hale getirmek için aşağıdaki manuel iyileştirmeleri yaptım.

**1. Dependency Injection & Mimari Düzeltmesi:**

* **Sorun:** AI tarafından üretilen kodlar `FindObjectOfType` metodunu kullanıyordu. Bu performans açısından maliyetliydi.
* **Çözüm:** `Interact` metoduna `GameObject interactor` parametresi ekledim. Böylece objeler oyuncuya ve envantere doğrudan erişebildi.

**2. Observer Pattern (Gelişmiş):**

* **Çözüm:** Objeler ve UI arasındaki bağı tamamen kopardım (Decoupling). Objeler artık durumu `InteractionDetector` üzerinden bir event (`OnInteractionFeedback`) ile raporluyor.

**3. UniTask Entegrasyonu:**

* **Çözüm:** Unity Coroutine'lerinin oluşturduğu Garbage Allocation'ı önlemek için `UniTask` kütüphanesini projeye entegre ettim. `GetCancellationTokenOnDestroy()` kullanarak asenkron işlemlerin güvenliğini sağladım.

---

## Genel Değerlendirme

### LLM'in En Çok Yardımcı Olduğu Alanlar

1. **Boilerplate Kod Üretimi:** Interface tanımları, #region blokları ve XML yorum satırları gibi zaman alıcı standart yapıları çok hızlı oluşturdu.
2. **DOTween Syntax:** Animasyon kütüphanesinin syntax'ını hatırlamakla uğraşmadan hızlıca efekt eklememi sağladı.
3. **Prototipleme:** 12 saatlik süreyi verimli kullanmak adına temel mantığı (Kapı açma, switch vb.) saniyeler içinde kurdu.

### LLM'in Yetersiz Kaldığı Alanlar

1. **Mimari Bütünlük:** Kod parçalarını tek tek doğru yazsa da, Interface'e metod eklemeyi unutmak gibi sistemin bütününü ilgilendiren detayları bazen atladı.
2. **Performans:** `FindObjectOfType` veya `GetComponent` çağrılarını Update veya sık çalışan metodlar içine koyma eğilimindeydi. Bu kısımları manuel optimize etmem gerekti.

### LLM Kullanımı Hakkında Düşüncelerim

Bu case çalışmasında LLM, bir "Kod Yazarı"ndan ziyade bir "Asistan" olarak rol aldı. Temel angarya işleri ona yaptırarak, ben daha çok mimari kararlara, optimizasyona ve kodun genel kalitesine (Refactoring) odaklanabildim. LLM olmasaydı bu standartlarda bir projeyi bu sürede bitirmek çok daha zor olurdu.

---

```

```
