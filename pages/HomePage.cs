using OpenQA.Selenium;

using System;
using System.Collections.Generic;
using System.Text;

namespace BDDFrameWork.pages
{
    class HomePage
    {
        public IWebDriver WebDriver { get; }

        public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement DelayInput => WebDriver.FindElement(By.Id("delayInput"));
        public IWebElement DurationInput => WebDriver.FindElement(By.Id("durationInput"));
        public IWebElement Button => WebDriver.FindElement(By.TagName("button"));
        public IWebElement BusySpinner => WebDriver.FindElement(By.XPath("//div[@class='cg-busy-default-spinner']"));
        public IWebElement TemplateUrl => WebDriver.FindElement(By.XPath("//select[@ng-model='templateUrl']"));
        public IWebElement animationElement => WebDriver.FindElement(By.XPath("//table/following-sibling::div/div[2]"));
        public IWebElement Message => WebDriver.FindElement(By.XPath("//input[@id='message']"));

        public IWebElement SpinnerMessage => WebDriver.FindElement(By.XPath("//div[@class='cg-busy-default-text ng-binding']"));
        public void ClickDemo() => Button.Click();
        public void ProvideMS(string delay, string duration)
        {
            DelayInput.Clear();
            DurationInput.Clear();

            DelayInput.SendKeys(delay);
            DurationInput.SendKeys(duration);

            Button.Click();
        }



    }
}
