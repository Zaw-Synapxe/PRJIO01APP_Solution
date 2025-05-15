using DatatableServerSide.WebAppRazor.Models;

namespace DatatableServerSide.WebAppRazor.Services.ExportService
{
    public interface IExportService
    {
        Task<byte[]> ExportToExcel(List<TestRegister> registers);

        byte[] ExportToCsv(List<TestRegister> registers);

        byte[] ExportToHtml(List<TestRegister> registers);

        byte[] ExportToJson(List<TestRegister> registers);

        byte[] ExportToXml(List<TestRegister> registers);

        byte[] ExportToYaml(List<TestRegister> registers);
    }
}
