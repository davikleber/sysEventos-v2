using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sysEventos.Domain;
using sysEventos.Application.Contratos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace sysEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {      
     
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null)
                {
                    return NotFound("Nenhum Evento Encontrado");
                }
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
           
        }
         [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null)
                {
                    return NotFound("Eventos pelo Id não encontrados");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
           
        }

        [HttpGet("tema/{tema}")]
         public async Task<IActionResult> GetByTheme(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByThemeAsync(tema, true);
                if (evento == null)
                {
                    return NotFound("Eventos por Tema não encontrados");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }           
        }

        [HttpPost]
        public async Task<IActionResult>Post( Evento model)
        {
             try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null)
                {
                    return BadRequest("Erro ao tentar Adicionar Evento");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Adicionar Evento. Erro: {ex.Message}");
            }     
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>Put(int id, Evento model)
        {
             try
            {
                var evento = await _eventoService.UpadateEventos(id, model);
                if (evento == null)
                {
                    return BadRequest("Erro ao tentar Atualizar Evento");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar Evento Erro: {ex.Message}");
            }     
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id, Evento model)
        {
            // return await _eventoService.DeleteEventos(id)) ? return Ok("Deletado") :  BadRequest("Evento não deletado!");
             try
            {
                if ( await _eventoService.DeleteEventos(id))
                {
                    return Ok("Deletado");
                }
                else
                {
                    return BadRequest("Evento não deletado!");
                }
            }
            catch (Exception ex)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Deletar Eventos. Erro: {ex.Message}");
            }     
        }

    }
}
