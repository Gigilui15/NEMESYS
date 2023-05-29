namespace NEMESYS.Models.Interfaces
{
    public interface INEMESYSRepository
    {
        IEnumerable<Report> GetAllReports();
        Report GetReportById(int ReportId);
        void CreateReportPost(Report reportPost);
        void UpdateReportPost(Report updatedReportPost);

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        void Delete(Report report);
        Report GetReportByInv(Investigation investigation);
        IEnumerable<Status> GetAllStatuses();
        Status GetStatusById(int statusId);
    }
}