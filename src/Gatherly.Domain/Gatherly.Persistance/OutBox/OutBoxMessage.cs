using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Gatherly.Persistance.OutBox
{
    public sealed class OutBoxMessage
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime OccuredOnUTC { get; set; }
        public DateTime ProcessedOnUTC { get; set; }
        public string? Error { get; set; }
    }
}
