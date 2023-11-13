using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class ChangePass
    {
        public int seql_User { get; set; }
        public string password{ get; set; }
        public string oldPassword { get; set; }
    }
}
