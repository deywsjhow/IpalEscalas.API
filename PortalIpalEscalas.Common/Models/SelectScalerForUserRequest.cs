using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class SelectScalerForUserRequest
    {
        public string user { get; set; }
        public DateTime dateScaleInit { get; set; }
        public DateTime dateScaleFinish { get; set; }
    }
}
