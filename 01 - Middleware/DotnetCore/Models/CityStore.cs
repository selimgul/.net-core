using System.Collections.Generic;

namespace DotnetCore.Models
{
    public static class CityStore
    {        
        public static List<City> Cities { get; set; }

        static CityStore()
        {    
            Cities = new List<City>
            {
                new City{ID = 1, Name = "İstanbul" },
                new City{ID = 2, Name = "Ankara" },
                new City{ID = 3, Name = "İzmir" }
            };
        }
    }
}
