using NEMESYS.Models.Interfaces;
using NEMESYS.Models;

namespace NEMESYS.Models.Repositories
{
    public class MockNEMESYSRepository : INEMESYSRepository
    {
        private List<Report>? _posts;
        private List<Category>? _categories;

        public MockNEMESYSRepository()
        {
            if (_categories == null)
            {
                InitializeCategories();
            }

            if (_posts == null)
            {
                InitializeReports();
            }
        }

        private void InitializeCategories()
        {
            _categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Accident"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Danger"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Health"
                }
            };
        }

        private void InitializeReports()
        {
            _posts = new List<Report>()
            {
                 new Report()
                {
                    Id = 1,
                    Title = "Bumper-to-Bumper in RingRoad",
                    Content = "Today at around 2.15pm a bumper-to-bumper incident caused a traffic jam...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/uom.jpg",
                    CategoryId = 1
                },
                new Report() {
                    Id = 2,
                    Title = "Hornet Nests Around Quad!",
                    Content = "Two hornet nests have been spotted under...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/quad.jpg",
                    CategoryId = 2
                },
                new Report()
                {
                    Id = 3,
                    Title = "AC Filters in the Faculty of ICT",
                    Content = "Numerous students have been noticing the quality of air in...",
                    CreatedDate = DateTime.Now,
                    ImageUrl = "/images/ICT.jpg",
                    CategoryId = 3
                }
            };
        }


        public IEnumerable<Report> GetAllReports()
        {
            List<Report> result = new List<Report>();
            foreach (var post in _posts)
            {
                post.Category = _categories.FirstOrDefault(c => c.Id == post.CategoryId);
                result.Add(post);
            }
            return result;
        }

        public Report GetReportById(int ReportId)
        {
            var post = _posts.FirstOrDefault(p => p.Id == ReportId); //if not found, it returns null
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
            category.Reports = _posts.Where(p => p.CategoryId == categoryId).ToList();
            return category;
        }

    }
}
