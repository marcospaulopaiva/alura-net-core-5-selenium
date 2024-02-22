using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        private IWebDriver _driver;

        public AposRealizarLogin()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            //Arrange
            var login = _driver.FindElement(By.Id("Email")); 
            var senha = _driver.FindElement(By.Id("Senha")); 
            var btnLogar = _driver.FindElement(By.Id("btn-logar"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Agência", _driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            //Arrange
            var login = _driver.FindElement(By.Id("Email"));
            var senha = _driver.FindElement(By.Id("Senha"));
            var btnLogar = _driver.FindElement(By.Id("btn-logar"));

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("The Email field is required.", _driver.PageSource);
            Assert.Contains("The Senha field is required.", _driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginComSenhaInvalida()
        {
            //Arrange
            var login = _driver.FindElement(By.Id("Email"));
            var senha = _driver.FindElement(By.Id("Senha"));
            var btnLogar = _driver.FindElement(By.Id("btn-logar"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha0102");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Login", _driver.PageSource);
        }

    }
}
