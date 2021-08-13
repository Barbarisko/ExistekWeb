//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistekWEbProject.CustomConfiguration
{
    public class PublishContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<PublishOptions> Options { get; set; }

        public PublishContext(string connectionString) =>
            _connectionString = connectionString;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    _ = _connectionString switch
        //    {
        //        //if string is not null
        //        { Length: > 0 } => optionsBuilder.UseSqlServer(_connectionString),

        //        _ => optionsBuilder.UseInMemoryDatabase("InMemoryDatabase")
        //    };
        //}
    }
}
