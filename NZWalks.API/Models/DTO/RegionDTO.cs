using System.ComponentModel.DataAnnotations;
using System;

namespace NZWalks.API.Models.DTO
{
    public record RegionDTO(Guid Id, string Code, string Name, string? RegionalImageUrl);

    //public class RegionDTO
    //{
    //    public Guid Id { get; set; }
    //    public string Code { get; set; }
    //    public string Name { get; set; }
    //    public string? RegionalImageUrl { get; set; }
    //}
}
