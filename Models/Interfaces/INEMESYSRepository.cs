using NEMESYS.Models;

namespace NEMESYS.Models.Interfaces
{
    public interface INEMESYSRepository
    {
        IEnumerable<Report> GetAllReports();
        Report GetReportById(int ReportId);

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);

    }

}
