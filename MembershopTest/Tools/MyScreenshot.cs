

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace MembershopTest.Tools
{
    public class MyScreenshot
    {
        public static void TakeScreenshot(IWebDriver driver)
        {
            Screenshot myScreenshot = driver.TakeScreenshot();
            string screenshotDirectory = Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            string screenshotFolder = Path.Combine(screenshotDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotFolder);
            string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:HH_mm_ss}.png";
            string screenshotPath = Path.Combine(screenshotFolder, screenshotName);
            myScreenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            //Console.Out.WriteLine(screenshotDirectory);
        }
    }
}
