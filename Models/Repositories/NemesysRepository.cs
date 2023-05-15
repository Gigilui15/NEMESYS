using Microsoft.EntityFrameworkCore;
using NEMESYS.Data;
using NEMESYS.Models.Interfaces;
using System;

namespace NEMESYS.Models.Repositories
{
    public class NemesysRepository : INEMESYSRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public NemesysRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Report> GetAllReports()
        {
            return _appDbContext.Reports.Include(b => b.Category).OrderBy(b => b.CreatedDate);

        }

        public Report GetReportById(int ReportId)
        {
            return _appDbContext.Reports.Include(b => b.Category).FirstOrDefault(b => b.Id == ReportId);
        }

        public void CreateReportPost(Report reportPost)
        {
            _appDbContext.Reports.Add(reportPost);
            _appDbContext.SaveChanges();
        }

        public void UpdateReportPost(Report reportPost)
        {
            var existingReportPost = _appDbContext.Reports.SingleOrDefault(bp => bp.Id == reportPost.Id);
            if (existingReportPost != null)
            {
                existingReportPost.Title = reportPost.Title;
                existingReportPost.Content = reportPost.Content;
                existingReportPost.UpdatedDate = reportPost.UpdatedDate;
                existingReportPost.ImageUrl = reportPost.ImageUrl;
                existingReportPost.CategoryId = reportPost.CategoryId;

                _appDbContext.Entry(existingReportPost).State = EntityState.Modified;
                _appDbContext.SaveChanges();
            }
        }



        public IEnumerable<Category> GetAllCategories()
        {
            //Not loading realted reports (because we don't know how many there are)
            return _appDbContext.Categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            //Not loading related reports
            return _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);

        }
    }
}
