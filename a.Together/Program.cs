using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("def")
    .AddCookie("def");

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use((ctx, next) =>
{
    if (ctx.User.Identity.IsAuthenticated)
    {
        if(!ctx.Request.Headers.Cookie.Any(x => x.Contains("user-info", System.StringComparison.CurrentCulture))){
        var user = new {username = "magnus"};
        var UserJson = JsonSerializer.Serialize(user);
        var userBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(UserJson));
        ctx.Response.Cookies.Append("user-info-payload", userBase64);
        ctx.Response.Cookies.Append("user-info", "1");
        }
    }
    return next();
});

app.UseEndpoints(_ => { });

app.UseSpa(x => x.UseProxyToSpaDevelopmentServer("http://localhost:5174"));

app.MapGet("/api/test", () => "secret!").RequireAuthorization();

//app.MapGet("/", () => "Hello World!");

app.MapPost("/api/login", async ctx =>
{
    await ctx.SignInAsync("def", new ClaimsPrincipal(

        new ClaimsIdentity(
            new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            },
            "def"
        )
      ),
      new AuthenticationProperties()
      {
          IsPersistent = true
      });
});

app.Run();


//builder.Services.AddAuthentication("def")
//    .AddCookie("def");

//builder.Services.AddAuthorization();

//var app = builder.Build();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();




//app.MapGet("/api/test", () => "secret!").RequireAuthorization();

//app.MapGet("/api/login", async ctx =>
//{
//    await ctx.SignInAsync("def", new ClaimsPrincipal(

//        new ClaimsIdentity(
//            new Claim[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
//            },
//            "def"
//        )
//      ),
//      new AuthenticationProperties()
//      {
//          IsPersistent = true
//      });
//});
