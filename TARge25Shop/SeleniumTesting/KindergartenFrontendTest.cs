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
        IWebElement navigateToKindergarten = driver.FindElement(By.LinkText("Kindergarten"));
        //selle elemendiga tehtav tegevus
        navigateToKindergarten.Click();
        return driver;
    }


    private void InsertKindergartenData(IWebDriver driver, string name = null, bool isOneOrTwo = false)
    {

        if (isOneOrTwo)
        {
            IWebElement cu_KindergartenNameEntryKindergarten = driver.FindElement(By.Id("CU_KindergartenNameEntryKindergarten"));
            cu_KindergartenNameEntryKindergarten.Clear();
            if (name != null)
            {
                cu_KindergartenNameEntryKindergarten.SendKeys(name);
            }
            else
            {
                cu_KindergartenNameEntryKindergarten.SendKeys("Kasevits");
            }
            IWebElement cu_GroupNameEntryKindergarten = driver.FindElement(By.Id("CU_GroupNameEntryKindergarten"));
            cu_GroupNameEntryKindergarten.Clear();
            cu_GroupNameEntryKindergarten.SendKeys("Tobukesed");
            IWebElement cu_ChildrenCountEntryKindergarten = driver.FindElement(By.Id("CU_ChildrenCountEntryKindergarten"));
            cu_ChildrenCountEntryKindergarten.Clear();
            cu_ChildrenCountEntryKindergarten.SendKeys("3");
            IWebElement cu_TeacherNameEntryKindergartengarten = driver.FindElement(By.Id("CU_TeacherNameEntryKindergarten"));
            cu_TeacherNameEntryKindergartengarten.Clear();
            cu_TeacherNameEntryKindergartengarten.SendKeys("Priiduk Pannal");
        }
        else
        {
            IWebElement cu_NameEntryKindergarten = driver.FindElement(By.Id("CU_NameEntryKindergarten"));
            cu_NameEntryKindergarten.Clear();
            if (name != null)
            {
                cu_NameEntryKindergarten.SendKeys(name);
            }
            else
            {
                cu_NameEntryKindergarten.SendKeys("Tammejuurikas");
            }
            IWebElement cu_GroupNameEntryKindergarten = driver.FindElement(By.Id("CU_GroupNameEntryKindergarten"));
            cu_GroupNameEntryKindergarten.Clear();
            cu_GroupNameEntryKindergarten.SendKeys("Otukesed");
            IWebElement cu_ChildrenCountEntryKindergarten = driver.FindElement(By.Id("CU_ChildrenCountEntryKindergarten"));
            cu_ChildrenCountEntryKindergarten.Clear();
            cu_ChildrenCountEntryKindergarten.SendKeys("5");
            IWebElement cu_TeacherNameEntryKindergartengarten = driver.FindElement(By.Id("CU_TeacherNameEntryKindergarten"));
            cu_TeacherNameEntryKindergartengarten.Clear();
            cu_TeacherNameEntryKindergartengarten.SendKeys("Rita Rihm");
        }
    }

    private string InsertSpaceShipData_ReturnName(IWebDriver driver, bool isOneOrTwo = false)
    {
        Guid returnGuid = Guid.NewGuid();
        if (isOneOrTwo)
        {
            IWebElement cu_GroupNameEntryKindergarten = driver.FindElement(By.Id("CU_GroupNameEntryKindergarten"));
            cu_GroupNameEntryKindergarten.Clear();
            cu_GroupNameEntryKindergarten.SendKeys("Nupsukesed");
            IWebElement cu_ChildrenCountEntryKindergarten = driver.FindElement(By.Id("CU_ChildrenCountEntryKindergarten"));
            cu_ChildrenCountEntryKindergarten.Clear();
            cu_ChildrenCountEntryKindergarten.SendKeys("2");
            IWebElement cu_TeacherNameEntryKindergartengarten = driver.FindElement(By.Id("CU_TeacherNameEntryKindergarten"));
            cu_TeacherNameEntryKindergartengarten.Clear();
            cu_TeacherNameEntryKindergartengarten.SendKeys("Kalju Tugev");
        }
        else
        {
            IWebElement cu_GroupNameEntryKindergarten = driver.FindElement(By.Id("CU_GroupNameEntryKindergarten"));
            cu_GroupNameEntryKindergarten.Clear();
            cu_GroupNameEntryKindergarten.SendKeys("Tobukesed");
            IWebElement cu_ChildrenCountEntryKindergarten = driver.FindElement(By.Id("CU_ChildrenCountEntryKindergarten"));
            cu_ChildrenCountEntryKindergarten.Clear();
            cu_ChildrenCountEntryKindergarten.SendKeys("3");
            IWebElement cu_TeacherNameEntryKindergartengarten = driver.FindElement(By.Id("CU_TeacherNameEntryKindergarten"));
            cu_TeacherNameEntryKindergartengarten.Clear();
            cu_TeacherNameEntryKindergartengarten.SendKeys("Priiduk Pannal");
        }
        return returnGuid.ToString();
    }
}
