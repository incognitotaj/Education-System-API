using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cors
{
    internal class CorsSettings
    {
        public string? Angular { get; set; }
        public string? Blazor { get; set; }
        public string? React { get; set; }
        public string? JavaScript { get; set; }
    }
}
