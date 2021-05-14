using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetMoovies.Models;

namespace NetMoovies.Data
{
    public class NetMooviesContext : DbContext
    {
        public NetMooviesContext (DbContextOptions<NetMooviesContext> options)
            : base(options)
        {
        }

        public DbSet<NetMoovies.Models.Usuario> Usuario { get; set; }

        public DbSet<NetMoovies.Models.Filme> Filme { get; set; }
    }
}
