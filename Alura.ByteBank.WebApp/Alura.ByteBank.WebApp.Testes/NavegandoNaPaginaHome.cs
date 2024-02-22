using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome
    {
        private IWebDriver _driver;

        public NavegandoNaPaginaHome()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309");
            
            //Assert
            Assert.Contains("WebApp", _driver.Title);
        }

        [Fact]
        public void CarregaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309");
            
            //Assert
            Assert.Contains("Login", _driver.PageSource);
            Assert.Contains("Home", _driver.PageSource);
        }

        [Fact]
        public void LogandoNoSistema()
        {
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309/");
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
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309/");
            var linkLogin = _driver.FindElement(By.LinkText("Login"));
            linkLogin.Click();

            //Assert
            Assert.Contains("img", _driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("401", _driver.PageSource);
        }

        [Fact]
        public void AcessarPaginaSemEstarLogadoVerificaURL()
        {
            //Arrange
            //Act
            _driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", _driver.Url);
            Assert.Contains("401", _driver.PageSource);
        }


    }
}
