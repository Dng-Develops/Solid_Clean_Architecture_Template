using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //OperationClaim
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            
            //User
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>(); 
            
            //UserOperationClaim
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            //AuthManager
            builder.RegisterType<AuthManager>().As<IAuthService>();

            // Acpects

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new
                Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
        

    }
}
