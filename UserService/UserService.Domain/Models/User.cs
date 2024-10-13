using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models
{
    public class User: IdentityUser<Guid>
    {
        public User() 
        {
            Id = Guid.NewGuid();    
        }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
