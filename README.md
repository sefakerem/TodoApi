# 🚀 .NET MAUI Todo API

Bu API, [.Net Maui | C# ile Mobil ve Masaüstü Uygulama Geliştirin](https://www.udemy.com/course/net-maui/) Udemy kursu için özel olarak hazırlanmıştır.

## 📱 Kurs Hakkında

Bu API, .NET MAUI kursunda geliştireceğimiz Todo uygulaması için backend servisi olarak kullanılacaktır. Kursumuzda bu API'yi kullanarak tam kapsamlı bir mobil uygulama geliştireceğiz.

## ✨ Özellikler

- 👤 Kullanıcı Kaydı ve Girişi
- 🔐 JWT Tabanlı Kimlik Doğrulama
- ✅ Todo Ekleme, Silme, Güncelleme
- 📝 Todo Detayları
- ✔️ Todo Tamamlama Durumu Değiştirme
- 💾 SQLite Veritabanı

## 🛠️ Teknolojiler

- .NET 9
- Entity Framework Core
- SQLite
- JWT Authentication

## 🚦 API Endpoints

### 🔑 Kimlik Doğrulama
- `POST /api/Auth/register` - Yeni kullanıcı kaydı
- `POST /api/Auth/login` - Kullanıcı girişi

### 📋 Todo İşlemleri
- `GET /api/Todo` - Tüm todoları getir
- `GET /api/Todo/{id}` - Belirli bir todoyu getir
- `POST /api/Todo` - Yeni todo ekle
- `PUT /api/Todo/{id}` - Todo güncelle
- `PUT /api/Todo/{id}/complete` - Todo durumunu değiştir
- `DELETE /api/Todo/{id}` - Todo sil

## 📚 Kurs İçeriği

Bu API, kursumuzda aşağıdaki konuları öğrenirken kullanılacaktır:

- 🏗️ MVVM Mimarisi
- 🌐 HTTP Client Kullanımı
- 🔒 Güvenli Depolama
- 🎨 XAML ile UI Tasarımı
- 🔄 Veri Bağlama
- 📱 Cross-Platform Geliştirme

## 🎓 Kursa Katılın

Detaylı .NET MAUI eğitimi için [kursumuza katılın](https://www.udemy.com/course/net-maui/)!
