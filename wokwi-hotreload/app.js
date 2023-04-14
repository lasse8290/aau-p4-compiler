import fs from "fs";
import puppeteer from "puppeteer";
import clipboardy from "clipboardy";

// Get the directory from the command line
// --dir="C:\Users\user\Documents\Arduino\libraries\GenCode\examples\GenCode\GenCode.ino"

const args = process.argv.slice(2);
const dir = args[0].split("=")[1];

(async () => {
  const browser = await puppeteer.launch({ headless: false });

  var previousPage = null;
  var lastTime = Date.now();
  fs.watch(dir, async (event, filename) => {
    const oneSecHasPassed = Date.now() - lastTime > 5000;
    if (filename == "GenCode.txt" && oneSecHasPassed) {
      lastTime = Date.now();

      // If previous page exists, close it
      if (previousPage) {
        await previousPage.close();
      }

      // Read the file
      const data = fs.readFileSync(dir + "\\GenCode.txt", "utf8");
      clipboardy.writeSync(data);

      // Make the browser window full screen
      const page = await browser.newPage();
      await page.setViewport({ width: 1920, height: 1080 });
      await page.goto("https://wokwi.com/projects/361970165060110337");

      // Wait for the page to load
      await page.waitForSelector('div[class="react-draggable"]');

      // Hold down Ctrl and press the "a" key
      await page.keyboard.down("Control");
      await page.keyboard.press("a");
      await page.keyboard.up("Control");

      // Press the "Backspace" key
      await page.keyboard.press("Backspace");

      // Paste from clipboard
      await page.keyboard.down("Control");
      await page.keyboard.press("v");
      await page.keyboard.up("Control");

      // Click the start button
      await page.click('button[aria-label="Start the simulation"]');

      // Set the previous page to the current page
      previousPage = page;
    }
  });
})();
