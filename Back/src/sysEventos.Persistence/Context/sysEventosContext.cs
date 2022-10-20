using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sysEventos.Domain;

namespace sysEventos.Persistence.Context
{
    public class sysEventosContext : DbContext
    {
        public sysEventosContext(DbContextOptions<sysEventosContext> options) : base(options)
        {
            
        }
        public DbSet <Evento> Eventos { get; set; }
        public DbSet <Lote> Lotes { get; set; }
        public DbSet <Palestrante> Palestrantes { get; set; }
        public DbSet <PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet <RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new{ PE.EventoId, PE.PalestranteId});
            
            //Configurando o cascate
            modelBuilder.Entity<Evento>().HasMany(e => e.RedeSociais).WithOne(rs => rs.Evento).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Palestrante>().HasMany(e => e.RedeSociais).WithOne(rs => rs.Palestrante).OnDelete(DeleteBehavior.Cascade);
        }

        
    }
}