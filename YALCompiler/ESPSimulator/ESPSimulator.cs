﻿using PuppeteerSharp;
using PuppeteerSharp.Input;

namespace ESPSimulation
{
    public class ESPSimulator
    {
        string Code;
        string WokwiURL = "https://wokwi.com/projects/new/esp32";
        int? SimulationDuration;
        public List<string> Output = new List<string>();

        public ESPSimulator(string code, int? duration)
        {
            this.Code = code;
            this.SimulationDuration = duration;
        }

        public ESPSimulator(string code, int? duration, string wokwiURL) : this(code, duration)
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
                if (eventArgs.Message.Type != ConsoleType.Log || eventArgs.Message.Text == "Build aborted") return;

                string output = eventArgs.Message.Text.Substring(10).Trim();
                Console.WriteLine(output);
                Output.Add(output);
            };

            page.Dialog += async (sender, e) =>
            {
                await e.Dialog.Accept();
            };

            await page.WaitForSelectorAsync("div[class='react-draggable']");
            await page.WaitForTimeoutAsync(200);
            await page.SelectAllText();
            await page.Paste(Code);

            string startButtonSelector = "button[aria-label='Start the simulation']";
            await page.WaitForSelectorAsync(startButtonSelector);
            await page.ClickAsync(startButtonSelector);

            await page.WaitForTimeoutAsync(SimulationDuration ?? 99999999); // wait simulation duration or forever

            string stopButtonSelector = "button[aria-label='Stop the simulation']";
            await page.WaitForSelectorAsync(stopButtonSelector);
            await page.ClickAsync(stopButtonSelector);

            await page.Keyboard.DownAsync("Meta");
            await page.Keyboard.PressAsync("z");
            await page.Keyboard.UpAsync("Meta");

            await page.CloseAsync();
        }
    }
}