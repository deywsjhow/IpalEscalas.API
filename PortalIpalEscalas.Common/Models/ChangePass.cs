using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class ChangePass
    {
        public int Seql_Usuario { get; set; }
        public string Nom_Senha{ get; set; }
        public string Nom_SenhaOld { get; set; }
    }
}
