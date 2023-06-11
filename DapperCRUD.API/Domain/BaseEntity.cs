namespace DapperCRUD.API.Domain
{
    public class BaseEntity
    {
        // CreatedOn, CreatedBy, ModifiedOn and ModfiedBy
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
