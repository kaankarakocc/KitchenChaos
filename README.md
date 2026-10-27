# Kitchen Chaos

Unity ve C# ile geliştirdiğim, siparişlere göre yemek hazırlayıp teslim ettiğim bir mutfak oyunu. Projeyi Kitchen Chaos eğitimi üzerinden; nesne yönelimli programlama, oyun algoritmaları ve Unity iş akışını öğrenmek için geliştirdim.

## Özellikler

- Malzeme alma, taşıma ve tezgâhlara bırakma.
- ScriptableObject tarifleriyle kesme, pişirme ve yanma aşamaları.
- Tabağa malzeme ekleme; tabakta model ve malzeme ikonlarını gösterme.
- Kimlik taşıyan siparişler ve Dictionary üzerinden sipariş UI satırlarını ayrı güncelleme.
- Tarif eşleştirme, başarılı teslimat sayacı ve başarılı teslimatta ek süre.
- Başlangıç yardım ekranı, geri sayım, oyun sonu, ana menü ve duraklatma ekranları.
- Ses ayarları, klavye tuşlarını yeniden atama ve PlayerPrefs ile ayar kaydı.
- Hareket parçacıkları, yanma uyarıları, animasyonlar ve teslimat sonucu bildirimleri.

## Kullanılan teknolojiler

- Unity **6.6 / 6000.6.4f1**
- C#, Universal Render Pipeline, Input System, Cinemachine ve TextMeshPro
- Kalıtım ve polymorphism, interface'ler, event'ler, callback'ler ve durum makineleri
- Prefab'lar ve ScriptableObject veri kartları

## Çalıştırma

1. Depoyu klonla.
2. Unity Hub üzerinden proje klasörünü ekle ve **6000.6.4f1** sürümüyle aç.
3. Unity'nin paketleri ve Library klasörünü yeniden oluşturmasını bekle.
4. `Assets/Scenes/MainMenuScene.unity` sahnesini açıp Play'e bas.
5. Play düğmesinden oyuna geç; yardım ekranında etkileşim tuşuna basarak turu başlat.

## Varsayılan kontroller

| İşlem | Tuş |
| --- | --- |
| Hareket | W A S D / yön tuşları |
| Etkileşim ve turu başlatma | E |
| Alternatif etkileşim / kesme | F |
| Duraklatma | Escape |

Tuşlar, duraklatma menüsündeki **Options → Key Bindings** ekranından değiştirilebilir. Yeni atama beklenirken Escape işlemi iptal eder. Bu sürüm klavye ve fare kullanımına odaklanır.

## Proje yapısı

- `Assets/Scripts`: Oyun mantığı, tezgâhlar, nesneler, UI ve ses yönetimi.
- `Assets/Prefabs`: Oyunda kullanılan prefab'lar ve veri kartları.
- `Assets/Scenes`: MainMenuScene, LoadingScene ve GameScene.
- `Assets/Animations`: UI ve oyun animasyonları.
- `Packages` ve `ProjectSettings`: Unity paketleri ve proje ayarları.

Unity'nin ürettiği Library, Temp, Logs, IDE dosyaları ve yerel ayarlar depoya eklenmez. Nesne bağlantılarının korunması için `.meta` dosyaları kaynaklarla birlikte tutulur.

## Kaynak

Bu bir öğrenme projesidir; Code Monkey'nin Kitchen Chaos eğitimi ve eğitimle sağlanan görsel/ses varlıkları temel alınmıştır. Üçüncü taraf varlıkların hakları ilgili sahiplerine aittir.
