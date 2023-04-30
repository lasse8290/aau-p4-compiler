using PuppeteerSharp;
using TextCopy;
using System.Diagnostics;

public class ESPSimulator
{
    string Code;

    public ESPSimulator(string code)
    {
        this.Code = code;
    }

    public async Task Run()
    {
        await new BrowserFetcher(Product.Chrome).DownloadAsync();
        using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = false,
            Product = Product.Chrome
        });

        var page = await browser.NewPageAsync();
        await page.GoToAsync("https://wokwi.com/projects/new/esp32");

        page.Console += async (sender, eventArgs) =>
        {
            Console.WriteLine(eventArgs.Message.Text);
        };
        page.PageError += (sender, eventArgs) =>
        {
            Console.WriteLine(eventArgs.Message);
        };

        await page.WaitForSelectorAsync("div[class='react-draggable']");
        await ClipboardService.SetTextAsync(Code);

        await page.Keyboard.DownAsync("Control");
        await page.Keyboard.PressAsync("a");
        await page.Keyboard.UpAsync("Control");

        await page.Keyboard.PressAsync("Backspace");

        await page.Keyboard.DownAsync("Control");
        await page.Keyboard.PressAsync("v");
        await page.Keyboard.UpAsync("Control");

        await page.ScreenshotAsync("screenshot1.png");

        var buttonSelector = "button[aria-label='Start the simulation']";
        await page.WaitForSelectorAsync(buttonSelector);
        await page.ClickAsync(buttonSelector);

        await page.ScreenshotAsync("screenshot2.png");

        await page.WaitForTimeoutAsync(15000);
        await page.ScreenshotAsync("screenshot3.png");
    }
}
