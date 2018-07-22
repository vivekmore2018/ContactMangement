using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace CM.WebApi
{
    /// <summary>
    /// Custom Service Resolver
    /// </summary>
    public class ServiceResolver : IDependencyResolver
    {
        protected IUnityContainer _container;

        /// <summary>
        /// Custom service resolver constructor 
        /// </summary>
        /// <param name="container">Dependency container </param>
        public ServiceResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }
        /// <summary>
        /// If multiple container needed then register here
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new ServiceResolver(child);
        }
        /// <summary>
        /// Disponse object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Manually resove service
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
        /// <summary>
        /// Resolve service type
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
        /// <summary>
        /// Dispose container
        /// </summary>
        /// <param name="disposing"></param>

        protected virtual void Dispose(bool disposing)
        {
            _container.Dispose();
        }
    }
}