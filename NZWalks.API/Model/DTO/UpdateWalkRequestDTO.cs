namespace NZWalks.API.Model.DTO
{
    public class UpdateWalkRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

    }
}
