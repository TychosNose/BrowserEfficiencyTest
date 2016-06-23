//--------------------------------------------------------------
//
// Microsoft Edge Power Test
// Copyright(c) Microsoft Corporation
// All rights reserved.
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files(the ""Software""),
// to deal in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF
// OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//--------------------------------------------------------------

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Threading;

namespace TestingPower
{
    class OpenFacebookAndScroll : Scenario
    {
        public OpenFacebookAndScroll()
        {
            Name = "facebook";
            Duration = 60;
        }

        public override void Run(RemoteWebDriver driver, string browser, List<UserInfo> logins)
        {
            driver.Navigate().GoToUrl("http://www.facebook.com");

            // if not logged on, log on
            var elems = driver.FindElements(By.CssSelector("H2"));
            foreach (IWebElement elem in elems)
            {
                if (elem.Text.Contains("Sign Up")) // not signed in
                {
                    string userName = string.Empty, passWord = string.Empty;

                    foreach (var item in logins)
                    {
                        if (item.Domain == "facebook.com")
                        {
                            userName = item.UserName;
                            passWord = item.PassWord;
                            break;
                        }
                    }

                    Thread.Sleep(2000);
                    var username = driver.FindElement(By.Id("email"));
                    var password = driver.FindElement(By.Id("pass"));
                    // var button = _driver.FindElement(By.Id("loginbutton"));
                    // Avoding applying click to button because of ObscureElement bug in Edge

                    username.Clear();
                    username.SendKeys(userName);
                    Thread.Sleep(1000);

                    password.Clear();
                    Thread.Sleep(3000);
                    password.SendKeys(passWord);
                    Thread.Sleep(1000);

                    driver.Keyboard.SendKeys(Keys.Tab);
                    driver.Keyboard.SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                    break;
                }
            }

            Program.scrollPage(20);
        }
    }
}
