using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Nexmuv.Biometria.Websocket
{
    [RunInstaller(true)]
    public class ServicoInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;

        public ServicoInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            serviceInstaller.ServiceName = "NexmuvBiometria";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            processInstaller.Account = ServiceAccount.LocalSystem;

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
