using Microsoft.EntityFrameworkCore;
using NEMESYS.Data;
using NEMESYS.Models.Interfaces;
using System;

namespace NEMESYS.Models.Repositories
{
    public class InvestigationsRepository : IInvestigationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InvestigationsRepository(ApplicationDbContext appDbContext)
        {
            _applicationDbContext = appDbContext;
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            return _applicationDbContext.Investigations.OrderBy(i => i.Id);
        }

        public Investigation GetInvestigationById(int investigationId)
        {
            return _applicationDbContext.Investigations.FirstOrDefault(i => i.Id == investigationId);
        }


        public void Create(Investigation investigation)
        {
            _applicationDbContext.Investigations.Add(investigation);
            _applicationDbContext.SaveChanges();
        }

        public void Update(Investigation investigation)
        {
            var existingInv = _applicationDbContext.Investigations.SingleOrDefault(i => i.Id == investigation.Id);
            if (existingInv != null)
            {
                existingInv.Content = investigation.Content;
                existingInv.UpdatedDate = DateTime.Now;

                _applicationDbContext.Entry(existingInv).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
            }

        }

        public void Delete(Investigation investigation)
        {

            _applicationDbContext.Investigations.Remove(investigation);
            _applicationDbContext.Entry(investigation).State = EntityState.Deleted;
            _applicationDbContext.SaveChanges();

        }


        public void Delete(int id)
        {

            Investigation investigation = _applicationDbContext.Investigations.Find(id);
            if (investigation != null)
            {
                _applicationDbContext.Investigations.Remove(investigation);
                _applicationDbContext.Entry(investigation).State = EntityState.Deleted;
                _applicationDbContext.SaveChanges();
            }

        }

    }
}


