using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTesting;

public class KindergartenFrontendTest
{
    [Fact]
    public void TestMethod1()
    {
    }


    //ülesseade

    private static IWebDriver SetupAndNavigateSIndex()
    {
        //firefoxi käskiv ja juhtiv draiver
        IWebDriver driver = new FirefoxDriver();
        //aadress millele draiver navigeerib
        driver.Url = "https://localhost:7227/";
        //lehelt otsitav element
        IWebElement navigateToKindergarten = driver.FindElement(By.LinkText("Kindergarden"));
        //selle elemendiga tehtav tegevus
        navigateToKindergarten.Click();
        return driver;
    }


    private void InsertKindergartenData(IWebDriver driver, string name = null, bool isOneOrTwo = false)
    {

        if (isOneOrTwo)
        {
            IWebElement cu_NameEntryKindergarten = driver.FindElement(By.Id("CU_NameEntrySpaceship"));
            cu_NameEntrySpaceship.Clear();
            if (name != null)
            {
                cu_NameEntrySpaceship.SendKeys(name);
            }
            else
            {
                cu_NameEntrySpaceship.SendKeys("DISISENJUNAEM");
            }
            IWebElement cu_ShipTypeEntrySpaceship = driver.FindElement(By.Id("CU_ShipTypeEntrySpaceship"));
            cu_ShipTypeEntrySpaceship.Clear();
            cu_ShipTypeEntrySpaceship.SendKeys("completely unheard of haunebu");
            IWebElement cu_CrewEntrySpaceship = driver.FindElement(By.Id("CU_CrewEntrySpaceship"));
            cu_CrewEntrySpaceship.Clear();
            cu_CrewEntrySpaceship.SendKeys("67890");
            IWebElement cu_EnginePowerEntrySpaceship = driver.FindElement(By.Id("CU_EnginePowerEntrySpaceship"));
            cu_EnginePowerEntrySpaceship.Clear();
            cu_EnginePowerEntrySpaceship.SendKeys("444");
        }
        else
        {
            IWebElement cu_NameEntrySpaceship = driver.FindElement(By.Id("CU_NameEntrySpaceship"));
            cu_NameEntrySpaceship.Clear();
            if (name != null)
            {
                cu_NameEntrySpaceship.SendKeys(name);
            }
            else
            {
                cu_NameEntrySpaceship.SendKeys("i add name for spaceship");
            }
            IWebElement cu_ShipTypeEntrySpaceship = driver.FindElement(By.Id("CU_ShipTypeEntrySpaceship"));
            cu_ShipTypeEntrySpaceship.Clear();
            cu_ShipTypeEntrySpaceship.SendKeys("i add ship type for spaceship");
            IWebElement cu_CrewEntrySpaceship = driver.FindElement(By.Id("CU_CrewEntrySpaceship"));
            cu_CrewEntrySpaceship.Clear();
            cu_CrewEntrySpaceship.SendKeys("12345");
            IWebElement cu_EnginePowerEntrySpaceship = driver.FindElement(By.Id("CU_EnginePowerEntrySpaceship"));
            cu_EnginePowerEntrySpaceship.Clear();
            cu_EnginePowerEntrySpaceship.SendKeys("666667");
        }
    }

    private string InsertSpaceShipData_ReturnName(IWebDriver driver, bool isOneOrTwo = false)
    {
        Guid returnGuid = Guid.NewGuid();
        if (isOneOrTwo)
        {
            IWebElement cu_NameEntrySpaceship = driver.FindElement(By.Id("CU_NameEntrySpaceship"));
            cu_NameEntrySpaceship.Clear();
            cu_NameEntrySpaceship.SendKeys(returnGuid.ToString());
            IWebElement cu_ShipTypeEntrySpaceship = driver.FindElement(By.Id("CU_ShipTypeEntrySpaceship"));
            cu_ShipTypeEntrySpaceship.Clear();
            cu_ShipTypeEntrySpaceship.SendKeys("completely unheard of haunebu");
            IWebElement cu_CrewEntrySpaceship = driver.FindElement(By.Id("CU_CrewEntrySpaceship"));
            cu_CrewEntrySpaceship.Clear();
            cu_CrewEntrySpaceship.SendKeys("67890");
            IWebElement cu_EnginePowerEntrySpaceship = driver.FindElement(By.Id("CU_EnginePowerEntrySpaceship"));
            cu_EnginePowerEntrySpaceship.Clear();
            cu_EnginePowerEntrySpaceship.SendKeys("444");
        }
        else
        {
            IWebElement cu_NameEntrySpaceship = driver.FindElement(By.Id("CU_NameEntrySpaceship"));
            cu_NameEntrySpaceship.Clear();
            cu_NameEntrySpaceship.SendKeys(returnGuid.ToString());
            IWebElement cu_ShipTypeEntrySpaceship = driver.FindElement(By.Id("CU_ShipTypeEntrySpaceship"));
            cu_ShipTypeEntrySpaceship.Clear();
            cu_ShipTypeEntrySpaceship.SendKeys("i add ship type for spaceship");
            IWebElement cu_CrewEntrySpaceship = driver.FindElement(By.Id("CU_CrewEntrySpaceship"));
            cu_CrewEntrySpaceship.Clear();
            cu_CrewEntrySpaceship.SendKeys("12345");
            IWebElement cu_EnginePowerEntrySpaceship = driver.FindElement(By.Id("CU_EnginePowerEntrySpaceship"));
            cu_EnginePowerEntrySpaceship.Clear();
            cu_EnginePowerEntrySpaceship.SendKeys("666667");
        }
        return returnGuid.ToString();
    }
}
