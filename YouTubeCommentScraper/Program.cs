using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
namespace YouTubeCommentScraper
{
    class ScraperMain : Driver
    {
        static void Main(string[] args)

        {
            Initialize();
            SearchFor.Search("your search here");
            GoToFirstOption();
            ScrollToComments();
            WaitForCommentsLoad();
            StoreComments();
        }
        private static bool ACommentIsLoaded()
        {
            var elements = Instance.FindElements(By.XPath("(//yt-formatted-string)[@class='style-scope ytd-comment-renderer']"));
            return elements.Any();
        }
        private static void WaitUntilTrueOrTimeout(TimeSpan timeToWait,TimeSpan pollRate,Func<bool> condition)
        {
            double secondsElapsed = 0;
            while(secondsElapsed < timeToWait.TotalSeconds)
            {
                Thread.Sleep(pollRate);
                secondsElapsed += pollRate.TotalSeconds;
                if (condition.Invoke() == true)
                    return;
            }
            throw new Exception("Condition didn't become true within " + timeToWait.TotalSeconds.ToString() + "seconds.");  
        }
        public static void WaitForCommentsLoad()
        {
            var wait = new WebDriverWait(Instance, TimeSpan.FromSeconds(10));
            WaitUntilTrueOrTimeout(TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(2), ACommentIsLoaded);
        }

        public static void StoreComments()
        {
            FileStream fs = File.OpenWrite("C:/desktop/WebScrapping.txt");
            StreamWriter writer = new StreamWriter(fs);

            IList<IWebElement> ElemList = Instance.FindElements(By.Id("comment")).ToList();

            string[] ElemListText = new string[15];

            ElemListText = new string[ElemList.Count];

            int i = 0;
            foreach (IWebElement element in ElemList)
            {
                ElemListText[i++] = element.Text;
                writer.Write("These are your comments:", element.Text, "This is the end of your comments");    
            }
        }

        public static void ScrollToComments()
        {
            Actions Interact = new Actions(Instance);

            Interact.SendKeys(Keys.PageDown).SendKeys(Keys.PageDown).SendKeys(Keys.PageDown).SendKeys(Keys.PageDown). 
                SendKeys(Keys.PageDown).Build().Perform();

            System.Threading.Thread.Sleep(2000);
        }

        public static void GoToFirstOption()
        {
            Actions Interact = new Actions(Instance);

            var SearhedItem = Instance.FindElements(By.ClassName("ytd-video-renderer"));

            Interact.MoveToElement(SearhedItem[0]).Click().Build().Perform();
        }
    }
}
