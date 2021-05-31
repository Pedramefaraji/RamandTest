using MediatR;
using RamandTestSolution.Application.Features.User.Commands;
using RamandTestSolution.Application.Features.User.DTOs;
using RamandTestSolution.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Features.User.Handlers
{
    public class LoginCommandHandler : IRequestHandler<RamandTestSolution.Application.Features.User.Commands.Login, LoginOutputDTO>
    {
        private readonly IUnitOfWorks _unitOfWork;
        public LoginCommandHandler(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginOutputDTO> Handle(Login request, CancellationToken cancellationToken)
        {
            var z = await _unitOfWork._UserRepository.Login(request);
            return z;
        }
    }
}
