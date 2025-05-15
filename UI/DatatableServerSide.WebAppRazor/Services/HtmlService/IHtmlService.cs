namespace DatatableServerSide.WebAppRazor.Services.HtmlService
{
    public interface IHtmlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
