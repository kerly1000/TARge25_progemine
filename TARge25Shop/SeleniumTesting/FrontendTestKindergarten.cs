using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace TARge25Shop.SeleniumTesting
{
    [TestCaseOrderer("TARge25Shop.SeleniumTesting.PriorityOrderer", "TARge25Shop.SeleniumTesting")]
    public class KindergartenFrontendTest
    {
        public static string ThisRunGroupName { get; set; } = string.Empty;

        private const string TestKindergartenName = "Test Kindergarten";
        private const string TestTeacherName = "Test Teacher";
        private const string UpdatedKindergartenName = "Updated Kindergarten";
        private const string UpdatedTeacherName = "Updated Teacher";

        [Fact]
        [Order(1)]
        public void Should_CreateKindergarten_WithCorrectData()
        {
            IWebDriver driver = SetupAndNavigateIndex();

            try
            {
                driver.FindElement(By.Id("IndexKindergartenCreate")).Click();

                ThisRunGroupName = $"Test Group {Guid.NewGuid()}";
                InsertKindergartenData(
                    driver,
                    ThisRunGroupName,
                    20,
                    TestKindergartenName,
                    TestTeacherName);

                driver.FindElement(By.Id("CU_CreateKindergarten")).Click();

                var createdRow = FindKindergartenRow(driver, ThisRunGroupName);
                Assert.NotNull(createdRow);
                Assert.Equal(ThisRunGroupName,
                    createdRow!.FindElement(By.Id("IndexGroupNameKindergarten")).Text);
                Assert.Equal("5",
                    createdRow.FindElement(By.Id("IndexChildrenCountKindergarten")).Text);
                Assert.Equal(TestKindergartenName,
                    createdRow.FindElement(By.Id("IndexKindergartenName")).Text);
                Assert.Equal(TestTeacherName,
                    createdRow.FindElement(By.Id("IndexTeacherNameKindergarten")).Text);
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        [Order(2)]
        public void Should_ShowKindergartenDetails_WithCorrectData()
        {
            Assert.False(string.IsNullOrWhiteSpace(ThisRunGroupName),
                "Enne andmete näitamist tuleb käivitada Create test");

            IWebDriver driver = SetupAndNavigateIndex();

            try
            {
                var row = FindKindergartenRow(driver, ThisRunGroupName);
                Assert.NotNull(row);

                row!.FindElement(By.Id("IndexKindergartenDetails")).Click();

                string id = driver.FindElement(By.Id("Details_KindergartenId")).Text;
                string groupName = driver.FindElement(By.Id("Details_KindergartenGroupName")).Text;
                string childrenCount = driver.FindElement(By.Id("Details_KindergartenChildenCount")).Text;
                string kindergartenName = driver.FindElement(By.Id("Details_KindergartenName")).Text;
                string teacherName = driver.FindElement(By.Id("Details_KindergartenTeacherName")).Text;

                Assert.False(string.IsNullOrWhiteSpace(id));
                Assert.Equal(ThisRunGroupName, groupName);
                Assert.Equal("5", childrenCount);
                Assert.Equal(TestKindergartenName, kindergartenName);
                Assert.Equal(TestTeacherName, teacherName);
                Assert.True(Guid.TryParse(id, out _));
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        [Order(3)]
        public void Should_UpdateKindergarten_WithNewData()
        {
            Assert.False(string.IsNullOrWhiteSpace(ThisRunGroupName),
                "Enne andmete uuendamist tuleb käivitada Create test.");

            IWebDriver driver = SetupAndNavigateIndex();

            try
            {
                var row = FindKindergartenRow(driver, ThisRunGroupName);
                Assert.NotNull(row);

                row!.FindElement(By.Id("IndexKindergartenUpdate")).Click();

                InsertKindergartenData(
                    driver,
                    ThisRunGroupName,
                    10,
                    UpdatedKindergartenName,
                    UpdatedTeacherName);

                driver.FindElement(By.Id("CU_UpdateKindergarten")).Click();

                var updatedRow = FindKindergartenRow(driver, ThisRunGroupName);
                Assert.NotNull(updatedRow);
                Assert.Equal(ThisRunGroupName,
                    updatedRow!.FindElement(By.Id("IndexGroupNameKindergarten")).Text);
                Assert.Equal("10",
                    updatedRow.FindElement(By.Id("IndexChildrenCountKindergarten")).Text);
                Assert.Equal(UpdatedKindergartenName,
                    updatedRow.FindElement(By.Id("IndexKindergartenName")).Text);
                Assert.Equal(UpdatedTeacherName,
                    updatedRow.FindElement(By.Id("IndexTeacherNameKindergarten")).Text);
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        [Order(4)]
        public void Should_DeleteKindergarten()
        {
            Assert.False(string.IsNullOrWhiteSpace(ThisRunGroupName),
                "Enne andmete kustutamist tuleb käivitada Create test.");

            IWebDriver driver = SetupAndNavigateIndex();

            try
            {
                var row = FindKindergartenRow(driver, ThisRunGroupName);
                Assert.NotNull(row);

                row!.FindElement(By.Id("IndexKindergartenDelete")).Click();
                driver.FindElement(By.Id("Delete_Delete")).Click();

                Assert.Null(FindKindergartenRow(driver, ThisRunGroupName));
            }
            finally
            {
                driver.Quit();
            }
        }

        private static IWebDriver SetupAndNavigateIndex()
        {
            IWebDriver driver = new FirefoxDriver();

            try
            {
                driver.Navigate().GoToUrl("https://localhost:7085/Kindergarten");

                return driver;
            }
            catch
            {
                driver.Quit();
                throw;
            }
        }

        private static void InsertKindergartenData(
            IWebDriver driver,
            string groupName,
            int childrenCount,
            string kindergartenName,
            string teacherName)
        {
            IWebElement groupNameInput =
                driver.FindElement(By.Id("CU_GroupNameEntryKindergarten"));
            groupNameInput.Clear();
            groupNameInput.SendKeys(groupName);

            IWebElement childrenCountInput =
                driver.FindElement(By.Id("CU_ChildrenCountEntryKindergarten"));
            childrenCountInput.Clear();
            childrenCountInput.SendKeys(childrenCount.ToString());

            IWebElement kindergartenNameInput =
                driver.FindElement(By.Id("CU_KindergartenNameEntryKindergarten"));
            kindergartenNameInput.Clear();
            kindergartenNameInput.SendKeys(kindergartenName);

            IWebElement teacherNameInput =
                driver.FindElement(By.Id("CU_TeacherNameEntryKindergarten"));
            teacherNameInput.Clear();
            teacherNameInput.SendKeys(teacherName);
        }

        private static IWebElement? FindKindergartenRow(IWebDriver driver, string groupName)
        {
            var rows = driver.FindElements(By.CssSelector("table tbody tr"));

            foreach (var row in rows)
            {
                var groupNameElements =
                    row.FindElements(By.Id("IndexGroupNameKindergarten"));

                if (groupNameElements.Count > 0 &&
                    groupNameElements[0].Text == groupName)
                {
                    return row;
                }
            }

            return null;
        }
    }
}
