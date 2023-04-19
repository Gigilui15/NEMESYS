using NEMESYS.Models;

namespace NEMESYS.ViewModels
{
    public class BlogPostListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<BlogPost> BlogPosts { get; set; }

    }
}
