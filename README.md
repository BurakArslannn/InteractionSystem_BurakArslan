```markdown
# Interaction System - Burak Arslan

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 6000.0.23f1 |
| Render Pipeline | URP |
| Case Süresi | ~12 Saat |
| Tamamlanma Oranı | %100 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/BurakArslannn/InteractionSystem_BurakArslan.git
```

2. Unity Hub'da projeyi açın.
3. `Assets/InteractionSystem_Burak/Scenes/TestScene.unity` sahnesini açın.
4. Play tuşuna basın.

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
| --- | --- |
| WASD | Karakter Hareketi |
| Mouse | Kamera Bakış Yönü |
| E | Etkileşim (Bas-Çek veya Basılı Tut) |
| ESC | Çıkış |

### Test Senaryoları

1. **Door Test (Kilitli Değil):**
* Kapıya yaklaşın, ekranda "Interact" mesajını ve DOTween animasyonunu görün.
* 'E' tuşuna basın, kapının açıldığını doğrulayın.


2. **Key + Locked Door Test:**
* Kilitli kapıya (Locked) yaklaşın, etkileşime girmeye çalışın.
* UI üzerinde **"Requires Key: RedKey"** hatasını ve titreme (Shake) efektini görün.
* Yerdeki kırmızı anahtarı (SimpleInteractable/Key) bulun ve 'E' ile toplayın.
* UI üzerinde **"Picked up: RedKey"** mesajını görün.
* Kapıya dönün ve açın.


3. **Switch Test:**
* Switch nesnesine yaklaşın ve basın.
* UI üzerinde durumun "Switch ON/OFF" olarak değiştiğini gözlemleyin.


4. **Chest Test (Hold Interaction):**
* Sandığa yaklaşın.
* 'E' tuşuna **basılı tutun**.
* Progress Bar'ın (Yuvarlak bar) dolduğunu ve dolunca sandığın açıldığını (Renginin değiştiğini) görün.



---

## Mimari Kararlar

### Interaction System Yapısı

Sistem, **SOLID** prensipleri gözetilerek modüler bir yapıda kurulmuştur.

* **Core:** `IInteractable` arayüzü tüm etkileşimlerin temelini oluşturur.
* **Player:** `InteractionDetector` sadece raycast ve input dinler (SRP).
* **UI:** `InteractionUI`, Observer Pattern ile dedektörden gelen eventleri dinler.
* **Interactables:** `Door`, `Chest`, `Key` gibi nesneler `InteractableBase` sınıfından türetilmiştir.

**Neden bu yapıyı seçtim:**

> Projenin genişletilebilir olması (Scalability) önceliğimdi. Yeni bir etkileşim türü (örn: Lever, NPC) eklenmek istendiğinde, mevcut `InteractionDetector` veya `UI` kodlarında değişiklik yapılmasına gerek yoktur (Open/Closed Principle).

**Alternatifler:**

> Monolithic (Tek parça) bir "PlayerController" içinde tüm `if(hit.tag == "Door")` mantığını yazmak.

**Neden seçmedim:**

> Bu yaklaşım, proje büyüdükçe yönetilemez (Spaghetti Code) hale gelirdi ve SOLID prensiplerine aykırı olurdu.

**Trade-off'lar:**

> Event-based ve Interface-based yapı, başlangıçta kurulum maliyeti (boilerplate code) yaratır ancak uzun vadede bakım maliyetini düşürür.

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
| --- | --- | --- |
| **Observer Pattern** | `InteractionDetector` -> `InteractionUI` | UI ve Logic sistemlerini birbirinden tamamen ayırmak (Decoupling) için. |
| **Dependency Injection** | `OnInteract(GameObject interactor)` | Objelerin, oyuncu üzerindeki sistemlere (Inventory) `FindObjectOfType` kullanmadan erişebilmesi için. |
| **Template Method** | `InteractableBase` | Ortak mantığı (Feedback gönderme vb.) base class'ta tutup, özel mantığı türetilmiş sınıflara bırakmak için. |

