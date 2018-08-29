using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace YouTubeCommentScraper
{
    class SearchFor : Driver
    {
        public string SearchItem;

        public void Searched(string SearchItem)

        {
            this.SearchItem = SearchItem;
        }

        public static void Search(string Searched)
        {
            Actions Interact = new Actions(Instance);

            Instance.Navigate().GoToUrl("https://www.youtube.com");

            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            var SearchBar = Instance.FindElement(By.ClassName("ytd-searchbox-spt"));

            Interact.MoveToElement(SearchBar);
            Interact.Click();
            Interact.SendKeys(Searched);
            Interact.SendKeys(Keys.Enter);
            Interact.Build().Perform();
        }

    }
}
