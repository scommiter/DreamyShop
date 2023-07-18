namespace Dreamy.Domain
{
    public class Attribute : AuditEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnique { get; set; }
        public string Note { get; set; }
    }
}