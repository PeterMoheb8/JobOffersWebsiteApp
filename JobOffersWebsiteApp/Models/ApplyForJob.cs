using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Web.Mvc;

namespace JobOffersWebsiteApp.Models
{
    public class ApplyForJob
    {
        //This Tbale Many To Many >> Table Job and table Users to create all details

        public int Id { get; set; }

   
        public string Meesage { get; set; }

        public DateTime ApplayDate { get; set; }

        //Navication Prop
        public virtual Job Job { get; set; }

        public int JobId { get; set; }

        public string UserId { get; set; }
        //Navication Prop
        public virtual ApplicationUser User { get; set; }
    }
}