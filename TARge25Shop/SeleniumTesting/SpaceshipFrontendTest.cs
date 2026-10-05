using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;



namespace SeleniumTesting
{
    public class SpaceshipFrontendTest
    {
        private IWebElement createInIndex;

        public IWebElement IWebElement { get; private set; }

        [Fact]

        public void Should_NavigateToCreate_AddSpaceshipWithCorrectData_returnToIndex()
        {
            //firefoxi käskiv ja juhtiv draiver
            IWebDriver driver = new FirefoxDriver();
            //aadress, millele draiver navigeerib
            driver.Url = "https://localhost:7085/";
            //lehelt otsitav element
            IWebElement navigateToSpaceship = driver.FindElement(By.LinkText("Spaceship"));
            navigateToSpaceship.Click();
            IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
            createInIndex.Click();

        }
    }
}
