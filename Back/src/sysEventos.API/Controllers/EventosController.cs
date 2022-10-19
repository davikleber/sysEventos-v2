using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sysEventos.Persistence;
using sysEventos.Domain;

namespace sysEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        
        private readonly sysEventosContext _context;

        public EventosController(sysEventosContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
           
        }
         [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.FirstOrDefault(evento => evento.Id == id);
           
        }
    }
}
