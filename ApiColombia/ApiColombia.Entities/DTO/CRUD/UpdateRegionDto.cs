using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiColombia.Entities.DTO.CRUD
{
    public class UpdateRegionDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
