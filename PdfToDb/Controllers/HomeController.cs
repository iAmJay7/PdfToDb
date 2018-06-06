using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using PdfToDb.Models;

namespace PdfToDb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                using (PdfReader reader=new PdfReader(file.InputStream))
                {
                    int pageNo = reader.NumberOfPages;
                    string[] words;
                    for (int i = 1; i < pageNo; i++)
                    {
                        var text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                        words = text.Split('\n');
                        var obj=EmpDataBao.GetsEmpData(words, new string[] { "CODE", "NAME", "WORK HOURS", "OVER TIME" });
                        EmpDataDao.Add(obj);
                    }
                }
                return RedirectToAction("List");
            }
            return View();
        }
        
        public ActionResult List()
        {
            return View(EmpDataDao.Gets());
        }
    }
}