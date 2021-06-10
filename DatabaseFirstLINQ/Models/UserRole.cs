
﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseFirstLINQ.Models
{
    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}