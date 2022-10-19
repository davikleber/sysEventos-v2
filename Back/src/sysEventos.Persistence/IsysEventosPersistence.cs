using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysEventos.Domain;

namespace sysEventos.Persistence
{
    public interface IsysEventosPersistence
    {
        //Geral (Todas os Add, Update,Deletes)    
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;
        void DeleteRange<T> (T[] entity) where T: class;

        Task<bool> SaveChangeSymc();

        //EVENTOS
        Task<Evento[]> GetAllEventosByThemeAsync(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes);

        //PALESTRANTES
         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos);


    }
}