namespace Dreamy.Domain
{
    public class Manufacturer
    {
        public Manufacturer() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
    }
}