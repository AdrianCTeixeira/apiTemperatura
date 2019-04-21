using System;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1 : ApiController
    {
        [TestMethod]
        public void TestMethod1()
        {
            TemperaturaController tempController = new TemperaturaController();
            Assert.IsTrue(tempController.GetCityTemperatures("Petropolis") != null);            
            Assert.IsTrue(tempController.CadastrarCidadePorCEPAsync("25650028") == Ok());
            Assert.IsTrue(tempController.GetAllTemperatures() != null);
            Assert.IsTrue(tempController.DeleteCityTemps("Nova Petropolis") == Ok());
            Assert.IsTrue(tempController.DeleteCity("Petropolis") == Ok());
            
        }
    }
}
