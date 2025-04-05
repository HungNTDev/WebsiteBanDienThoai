using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.VariationOptionManagement.Commands.Update
{
    public class UpdateVariationOptionDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Guid VariationId { get; set; }
        public string? VariationName { get; set; }
    }
}
