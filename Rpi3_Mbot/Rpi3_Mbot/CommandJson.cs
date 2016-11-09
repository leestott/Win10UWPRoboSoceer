//protocol  v1.1
namespace Rpi3_Mbot
{

    public class TypeJson
    {
        public int Type { get; set; }
        public string Command { get; set; }
    }
    public class LoginInJson
    {
        public string deviceId { get; set; }
        public string name { get; set; }

    }
    public class SendMotorStatusJson
    {
        public string deviceId { get; set; }
        public bool MotorStatus { get; set; }

    }
    public class SendXboxStatusJson
    {
        public string deviceId { get; set; }
        public bool XboxStatus { get; set; }
    }


    public class SendShootStatusJson
    {
        public string deviceId { get; set; }
        public bool ShootStatus { get; set; }
    }

    public class SendMotorRunStatusJson
    {
        public string deviceId { get; set; }
        public int RunTime { get; set; }
        public int RunDistance { get; set; }
    }

    public class EnableTransferJson
    {
        public string deviceId { get; set; }
        public int EnableTransfer { get; set; }
    }

}
