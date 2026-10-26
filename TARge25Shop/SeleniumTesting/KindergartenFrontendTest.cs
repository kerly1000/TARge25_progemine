using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace SeleniumTesting;

public class KindergartenFrontendTest
{

    public static string thisRunGuid { get; set; }

    [Fact]
    public void Should_NavigateToCreate_AddUniqueKindergarten_WithCorrectData_ReturnToIndex()
    {
        // Ülesseade
        IWebDriver driver = SetupAndNavigateKindergartenSIndex();

        // Tegevus
        IWebElement createInIndex = driver.FindElement(By.Id("CreateInIndex"));
        createInIndex.Click();

        // Sisestatavad andmed, tagastab unikaalse rühma nime (GUID)
        string concreteGroupName = InsertKindergartenData_ReturnGroupName(driver, false);
        thisRunGuid = concreteGroupName;

        IWebElement cu_CreateKindergarten = driver.FindElement(By.Id("CU_CreateKindergarten"));
        cu_CreateKindergarten.Click();

        // Andmete kogumine pärast reloadi tabelist
        var tableOfIndex = driver.FindElement(By.Id("IndexTable"));
        List<IWebElement> rowsOfTable = tableOfIndex.FindElements(By.TagName("tr")).ToList();
        rowsOfTable.RemoveAt(0); // Eemaldame päise (header row)

        string kindergartenGroupNameData = "";
        string kindergartenChildrenCountData = "";
        string kindergartenKindergartenNameData = "";
        bool doesItExist = false;

        foreach (var row in rowsOfTable)
        {
            if (row.FindElement(By.Id("IndexGroupNameKindergarten")).Text == thisRunGuid)
            {
                doesItExist = true;
                kindergartenGroupNameData = thisRunGuid;
                kindergartenChildrenCountData = row.FindElement(By.Id("IndexChildrenCountKindergarten")).Text;
                kindergartenKindergartenNameData = row.FindElement(By.Id("IndexKindergartenNameKindergarten")).Text;
                break;
            }
        }
        driver.Quit();

        // Kontroll
        Assert.True(doesItExist);
        Assert.Equal(kindergartenGroupNameData, thisRunGuid);
        Assert.Equal(kindergartenChildrenCountData, "2");
        Assert.Equal("Nupsukesed", kindergartenKindergartenNameData);
    }




    //andmed testimiseks

    private static IWebDriver SetupAndNavigateKindergartenSIndex()
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

    private string InsertKindergartenData_ReturnGroupName(IWebDriver driver, bool isOneOrTwo = false)
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
