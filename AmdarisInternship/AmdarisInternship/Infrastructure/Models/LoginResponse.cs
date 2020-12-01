using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Infrastructure.Models
{
    public class LoginResponse
    {
        public bool isSuccess { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpiration { get; set; }

        public string MaxRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
