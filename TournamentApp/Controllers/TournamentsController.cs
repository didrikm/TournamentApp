﻿using Microsoft.AspNetCore.Mvc;
using TournamentCore.Entities;
using TournamentCore.Repositories;
using TournamentCore.Mapping;
using TournamentCore.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TournamentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TournamentsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament(bool includeGames)
        {
            var tournaments = await _uow.TournamentRepo.GetTournamentsAsync(includeGames);
            var tournamentDTOs = tournaments.Select(_mapper.MapToTournamentDTO).ToList();
            return Ok(tournamentDTOs);
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDTO>> GetTournament(int id)
        {
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }
            var tournamentDTO = _mapper.MapToTournamentDTO(tournament);

            return Ok(tournamentDTO);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if(!await _uow.TournamentRepo.AnyTournamentAsync(id))
            {
                return NotFound();
            }

            var tournament = _mapper.MapToUpdateTournament(dto);
            _uow.TournamentRepo.Update(tournament);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.TournamentRepo.AnyTournamentAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(TournamentCreationDTO dto)
        {
            var tournament = _mapper.MapToCreationTournament(dto);
            _uow.TournamentRepo.Add(tournament);
            await _uow.CompleteAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            _uow.TournamentRepo.Remove(tournament);
            await _uow.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> TournamentExists(int id)
        {
            return await _uow.TournamentRepo.AnyTournamentAsync(id);
        }
    }
}