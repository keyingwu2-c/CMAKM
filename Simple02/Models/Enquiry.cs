using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Simple02.Models
{
    public class Enquiry
    {

        [Column("Enquiry_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EID { get; set; }

        public DateTime lastUpated { get; set; }

        public int pageview { get; set; }

        public virtual Vote cvoting { get; set; }

        [Required]
        [Column("qtn_tile")]
        public string Title { get; set; }

        //the original string of products/services/destinations specified.
        [Required]
        public string Product { get; set; }

        public string Service { get; set; }

        public string Destination { get; set; }

        //----------------------------------------------------------------------------------------------$
        public ICollection<Topic> Topics { get; set; }

        //What is the difference between navigation properties with "virtual" and without it ?

        public virtual ICollection<FilePath> FilePaths { get; set; }
        //may add another for attachments to do with answer/consolidation !

        public virtual Answer ExpertAnswer { get; set; }

        //If not null, the main life cycle end.
        public string AnswerToCustomer { get; set; }

        public virtual ICollection<Post> CaseComments { get; set; }

        //Who create this enquiry record.
        public virtual ApplicationUser Noter { get; set; }

        //public virtual ICollection<Expert> Exptscalled{ get; set; }//may not need !

        //Has Deal, No Deal (Should be bool or enum ?)
        public string BssResult { get; set; }

        //----------------------------------------------------------------------------------------------$

        public virtual Customer fromCstr { get; set; }

        [Column("Expected Rely Date")]
        public DateTime? ExpRpDate { get; set; }//where does "0001-01-01 00:00:00" come from ? Why out of range ?

        public DateTime CompletedDate { get; set; }

        public string Qdetail { get; set; }

        //public byte[] File { get; set; }
        public int FileCount { get; set; }

        public enum QType
        {
            Test=0,
            Regulation=1,
            Other=2
        }

        public QType Qtype { get; set; }

        public string Regulation { get; set; }

        public string TypeSpecified { get; set; }

        public string KeyElementsSpecified { get; set; }

        //Used by BD AddNote
        public Enquiry(string Qtn, string Ansr, ApplicationUser noter)
        {
            
            Title = Qtn;
            AnswerToCustomer = Ansr;
            Noter = noter;
            lastUpated = DateTime.Now;
            CompletedDate = lastUpated;
            //dcsnStatus = "off";
        }

        //Used by Consult
        public Enquiry(string Qtn, ApplicationUser noter)
        {
            ExpRpDate = DateTime.Today.AddDays(2);
            Title = Qtn;
            Noter = noter;
            lastUpated = DateTime.Now;
            CompletedDate = DateTime.Today.AddDays(2);
            //dcsnStatus = "wait";//Should move to Answer Class !
            //AnswerToCustomer = "[Will be set by Facilitator]";
        }

        public Enquiry()
        {
            
        }

        public bool Deng(Enquiry other)
        {
            if (this.Title == other.Title)
                return true;
            else
                return false;
        }

        public void GetTopics()
        {
            ApplicationDbContext topicing = new ApplicationDbContext();
            string[] splt =Product.Split(',', ' ');
            for (int i = 0; i < splt.Length; i++)
            {
                //topicing.Topics.Where()
            }
        }
    }


}