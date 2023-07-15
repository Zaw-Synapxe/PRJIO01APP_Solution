namespace DatatableServerSide.WebAppRazor.Services.YamlService
{
    public interface IYamlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
