using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentCore.Entities;

namespace TournamentData.Data
{
    public class TournamentApiContext : DbContext
    {
        public TournamentApiContext (DbContextOptions<TournamentApiContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentCore.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<TournamentCore.Entities.Game> Game { get; set; } = default!;
    }
}
