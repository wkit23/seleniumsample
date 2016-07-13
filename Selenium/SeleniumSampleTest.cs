using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;

namespace Selenium
{
    [TestClass]
    public class SeleniumSampleTest
    {
        protected IWebDriver WebDriver { get; set; }

        [TestInitialize]
        public void Init()
        {
            WebDriver = new FirefoxDriver();
            WebDriver.Navigate().GoToUrl("http://www.google.com");
        }

        [TestCleanup]
        public void CleanUp()
        {
            WebDriver.Close();
        }

        [TestMethod]
        public void CanShowStartPageByDefault()
        {
            var title = WebDriver.Title;
            Assert.AreEqual("Google", title);

            var imgLogo = WebDriver.FindElement(By.Id("hplogo"));
            Assert.IsTrue(imgLogo.Displayed);
        }

        [TestMethod]
        public void CanShowResultPageWhenSearch()
        {
            var input = WebDriver.FindElement(By.CssSelector(".gsfi:first-child"));
            input.SendKeys("Haha");
            input.SendKeys(Keys.Enter);

            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("g")).Displayed);

            input = WebDriver.FindElement(By.Id("lst-ib"));
            var value = input.GetAttribute("value");
            Assert.AreEqual("Haha", value);

            var results = WebDriver.FindElements(By.CssSelector("div.g"));
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CanShowVideoForVideoSearch()
        {
            var input = WebDriver.FindElement(By.Id("lst-ib"));
            input.SendKeys("Train to Busan");
            input.SendKeys(Keys.Enter);

            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("kno-ecr-pt")).Displayed);

            var text = WebDriver.FindElement(By.ClassName("kno-ecr-pt")).Text;
            Assert.AreEqual("Train To Busan", text);
        }

        [TestMethod]
        public void CanShowRespectivePageWhenClickOnResult()
        {
            var input = WebDriver.FindElement(By.Id("lst-ib"));
            input.SendKeys("arvato malaysia");
            input.SendKeys(Keys.Enter);

            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("kno-ecr-pt")).Displayed);

            var result = WebDriver.FindElement(By.CssSelector(".g .r:nth-child(1)"));
            result.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a.logo")).Displayed);

            var title = WebDriver.Title;
            Assert.AreEqual("arvato Systems - Empowering Digital Leaders", title);
        }
    }
}
