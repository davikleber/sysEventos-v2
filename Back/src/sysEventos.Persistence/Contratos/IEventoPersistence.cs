using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysEventos.Domain;

namespace sysEventos.Persistence.Contratos
{
    public interface IEventoPersistence
    {
       //EVENTOS
        Task<Evento[]> GetAllEventosByThemeAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);        

    }
}