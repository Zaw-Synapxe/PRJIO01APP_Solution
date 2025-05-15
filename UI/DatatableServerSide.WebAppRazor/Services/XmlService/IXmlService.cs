namespace DatatableServerSide.WebAppRazor.Services.XmlService
{
    public interface IXmlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
