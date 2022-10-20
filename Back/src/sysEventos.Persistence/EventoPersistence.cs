using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sysEventos.Domain;
using sysEventos.Persistence.Context;
using sysEventos.Persistence.Contratos;

namespace sysEventos.Persistence
{
     public class EventoPersistence : IEventoPersistence
    {
        private readonly sysEventosContext _context;
        public EventoPersistence(sysEventosContext context)
        {
            _context = context;

            //Só que está fazendo pra todos mundo
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }       
        // #region EVENTOS      
        public async Task<Evento[]> GetAllEventosByThemeAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e =>e.Id).Where(e=> e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }
         public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
           IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e =>e.Id);
            return await query.ToArrayAsync();
        }

         public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e =>e.Id).Where(e=> e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        //#endregion

       
       
    }
}