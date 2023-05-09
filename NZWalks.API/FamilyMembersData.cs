using System;
using System.Collections.Generic;
using System.Linq;
using NZWalks.API.Models.Domain;

namespace NZWalks.API
{
    public class FamilyMembersData
    {
        public static List<FamilyMembers> GetMembers()
        {
            var members = new[]
            {
                new{Id=new Guid(), Name = "Abel"},
                new{Id=new Guid(), Name = "Aleksandra"},
                new{Id=new Guid(), Name = "Gheeta"},
                new{Id=new Guid(), Name = "MaybeBean"}
            };

            return members.Select(m => new FamilyMembers
            {
                Id = m.Id,
                Name = m.Name

            }).ToList();
        }
    }
}
