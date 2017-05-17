using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;
using WebUI.Infrastructure.Abstract;
using WebUI.Infrastructure.Concrete;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //привязки
            // первая привязка без использования хранилища

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Prod_Name = "Шашлык свинной", Prod_Price = 45 },
            //    new Product { Prod_Name = "Шашлык куринный", Prod_Price = 35 },
            //    new Product { Prod_Name = "Шашлык говяжий", Prod_Price = 50 },
            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);
            
            // вторая привязка с использованием базы данных
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            //привязка при использовании аутентификации
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();

            // все что связано с отправкой заказов на почту
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
        
    }
}