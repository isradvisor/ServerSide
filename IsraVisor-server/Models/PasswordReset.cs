using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class PasswordReset
    {
        public string TouristEmail { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}