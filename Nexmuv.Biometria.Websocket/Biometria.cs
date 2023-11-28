namespace Nexmuv.Biometria.Websocket
{
    public class Biometria
    {
        public string Template { get; set; }
        public byte[] Imagem { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public int Quality { get; set; }
    }
}
