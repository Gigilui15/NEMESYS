using Microsoft.EntityFrameworkCore;
using NEMESYS.Data;
using NEMESYS.Models.Interfaces;

namespace NEMESYS.Models.Repositories
{
    public class NemesysRepository : INEMESYSRepository
    {

        private readonly ApplicationDbContext _appDbContext;
        private readonly ILogger _logger;


        public NemesysRepository(ApplicationDbContext appDbContext, ILogger<NemesysRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public IEnumerable<Report> GetAllReports()
        {
            try
            {
                return _appDbContext.Reports.Include(b => b.Category).OrderBy(b => b.CreatedDate);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Report GetReportById(int ReportId)
        {
            try
            {
                return _appDbContext.Reports.Include(b => b.Category).FirstOrDefault(b => b.Id == ReportId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void CreateReportPost(Report reportPost)
        {
            try
            {

                _appDbContext.Reports.Add(reportPost);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }


        public void UpdateReportPost(Report reportPost)
        {
            try
            {
                var existingReportPost = _appDbContext.Reports.SingleOrDefault(bp => bp.Id == reportPost.Id);
                if (existingReportPost != null)
                {
                    existingReportPost.Title = reportPost.Title;
                    existingReportPost.Content = reportPost.Content;
                    existingReportPost.UpdatedDate = reportPost.UpdatedDate;
                    existingReportPost.ImageUrl = reportPost.ImageUrl;
                    existingReportPost.CategoryId = reportPost.CategoryId;
                    existingReportPost.UserId = reportPost.UserId;

                    _appDbContext.Entry(existingReportPost).State = EntityState.Modified;
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {

                //Not loading realted reports (because we don't know how many there are)
                return _appDbContext.Categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public Category GetCategoryById(int categoryId)
        {
            try
            {
                //Not loading related reports
                return _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
