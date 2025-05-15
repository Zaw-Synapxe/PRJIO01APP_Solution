using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using XYZ.API.Data;
using XYZ.API.Service;

namespace XYZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDataController : ControllerBase
    {
        private IRepository<PersonalInfo> _personalInfoRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Branch> _branchRepository;
        private IRepository<Department> _departmentRepository; 
        public SeedDataController(
            IRepository<PersonalInfo> personalInfoRepository,
            IRepository<Category> categoryRepository,
            IRepository<Branch> branchRepository,
            IRepository<Department> departmentRepository)
        {
            _personalInfoRepository = personalInfoRepository;
            _categoryRepository = categoryRepository;
            _branchRepository = branchRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpPost]
        [Route("AddSeedDataPersonInfo")]
        public async Task<IActionResult> AddSeedData()
        {
            SeedData _SeedData = new();
            int totalPersonalInfoAdded = 0;

            var allBranch = await _personalInfoRepository.GetAllAsync();
            if (allBranch.Count() < 1)
            {
                var _GetBranchList = _SeedData.GetPersonalInfoList();
                foreach (PersonalInfo item in _GetBranchList)
                {
                    _personalInfoRepository.Add(item);
                    await _personalInfoRepository.SaveChangesAsync();
                    totalPersonalInfoAdded = totalPersonalInfoAdded + 1;
                }
            }

            return Ok("Total Personal Info Added:" + totalPersonalInfoAdded);
        }


        [HttpPost]
        [Route("AddSeedDataCategory")]
        public async Task<IActionResult> AddSeedDataCategory()
        {
            SeedData _SeedData = new();
            int totalCatAdded = 0;

            var allCategory = await _categoryRepository.GetAllAsync();
            if (allCategory.Count() < 1)
            {
                var _GetCatList = _SeedData.GetCategoryList();
                foreach (Category item in _GetCatList)
                {
                    _categoryRepository.Add(item);
                    await _categoryRepository.SaveChangesAsync();
                    totalCatAdded = totalCatAdded + 1;
                }
            }

            return Ok("Total Cat Added:" + totalCatAdded);
        }

        [HttpPost]
        [Route("AddSeedDataBranch")]
        public async Task<IActionResult> AddSeedDataBranch()
        {
            SeedData _SeedData = new();
            int totalBranchAdded = 0;

            var allBranch = await _branchRepository.GetAllAsync();
            if (allBranch.Count() < 1)
            {
                var _GetBranchList = _SeedData.GetBranchList();
                foreach (Branch item in _GetBranchList)
                {
                    _branchRepository.Add(item);
                    await _branchRepository.SaveChangesAsync();
                    totalBranchAdded = totalBranchAdded + 1;
                }
            }

            return Ok("Total Branch Added:" + totalBranchAdded);
        }

        [HttpPost]
        [Route("AddSeedDataDepartment")]
        public async Task<IActionResult> AddSeedDataDepartment()
        {
            SeedData _SeedData = new();
            int totalDepartmentAdded = 0;

            var allDepartment = await _departmentRepository.GetAllAsync();
            if (allDepartment.Count() < 1)
            {
                var _GetDepartmentList = _SeedData.GetDepartmentList();
                foreach (Department item in _GetDepartmentList)
                {
                    _departmentRepository.Add(item);
                    await _departmentRepository.SaveChangesAsync();
                    totalDepartmentAdded = totalDepartmentAdded + 1;
                }
            }

            return Ok("Total Department Added:" + totalDepartmentAdded);
        }


        //
    }
}
