using Microsoft.AspNet.SignalR;

namespace Nexmuv.Biometria.Websocket
{
    public class BroadcastHub : Hub
    {
        public void GetTemplateImage()
        {
            var biometria = new Biometria
            {
                Height = 300,
                Imagem = new byte[1024],
                Quality = 98,
                Template = "template",
                Width = 300
            };

            Clients.All.returnTemplateImage(biometria);
        }
    }
}
