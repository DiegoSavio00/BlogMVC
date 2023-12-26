using BlogMVC.Infra;
using BlogMVC.Repositories.BlogPostComentarios;
using BlogMVC.Repositories.BlogPostLikes;
using BlogMVC.Repositories.BlogPosts;
using BlogMVC.Repositories.Images;
using BlogMVC.Repositories.Tags;
using BlogMVC.Repositories.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:MeuBlogDb"]));
builder.Services.AddDbContext<AuthDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:MeuBlogAuthDb"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 6;
    opt.Password.RequiredUniqueChars = 1;
});
builder.Services.AddScoped<ITagRepositorio, TagRepositorio>();
builder.Services.AddScoped<IBlogPostRepositorio, BlogPostRepositorio>();
builder.Services.AddScoped<IImageRepositorio, CloudnaryImageRepositorio>();
builder.Services.AddScoped<IBlogPostLikeRepositorio, BlogPostLikeRepositorio>();
builder.Services.AddScoped<IBlogPostComentarioRepositorio, BlogPostComentarioRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio,  UsuarioRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
