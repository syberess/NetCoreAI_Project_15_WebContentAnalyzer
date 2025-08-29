# 🌐 NetCoreAI Project 15 - Web Content Analyzer & Summarizer

Bu proje, **.NET Console Application** ile bir web sayfasının içeriğini alır, temizler ve **OpenAI Chat Completions API** kullanarak Türkçe analiz/özet üretir.  

---

## 🛠️ Kullanılan Teknolojiler
- .NET 8 / 9 Console Application  
- OpenAI API (Chat Completions - `gpt-3.5-turbo`)  
- HtmlAgilityPack (HTML parse & text extraction)  
- Regex (metin temizleme)  
- Newtonsoft.Json (JSON serialize/deserialize)  
- HttpClient (API isteği için)  

---

## 📂 Proje Yapısı
- `Program.cs` → Ana uygulama  
  - `ExtractTextFromWeb()` → Web sayfasını indirip body içeriğini çıkarır  
  - `CleanText()` → Gereksiz karakterleri, fazla boşlukları temizler  
  - `AnalyzeWithAI()` → OpenAI API’ye gönderip Türkçe analiz/özet alır  

---

## ⚙️ Kurulum ve Çalıştırma
1. Repo’yu klonla:
   git clone https://github.com/kullaniciadiniz/NetCoreAI_Project_15_WebContentAnalyzer.git
   cd NetCoreAI_Project_15_WebContentAnalyzer
NuGet paketlerini yükle:
dotnet add package HtmlAgilityPack
dotnet add package Newtonsoft.Json
Program.cs içinde kendi OpenAI API anahtarını ekle:
private static readonly string apiKey = "YOUR_API_KEY";
Konsol uygulamasını çalıştır:
dotnet run


Örnek kullanım:

Lütfen analiz yapmak istediğiniz web sayfasının URL'ini giriniz:
> https://en.wikipedia.org/wiki/Artificial_intelligence

Web sayfası içeriği:
(sayfadan çekilen temizlenmiş metin)

AI Analizi(Web Sayfası İçeriği):
Bu sayfa yapay zekânın tanımı, tarihi ve kullanım alanları hakkında bilgi vermektedir...
✨ Özellikler
✔️ Web sayfasından içerik çekme (HTML parsing)

✔️ Metin temizleme (gereksiz boşluk, zero-width karakter, soft hyphen kaldırma)

✔️ OpenAI API ile Türkçe özet/analiz üretme

✔️ Hata durumlarını konsola yazdırma

﻿
