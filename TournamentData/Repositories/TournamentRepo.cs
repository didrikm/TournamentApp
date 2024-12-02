﻿using Microsoft.EntityFrameworkCore;
using TournamentCore.Entities;
using TournamentCore.Repositories;
using TournamentData.Data;

namespace TournamentData.Repositories
{
    internal class TournamentRepo : ITournamentRepo
    {

        private readonly TournamentApiContext _context;

        public TournamentRepo(TournamentApiContext context)
        {
            _context = context;
        }
        public void Add(Tournament tournament)
        {
            _context.Add(tournament);
        }

        public async Task<bool> AnyTournamentAsync(int id)
        {
            return await _context.Tournament.AnyAsync(t => t.Id == id);
        }

        public async Task<Tournament?> GetTournamentAsync(int id)
        {
            return await _context.Tournament.Include(t => t.Games).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync(bool includeGames = false)
        {
            return includeGames ? await _context.Tournament.Include(t => t.Games).ToListAsync() :
                                     await _context.Tournament.ToListAsync();
        }

        public void Remove(Tournament tournament)
        {
            _context.Remove(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Update(tournament);
        }
    }
}