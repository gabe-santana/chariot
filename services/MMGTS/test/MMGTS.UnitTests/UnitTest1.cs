using AutoMapper;
using MediatR;
using MMGTS.Domain.Contracts.Repositories;
using MMGTS.Domain.Entities;
using MMGTS.Server.Commands;
using MMGTS.Server.Handlers;
using MMGTS.Server.Repositories;
using MMGTS.SharedKernel.Adapters;
using Moq;

namespace MMGTS.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            IMapper mapper = new MapperConfiguration(mc => mc.AddProfile(new MMGTSMapperProfile())).CreateMapper();

            var mediatorMock = new Mock<IMediator>();
            


            CreateMatchCommand cmd = new CreateMatchCommand
            {
                WhitePlayerId = "wid",
                BlackPlayerId = "bid",
                TimeControl = "123"
            };


          


        }
    }
}