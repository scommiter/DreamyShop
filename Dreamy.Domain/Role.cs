namespace Dreamy.Domain
{
    public class Role : AuditEntity
    {
        public int Id { get; set; }

        public int UserID { get; set; }
        public byte RoleType { get; set; }

        public string ProfileUrl { get; set; }

    }
}