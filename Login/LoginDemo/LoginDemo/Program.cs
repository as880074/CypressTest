using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// �]�m cookie ���ҧ@�����ε{���w�]�����Ҥ��
// �N cookie �A�ȲK�[��A�Ȯe����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        // �p�G�n�J���ѷ|����ƻ򭶭�
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
    // �Ҧ� Cookie.SamSite �]�m���|�Q���ɬ� Strict
    MinimumSameSitePolicy = SameSiteMode.Strict,
    // Cookie.SamSite �]�m�� None ���ܷ|�Q���ɬ� Lax
    //MinimumSameSitePolicy = SameSiteMode.Lax,  
    // MinimumSameSitePolicy �]�m���̼e�P�A�]�����|�v�T Cookie.SamSite
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
