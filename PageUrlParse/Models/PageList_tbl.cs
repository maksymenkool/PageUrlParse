using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PageUrlParse.Models
{
    [Table("PageLists")]
    public class PageList_tbl
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPageList { get; set; }
        public string NamePage { get; set; }
        public decimal Speed { get; set; }

        public int? UrlId { get; set; }
        public virtual Url_tbl Url_tbl { get; set; }
    }
}
