using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Tavisca.EmployeeManagement.EnterpriseLibrary.Unity
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        private readonly IUnityContainer container;

        public UnityServiceHostFactory()
        {
            container = new UnityContainer();
            container.LoadConfiguration();
        }

        protected override ServiceHost CreateServiceHost(
          Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(this.container,
              serviceType, baseAddresses);
        }
    }

    public class UnityServiceHost : ServiceHost
    {
        public UnityServiceHost(IUnityContainer container,
          Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            foreach (var contractDescription in this.ImplementedContracts.Values)
            {
                contractDescription.Behaviors.Add(new UnityInstanceProvider(container));
            }
        }
    }

    public class UnityInstanceProvider
  : IInstanceProvider, IContractBehavior
    {
        private readonly IUnityContainer container;

        public UnityInstanceProvider(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext,
          Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.container.Resolve(
              instanceContext.Host.Description.ServiceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext,
          object instance)
        {
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
