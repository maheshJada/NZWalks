﻿namespace NZWalks.API.Model.DTO
{
    public class UpdateRegionRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}