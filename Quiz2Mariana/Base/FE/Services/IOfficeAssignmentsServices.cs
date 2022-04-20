using FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FE.Services
{
    public interface IOfficeAssignmentsServices
    {
        void Insert(OfficeAssignment t);
        void Update(OfficeAssignment t);
        void Delete(OfficeAssignment t);
        IEnumerable<OfficeAssignment> GetAll();
        OfficeAssignment GetOneById(int id);
        Task<IEnumerable<OfficeAssignment>> GetAllAsync();
        Task<OfficeAssignment> GetOneByIdAsync(int id);
    }
}
