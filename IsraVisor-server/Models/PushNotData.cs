namespace IsraVisor_server.Controllers
{
    public class PushNotData
    {

        public object to { get; set; }
        public object title { get; set; }
        public object body { get; set; }
        public object sound { get; set; }
        internal object badge;
        public Data data { get; set; }
    }
}