using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using CodeSOSProject.ServiceLayer;
using System.Web.Mvc;

namespace CodeSOS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();            

            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IUserService, UsersService>();
            container.RegisterType<ICategoryService, CategoryService>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}