namespace CartingService.API.DTOs
{
    public class UpdateItemInformation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }
    }
}
