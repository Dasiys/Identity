using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Model
{
    public enum Cities
    {
        LONDON,PARIS,CHICAGO
    }
    public class AppUser:IdentityUser
    {
        public Cities city { get; set; }
    }
}
