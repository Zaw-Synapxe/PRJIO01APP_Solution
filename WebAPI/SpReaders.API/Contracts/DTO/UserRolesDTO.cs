namespace SpReaders.API.Contracts.DTO
{
    public class UserRolesDTO
    {
        public int UserProfileId { get; set; }
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
