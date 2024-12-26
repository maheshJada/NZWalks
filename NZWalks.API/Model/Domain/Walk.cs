namespace NZWalks.API.Model.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigate Propertoes
        public Difficulity Difficulty { get; set; }
        public Region Region { get; set; }
    }
  
   
}
