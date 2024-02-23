using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        private IWebDriver _driver;
        private ITestOutputHelper _saidaConsoleTeste;

        public AposRealizarLogin(ITestOutputHelper saidaConsoleTeste)
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            _saidaConsoleTeste = saidaConsoleTeste;
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

        [Fact]
        public void RealizarLoginAcessaMenuECadastraCliente()
        {
            //Arrange
            var login = _driver.FindElement(By.Id("Email"));
            var senha = _driver.FindElement(By.Id("Senha"));
            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            _driver.FindElement(By.Id("btn-logar")).Click();
            _driver.FindElement(By.LinkText("Cliente")).Click();
            _driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            _driver.FindElement(By.Name("Identificador")).Click();
            _driver.FindElement(By.Name("Identificador")).SendKeys("20cd1c01-5fbf-40b7-b41b-0341bd38fc32");
            
            _driver.FindElement(By.Name("CPF")).Click();
            _driver.FindElement(By.Name("CPF")).SendKeys("536.186.690-38");

            _driver.FindElement(By.Name("Nome")).Click();
            _driver.FindElement(By.Name("Nome")).SendKeys("Marcos Paulo");

            _driver.FindElement(By.Name("Profissao")).Click();
            _driver.FindElement(By.Name("Profissao")).SendKeys("Desenvolvedor");

            //Act
            _driver.FindElement(By.CssSelector(".btn-primary")).Click();
            _driver.FindElement(By.LinkText("Home")).Click();

            //Assert
            Assert.Contains("Logout", _driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {
            //Arrange
            var login = _driver.FindElement(By.Id("Email"));
            var senha = _driver.FindElement(By.Id("Senha"));
            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");
            
            _driver.FindElement(By.Id("btn-logar")).Click();
            _driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.TagName("a"));

            foreach (IWebElement e in elements) 
            { 
                _saidaConsoleTeste.WriteLine(e.Text); 
            }

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();
            //Act
            elemento.Click();

            //Assert
            Assert.Contains("Voltar", _driver.PageSource);
        }


    }
}
