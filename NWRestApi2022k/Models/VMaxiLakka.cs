using System;
using System.Collections.Generic;

namespace NWRestApi2022k.Models
{
    public partial class VMaxiLakka
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
    }
}
