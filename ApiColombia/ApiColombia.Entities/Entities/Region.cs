using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.Entities.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public int? ExternalId { get; set; } = null;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
