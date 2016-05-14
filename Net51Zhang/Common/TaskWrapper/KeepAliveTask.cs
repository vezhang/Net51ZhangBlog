using System.Net;

namespace Net51Zhang.Common.TaskWrapper
{
    public class KeepAliveTask : ITask
    {
        public void Execute()
        {
            string url = "http://51zhang.net/Movie";
            using (var client = new WebClient())
            {
                var ss = client.DownloadString(url);
            }
        }
    }
}