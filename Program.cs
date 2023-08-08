using App.ExtendMethods;
using App.Services;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// này controller vào view
builder.Services.AddControllersWithViews();
//này controller vào razorpages
builder.Services.AddRazorPages();


// builder.Services.AddTransient(typeof(ILogger<>), typeof(Logger<>)); //Serilog
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // /View/Controller/Action.cshtml
    // /MyView/Controller/Action.cshtml

    // {0} -> ten Action
    // {1} -> ten Controller
    // {2} -> ten Area
    options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
});


// builder.Services.AddSingleton<ProductService>();
// builder.Services.AddSingleton<ProductService, ProductService>();
// builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();

var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.AddStatusCodePage(); // Tuy bien Response loi: 400 - 599

    app.UseRouting();// EndpointRoutingMiddleware

    app.UseAuthentication(); // xac dinh danh tinh 
    app.UseAuthorization();  // xac thuc  quyen truy  cap

    app.MapGet("/sayhi", async (context) => {
        await context.Response.WriteAsync($"Hello ASP.NET MVC {DateTime.Now}");
    });


    // endpoints.MapControllers
    // endpoints.MapControllerRoute
    // endpoints.MapDefaultControllerRoute
    // endpoints.MapAreaControllerRoute

    // [AcceptVerbs]

    // [Route]

    // [HttpGet]
    // [HttpPost]
    // [HttpPut]
    // [HttpDelete]
    // [HttpHead]
    // [HttpPatch]

    // Area

    app.MapControllers();
    app.MapControllerRoute(
                    name: "first",
                    pattern: "{url:regex(^((xemsanpham)|(viewproduct))$)}/{id:range(2,4)}",
                    defaults: new
                    {
                        controller = "First",
                        action = "ViewProduct"
                    }

                );

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapAreaControllerRoute(
                    name: "product",
                    pattern: "/{controller}/{action=Index}/{id?}",
                    areaName: "ProductManage"
                );

    // Controller khong co Area
    app.MapControllerRoute(
                    name: "default",
                    pattern: "/{controller=Home}/{action=Index}/{id?}"
                );
    app.MapRazorPages();

    app.Run();

