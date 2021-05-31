using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Interfaces
{
    public interface IUnitOfWorks
    {
        public IUserRepository _UserRepository  { get; }
    }
}
