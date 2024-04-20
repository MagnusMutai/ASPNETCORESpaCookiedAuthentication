using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("def")
    .AddCookie("def");

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

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