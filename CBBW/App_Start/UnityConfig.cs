using System.Web.Mvc;
using CBBW.BLL.IRepository;
using CBBW.BLL.Repository;
using Unity;
using Unity.Mvc5;

namespace CBBW
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IMasterRepository, MasterRepository>();
            container.RegisterType<IToursRuleRepository, ToursRuleRepository>();
            container.RegisterType<ITADARulesRepository, TADARulesRepository>();
            container.RegisterType<ICTVRepository, CTVRepository>();
            container.RegisterType<IMGPRepository, MGPRepository>(); 
            container.RegisterType<IEHGRepository, EHGRepository>();
            container.RegisterType<IMyHelperRepository, MyHelperRepository>();
            container.RegisterType<IGVMRRepository, GVMRRepository>();
            container.RegisterType<IETSRepository, ETSRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}