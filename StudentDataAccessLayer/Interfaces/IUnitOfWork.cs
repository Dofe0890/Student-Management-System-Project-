using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentDomainLayer.Models;
namespace StudentDomainLayer.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Student> Students { get; }

       Task<int>  Complete();

    }
}
