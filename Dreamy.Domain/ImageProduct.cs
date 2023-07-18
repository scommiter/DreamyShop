namespace Dreamy.Domain
{
    public class ImageProduct : TrackEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
    }
}
