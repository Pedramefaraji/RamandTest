using RamandTestSolution.Application.Features.User.DTOs;
using RamandTestSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<LoginOutputDTO> Login(RamandTestSolution.Application.Features.User.Commands.Login Input);
        Task<List<Users>> GetAllUser();
    }
}
