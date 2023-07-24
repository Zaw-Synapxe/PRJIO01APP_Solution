using DapperCRUD.API.Domain;
using DapperCRUD.API.Dto;

namespace DapperCRUD.API.Services
{
    public class CompanyService : ICompanyService
    {
        public Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            throw new NotImplementedException();
        }

        public Task CreateMultipleCompanies(List<CompanyForCreationDto> companies)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetCompanies()
        {
            throw new NotImplementedException();
        }

        public Task<List<Company>> GetCompaniesEmployeesMultipleMapping()
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompany(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompanyEmployeesMultipleResults(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            throw new NotImplementedException();
        }
    }
}
