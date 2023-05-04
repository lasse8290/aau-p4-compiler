using PuppeteerSharp;
using TextCopy;

namespace ESPSimulation
{
    public static class IPageExtension
    {
        public static async Task SelectAllText(this IPage page)
        {
            string MetaKey = GetMetaKey();

            await page.Keyboard.DownAsync(MetaKey);
            await page.Keyboard.PressAsync("a");
            await page.Keyboard.UpAsync(MetaKey);
        }

        public static async Task Paste(this IPage page, string code)
        {
            await ClipboardService.SetTextAsync(code);
            string MetaKey = GetMetaKey();

            await page.Keyboard.DownAsync(MetaKey);
            await page.Keyboard.PressAsync("v");
            await page.Keyboard.UpAsync(MetaKey);
        }

        private static string GetMetaKey()
        {
            string MetaKey;
            if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
            {
                MetaKey = "Control";
            }
            else if (OperatingSystem.IsMacOS())
            {
                MetaKey = "Meta";
            }
            else
            {
                throw new Exception("Unknown OS");
            }
            return MetaKey;
        }
    }

}