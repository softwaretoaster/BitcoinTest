using BDDFrameWork.pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace BDDFrameWork.steps
{
    [Binding]
    public sealed class HomePageSteps 
    {
        HomePage homePage = null;
        static IWebDriver webDriver;
       
            private static IWebDriver getDriver()
        {
            if (webDriver == null)
            {
                return new ChromeDriver();

            }
            return webDriver;

        }
      


        [BeforeScenario]
        public void beforeScenario()
        {
            Console.WriteLine("from before scenario");
            webDriver = getDriver();
        }


        [AfterScenario]
        public void afterScenario()
        {
          
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
                screenshot.SaveAsFile(DateTime.Now.ToShortDateString() + ".png", ScreenshotImageFormat.Png);
            }

            webDriver.Quit();
            webDriver = null;
        }
        

                [Given(@"User launches the application")]
        public void GivenUserLaunchesTheApplication()
        {
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl("http://cgross.github.io/angular-busy/demo/");
            homePage = new HomePage(webDriver);
            Assert.That(webDriver.Title, Is.EqualTo("Angular Busy Demo"));
        }

        // == 1== //
        [When(@"User provides ""(.*)"" ms into Delay and ""(.*)"" into Min Duration input")]
        public void WhenUserProvidesMsIntoDelayAndIntoMinDurationInput(string delay, string duration)
        {
            homePage.ProvideMS(delay, duration);
        }

        [Then(@"User verifies indicator is not visible for ""(.*)"" ms and it will be visible for (.*) ms")]
        public void ThenUserVerifiesIndicatorIsNotVisibleForMsAndItWillBeVisibleForMs(int delay, int duration)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Local time {startTime:HH:mm:ss}");
            Assert.That(homePage.BusySpinner.Displayed, Is.False);
            Thread.Sleep(delay);
            Assert.That(homePage.BusySpinner.Displayed, Is.True);
            Console.WriteLine($"Local time {endTime:HH:mm:ss}");
            Thread.Sleep(duration);
            Assert.That(homePage.BusySpinner.Displayed, Is.False);
        }

        
        [When(@"User changes from Standard to Templete Url")]
        public void WhenUserChangesFromStandardToTempleteUrl()
        {
            homePage.Button.Click();
            Assert.That(homePage.animationElement.GetAttribute("style").Contains("finalfantasy.gif"), Is.False);
        }

        [Then(@"User verifies that busy indicator switches from a spinner to a dancing wizard")]
        public void ThenUserVerifiesThatBusyIndicatorSwitchesFromASpinnerToADancingWizard()
        {
            var selectElement = new SelectElement(homePage.TemplateUrl);
            selectElement.SelectByText("custom-template.html");
            homePage.Button.Click();
            Assert.That(homePage.animationElement.GetAttribute("style").Contains("finalfantasy.gif"), Is.True);
        }

        // == 3== //
        // We reused this step ->  'User provides "0" ms into Delay and "3000" into Min Duration input'

        [When(@"User verifies that ""(.*)"" is being shown as Please Wait\.\.\. in the busy indicator")]
        public void WhenUserVerifiesThatIsBeingShownAsPleaseWait_InTheBusyIndicator(string message)
        {
            homePage.Message.Clear();
            homePage.Message.SendKeys(message);
            homePage.Button.Click();
            Thread.Sleep(1000);
            Console.WriteLine("MessageOfPL: " + homePage.Message.Text);
            Console.WriteLine("SpinnerOfPL: " + homePage.SpinnerMessage.Text);
            Assert.IsTrue(homePage.SpinnerMessage.Text.Contains(homePage.Message.Text));
        }
 
        [When(@"User verifies that ""(.*)"" is being shown as ""(.*)"" in the busy indicator")]
        public void WhenUserVerifiesThatIsBeingShownAsInTheBusyIndicator(string expectedMsg, string actualMsg)
        {

            homePage.Message.Clear();
            homePage.Message.SendKeys(expectedMsg);
            homePage.Button.Click();
            Thread.Sleep(1000);
            Console.WriteLine("MessageOfW: " + homePage.Message.Text);
            Console.WriteLine("SpinnerOfW: " + homePage.SpinnerMessage.Text);
            Assert.AreEqual(expectedMsg, actualMsg);

        }

        [When(@"User verifies that ""(.*)"" messages shown in the busy indicator as ""(.*)""")]
        public void WhenUserVerifiesThatMessagesShownInTheBusyIndicatorAs(string expectedMsg, string actualMsg)
        {
            try
            {
                homePage.Message.Clear();
                homePage.Message.SendKeys(expectedMsg);
                ((ITakesScreenshot)webDriver).GetScreenshot().SaveAsFile(DateTime.Now.ToShortDateString() + ".png", ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Assert.IsFalse(homePage.SpinnerMessage.Text.Equals(actualMsg));
            }
            Console.WriteLine("MessageOfW: " + homePage.Message.Text);
        }

        [Then(@"User sets minimum duration to ""(.*)"" ms and press Demo button")]
        public void ThenUserSetsMinimumDurationToMsAndPressDemoButton(string duration)
        {
            homePage.DurationInput.Clear();
            homePage.DurationInput.SendKeys(duration);
            homePage.Button.Click();
        }

        [Then(@"User verifies that ""(.*)"" message is shown in the busy indicator for ""(.*)"" ms")]
        public void ThenUserVerifiesThatMessageIsShownInTheBusyIndicatorForMs(string msg3, long expTime)
        {
            Assert.IsFalse(homePage.SpinnerMessage.Text.Contains(msg3));
            Stopwatch stopWatch = new Stopwatch();

            if (homePage.SpinnerMessage.Displayed)
            {
                stopWatch.Start();
            }
            if (!homePage.SpinnerMessage.Displayed)
            {
                stopWatch.Stop();
            }

            Assert.True(homePage.SpinnerMessage.Displayed);
            long duration = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("Amount of time: " + duration);
            Assert.False(expTime <= duration);
        }
    }
}

