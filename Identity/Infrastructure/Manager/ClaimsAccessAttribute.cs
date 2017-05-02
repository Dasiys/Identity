using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Manager
{
    public class ClaimsAccessAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string Issuer { set; get; }
        public string ClaimType { set; get; }
        public string Value { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.Identity is ClaimsIdentity
                && ((ClaimsIdentity)httpContext.User.Identity).HasClaim(x => x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value);
        }
    }
}
