using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Infrastructure.Manager
{
    public class CustomPasswordValidator: PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string item)
        {
            IdentityResult resullt = await base.ValidateAsync(item);
            if (item.Contains("123456"))
            {
                var errors = resullt.Errors.ToList();
                errors.Add("密码不能包含123456");
                resullt = new IdentityResult(errors);
            }
            return resullt;
        }
    }
}
