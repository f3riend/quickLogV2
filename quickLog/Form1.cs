using System;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace quickLog
{
    public partial class Form1 : Form
    {
        static dynamic data;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);


            
            this.TopMost = true;
            string jsonString = File.ReadAllText("C:\\Users\\blask\\source\\repos\\quickLog\\quickLog\\data.json");
            data = JsonConvert.DeserializeObject(jsonString);
        }

        public class LoginSystem
        {
            private string gmailPath = "https://accounts.google.com/v3/signin/identifier?authuser=0&continue=https%3A%2F%2Fmyaccount.google.com%2F%3Fpli%3D1&ec=GAlAwAE&hl=tr&service=accountsettings&flowName=GlifWebSignIn&flowEntry=AddSession&dsh=S1292135306%3A1719765769885536&ddm=0";
            private string githubPath = "https://github.com/login";
            private string instagramPath = "https://www.instagram.com/";
            private string steamPath = "https://store.steampowered.com/login/?redir=&redir_ssl=1&snr=1_4_600__global-header";
            private string discordPath = "https://discord.com/login";

            private ChromeOptions options;
            private IWebDriver driver;

            public LoginSystem()
            {
                options = new ChromeOptions();
                options.AddExcludedArgument("enable-automation");
                options.AddAdditionalOption("useAutomationExtension", false);
                options.AddArgument("--disable-blink-features=AutomationControlled");

                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
            }

            private void OpenNewTab(string url)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript($"window.open('{url}', '_blank');");
            }

            public void LoginToGmail(string email, string password)
            {
                try
                {
                    OpenNewTab(gmailPath);
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

                    var emailBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"identifierId\"]")));
                    emailBox.SendKeys(email);
                    driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button")).Click();

                    var passwordBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"password\"]/div[1]/div/div[1]/input")));
                    passwordBox.SendKeys(password);
                    driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button")).Click();

                    Thread.Sleep(2000);

                    new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.UrlContains("myaccount.google.com"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                }
            }

            public void LoginToGithub(string email, string password)
            {
                try
                {
                    OpenNewTab(githubPath);
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

                    var emailBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"login_field\"]")));
                    emailBox.SendKeys(email);

                    var passwordBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"password\"]")));
                    passwordBox.SendKeys(password);

                    driver.FindElement(By.XPath("//*[@id=\"login\"]/div[4]/form/div/input[13]")).Click();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                }
            }

            public void LoginToInstagram(string email, string password)
            {
                try
                {
                    OpenNewTab(instagramPath);
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

                    var emailBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"loginForm\"]/div/div[1]/div/label/input")));
                    emailBox.SendKeys(email);

                    var passwordBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"loginForm\"]/div/div[2]/div/label/input")));
                    passwordBox.SendKeys(password);

                    driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[3]/button")).Click();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                }
            }

            public void LoginToSteam(string email, string password)
            {
                try
                {
                    OpenNewTab(steamPath);
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

                    var emailBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[3]/div[1]/div/div/div/div[2]/div/form/div[1]/input")));
                    emailBox.SendKeys(email);

                    var passwordBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[3]/div[1]/div/div/div/div[2]/div/form/div[2]/input")));
                    passwordBox.SendKeys(password);

                    driver.FindElement(By.XPath("//*[@id=\"responsive_page_template_content\"]/div[3]/div[1]/div/div/div/div[2]/div/form/div[4]/button")).Click();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                }
            }

            public void LoginToDiscord(string email, string password)
            {
                try
                {
                    OpenNewTab(discordPath);
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);

                    var emailBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"uid_7\"]")));
                    emailBox.SendKeys(email);

                    var passwordBox = new WebDriverWait(driver, TimeSpan.FromSeconds(45))
                        .Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"uid_9\"]")));
                    passwordBox.SendKeys(password);

                    driver.FindElement(By.XPath("//*[@id=\"app-mount\"]/div[2]/div[1]/div[1]/div/div/div/div/form/div[2]/div/div[1]/div[2]/button[2]")).Click();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong: {e.Message}");
                }
            }
        }

        LoginSystem snc = new LoginSystem();


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (data.ContainsKey("email") && data["email"] is JObject)
            {
                var emails = (JObject)data["email"];
                foreach (var email in emails)
                {
                    var username = email.Value["username"].ToString();
                    var password = email.Value["pass"].ToString();
                    snc.LoginToGmail(username, password);
                }
            }
            else
            {
                MessageBox.Show("No email data found or invalid format.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            dynamic discord = data.discord;
            string username = discord.username;
            string password = discord.pass;
            snc.LoginToDiscord(username, password);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            dynamic instagram = data.instagram;
            string username = instagram.username;
            string password = instagram.pass;
            snc.LoginToInstagram(username, password);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            dynamic github = data.github;
            string username = github.username;
            string password = github.pass;
            snc.LoginToGithub(username, password);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            dynamic steam = data.steam;
            string username = steam.username;
            string password = steam.pass;
            snc.LoginToSteam(username, password);
        }
    }
}
