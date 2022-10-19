using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sysEventos.Domain;

namespace sysEventos.Persistence
{
     public class sysEventosPersistence : IsysEventosPersistence
    {
        private readonly sysEventosContext _context;
        public sysEventosPersistence(sysEventosContext context)
        {
            _context = context;
            
        }
        //#region GERAL
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }      
        
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

         public async Task<bool> SaveChangeSymc()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        //#endregion

        // #region EVENTOS      
        public async Task<Evento[]> GetAllEventosByThemeAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.OrderBy(e =>e.Id).Where(e=> e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }
         public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
           IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.OrderBy(e =>e.Id);
            return await query.ToArrayAsync();
        }

         public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedeSociais);

            if (includePalestrantes )
            {
                query = query.Include(e=> e.PalestranteEventos).ThenInclude(e=> e.Palestrante);
            }

            query = query.OrderBy(e =>e.Id).Where(e=> e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        //#endregion


        //#region PALESTRANTE
        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.OrderBy(p =>p.Id).Where(p=> p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }      
        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.OrderBy(p =>p.Id);
            return await query.ToArrayAsync();
        }        

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.OrderBy(p =>p.Id).Where(p=> p.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }
         //#endregion
       
    }
}