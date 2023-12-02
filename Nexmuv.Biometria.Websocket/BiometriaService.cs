using Microsoft.Owin.Hosting;
using System.ServiceProcess;

namespace Nexmuv.Biometria.Websocket
{
    public partial class BiometriaService : ServiceBase
    {
        public BiometriaService()
        {
            InitializeComponent();
            this.ServiceName = "NexmuvBiometria";
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            InitializeSelfHosting();
        }

        protected override void OnStop()
        {

        }

        public void InitializeSelfHosting()
        {
            const string url = "http://localhost:8585";
            WebApp.Start(url);
        }
    }
}
