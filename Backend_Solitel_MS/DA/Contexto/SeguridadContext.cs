using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Contexto
{
    public class SeguridadContext : IdentityDbContext
    {
        public SeguridadContext(DbContextOptions options) : base(options)
        {
        }
    }
}
