namespace NEMESYS.Models.Interfaces
{
    public interface IInvestigationRepository
    {
        IEnumerable<Investigation> GetAllInvestigations();
        Investigation GetInvestigationById(int invId);
        void Create(Investigation inv);
        void Update(Investigation inv);
        void Delete(Investigation inv);
        void Delete(int id);
    }
}
