
using Blazor_Quiz_Api.Api;
using Blazor_Quiz_Api.Api.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;  //no underlines and imported via Nuget
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<HttpClient>();

builder.Services.AddDbContext<Context>(
 options => options.UseSqlite("name=ConnectionStrings:DBConnection"));

//builder.Services.AddScoped<ContextServices>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(cfg =>
{
    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtIssuer"], //mo¿e byc wpisane jako zwyk³y string teraz jest zapisane a appsetting.json
        ValidAudience = builder.Configuration["JwtIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(MinimalAPIValidator));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(option => option.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
    new[] { "application/octet-stream" }));

//builder.Services.AddDbContext<Context>(options => options.UseSqlServer("Server=LAPTOP-633GPHS7; database=Main; User Id=Entity; Password=Entity"));






var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCompression();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.MapHub<UserHub>("/UserHub");
app.MapGet("/token", () =>
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier,"user-id"),
        new Claim(ClaimTypes.Name,"Test Name"),
                new Claim(ClaimTypes.Role,"Adim"),
    };
    var token = new JwtSecurityToken(
        issuer: builder.Configuration["JwtIssuer"],
        audience: builder.Configuration["JwtIssuer"],
        claims: claims,
        expires: DateTime.UtcNow.AddDays(1),
        notBefore: DateTime.UtcNow,
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])), SecurityAlgorithms.HmacSha256
            )
        );
    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
    return jwtToken;
});


#region SimpleMinApi
app.MapGet("/questions", (Context context) => context.Questions.ToList());

//app.MapGet("/User/{id}", (Context context, [FromRoute] int id) => context.Users.Where(n => n.Id == id).FirstOrDefault());

//app.MapPost("/User", (Context context, Blazor_Quiz_Api.Api.Models.Users user) =>
//{
//    context.Entry(user).State = EntityState.Added;
//    context.SaveChanges();
//});



//app.MapPut("/User/{id}", (Context context, Blazor_Quiz_Api.Api.Models.Users user, int id) =>
//{
//    context.Entry(user).State = EntityState.Modified;
//    context.SaveChanges();
//});


//app.MapDelete("/User/{id}", (Context context, int id) =>
//{
//    var a = context.Users.Where(n => n.Id == id).FirstOrDefault() ?? new();
//    context.Entry(a).State = EntityState.Deleted;
//    context.SaveChanges();
//});


#endregion

#region AdvancedMinimalApi

//app.MapGet("/Users", MinimalAPI.GetAll);
//app.MapGet("/User/{id}", MinimalAPI.GetById);
//app.MapPost("/User",MinimalAPI.Create);
//app.MapPut("/User/{id}",MinimalAPI.Update);
//app.MapDelete("/User/{id}", MinimalAPI.Delete);

//MinimalAPI.RegisterEndPoints(app);


app.RegisterEndPoints();
#endregion

#region MasterMinimalAPI


#endregion











//app.UseEndpoints(endpoints =>
//{
//    //endpoints.MapControllers();
//    //endpoints.MapRazorPages();
//    endpoints.MapHub<UserHub>("/UserHub");
//    endpoints.MapFallbackToFile("index.html");
//});
app.Run();
