using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace SportsLeague.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(LeagueDbContext context)
    {
        // Solo ejecutar si no hay equipos (BD vacía)
        if (await context.Teams.AnyAsync()) return;

        // ═══ 1. EQUIPOS (Liga BetPlay 2026) ═══
        var teams = new List<Team>
        {
            new() { Name="Atlético Nacional", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="Independiente Medellín", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="América de Cali", City="Cali", Stadium="Pascual Guerrero" },
            new() { Name="Deportivo Cali", City="Cali", Stadium="Deportivo Cali" },
            new() { Name="Junior FC", City="Barranquilla", Stadium="Metropolitano" },
            new() { Name="Millonarios FC", City="Bogotá", Stadium="El Campín" },
            new() { Name="Independiente Santa Fe", City="Bogotá", Stadium="El Campín" },
            new() { Name="Deportes Tolima", City="Ibagué", Stadium="Manuel Murillo Toro" },
            new() { Name="Atlético Bucaramanga", City="Bucaramanga", Stadium="Alfonso López" },
            new() { Name="Once Caldas", City="Manizales", Stadium="Palogrande" },
            new() { Name="Deportivo Pasto", City="Pasto", Stadium="Departamental Libertad" },
            new() { Name="Deportivo Pereira", City="Pereira", Stadium="Hernán Ramírez Villegas" },
            new() { Name="Águilas Doradas", City="Rionegro", Stadium="Alberto Grisales" },
            new() { Name="Boyacá Chicó FC", City="Tunja", Stadium="La Independencia" },
            new() { Name="Jaguares de Córdoba", City="Montería", Stadium="Jaraguay" },
            new() { Name="Alianza Valledupar FC", City="Valledupar", Stadium="Armando Maestre" },
            new() { Name="Fortaleza FC", City="Bogotá", Stadium="Metropolitano de Techo" },
            new() { Name="Llaneros FC", City="Villavicencio", Stadium="Bello Horizonte" },
            new() { Name="Cúcuta Deportivo", City="Cúcuta", Stadium="General Santander" },
            new() { Name="Internacional de Bogotá", City="Bogotá", Stadium="Metropolitano de Techo" },
        };

        context.Teams.AddRange(teams);
        await context.SaveChangesAsync();

        // ═══ 2. JUGADORES (4 por equipo = 80 total) ═══
        var playersData = new (string First, string Last, PlayerPosition Pos, int Number)[][]
        {
            // 1. Atlético Nacional
            new[] {
                ("David", "Ospina", PlayerPosition.Goalkeeper, 1),
                ("William", "Tesillo", PlayerPosition.Defender, 3),
                ("Edwin", "Cardona", PlayerPosition.Midfielder, 10),
                ("Alfredo", "Morelos", PlayerPosition.Forward, 9),
            },
            // 2. Independiente Medellín
            new[] {
                ("Salvador", "Ichazo", PlayerPosition.Goalkeeper, 1),
                ("Andrés", "Cadavid", PlayerPosition.Defender, 4),
                ("Adrián", "Arregui", PlayerPosition.Midfielder, 5),
                ("Luciano", "Pons", PlayerPosition.Forward, 9),
            },
            // 3. América de Cali
            new[] {
                ("Joel", "Graterol", PlayerPosition.Goalkeeper, 1),
                ("Jorge", "Segura", PlayerPosition.Defender, 3),
                ("Rodrigo", "Ureña", PlayerPosition.Midfielder, 8),
                ("Adrián", "Ramos", PlayerPosition.Forward, 9),
            },
            // 4. Deportivo Cali
            new[] {
                ("Pedro", "Gallese", PlayerPosition.Goalkeeper, 1),
                ("Fernando", "Álvarez", PlayerPosition.Defender, 4),
                ("Kevin", "Velasco", PlayerPosition.Midfielder, 10),
                ("Juan", "Dinenno", PlayerPosition.Forward, 9),
            },
            // 5. Junior FC
            new[] {
                ("Mauro", "Silveira", PlayerPosition.Goalkeeper, 1),
                ("Edwin", "Herrera", PlayerPosition.Defender, 4),
                ("Fabián", "Ángel", PlayerPosition.Midfielder, 8),
                ("Carlos", "Bacca", PlayerPosition.Forward, 7),
            },
            // 6. Millonarios FC
            new[] {
                ("Guillermo", "De Amores", PlayerPosition.Goalkeeper, 1),
                ("Omar", "Bertel", PlayerPosition.Defender, 4),
                ("Daniel", "Cataño", PlayerPosition.Midfielder, 10),
                ("Leonardo", "Castro", PlayerPosition.Forward, 9),
            },
            // 7. Independiente Santa Fe
            new[] {
                ("Leandro", "Castellanos", PlayerPosition.Goalkeeper, 1),
                ("Elvis", "Mosquera", PlayerPosition.Defender, 3),
                ("Daniel", "Giraldo", PlayerPosition.Midfielder, 5),
                ("Hugo", "Rodallega", PlayerPosition.Forward, 9),
            },
            // 8. Deportes Tolima
            new[] {
                ("William", "Cuesta", PlayerPosition.Goalkeeper, 1),
                ("Jersson", "González", PlayerPosition.Defender, 3),
                ("Junior", "Hernández", PlayerPosition.Midfielder, 10),
                ("Tatay", "Torres", PlayerPosition.Forward, 9),
            },
            // 9. Atlético Bucaramanga
            new[] {
                ("Juan Camilo", "Chaverra", PlayerPosition.Goalkeeper, 1),
                ("José", "Ortiz", PlayerPosition.Defender, 4),
                ("Sherman", "Cárdenas", PlayerPosition.Midfielder, 10),
                ("Sebastián", "Pons", PlayerPosition.Forward, 9),
            },
            // 10. Once Caldas
            new[] {
                ("Gerardo", "Ortiz", PlayerPosition.Goalkeeper, 1),
                ("Edisson", "Palomino", PlayerPosition.Defender, 3),
                ("Sebastián", "Gómez", PlayerPosition.Midfielder, 5),
                ("Dayro", "Moreno", PlayerPosition.Forward, 9),
            },
            // 11. Deportivo Pasto
            new[] {
                ("Diego", "Martínez", PlayerPosition.Goalkeeper, 1),
                ("Camilo", "Ayala", PlayerPosition.Defender, 4),
                ("Ray", "Vanegas", PlayerPosition.Midfielder, 10),
                ("Jown", "Cardona", PlayerPosition.Forward, 9),
            },
            // 12. Deportivo Pereira
            new[] {
                ("Harlen", "Castillo", PlayerPosition.Goalkeeper, 1),
                ("David", "González", PlayerPosition.Defender, 3),
                ("Brayan", "León", PlayerPosition.Midfielder, 8),
                ("Jonier", "Mosquera", PlayerPosition.Forward, 9),
            },
            // 13. Águilas Doradas
            new[] {
                ("José Fernando", "Cuadrado", PlayerPosition.Goalkeeper, 1),
                ("Éder", "Chaux", PlayerPosition.Defender, 4),
                ("Juan Pablo", "Ramírez", PlayerPosition.Midfielder, 10),
                ("Cristian", "Subero", PlayerPosition.Forward, 9),
            },
            // 14. Boyacá Chicó FC
            new[] {
                ("Ernesto", "Hernández", PlayerPosition.Goalkeeper, 1),
                ("Carlos", "Henao", PlayerPosition.Defender, 3),
                ("Brayan", "Moreno", PlayerPosition.Midfielder, 8),
                ("Juan David", "Valencia", PlayerPosition.Forward, 9),
            },
            // 15. Jaguares de Córdoba
            new[] {
                ("Diego", "Novoa", PlayerPosition.Goalkeeper, 1),
                ("Geovan", "Montes", PlayerPosition.Defender, 4),
                ("Larry", "Vásquez", PlayerPosition.Midfielder, 5),
                ("Pablo", "Bueno", PlayerPosition.Forward, 9),
            },
            // 16. Alianza Valledupar FC
            new[] {
                ("Luis", "Delgado", PlayerPosition.Goalkeeper, 1),
                ("Marvin", "Vallecilla", PlayerPosition.Defender, 3),
                ("Juan", "Sánchez", PlayerPosition.Midfielder, 8),
                ("Jeison", "Medina", PlayerPosition.Forward, 9),
            },
            // 17. Fortaleza FC
            new[] {
                ("Carlos", "Mosquera", PlayerPosition.Goalkeeper, 1),
                ("Nicolás", "Giraldo", PlayerPosition.Defender, 4),
                ("Jhonier", "Viveros", PlayerPosition.Midfielder, 10),
                ("Óscar", "Vanegas", PlayerPosition.Forward, 9),
            },
            // 18. Llaneros FC
            new[] {
                ("José Huber", "Escobar", PlayerPosition.Goalkeeper, 1),
                ("Cristian", "Arrieta", PlayerPosition.Defender, 3),
                ("Jhon", "Pajoy", PlayerPosition.Midfielder, 8),
                ("Brayan", "Gil", PlayerPosition.Forward, 9),
            },
            // 19. Cúcuta Deportivo
            new[] {
                ("Norberto", "Araujo", PlayerPosition.Goalkeeper, 1),
                ("Jefry", "Díaz", PlayerPosition.Defender, 4),
                ("Juan Camilo", "Portilla", PlayerPosition.Midfielder, 10),
                ("Edwar", "López", PlayerPosition.Forward, 9),
            },
            // 20. Internacional de Bogotá
            new[] {
                ("Neto", "Volpi", PlayerPosition.Goalkeeper, 1),
                ("Nicolás", "Hernández", PlayerPosition.Defender, 3),
                ("Carlos Darwin", "Quintero", PlayerPosition.Midfielder, 10),
                ("Facundo", "Boné", PlayerPosition.Forward, 9),
            },
        };

        var players = new List<Player>();
        for (int i = 0; i < teams.Count; i++)
        {
            foreach (var pd in playersData[i])
            {
                players.Add(new Player
                {
                    FirstName = pd.First,
                    LastName = pd.Last,
                    Number = pd.Number,
                    Position = pd.Pos,
                    BirthDate = new DateTime(1995, 1, 1).AddMonths(players.Count),
                    TeamId = teams[i].Id
                });
            }
        }
        context.Players.AddRange(players);
        await context.SaveChangesAsync();

        // ═══ 3. ÁRBITROS ═══
        var referees = new List<Referee>
        {
            new() { FirstName="Wilmar", LastName="Roldán", Nationality="Colombia" },
            new() { FirstName="Andrés", LastName="Rojas", Nationality="Colombia" },
            new() { FirstName="Carlos", LastName="Betancur", Nationality="Colombia" },
            new() { FirstName="Jhon", LastName="Hinestroza", Nationality="Colombia" },
        };
        context.Referees.AddRange(referees);
        await context.SaveChangesAsync();

        // ═══ 4. TORNEO ═══
        var tournament = new Tournament
        {
            Name = "Liga BetPlay 2026-I",
            Season = "2026-I",
            StartDate = new DateTime(2026, 1, 16),
            EndDate = new DateTime(2026, 6, 5),
            Status = TournamentStatus.InProgress
        };
        context.Tournaments.Add(tournament);
        await context.SaveChangesAsync();

        // ═══ 5. INSCRIBIR LOS 20 EQUIPOS ═══
        foreach (var team in teams)
        {
            context.TournamentTeams.Add(new TournamentTeam
            {
                TournamentId = tournament.Id,
                TeamId = team.Id
            });
        }
        await context.SaveChangesAsync();

        // ═══ 6. PARTIDOS (Todos contra todos: 19 fechas, 10 partidos por fecha = 190) ═══
        var matchDate = tournament.StartDate; // 16 de enero 2026
        var teamList = teams.ToList();
        int totalTeams = teamList.Count;

        // Round-Robin: fijar el primer equipo y rotar los demás
        var rotating = teamList.Skip(1).ToList(); // 19 equipos rotan

        for (int round = 0; round < totalTeams - 1; round++)
        {
            int matchDay = round + 1; // Fechas del 1 al 19

            for (int match = 0; match < totalTeams / 2; match++)
            {
                // Emparejar desde los extremos
                Team home, away;
                if (match == 0)
                {
                    home = teamList[0];
                    away = rotating[0];
                }
                else
                {
                    home = rotating[match];
                    away = rotating[rotating.Count - match];
                }

                // Alternar localía en fechas pares
                if (round % 2 == 1)
                    (home, away) = (away, home);

                context.Matches.Add(new Match
                {
                    TournamentId = tournament.Id,
                    HomeTeamId = home.Id,
                    AwayTeamId = away.Id,
                    RefereeId = referees[match % referees.Count].Id,
                    Matchday = matchDay, // Fecha 1 a 19
                    MatchDate = matchDate.AddHours(match < 5 ? 16 : 19),
                    Venue = home.Stadium,
                    Status = MatchStatus.Scheduled
                });
            }

            // Rotar: mover el primero al final
            var first = rotating[0];
            rotating.RemoveAt(0);
            rotating.Add(first);

            // Siguiente fecha: 7 días después
            matchDate = matchDate.AddDays(7);
        }

        await context.SaveChangesAsync();

        // ═══ 7. RESULTADOS, GOLES Y TARJETAS ═══
        var random = new Random(42); // Seed fijo = resultados reproducibles
        var allMatches = await context.Matches.ToListAsync();
        var allPlayers = await context.Players.ToListAsync();

        foreach (var m in allMatches)
        {
            // Cambiar estado a Finished
            m.Status = MatchStatus.Finished;

            // Generar marcador aleatorio (0-3 goles por equipo)
            int homeGoals = random.Next(0, 4);
            int awayGoals = random.Next(0, 4);

            // Registrar resultado
            context.MatchResults.Add(new MatchResult
            {
                MatchId = m.Id,
                HomeGoals = homeGoals,
                AwayGoals = awayGoals
            });

            // Jugadores de cada equipo (excluir arqueros para goles)
            var homeScorers = allPlayers
                .Where(p => p.TeamId == m.HomeTeamId && p.Position != PlayerPosition.Goalkeeper)
                .ToList();
            var awayScorers = allPlayers
                .Where(p => p.TeamId == m.AwayTeamId && p.Position != PlayerPosition.Goalkeeper)
                .ToList();

            // Registrar goles del local
            for (int g = 0; g < homeGoals; g++)
            {
                var scorer = homeScorers[random.Next(homeScorers.Count)];
                context.Goals.Add(new Goal
                {
                    MatchId = m.Id,
                    PlayerId = scorer.Id,
                    Minute = random.Next(1, 91),
                    Type = GoalType.Normal
                });
            }

            // Registrar goles del visitante
            for (int g = 0; g < awayGoals; g++)
            {
                var scorer = awayScorers[random.Next(awayScorers.Count)];
                context.Goals.Add(new Goal
                {
                    MatchId = m.Id,
                    PlayerId = scorer.Id,
                    Minute = random.Next(1, 91),
                    Type = GoalType.Normal
                });
            }

            // Tarjeta amarilla: ~25% de los partidos
            if (random.Next(100) < 25)
            {
                var allMatchPlayers = allPlayers
                    .Where(p => p.TeamId == m.HomeTeamId || p.TeamId == m.AwayTeamId)
                    .ToList();
                context.Cards.Add(new Card
                {
                    MatchId = m.Id,
                    PlayerId = allMatchPlayers[random.Next(allMatchPlayers.Count)].Id,
                    Type = CardType.Yellow,
                    Minute = random.Next(15, 85)
                });
            }

            // Tarjeta roja: ~5% de los partidos
            if (random.Next(100) < 5)
            {
                var allMatchPlayers = allPlayers
                    .Where(p => p.TeamId == m.HomeTeamId || p.TeamId == m.AwayTeamId)
                    .ToList();
                context.Cards.Add(new Card
                {
                    MatchId = m.Id,
                    PlayerId = allMatchPlayers[random.Next(allMatchPlayers.Count)].Id,
                    Type = CardType.Red,
                    Minute = random.Next(30, 90)
                });
            }
        }

        await context.SaveChangesAsync();
    }
}