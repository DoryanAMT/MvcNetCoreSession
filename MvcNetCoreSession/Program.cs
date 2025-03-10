using MvcNetCoreSession.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//  INYECTAMOS TAMBIEN EL HELPER
builder.Services.AddSingleton<HelperSessionContextAccessor>();
//  CON ESTE singleton PODREMOS USAR LA CLASE httpContextAccesor 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//  DEBEMOS HABILITAR EL SERVICIO DE MEMORIA CACHE
//  PORQUE COMPARTEN CARCTERISTICAS
builder.Services.AddDistributedMemoryCache();
//  CONFIGURAMOS SESSION CON UN TIEMPO DE INACTIVIDAD




builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//HABILITAMOS SESSION PARA EL SERVIDOR
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
