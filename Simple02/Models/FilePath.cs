using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class FilePath
    {
        public int FilePathId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public string OrgnFileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public FileType FileType { get; set; }

        public virtual Enquiry enquiry { get; set; }

        public virtual Answer answer { get; set; }

        public virtual Post post { get; set; }
    }
}