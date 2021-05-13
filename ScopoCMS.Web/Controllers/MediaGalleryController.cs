using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ScopoCMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScopoCMS.Web.Controllers
{
    public class MediaGalleryController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public MediaGalleryController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveFile()
        {
            var res = "";
            var mv = new MediaGalleryViewModel();

            if (Request.Form.Files.Count > 0)
            {
                mv.image = Request.Form.Files[0];

                if (mv.image != null && mv.image.Length > 0)
                {
                    var imgpath = Path.Combine(_appEnvironment.WebRootPath, "Images");
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(mv.image.FileName);

                    using (var fileStream = new FileStream(Path.Combine(imgpath, fileName), FileMode.Create))
                    {
                        mv.image.CopyTo(fileStream);
                        string filePath = "uploads\\img\\" + DateTime.Now + fileName;
                        res = filePath;
                    }
                }
            }
            return Json(res);
        }
    }
}
