﻿using System;
using System.Collections.Generic;

namespace QuanLyQuanCafe.Models
{
    public partial class RoleClaim
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
