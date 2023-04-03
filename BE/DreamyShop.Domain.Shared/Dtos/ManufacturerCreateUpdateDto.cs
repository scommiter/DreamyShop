namespace DreamyShop.Domain.Shared.Dtos
{
    public class ManufacturerCreateUpdateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public bool IsVisibility { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
    }
}
