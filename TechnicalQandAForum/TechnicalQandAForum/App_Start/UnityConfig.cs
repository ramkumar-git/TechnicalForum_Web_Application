using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using TechnicalQandAForum.ServiceLayer;
using System.Web.Mvc;
using System.Web.UI;

namespace TechnicalQandAForum
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IQuestionService, QuestionService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IAnswerService, AnswerService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            
        }
    }
}