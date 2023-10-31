using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class RegisterScaleResponse
    {
        public string managerName { get; set; }
        public string fisrtBack { get; set; }
        public string secondBack { get; set; }
        public string thirdBack { get; set; }
        public string guitarMusician { get; set; }
        public string drumMusician { get; set; }
        public string bassMusician { get; set; }
        public string keyboardMusician { get; set; }
        public DateTime dateScale { get; set; }
    }
}
