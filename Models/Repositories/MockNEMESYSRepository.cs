using NEMESYS.Models.Interfaces;
using NEMESYS.Models;

namespace NEMESYS.Models.Repositories
{
    public class MockNEMESYSRepository : INEMESYSRepository
    {
        private List<BlogPost>? _posts;
        private List<Category>? _categories;

        public MockNEMESYSRepository()
        {
            if (_categories == null)
            {
                InitializeCategories();
            }

            if (_posts == null)
            {
                InitializeBlogPosts();
            }
        }

        private void InitializeCategories()
        {
            _categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Comedy"
                },
                new Category()
                {
                    Id = 2,
                    Name = "News"
                }
            };
        }

        private void InitializeBlogPosts()
        {
            _posts = new List<BlogPost>()
            {
                new BlogPost()
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "AGA Today",
                    Content = "Today's AGA is characterized by a series of discussions and debates around ...",
                    CreatedDate = DateTime.UtcNow,
                    ImageUrl = "/images/seed1.jpg"
                },
                new BlogPost()
                {
                    Id = 2,
                    CategoryId = 2,
                    Title = "Traffic is incredible",
                    Content = "Today's traffic can't be described using words. Only an image can do that ...",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    ImageUrl = "/images/seed2.jpg"
                },
                new BlogPost()
                {
                    Id = 3,
                    CategoryId = 1,
                    Title = "When is Spring really starting?",
                    Content = "Clouds clouds all around us. I thought spring started already, but ...",
                    CreatedDate = DateTime.UtcNow.AddDays(-2),
                    ImageUrl = "/images/seed3.jpg"
                }
            };
        }


        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            List<BlogPost> result = new List<BlogPost>();
            foreach (var post in _posts)
            {
                post.Category = _categories.FirstOrDefault(c => c.Id == post.CategoryId);
                result.Add(post);
            }
            return result;
        }

        public BlogPost GetBlogPostById(int blogPostId)
        {
            var post = _posts.FirstOrDefault(p => p.Id == blogPostId); //if not found, it returns null
            var category = _categories.FirstOrDefault(c => c.Id == post.CategoryId);
            post.Category = category;
            return post;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            category.BlogPosts = _posts.Where(p => p.CategoryId == categoryId).ToList();
            return category;
        }

    }
}
