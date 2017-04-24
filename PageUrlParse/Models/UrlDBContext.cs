using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PageUrlParse.Models
{
    public class UrlDBContext : DbContext
    {
        public UrlDBContext()
            : base("UrlDBConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Url_tbl> UrlPages { get; set; }
        public DbSet<PageList_tbl> PageLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //one-to-many 
            modelBuilder.Entity<PageList_tbl>()
                        .HasOptional<Url_tbl>(s => s.Url_tbl)
                        .WithMany(s => s.PageLists_tbl)
                        .HasForeignKey(s => s.UrlId);

        }
    }
}
