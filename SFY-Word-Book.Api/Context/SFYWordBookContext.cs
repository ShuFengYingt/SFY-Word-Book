using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFY_Word_Book.Api.Context
{
    public class SFYWordBookContext : DbContext
    {
        public SFYWordBookContext(DbContextOptions<SFYWordBookContext> options) : base(options)
        {

        }


        public DbSet<LearningWordBook> LearningWords { get; set; }
        public DbSet<ReviewWordBook> ReviewWords { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sentence>()
                .HasKey(s => s.Id); // 配置主键
        }
    }
}
