using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class HomePO
    {
        private IWebDriver _driver;
        private By linkHome;
        private By linkContaCorrente;
        private By linkClientes;
        private By linkAgencias;

        public HomePO(IWebDriver driver)
        {
            _driver = driver;
            linkHome = By.Id("home");
            linkContaCorrente = By.Id("contacorrente");
            linkClientes = By.Id("clientes");
            linkAgencias = By.Id("agencia");
        }

        public void Navegar(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void LinkHomeClick()
        {
            _driver.FindElement(linkHome).Click();
        }

        public void linkContaCorrenteClick()
        {
            _driver.FindElement(linkContaCorrente).Click();
        }

        public void linkClientesClick()
        {
            _driver.FindElement(linkClientes).Click();
        }

        public void linkAgenciasClick()
        {
            _driver.FindElement(linkAgencias).Click();
        }
    }
}
