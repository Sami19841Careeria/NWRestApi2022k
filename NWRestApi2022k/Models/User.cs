﻿using System;
using System.Collections.Generic;

namespace NWRestApi2022k.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } = "aijsoiwoinswoiswois";
        public int AccesslevelId { get; set; }
    }
}
