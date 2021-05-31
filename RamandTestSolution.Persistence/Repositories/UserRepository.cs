using Dapper;
using RamandTestSolution.Application.Features.User.DTOs;
using RamandTestSolution.Application.Interfaces;
using RamandTestSolution.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<List<Users>> GetAllUser()
        {
            var sql = $"select * from [User]";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DapperCS")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.Entities.Users>(sql, commandType: System.Data.CommandType.Text);
                return result.ToList();
            }
        }

        public async Task<LoginOutputDTO> Login(RamandTestSolution.Application.Features.User.Commands.Login Input)
        {
            var sql = $"select ID from [User] where Username=@Username and password=@password";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DapperCS")))
            {
                connection.Open();
                var result = await connection.QueryAsync<LoginOutputDTO>(sql,Input, commandType: System.Data.CommandType.Text);
                return result.FirstOrDefault();
            }
        }
    }
}
