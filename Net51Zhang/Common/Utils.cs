using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Net51Zhang.Common
{
    public static class Utils
    {
        public static string AppFolder()
        {
            var appDomain = AppDomain.CurrentDomain;
            string folder = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;

            return folder;
        }

        public static string AbsoluteAppFile(string relativePath)
        {
            return Path.Combine(AppFolder(), relativePath);
        }

        public static Stream ExecuteWebRequest(string url, int retry, out string error)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            Stream stream = null;
            error = null;

            while (retry-- >= 0)
            {
                try
                {
                    stream = ExecuteWebRequest(webRequest);
                    break;
                }
                catch (Exception exception)
                {
                    error = exception.Message;
                    stream = null;
                }  
            }

            return stream;
        }

        public static Stream ExecuteWebRequest(WebRequest webRequest)
        {
            WebResponse response;
            try
            {
                response = webRequest.GetResponse();
            }
            catch (WebException webException)
            {
                string specificErrorMessage = "WebException: " + webException;
                if (webException.Response != null)
                {
                    response = (WebResponse)webException.Response;

                    specificErrorMessage += " WebResponse: " + ReadResponseStream(response);
                }

                string errorMessage = string.Format(
                    "Failed to process webRequest [{0}]. Error = [{1}]", webRequest.RequestUri.AbsolutePath, specificErrorMessage);
                throw new Exception(errorMessage);
            }

            return response.GetResponseStream();
        }

        private static string ReadResponseStream(WebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            string responseString = string.Empty;

            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                using (var sr = new StreamReader(responseStream))
                {
                    responseString = sr.ReadToEnd();
                }
            }

            return responseString;
        }

        public static List<T[]> Convert2DimArray<T>(ICollection<T> source, int lineCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var length = source.Count();
            var results = new List<T[]>(length/lineCount + 1);

            for (int index = 0; index < length; index += lineCount)
            {
                T[] tmps = new T[lineCount];
                for (int j = 0; j < lineCount && index + j < length; j++)
                {
                    tmps[j] = source.ElementAt(index + j);
                }

                results.Add(tmps);
            }

            return results;
        }

        public static bool IsLocalhost(HttpRequestBase request)
        {
            return "127.0.0.1".Equals(GetRequestIPAddress(request), StringComparison.OrdinalIgnoreCase);
        }

        public static string GetRequestIPAddress(HttpRequestBase request)
        {
            string result = "";
            const string forwardedHttpHeader = "X-FORWARDED-FOR";
            string xff =
                request.Headers.AllKeys.Where(
                    x => x.Equals(forwardedHttpHeader, StringComparison.InvariantCultureIgnoreCase))
                    .Select(k => request.Headers[k])
                    .FirstOrDefault();

            if (!string.IsNullOrEmpty(xff))
            {
                result = xff.Split(new[] { ',' }).FirstOrDefault();
            }

            if (string.IsNullOrEmpty(result) && request.UserHostAddress != null)
            {
                result = request.UserHostAddress;
            }

            if (result == "::1")
                result = "127.0.0.1";

            if (!string.IsNullOrEmpty(result))
            {
                var index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                {
                    result = result.Substring(0, index);
                }
            }

            return result;
        }

        public static Exception GetUnwrapperException(Exception exception)
        {
            var e = exception as AggregateException;
            if(e != null)
            {
                return GetUnwrapperException(e.InnerException);
            }

            return exception;
        }

        public static bool IsAjaxCall(HttpRequestBase request)
        {
            return request != null && request.Headers != null 
                && "XMLHttpRequest".Equals(request.Headers["X-Requested-With"], StringComparison.OrdinalIgnoreCase);
        }

        public static string GetMd5Hash(string input)
        {
            if (input == null)
                input = string.Empty;

            byte[] data = Encoding.UTF8.GetBytes(input.Trim().ToLowerInvariant());
            using (var md5 = new MD5CryptoServiceProvider())
            {
                data = md5.ComputeHash(data);
            }

            var ret = new StringBuilder();
            foreach (byte b in data)
            {
                ret.Append(b.ToString("x2").ToLowerInvariant());
            }

            return ret.ToString();
        }

        public static MemoryStream Image2Stream(Image image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;

            return stream;
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;
            //Get the image current height
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size
            nPercentW = ((float) size.Width/(float) sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float) size.Height/(float) sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width
            int destWidth = (int) (sourceWidth*nPercent);
            //New Height
            int destHeight = (int) (sourceHeight*nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image) b);
            g.InterpolationMode = InterpolationMode.Default;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image) b;
        }

        public static MemoryStream ParseBase64Image(string content, out string error)
        {
            if (content == null || !content.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
            {
                error = "Data format is not Image.";
                return null;
            }

            int index = content.IndexOf(",", StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                error = "Invalid Data Format. It expects: [data:image/jpg;base64,{base64-data}]";
                return null;
            }

            error = null;
            string base64 = content.Substring(index + 1);

            return new MemoryStream(Convert.FromBase64String(base64));
        }
    }
}