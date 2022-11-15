using AutoMapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MMGTS.Domain.Entities;
using MMGTS.Infra.EF.Context;
using MMGTS.Server.Commands;
using MMGTS.Server.Handlers;
using MMGTS.Server.Repositories;
using MMGTS.SharedKernel.Adapters;
using Moq;
using Xunit.Abstractions;
 
namespace MMGTS.UnitTests
{
    public class MatchCommandsTest : IDisposable
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly SqliteConnection _connection;

        private readonly IMapper _mapper;
        private readonly ITestOutputHelper output;

        public MatchCommandsTest(ITestOutputHelper output)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<DataContext>().UseSqlite(_connection).Options;
            _mapper = new MapperConfiguration(mc => mc.AddProfile(new MMGTSMapperProfile())).CreateMapper();

            this.output = output;

            Setup();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public void Setup()
        {
            // Setup
            using (var context = new DataContext(_options))
            {
                context.Database.EnsureCreated();
                context.MatchData.Add(new MatchData
                {
                    Id = new Guid(),
                    WPlayerId = "wid",
                    BPlayerId = "bid",
                    TimeControl = "5",
                    Result = "1-0",
                    PGN = "[Event \"F/S Return Match\"]\n[Site \"Belgrade, Serbia JUG\"]\n[Date \"1992.11.04\"]\n[Round \"29\"]\n[White \"Fischer, Robert J.\"]\n[Black \"Spassky, Boris V.\"]\n[Result \"1/2-1/2\"]\n\n1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 {This opening is called the Ruy Lopez.}\n4. Ba4 Nf6 5. O-O Be7 6. Re1 b5 7. Bb3 d6 8. c3 O-O 9. h3 Nb8 10. d4 Nbd7\n11. c4 c6 12. cxb5 axb5 13. Nc3 Bb7 14. Bg5 b4 15. Nb1 h6 16. Bh4 c5 17. dxe5\nNxe4 18. Bxe7 Qxe7 19. exd6 Qf6 20. Nbd2 Nxd6 21. Nc4 Nxc4 22. Bxc4 Nb6\n23. Ne5 Rae8 24. Bxf7+ Rxf7 25. Nxf7 Rxe1+ 26. Qxe1 Kxf7 27. Qe3 Qg5 28. Qxg5\nhxg5 29. b3 Ke6 30. a3 Kd6 31. axb4 cxb4 32. Ra5 Nd5 33. f3 Bc8 34. Kf2 Bf5\n35. Ra7 g6 36. Ra6+ Kc5 37. Ke1 Nf4 38. g3 Nxh3 39. Kd2 Kb5 40. Rd6 Kc5 41. Ra6\nNf2 42. g4 Bd3 43. Re6 1/2-1/2",
                    Date = DateTime.UtcNow

                });
                context.SaveChanges();
            }
        }

        [Fact]
        public async void ShouldCreateAMatch()
        {
            using (var context = new DataContext(_options))
            {
                var mediatorMock = new Mock<IMediator>();
                var _repo = new GenericRepo<MatchData>(_mapper, context);



                var cmd = new CreateMatchCommand
                {
                    WhitePlayerId = "wid",
                    BlackPlayerId = "bid",
                    TimeControl = "123"
                };

                mediatorMock
                    .Setup(s => s.Send(It.IsAny<CreateMatchCommand>(), It.IsAny<CancellationToken>()));

                var handler = new CreateMatchHandler(mediatorMock.Object, _repo);

                var response = await handler.Handle(cmd, new CancellationToken());
                Assert.DoesNotContain("error", response);
            }
        }
    }
}