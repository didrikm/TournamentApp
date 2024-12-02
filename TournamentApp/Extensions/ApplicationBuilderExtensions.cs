using Bogus;
using Microsoft.EntityFrameworkCore;
using TournamentCore.Entities;
using TournamentData.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TournamentApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceprovider = scope.ServiceProvider;
                var db = serviceprovider.GetRequiredService<TournamentApiContext>();

                await db.Database.MigrateAsync();
                if (await db.Tournament.AnyAsync()) return;

                try
                {
                    var companies = GenerateTournaments(4);
                    db.AddRange(companies);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        private static IEnumerable<Tournament> GenerateTournaments(int nrOfTournaments)
        {
            var faker = new Faker<Tournament>("sv").Rules((f, t) =>
            {
                t.Title = $"{f.Lorem.Word()} Tournament";
                t.StartDate = DateTime.Today;
                t.Games = GenerateGames(f.Random.Int(min: 1, max: 10));
            });

            return faker.Generate(nrOfTournaments);
        }

        private static ICollection<Game> GenerateGames(int nrOfGames)
        {

            var faker = new Faker<Game>("sv").Rules((f, g) =>
            {
                g.Title = $"{f.Lorem.Word()} Game";
                g.Time = DateTime.Now;
            });

            return faker.Generate(nrOfGames);
        }


    }
}
