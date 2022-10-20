using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sysEventos.Application.Contratos;
using sysEventos.Domain;
using sysEventos.Persistence.Contratos;

namespace sysEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IGeralPersistence _geralPersistence;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;
            
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersistence.Add<Evento>(model);

                //Tratamento de erros
                if (await _geralPersistence.SaveChangeSymc())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpadateEventos(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId,false);
                if (evento == null)
                {
                    return null;
                }
                
                model.Id = eventoId;

                _geralPersistence.Update(model);
                if (await _geralPersistence.SaveChangeSymc())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId,false);
                if (evento == null)
                {
                    throw new Exception("Evento para deletar não foi encontrado");
                }
                
                _geralPersistence.Delete<Evento>(evento);
                 return await _geralPersistence.SaveChangeSymc();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
           try
           {
             var eventos = await _eventoPersistence.GetAllEventosByAsync(includePalestrantes);
             if (eventos == null)
             {
                return null;
             }
             return eventos;
           }
           catch (Exception ex)
           {
            
            throw new Exception(ex.Message);
           }
        }

        public async Task<Evento[]> GetAllEventosByThemeAsync(string tema, bool includePalestrantes = false)
        {
             try
           {
             var eventos = await _eventoPersistence.GetAllEventosByThemeAsync(tema, includePalestrantes);
             if (eventos == null)
             {
                return null;
             }
             return eventos;
           }
           catch (Exception ex)
           {
            
            throw new Exception(ex.Message);
           }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
              try
           {
             var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
             if (eventos == null)
             {
                return null;
             }
             return eventos;
           }
           catch (Exception ex)
           {
            
            throw new Exception(ex.Message);
           }
        }

        
    }
}