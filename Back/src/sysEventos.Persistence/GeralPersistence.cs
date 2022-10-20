using System.Threading.Tasks;
using sysEventos.Persistence.Context;
using sysEventos.Persistence.Contratos;

namespace sysEventos.Persistence
{
    public class GeralPersistence : IGeralPersistence
    {
        private readonly sysEventosContext _context;
        public GeralPersistence (sysEventosContext context)
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
       
    }
}