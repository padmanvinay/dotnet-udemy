using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class CharacterDbContext : DbContext
    {
        public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}