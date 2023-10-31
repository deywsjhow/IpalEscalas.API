using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class RegisterScaleDB
    {
        public string Nom_Dirigente { get; set; }
        public string Nom_PrimeiroBack { get; set; }
        public string Nom_SegundoBack { get; set; }
        public string Nom_TerceiroBack { get; set; }
        public string Nom_MusicoViolao { get; set; }
        public string Nom_MusicoBateria { get; set; }
        public string Nom_MusicoBaixo { get; set; }
        public string Nom_MusicoTeclado { get; set; }
        public DateTime Dat_DiaDaEscala { get; set; }        
    }
}
