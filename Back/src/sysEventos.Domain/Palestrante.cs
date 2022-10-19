using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sysEventos.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriulo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

         public IEnumerable<RedeSocial> RedeSociais { get; set; }
         public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
        
    }
}