# 🐱‍🏍 PokemonApi

**PokemonApi**, ASP.NET Core kullanılarak geliştirilen bir RESTful Web API uygulamasıdır. Bu API, Pokemon'lar hakkında bilgi sağlar ve temel CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştirmek için kullanılır. Swagger arayüzü sayesinde API'yi kolayca test edebilirsiniz.

---

## 📦 Proje Yapısı

```
PokemonApi/
├── .vs/                            # Visual Studio geçici dosyaları
├── PokemonReviewAppYeni/          # API kaynak kodları
├── PokemonReviewAppYeni.sln       # Visual Studio çözüm dosyası
```

---

## 🚀 Başlarken

### Gereksinimler

- [.NET 6.0 SDK veya üzeri](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 (veya başka bir C# IDE)
- (Opsiyonel) Postman veya Swagger UI

### Kurulum

```bash
git clone https://github.com/salihaisgoren/PokemonApi.git
cd PokemonApi
```

Visual Studio ile `PokemonReviewAppYeni.sln` dosyasını açıp çalıştırabilirsiniz.

---

## 🌐 Swagger UI

Proje çalıştırıldığında Swagger arayüzü otomatik olarak yüklenir:

🔗 [https://localhost:7170/swagger/index.html](https://localhost:7170/swagger/index.html)

Swagger üzerinden tüm endpoint'leri test edebilir, veri gönderebilir ve dönen yanıtları inceleyebilirsiniz.

---

## 📋 API Endpoint'leri

### 🔍 Pokemon İşlemleri

#### ✅ Tüm Pokemon'ları Listele
```
GET /api/pokemon
```

#### 🔍 Belirli Bir Pokemon'u Getir
```
GET /api/pokemon/{id}
```

#### ➕ Yeni Pokemon Ekle
```
POST /api/pokemon
Content-Type: application/json

{
  "name": "Pikachu",
  "type": "Electric",
  "rating": 5
}
```

#### ✏️ Pokemon Güncelle
```
PUT /api/pokemon/{id}
```

#### ❌ Pokemon Sil
```
DELETE /api/pokemon/{id}
```

---

## 💡 Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- Swagger 
- AutoMapper 
- Visual Studio 2022

---

## 📌 Notlar

- Proje geliştirme aşamasındadır.
- Swagger UI aktif ve kullanıma hazırdır.
- Katmanlı mimari yapısı sayesinde kolayca genişletilebilir.




