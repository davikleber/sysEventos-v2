using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysEventos.Domain;

namespace sysEventos.Persistence.Contratos
{
    public interface IPalestrantePersistence
    {
        //PALESTRANTES
         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);


    }
}