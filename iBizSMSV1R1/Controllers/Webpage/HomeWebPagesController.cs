using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace iBizSMSV1R1.Controllers.Webpage
{
    //[Authorize(Roles = "Admin")]
    public class HomeWebPagesController : Controller
    {
        private static ApplicationDbContext _context;

        public HomeWebPagesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        public JsonResult getWebPageTitles2()
        {
            IList<string> result = new List<string>();

            var query = (from p in _context.WebPageTitles                        
                         select p.title);
            result.Add("");
            foreach (var x in query)
            {
                result.Add(x.ToString());
            }
            return Json(result);
        }
        public static IEnumerable<WebPageTitles> getWebPageTitles()
        {

            IEnumerable<WebPageTitles> smlist = (from c in _context.WebPageTitles
                                                 orderby c.recno
                                                 select new WebPageTitles
                                                 {
                                                     title = c.title,
                                                     recno = c.recno
                                                 }).Distinct();

            return smlist;
        }

        public static IEnumerable<WebPages> getWebPages()
        {

            IEnumerable<WebPages> smlist = (from c in _context.WebPages
                                                 orderby c.recno
                                                 select new WebPages
                                                 {
                                                     title = c.title,
                                                     recno = c.recno
                                                 }).Distinct();

            return smlist;
        }
    }


    
}