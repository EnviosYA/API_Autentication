using Moq;
using NUnit.Framework;
using PS.Template.Application.Services;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.RequestApis;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Autenticacion.UnitTest
{
    public class TestCuenta
    {
        private Mock<IGenerateRequest> _request;
        protected Mock<ICuentaRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _request = new Mock<IGenerateRequest>();
            _repository = new Mock<ICuentaRepository>();
        }

        [Test]
        public void FindDataAccountInvalido()
        {
            //Arrange
            UserInfo prueba = new UserInfo()
            {
                Email = "lea@gmail.com",
                Password = "lea123456"
            };

            CuentaService cuentaService = new CuentaService(_repository.Object, _request.Object);

            //Act
            var result = cuentaService.FindDataAccount(prueba);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ValidarAltaCuenta()
        {
            //Arrange
            CuentaDTO cuenta = new CuentaDTO
            {
                Mail = "lea@gmail.com",
                Contraseña = "lea123456",
                IdUsuario = 1,
                IdTipoCuenta = 1
            };

            CuentaService cuentaService = new CuentaService(_repository.Object, _request.Object);

            //Act
            var result = cuentaService.AltaCuenta(cuenta);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ValidarCuenta()
        {
            //Arrange
            string mail = "lea@gmail.com";
      
            CuentaService cuentaService = new CuentaService(_repository.Object, _request.Object);

            //Act
            var result = cuentaService.ValidarCuenta(mail);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
