using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Dominio
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRole { get; set; }
    }
}
