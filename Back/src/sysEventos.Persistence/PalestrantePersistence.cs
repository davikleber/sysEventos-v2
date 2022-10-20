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
     public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly sysEventosContext _context;
        public PalestrantePersistence (sysEventosContext context)
        {
            _context = context;
            
        }       
        //#region PALESTRANTE
        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p =>p.Id).Where(p=> p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }      
        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p =>p.Id);
            return await query.ToArrayAsync();
        }        

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedeSociais);

            if (includeEventos )
            {
                query = query.Include(p=> p.PalestrantesEventos).ThenInclude(pe=> pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(p =>p.Id).Where(p=> p.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }
         //#endregion
       
    }
}