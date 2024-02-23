using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin: IClassFixture<Gerenciador>
    {
        private IWebDriver _driver;
        private ITestOutputHelper _saidaConsoleTeste;
        private LoginPO _loginPO;

        public AposRealizarLogin(Gerenciador gerenciador, ITestOutputHelper saidaConsoleTeste)
        {
            _driver = gerenciador.Driver;
            
            _driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            _saidaConsoleTeste = saidaConsoleTeste;

            _loginPO = new LoginPO(_driver);
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            _loginPO.PreencherCampos("andre@email.com", "senha01");
            _loginPO.Logar();

            //Assert
            Assert.Contains("Agência", _driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            _loginPO.Logar();

            //Assert
            Assert.Contains("The Email field is required.", _driver.PageSource);
            Assert.Contains("The Senha field is required.", _driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginComSenhaInvalida()
        {
            _loginPO.PreencherCampos("andre@email.com", "senha0101");
            _loginPO.Logar();

            //Assert
            Assert.Contains("Login", _driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaMenuECadastraCliente()
        {
            _loginPO.PreencherCampos("andre@email.com", "senha01");
            _loginPO.Logar();

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

            _driver.FindElement(By.CssSelector(".btn-primary")).Click();
            _driver.FindElement(By.LinkText("Home")).Click();

            //Assert
            Assert.Contains("Logout", _driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {
            _loginPO.PreencherCampos("andre@email.com", "senha01");
            _loginPO.Logar();

            _driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.TagName("a"));

            foreach (IWebElement e in elements) 
            { 
                _saidaConsoleTeste.WriteLine(e.Text); 
            }

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();
            elemento.Click();

            //Assert
            Assert.Contains("Voltar", _driver.PageSource);
        }
        
    }
}
