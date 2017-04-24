using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PageUrlParse.Models
{
    [Table("UrlPages")]
    public class Url_tbl
    {
        public Url_tbl()
        {
            this.PageLists_tbl = new HashSet<PageList_tbl>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUrl { get; set; }
        public string NameUrl { get; set; }
        public decimal MaxSpeed { get; set; }
        public decimal MinSpeed { get; set; }

        public virtual ICollection<PageList_tbl> PageLists_tbl { get; set; }
    }
}
