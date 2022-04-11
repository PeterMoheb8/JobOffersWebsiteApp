﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace JobOffersWebsiteApp.Models
{
    public class Job
    {

        public int Id { get; set; }

        [Display(Name = ( "اسم الوظيفة"))]
        public string JobTitle { get; set; }

        [Display (Name = ("وصف الوظيفة") ) ]
        [AllowHtml]
        public string JobContent { get; set; }

        [Display(Name = ("صورة الوظيفة"))]
        public string JobImage { get; set; }

        [Display(Name = ("نوع الوظية "))]
        public int CategoryId { get; set; }

        public string UserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}