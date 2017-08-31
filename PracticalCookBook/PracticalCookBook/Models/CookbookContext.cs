using System;
using System.Collections.Generic;
using System.Data.Entity;
using SQLite.CodeFirst;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCookBook.Models
{
    public class CookbookContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CookbookContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);//there's also DbContext.Database
        }

        public CookbookContext() : base("SQLiteDatabase") { }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
