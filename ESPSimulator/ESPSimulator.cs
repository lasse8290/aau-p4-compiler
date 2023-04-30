using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using static System.Console;

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
            ExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
            Product = Product.Chrome
        });

        var page = await browser.NewPageAsync();
        await page.GoToAsync("https://wokwi.com/projects/new/esp32");

        page.Console += async (sender, eventArgs) =>
        {
            WriteLine(eventArgs.Message.Text);
        };
        page.PageError += (sender, eventArgs) =>
        {
            WriteLine(eventArgs.Message);
        };

        await page.WaitForSelectorAsync("div[class='react-draggable']");
        // await Clipboard.WriteTextAsync(code);

        // Hold down Ctrl and press the "a" key
        await page.Keyboard.DownAsync("Control");
        await page.Keyboard.PressAsync("a");
        await page.Keyboard.UpAsync("Control");

        // Press the "Backspace" key
        await page.Keyboard.PressAsync("Backspace");

        // Paste from clipboard
        await page.Keyboard.DownAsync("Control");
        await page.Keyboard.PressAsync("v");
        await page.Keyboard.UpAsync("Control");

        var buttonSelector = "button[aria-label='Start the simulation']";
        await page.WaitForSelectorAsync(buttonSelector);
        await page.ClickAsync(buttonSelector);

        await page.WaitForTimeoutAsync(7000);
    }
}
