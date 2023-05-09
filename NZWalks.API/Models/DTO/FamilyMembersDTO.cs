using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class FamilyMembersDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
