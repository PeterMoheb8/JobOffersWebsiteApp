using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffersWebsiteApp.Models
{
    public class Category
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = ("نوع الوظية "))]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = ("وصف الوظيفة "))]
        public string CategoryDeccriptions { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}