using NZWalks.API.Model.Domain;

namespace NZWalks.API.Model.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKM { get; set; }
        public string? WalkImageUrl { get; set; }
        public DifficultyDTO Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}
