using BlogMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Infra
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> Likes { get; set; }
        public DbSet<BlogPostComentario> Comentarios { get; set; }
    }
}
