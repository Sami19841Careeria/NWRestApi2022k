using System;
using System.Collections.Generic;

namespace NWRestApi2022k.Models
{
    public partial class Documentation
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Keycode { get; set; } = null!;
    }
}
