using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain
{
    public class FamilyMembers
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
