using PuppeteerSharp;

public class ESPSimulation
{
    string Code;
    string WokwiURL = "https://wokwi.com/projects/new/esp32";
    int SimulationDuration;
    public List<string> Output = new List<string>();

    public ESPSimulation(string code, int timeout)
    {
        this.Code = code;
        this.SimulationDuration = timeout;
    }

    public ESPSimulation(string code, int timeout, string wokwiURL) : this(code, timeout)
    {
        this.WokwiURL = wokwiURL;
    }

    public async Task Run()
    {
        await new BrowserFetcher(Product.Firefox).DownloadAsync();
        using IBrowser? browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = false,
            Product = Product.Firefox,
        });

        IPage page = await browser.NewPageAsync();
        await page.GoToAsync(WokwiURL);

        page.Console += (sender, eventArgs) =>
        {
            if (eventArgs.Message.Type != ConsoleType.Log) return;

            string output = eventArgs.Message.Text.Substring(10).Trim();
            Console.WriteLine(output);
            Output.Add(output);
        };

        await page.WaitForSelectorAsync("div[class='react-draggable']");

        await page.SelectAllText();
        await page.Paste(Code);

        string buttonSelector = "button[aria-label='Start the simulation']";
        await page.WaitForSelectorAsync(buttonSelector);
        await page.ClickAsync(buttonSelector);

        await page.WaitForTimeoutAsync(SimulationDuration);
    }
}
