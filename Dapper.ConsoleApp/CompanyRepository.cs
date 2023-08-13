using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.ConsoleApp
{
    public class CompanyRepository : ICompanyRepository
    {
        //
        private readonly Context _context;
        private readonly IOptions<CompanyOptions> _companyOptions;

        public CompanyRepository(Context context, IOptions<CompanyOptions> companyOptions)
        {
            _context = context;
            _companyOptions = companyOptions;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            //var query = "SELECT * FROM Companies";
            var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";
            
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id";
            
            var companyOptions = _companyOptions.Value;

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

                company.Name = company.Name + " - " + companyOptions.Code;

                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

                return createdCompany;
            }
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Companies WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var procedureName = "ShowCompanyForProvidedEmployeeId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return company;
            }
        }

        public async Task<Company> GetCompanyEmployeesMultipleResults(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id;" +
                        "SELECT * FROM Employees WHERE CompanyId = @Id";

            using (var connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                {
                    var company = await multi.ReadSingleOrDefaultAsync<Company>();
                    if (company is not null) // if (IsNotNull(company))
                    {
                        company.Employees = (await multi.ReadAsync<Employee>()).ToList();
                    }
                    else
                    {
                        company!.Employees = new List<Employee>();
                    }

                    return company;
                }
            }
        }

        public async Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
        {
            throw new NotImplementedException();
        }

        public async Task CreateMultipleCompanies(List<CompanyForCreationDto> companies)
        {
            throw new NotImplementedException();
        }



        //
    }
}
