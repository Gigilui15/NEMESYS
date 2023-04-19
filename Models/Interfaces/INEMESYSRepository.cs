using NEMESYS.Models;

namespace NEMESYS.Models.Interfaces
{
    public interface INEMESYSRepository
    {
        IEnumerable<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPostById(int blogPostId);

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);

    }

}
