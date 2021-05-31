using MediatR;
using RamandTestSolution.Application.Features.User.Queries;
using RamandTestSolution.Application.Interfaces;
using RamandTestSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Features.User.Handlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUser, List<Domain.Entities.Users>>
    {
        private readonly IUnitOfWorks _unitOfWork;

        public GetAllUserQueryHandler(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Users>> Handle(GetAllUser request, CancellationToken cancellationToken)
        {
            var x =await _unitOfWork._UserRepository.GetAllUser();
            return x;
        }
    }
}
