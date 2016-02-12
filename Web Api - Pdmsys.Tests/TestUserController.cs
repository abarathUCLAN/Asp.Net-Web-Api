using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Web_Api___Pdmsys.Controllers;
using Web_Api___Pdmsys.Models;

namespace Web_Api___Pdmsys.Tests
{
    [TestClass]
    public class TestUserController
    {

        private ModelCreatorHelper helper;
        private UserController uc;

        [TestInitialize]
        void init()
        {
            this.helper = new ModelCreatorHelper();
            this.uc = new UserController();

        }

        [TestMethod]
        public void Register_ShouldFailBecauseOfInvalidUsername()
        {
            RegisterModel registerUser = new RegisterModel();
            registerUser.Firstname = "swasdg";
            registerUser.Lastname = "lastname";
            registerUser.email = "test@test.com";
            registerUser.Password = "Test12§$";
            //RegisterModel registerUser = helper.CreateRegisterModel(null, "lastname", "test@test.com", "Test12§$");
            var response = new PreliminaryStudyController().GetProjectDescription(49);
            Console.WriteLine(response);
            Assert.AreEqual(response, HttpStatusCode.OK);
            
        }
    }
}
