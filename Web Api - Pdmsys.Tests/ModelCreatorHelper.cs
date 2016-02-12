using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api___Pdmsys.Models;

namespace Web_Api___Pdmsys.Tests
{
    class ModelCreatorHelper
    {
        public RegisterModel CreateRegisterModel(String firstname, String lastname,
                                                 String email, String password)
        {
            RegisterModel model = new RegisterModel();
            model.Firstname = firstname;
            model.Lastname = lastname;
            model.email = email;
            model.Password = password;
            return model;
        }
    }
}
