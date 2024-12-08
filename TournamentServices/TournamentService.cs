using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ServicesContracts;
using TournamentCore.DTOs;
using TournamentCore.Repositories;
using TournamentShared;

namespace TournamentServices
{
    public class TournamentService : ITournamentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ServiceResult<IEnumerable<TournamentDTO>>> GetTournamentsAsync(bool includeGames, bool trackChanges = false)
        {
            var tournaments = await _uow.TournamentRepo.GetTournamentsAsync(includeGames);
            var tournamentDTOs = tournaments.Select(_mapper.MapToTournamentDTO).ToList();
            return ServiceResult<IEnumerable<TournamentDTO>>.Ok(tournamentDTOs);
        }

        public async Task<ServiceResult<TournamentDTO>> GetTournamentAsync(int id, bool trackChanges = false)
        {
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(id);

            if (tournament == null)
            {
                return ServiceResult<TournamentDTO>.NotFound("The tournament could not be found.");
            }
            var tournamentDTO = _mapper.MapToTournamentDTO(tournament);
            return ServiceResult<TournamentDTO>.Ok(tournamentDTO);
        }

        public async Task<ServiceResult<TournamentDTO>> PostTournament(TournamentCreationDTO dto)
        {
            var tournament = _mapper.MapToCreationTournament(dto);
            _uow.TournamentRepo.Add(tournament);
            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return ServiceResult<TournamentDTO>.NotFound("Tournament not found in DB.");
            }
            var returnDTO = _mapper.MapToTournamentDTO(tournament);
            return ServiceResult<TournamentDTO>.Ok(returnDTO); //TODO: CreatedAtAction
        }

        public async Task<ServiceResult<TournamentDTO>> PutTournament(int id, TournamentDTO dto)
        {

            if (id != dto.Id)
            {
                return ServiceResult<TournamentDTO>.BadRequest("URL id and DTO id do not match.");
            }

            var oldTournament = await _uow.TournamentRepo.GetTournamentAsync(id);

            if (oldTournament == null)
            {
                return ServiceResult<TournamentDTO>.NotFound("Tournament not found.");
            }

            oldTournament.Title = dto.Title;
            oldTournament.StartDate = dto.StartDate;
            _uow.TournamentRepo.Update(oldTournament);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.TournamentRepo.AnyTournamentAsync(id))
                {
                    return ServiceResult<TournamentDTO>.NotFound("Tournament not found in DB.");
                }
            }
            return ServiceResult<TournamentDTO>.NoContent();
        }

        public async Task<ServiceResult<TournamentDTO>> DeleteTournament(int id)
        {
            var tournament = await _uow.TournamentRepo.GetTournamentAsync(id);
            if (tournament == null)
            {
                return ServiceResult<TournamentDTO>.NotFound("Tournament not found.");
            }
            _uow.TournamentRepo.Remove(tournament);
            await _uow.CompleteAsync();
            return ServiceResult<TournamentDTO>.NoContent();
        }

        public async Task<ServiceResult<TournamentDTO>> PatchTorunament(int id, JsonPatchDocument<TournamentDTO> patchDocument)
        {

            if (patchDocument == null)
            {
                return ServiceResult<TournamentDTO>.BadRequest("Missing PATCH document.");
            }

            var oldTournament = await _uow.TournamentRepo.GetTournamentAsync(id);

            if (oldTournament == null)
            {
                return ServiceResult<TournamentDTO>.NotFound("Tournament not found.");
            }

            var tournamentDTO = _mapper.MapToTournamentDTO(oldTournament);

            patchDocument.ApplyTo(tournamentDTO);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //TODO: implementera validering

            oldTournament.Title = tournamentDTO.Title;
            oldTournament.StartDate = tournamentDTO.StartDate;

            _uow.TournamentRepo.Update(oldTournament);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.TournamentRepo.AnyTournamentAsync(id))
                {
                    return ServiceResult<TournamentDTO>.NotFound("Tournament not found in DB.");
                }
            }
            return ServiceResult<TournamentDTO>.NoContent();

        }
    }
}
