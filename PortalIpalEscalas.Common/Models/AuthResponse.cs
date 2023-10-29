using System;
using System.Collections.Generic;
using System.Text;

namespace PortalIpalEscalas.Common.Models
{
    public class AuthResponse
    {
        public int userId { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string attribuation { get; set; }
        public string voiceType { get; set; }
        public string Instrument { get; set; }
        public string secondInstrument { get; set; }
        public int loginType { get; set; }
        public string nameType { get; set; }

    }
}
