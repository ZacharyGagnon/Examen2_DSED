using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSED_Examen2_Web.Controllers
{
    public class TableauDeBordController : Controller
    {
        // GET: TableauDeBordController
        public ActionResult Index()
        {
            return View();
        }
    }
}
