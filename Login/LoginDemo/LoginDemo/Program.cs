using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// 設置 cookie 驗證作為應用程式預設的驗證方案
// 將 cookie 服務添加到服務容器當中
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        // 如果登入失敗會跳轉甚麼頁面
        x.LoginPath = new PathString("/home");
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCookiePolicy(new CookiePolicyOptions
{
    // 所有 Cookie.SamSite 設置都會被提升為 Strict
    MinimumSameSitePolicy = SameSiteMode.Strict,
    // Cookie.SamSite 設置為 None 的話會被提升為 Lax
    //MinimumSameSitePolicy = SameSiteMode.Lax,  
    // MinimumSameSitePolicy 設置為最寬鬆，因此不會影響 Cookie.SamSite
    //MinimumSameSitePolicy = SameSiteMode.None, 
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
