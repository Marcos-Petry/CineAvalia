using CineAvalia.Data;
using CineAvalia.Helper;
using CineAvalia.Implementacoes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<UsuarioImplementacao, UsuarioImplementacao>();
builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSession(o => 
{ 
    o.Cookie.HttpOnly    = true;
    o.Cookie.IsEssential = true;
});


var connectionString = builder.Configuration.GetConnectionString("ConnectionPostgres"); // busca a string de conex�o do banco, que  foi definida no arq appsettings.development
builder.Services.AddDbContext<CineAvaliaContext>(options => options.UseNpgsql(connectionString));

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

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
