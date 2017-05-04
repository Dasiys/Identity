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
        CHICAGO,LONDON, PARIS
    }

    public enum Countries
    {
        NONE,UK,FRANCE,USA
    }
    public class AppUser:IdentityUser
    {
        public Cities City { get; set; }
            public Countries Country { set; get; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.LONDON:
                    Country = Countries.UK;
                    break;
                case Cities.PARIS:
                    Country = Countries.FRANCE;
                    break;
                case Cities.CHICAGO:
                    Country = Countries.USA;
                    break;
                default:
                    Country = Countries.NONE;
                    break;
            }
        }
    }
}
