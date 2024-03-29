﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;
using Alura.ByteBank.WebApp.Testes.PageObjects;
using System;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome : IDisposable
    {
        private IWebDriver _driver;
        private HomePO _homePO;

        public NavegandoNaPaginaHome()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Navigate().GoToUrl("https://localhost:44309");
            _homePO = new HomePO(_driver);
        }

        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            //Assert
            Assert.Contains("WebApp", _driver.Title);
        }

        [Fact]
        public void CarregaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            //Assert
            Assert.Contains("Login", _driver.PageSource);
            Assert.Contains("Home", _driver.PageSource);
        }

        [Fact]
        public void LogandoNoSistema()
        {
            _driver.Manage().Window.Size = new System.Drawing.Size(1294, 869);

            _driver.FindElement(By.LinkText("Login")).Click();
            _driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            _driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            _driver.FindElement(By.Id("btn-logar")).Click();

            _driver.FindElement(By.CssSelector(".btn")).Click();
            _driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            _driver.FindElement(By.Id("Senha")).SendKeys("senha01");
        }

        [Fact]
        public void ValidaLinkDeLoginNaHome()
        {
            _homePO.Navegar("https://localhost:44309/");
            
            var linkLogin = _driver.FindElement(By.LinkText("Login"));
            linkLogin.Click();

            //Assert
            Assert.Contains("img", _driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            _homePO.Navegar("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("401", _driver.PageSource);
        }

        [Fact]
        public void AcessarPaginaSemEstarLogadoVerificaURL()
        {
            _homePO.Navegar("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", _driver.Url);
            Assert.Contains("401", _driver.PageSource);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
