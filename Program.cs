using System.Text;
using System.Text.RegularExpressions;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen analiz yapmak istediğiniz web sayfasının URL'ini giriniz: ");
        string inputUrl;
        inputUrl = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Web sayfası içeriği: ");
        string webContent = ExtractTextFromWeb(inputUrl);
        await AnaylzeWithAI(webContent, "Web Sayfası İçeriği");

        // <<< TEMİZLEME FONKSİYONU EKLENDİ >>>
        static string CleanText(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            // Soft hyphen ve zero-width karakterleri sil
            input = input.Replace("\u00AD", "");
            input = Regex.Replace(input, "[\u200B-\u200D\u2060]", "");

            // Yanlış kelime bölmeleri: Harf + boşluk + harf → tekleştir
            input = Regex.Replace(input, @"(?<=\p{L})\s+(?=\p{L})", "");

            // Fazla boşlukları tek boşluğa indir
            input = Regex.Replace(input, @"\s+", " ");

            return input.Trim();
        }

        static string ExtractTextFromWeb(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var bodyText = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;
            return CleanText(bodyText ?? "Sayfa içeriği bulunamadı"); // <<< BURADA CLEAN TEXT KULLANILDI >>>
        }

        static async Task AnaylzeWithAI(string text, string sourceType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new {role="system",content="Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver!"},
                        new{role="user",content=$"Analyze and summarize the following {sourceType}:\n\n{text}"}
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    Console.WriteLine($"\n AI Analizi({sourceType}): \n{result.choices[0].message.content}");
                }
                else
                {
                    Console.WriteLine("Hata: " + responseJson);
                }
            }
        }
    }
}
