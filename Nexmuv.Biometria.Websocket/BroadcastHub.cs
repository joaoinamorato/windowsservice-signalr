using ControliD;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Threading.Tasks;

namespace Nexmuv.Biometria.Websocket
{
    public class BroadcastHub : Hub
    {
        public async void CadastrarBiometria(string cpf)
        {
            var idbio = new CIDBio();
            var ret = idbio.GetDeviceInfo(out string version, out string serialNumber, out string model);
            if (ret < RetCode.SUCCESS)
            {
                var error = "GetDeviceInfo Error: " + CIDBio.GetErrorMessage(ret) + "\r\n";
            }

            var img = await Task.Run(() => {
                return new FingerImage
                {
                    ret = idbio.CaptureImageAndTemplate(out string temp, out byte[] imageBuf,
                            out uint width, out uint height, out int quality),
                    imageBuf = imageBuf,
                    width = width,
                    height = height,
                    temp = temp,
                    quality = quality
                };
            });

            byte[] binario = img.imageBuf;
            var biometria = new Biometria
            {
                Imagem = Convert.ToBase64String(binario),
                Modelo = model,
                Qualidade = img.quality.ToString(),
                Serial = serialNumber,
                Versao = version,
                Template = img.temp
            };

            Clients.All.returnTemplateImage(biometria);
        }

        public async void ValidarBiometria(string template)
        {
            var idbio = new CIDBio();
            var ret = idbio.GetDeviceInfo(out string version, out string serialNumber, out string model);
            if (ret < RetCode.SUCCESS)
            {
                var error = "GetDeviceInfo Error: " + CIDBio.GetErrorMessage(ret) + "\r\n";
            }

            var img = await Task.Run(() => {
                return new FingerImage
                {
                    ret = idbio.CaptureImageAndTemplate(out string temp, out byte[] imageBuf,
                            out uint width, out uint height, out int quality),
                    imageBuf = imageBuf,
                    width = width,
                    height = height,
                    temp = temp,
                    quality = quality
                };
            });

            int score = 0;
            ret = idbio.MatchTemplates(template, img.temp, out score);
            bool success;
            if (ret == RetCode.SUCCESS)
            {
                success = true;
            }
            else
            //if(ret != RetCode.SUCCESS)
            {
                success = false;
            }

            Clients.All.returnAutentication(success);
        }

        struct FingerImage
        {
            public RetCode ret;
            public string temp;
            public byte[] imageBuf;
            public uint width;
            public uint height;
            public int quality;
        }
    }
}
