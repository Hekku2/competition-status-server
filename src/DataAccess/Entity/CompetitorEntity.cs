using System;

namespace DataAccess.Entity
{
    public class CompetitorEntity
    {
        public string Name { get; set; } = null!;
        public string? Team { get; set; }
        public string? CompetitorId { get; set; }
    }
}
