using FE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FE.Services
{
    public interface ICourseServices
    {
        void Insert(Course t);
        void Update(Course t);
        void Delete(Course t);
        IEnumerable<Course> GetAll();
        Course GetOneById(int id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetOneByIdAsync(int id);
    }
}
