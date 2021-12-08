using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace DrawingForm.Tests
{
    public class Robot
    {
        private WindowsDriver<WindowsElement> _driver;
        private Dictionary<string, string> _windowHandles;
        private string _root;
        private const string WIN_APP_DRIVER_URI = "http://127.0.0.1:4723";

        const string APPLICATION = "app";
        const string DEVICE_NAME = "deviceName";
        const string WINDOWS = "WindowsPC";
        const int WAITING_SECONDS = 5;

        // constructor
        public Robot(string targetAppPath, string root)
        {
            this.Initialize(targetAppPath, root);
        }

        // initialize
        public void Initialize(string targetAppPath, string root)
        {
            _root = root;
            var options = new AppiumOptions();
            options.AddAdditionalCapability(APPLICATION, targetAppPath);
            options.AddAdditionalCapability(DEVICE_NAME, WINDOWS);
            _driver = new WindowsDriver<WindowsElement>(new Uri(WIN_APP_DRIVER_URI), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WAITING_SECONDS);
            _windowHandles = new Dictionary<string, string>
            {
                {
                    _root, _driver.CurrentWindowHandle
                }
            };
        }

        // clean up
        public void CleanUp()
        {
            SwitchTo(_root);
            _driver.CloseApp();
            _driver.Dispose();
        }

        // test
        public void SwitchTo(string formId)
        {
            if (_windowHandles.ContainsKey(formId))
                _driver.SwitchTo().Window(_windowHandles[formId]);
            else
            {
                SwitchWithNotContain(formId);
            }
        }

        // test
        private void SwitchWithNotContain(string formId)
        {
            foreach (var windowHandle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(windowHandle);
                try
                {
                    _driver.FindElementByAccessibilityId(formId);
                    _windowHandles.Add(formId, windowHandle);
                    return;
                }
                catch
                {
                }
            }
        }

        // test
        public void Sleep(Double time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }

        // test
        public void ClickButton(string name)
        {
            _driver.FindElementByName(name).Click();
        }

        // test
        public void AssertEnableByName(string name, bool state)
        {
            WindowsElement element = _driver.FindElementByName(name);
            Assert.AreEqual(state, element.Enabled);
        }

        // get
        public bool GetEnable(string name)
        {
            WindowsElement element = _driver.FindElementByName(name);
            return element.Enabled;
        }

        // test
        public void AssertEnableById(string id, bool state)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(id);
            Assert.AreEqual(state, element.Enabled);
        }

        // drag
        public void DragAndDrop(string id, int x1, int y1, int x2, int y2)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(id);
            Actions action = new Actions(_driver);
            action.MoveToElement(element);
            action.MoveByOffset(x1, y1);
            action.ClickAndHold();
            action.MoveByOffset((x2 - x1), (y2 - y1));
            action.Release();
            action.Perform();
        }
    }
}
