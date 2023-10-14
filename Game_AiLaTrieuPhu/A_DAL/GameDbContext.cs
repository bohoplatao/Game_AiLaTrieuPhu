using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_AiLaTrieuPhu.A_DAL
{
    internal class GameDbContext: DbContext
    {
        public GameDbContext()
        {

        }

        public GameDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-CXH2410\SQLEXPRESS;Initial Catalog=AiLaTrieuPhu;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
