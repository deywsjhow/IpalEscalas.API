using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class Users
    {
        public int id { get; set; }
        public string Nom_Login { get; set; }
        public string Nom_Senha { get; set; }
        public string Nom_Email { get; set; }
        public string Atribuicao { get; set; }
        public string Num_TipoLogin { get; set; }
        public string Telefone { get; set; }
    }
}
