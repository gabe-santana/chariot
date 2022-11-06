using Autofac;
using AutoMapper;
using MMGTS.Domain.Contracts.Repositories;
using MMGTS.Server.Repositories;
using MMGTS.SharedKernel.Adapters;

namespace MMGTS.SharedKernel.IoC
{
    public class MMGTSIoCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all mappings (AutoMapper)
            #region Mappings
            builder.Register(context => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MMGTSMapperProfile());
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
             .As<IMapper>()
             .InstancePerLifetimeScope();
            #endregion

            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IGenericRepo<>));
        }
    }
}
