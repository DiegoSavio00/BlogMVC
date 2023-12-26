namespace BlogMVC.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string MostrarNome { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
