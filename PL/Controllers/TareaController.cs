﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult GetAll()
        {
            return View();
        }
        
    }
}