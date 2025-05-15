namespace DatatableServerSide.WebAppRazor.Services.ExcelService
{
    public interface IExcelService
    {
        Task<byte[]> Write<T>(IList<T> registers);
    }
}
