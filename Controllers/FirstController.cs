using System;
using System.IO;
using System.Linq;
using App.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _env;

        public FirstController(ILogger<FirstController> logger, ProductService productService, IWebHostEnvironment env)
        {
            _logger = logger;
            _productService = productService;
            _env = env;
        }

        public string Index()
        {
            // this.HttpContext
            // this.Request
            // this.Response
            // this.RouteData

            // this.User
            // this.ModelState
            // this.ViewData
            // this.ViewBag
            // this.Url
            // this.TempData

            // _logger.Log(LogLevel.Warning, "Thong bao abc");

            _logger.LogWarning("Thong bao");
            _logger.LogError("Thong bao");
            _logger.LogDebug("Thong bao");
            _logger.LogCritical("Thong bao");
            _logger.LogInformation("Index Action");
            // Serilog 


            return "Toi la Index cua First";
        }


        public void Nothing()
        {
            _logger.LogInformation("Nothing Action");
            Response.Headers.Add("hi", "xin chao cac ban");
        }

        public object Anything() => new int[] { 1, 2, 3 };

        public IActionResult Readme()
        {
            var content = @"
            Xin chao cac ban,
            cac ban dang hoc ve ASP.NET MVC




            XUANTHULAB.NET
            ";
            // kiểu plain
            return Content(content, "text/plain");
        }

        //public IActionResult Bird()
        //{
        //    string filePath = Path.Combine(_env.ContentRootPath, "Files", "bird.jpg");
        //    var bytes = System.IO.File.ReadAllBytes(filePath);
        //    string contentType = MimeKit.MimeTypes.GetMimeType(filePath);

        //    var cd = new System.Net.Mime.ContentDisposition
        //    {
        //        FileName = filePath,
        //        Inline = true,
        //    };

        //    Response.Headers.Add("Content-Disposition", cd.ToString());

        //    return File(bytes, contentType);
        //}

        public IActionResult IphonePrice()
        {
            return Json(
              new
              {
                  productName = "Iphone X",
                  Price = 1000
              }
            );
        }

        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Chuyen huong den " + url);
            return LocalRedirect(url); // local ~ host 

            // như nhau
            //return RedirectToAction("Privacy", "Home");
        }
        public IActionResult Google()
        {
            var url = "https://google.com";
            _logger.LogInformation("Chuyen huong den " + url);
            return Redirect(url); // local ~ host 
        }


        // ViewResult | View()
        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "Nhật";


            // View()  -> Razor Engine, doc .cshtml (template)
            //------------------------------------------------
            // View(template) - template đường dẫn tuyệt đối tới .cshtml
            // View(template, model) 
            // return View("/MyView/xinchao1.cshtml", username);

            // xinchao2.cshtml -> /View/First/xinchao2.cshtml
            // return View("xinchao2", username);

            // xinchao3.cshtml -> /MyView/First/xinchao3.cshtml
            // return View("xinchao3", username);

            // HelloView.cshtml -> /View/First/HelloView.cshtml
            // /View/Controller/Action.cshtml
             return View((object)username);


            // View();
            // View(Model);
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                // TempData["StatusMessage"] = "Sản phẩm bạn yêu cầu không có";
                StatusMessage = "Sản phẩm bạn yêu cầu không có";
                return Redirect(Url.Action("Index", "Home"));
            }


            // /View/First/ViewProduct.cshtml
            // /MyView/First/ViewProduct.cshtml

            // truyền theo cách Model
            // return View(product);

            // truyền theo ViewData
            // this.ViewData["product"] = product;
            // ViewData["Title"] = product.Name;
            // return View("ViewProduct2");




            ViewBag.product = product;
            return View("ViewProduct3");

        }



    }
}