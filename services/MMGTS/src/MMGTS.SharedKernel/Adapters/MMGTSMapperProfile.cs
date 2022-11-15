using AutoMapper;
using MMGTS.Domain.Entities;
using MMGTS.SharedKernel.Facade.MatchData;

namespace MMGTS.SharedKernel.Adapters
{
    /// <summary>
    /// Default mapping profile
    /// </summary>
    public class MMGTSMapperProfile : Profile
    {
        public MMGTSMapperProfile()
        {
            #region Entity to DTO and vice-versa
            CreateMap<MatchData, MatchDataDTO>().ReverseMap();
            #endregion

            #region Entity to  CreationObject and vice-versa
            CreateMap<MatchData, CreationMatchData>().ReverseMap();
            #endregion

        }
    }
}
