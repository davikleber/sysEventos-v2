using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysEventos.Domain;

namespace sysEventos.Persistence.Contratos
{
    public interface IGeralPersistence
    {
        //Geral (Todas os Add, Update,Deletes)    
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;
        void DeleteRange<T> (T[] entity) where T: class;

        Task<bool> SaveChangeSymc();
       


    }
}