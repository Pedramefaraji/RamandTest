using RamandTestSolution.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWorks
    {
        public UnitOfWork(IUserRepository userRepository)
        { 
            _UserRepository = userRepository;
        }
        public IUserRepository _UserRepository { get; }

    }
}
