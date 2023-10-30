using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class SecurityClaim
    {
        public AuthResponse authResponse { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }
}
