using Blazor_Quiz.Services;
using Microsoft.AspNetCore.Components;
using PuppeteerSharp;

namespace Blazor_Quiz.Pages
{
    public partial class HTML_To_PDF
    {
        [Inject]
        public NotifyMinService Notify { get; set; } = default!;

        public const string TargetPath = """"C:\Users\sebas\OneDrive\Pulpit\PDF_CSharp"""";
        public async  Task ExportToPDF()
        {
            var chromePath = """C:\Program Files\Google\Chrome\Application\chrome.exe""";

            for (int i = 0; i < 20; i++)
            {
                // Ustawienia uruchomienia Puppeteer z rêcznie pobranym Chrome
                var launchOptions = new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = chromePath
                };

                await new BrowserFetcher().DownloadAsync();
                using var browser = await Puppeteer.LaunchAsync(launchOptions);
                using var page = await browser.NewPageAsync();
                //  await page.SetContentAsync("<div>My Custom Content</div>");
                await page.GoToAsync("https://localhost:44375/EditQuiz/1");
                await page.WaitForSelectorAsync("#completeLoad"); // Zmieñ na odpowiedni selektor
                await page.PdfAsync($"C:\\Users\\sebas\\OneDrive\\Pulpit\\PDF_CSharp\\customContent_{i}.pdf");
                Notify.Success(i.ToString());

            }

        }
    }
}