using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GarageDatabse.Models;
using GarageDatabse.Repository;
using System.Web.Mvc;

namespace GarageDatabse.Controllers
{
    public class GarageController : Controller
    {
        GarageRepository garage = new GarageRepository();

        public ActionResult CheckIn()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult Detalis()
        {
           
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
    
}