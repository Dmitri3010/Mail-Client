using System.IO;
using System.Net;

namespace MailClient.Helpers {
    public static class ImageDownloader
    {
        public static Stream DownloadImage(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }
    }
}
