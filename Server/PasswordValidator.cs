using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Server
{
    class PasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.Equals(userName, "Alice", StringComparison.OrdinalIgnoreCase)
                && password == "Password1234") return;

            throw new SecurityTokenValidationException();
        }
    }
}
