using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class AuthResponse
    {
        public string user { get; set; }

        public string password { get; set; }

        public int? loginType { get; set; }
    }
}
