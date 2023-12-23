using Autofac;
using TaskManagment.Domain.Context.AggregatesModel.Tasks;
using TaskManagment.Infrastructure.Repositories;
using TaskManagment.Infrastructure.Services.Token;

namespace TaskManagment.Aplicacion.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string KeyPrivate { get; set; }
        public string Expires { get; set; }

        public ApplicationModule(string keyPrivate, string expires)
        {
            KeyPrivate = keyPrivate;
            Expires = expires;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterInfra(builder);
            RegisterApplication(builder);
        }

        private void RegisterInfra(ContainerBuilder builder)
        {
            builder.Register(c => new TokenService(KeyPrivate, Expires))
                   .As<ITokenService>()
                   .InstancePerLifetimeScope();


            //builder.Register(c => new DAEngine(ConnectionStrings,
            //    c.Resolve<IErrorLoggerService>(),
            //    c.Resolve<IAppConfig>()))
            //            .As<IDAEngine>()
            //            .InstancePerLifetimeScope();

            builder.RegisterType<TaskRepository>()
          .As<ITaskRepository>()
          .InstancePerLifetimeScope();


        }


        private static void RegisterApplication(ContainerBuilder builder)
        {
           // builder.RegisterType<SQSMessageBrokerService>()
           //.As<IMessageBrokerService>()
           //.InstancePerLifetimeScope();

            //builder.RegisterType<ServiceArchivo>()
            //.As<IServiceArchivo>()
            //.InstancePerLifetimeScope();

            //builder.RegisterType<SolicitudServiceAplicacion>()
            //.As<ISolicitudServiceAplicacion>()
            //.InstancePerLifetimeScope();
        }

    }

}
