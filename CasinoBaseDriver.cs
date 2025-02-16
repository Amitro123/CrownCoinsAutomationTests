using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

public class BaseDrivers
{
    private static IWebDriver _driver;
    private static readonly object _lock = new object();
    protected Dictionary<string, TestStatus> PipelineTestsStatuses = new Dictionary<string, TestStatus>();
    public static string TestUrl { get; private set; } = "https://app.dev.crowncoinscasino.com/";

    public static IWebDriver Driver
    {
        get
        {
            if (_driver == null)
            {
                lock (_lock)
                {
                    if (_driver == null)
                    {
                        InitializeDriver(TestUrl);
                    }
                }
            }
            return _driver;
        }
    }

    private static void InitializeDriver(string url)
    {
        for (int i = 0; i < 3; i++)
        {
            try
            {
                Console.WriteLine("Initializing WebDriver...");
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-notifications");

                _driver = new ChromeDriver(options);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

                Console.WriteLine($"Navigating to {url}...");
                _driver.Navigate().GoToUrl(url);
                Console.WriteLine("WebDriver initialized successfully.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Attempt {i + 1}: Error initializing WebDriver: {ex.Message}");
                DisposeDriver();
            }
        }
        throw new Exception("WebDriver failed to initialize after multiple attempts.");
    }

    public static void DisposeDriver()
    {
        if (_driver != null)
        {
            try
            {
                _driver.Quit();
                _driver.Dispose();
                Console.WriteLine("WebDriver disposed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing WebDriver: {ex.Message}");
            }
            finally
            {
                _driver = null;
            }
        }
    }

    public static void SetTestUrl(string newUrl)
    {
        if (!string.IsNullOrEmpty(newUrl))
        {
            TestUrl = newUrl;
            Console.WriteLine($"Test URL updated to: {TestUrl}");
        }
    }
}


