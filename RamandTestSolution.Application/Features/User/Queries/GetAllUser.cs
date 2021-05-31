using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Application.Features.User.Queries
{
    public class GetAllUser : IRequest<List<Domain.Entities.Users>>
    {

    }
}