---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
| --- | --- | --- |
| m_ prefix (private fields) | [x] | Tüm private değişkenlerde uygulandı. |
| s_ prefix (private static) | [x] | (Projede static field ihtiyacı olmadı) |
| k_ prefix (private const) | [x] |  |
| Region kullanımı | [x] | Fields, Methods, Unity Methods olarak ayrıldı. |
| Region sırası doğru | [x] |  |
| XML documentation | [x] | Tüm class ve kritik metodlara eklendi. |
| Silent bypass yok | [x] | TryGetComponent kullanıldı. |
| Explicit interface impl. | [x] | `IInteractable.Interact` explicit uygulandı. |

### Naming Convention

| Kural | Uygulandı | Örnekler |
| --- | --- | --- |
| P_ prefix (Prefab) | [x] | P_DoorInteractable, P_Chest, P_Player |
| M_ prefix (Material) | [x] | M_Red, M_Green, M_Chest |
| T_ prefix (Texture) | [ ] | (Texture asset kullanılmadı) |
| SO isimlendirme | [ ] | (ScriptableObject kullanılmadı - *Bkz. Limitasyonlar*) |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
| --- | --- | --- |
| Transform (0,0,0) | [x] |  |
| Pivot bottom-center | [x] |  |
| Collider tercihi | [x] | Performans için Box Collider tercih edildi. |
| Hierarchy yapısı | [x] | Container -> Visuals/Colliders yapısı. |

### Zorlandığım Noktalar

> Event-based UI sistemini DOTween animasyonları ile senkronize ederken (Feedback animasyonu oynarken yeni interaction gelmesi durumu) state yönetimine dikkat etmem gerekti.

---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

* [x] Core Interaction System
* [x] IInteractable interface
* [x] InteractionDetector
* [x] Range kontrolü


* [x] Interaction Types
* [x] Instant (Switch, Key)
* [x] Hold (Chest)
* [x] Toggle (Door)


* [x] Interactable Objects
* [x] Door (locked/unlocked)
* [x] Key Pickup
* [x] Switch/Lever
* [x] Chest/Container


* [x] UI Feedback
* [x] Interaction prompt (Dinamik Text)
* [x] Dynamic text (DOTween)
* [x] Hold progress bar
* [x] Cannot interact feedback (Locked uyarısı)


* [x] Simple Inventory
* [x] Key toplama
* [x] Envanter kontrolü



### Bonus (Nice to Have) / Ekstra Özellikler

* [x] **UniTask Integration:** Unity Coroutine yerine allocation-free async/await yapısı kullanıldı.
* [x] **DOTween Animations:** UI geçişleri, Shake efektleri ve Punch efektleri eklendi.
* [x] **Optimization:** `FindObjectOfType` kullanımları kaldırılarak Dependency Injection ve Caching mekanizmaları kuruldu.

---

## Bilinen Limitasyonlar & Roadmap

### İyileştirme Önerileri (Future Roadmap)

1. **ScriptableObject Entegrasyonu:**
* *Mevcut:* Anahtar ve Kapı eşleşmesi `string m_KeyID` ("RedKey") üzerinden yapılıyor.
* *Öneri:* `ItemDataSO` (ScriptableObject) kullanılarak Tip Güvenliği (Type Safety) sağlanabilir ve item ikonları eklenebilir.


2. **Input System Package:**
* *Mevcut:* `Input.GetKeyDown` (Legacy) kullanıldı.
* *Öneri:* Cross-platform destek için New Input System paketine geçilebilir.


3. **Object Pooling:**
* *Öneri:* Çok sayıda toplanabilir eşya (Coin vb.) olması durumunda Instantiate/Destroy yerine Object Pool kullanılmalıdır.



---

## Dosya Yapısı

```
Assets/
├── InteractionSystem_Burak/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/         (Interfaces)
│   │   │   ├── Interactables/ (Door, Chest, Key logic)
│   │   │   ├── Player/       (Detector, Inventory)
│   │   │   └── UI/           (Feedback logic)
│   ├── Prefabs/              (P_DoorInteractable, P_Key, etc.)
│   ├── Materials/            (M_Red, M_Green, etc.)
│   └── Scenes/
│       └── TestScene.unity
├── README.md
├── PROMPTS.md
└── .gitignore

```

---

## İletişim

| Bilgi | Değer |
| --- | --- |
| Ad Soyad | Burak Arslan |
| E-posta | burakarslandev0@gmail.com |
| LinkedIn | https://www.linkedin.com/in/burakarslannn
| GitHub | https://github.com/BurakArslannn |

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*

```

```
