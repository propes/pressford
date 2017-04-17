using Ninject;
using PressfordPublishingSystem.Mappers;
using PressfordPublishingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PressfordPublishingSystem.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<DbContext>().To<PublisherDbContext>();
            _kernel.Bind<IEntityRepository<Article>>().To<EntityRepository<Article>>();
            _kernel.Bind<IArticleRepository>().To<ArticleRepository>();
            _kernel.Bind<IModelMapper<Article, ArticleViewModel>>().To<ArticleMapper>();
        }
    }
}
