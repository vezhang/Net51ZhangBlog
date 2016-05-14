using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.ProjectOxford.Face;
using Net51Zhang.Common;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Controllers
{
    public class OxfordController : BaseController
    {
        public OxfordController(IServiceManager service) : base(service)
        {
        }

        public ActionResult HowOld()
        {

            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> HowOld_ApiContent(string input)
        {
            AjaxResponse response = new AjaxResponse();

            try
            {
                string error;
                MemoryStream stream = Utils.ParseBase64Image(input, out error);
                var image= System.Drawing.Image.FromStream(stream);
                if (image.Height > 534 || image.Width > 800)
                {
                    var resizedImage = Utils.ResizeImage(image, new Size(800, 534));
                    stream = Utils.Image2Stream(resizedImage);
                    string src = Convert.ToBase64String(stream.ToArray());
                    response.Attach = string.Format("data:image/bmp;base64,{0}", src);
                }
                else
                {
                    stream.Position = 0;
                }

                if (string.IsNullOrEmpty(error) && stream != null)
                {
                    response.Data = await this.DetectFaces(stream);
                }
            }
            catch (Exception e)
            {
                response.Error = e.Message;
            }

            return this.Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> HowOld_ApiFile(string input)
        {
            AjaxResponse response = new AjaxResponse();

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, input.TrimStart('/', '\\'));
                response.Data = await this.DetectFaces(System.IO.File.OpenRead(path));
            }
            catch (Exception e)
            {
                response.Error = e.Message;
            }

            return this.Json(response);
        }

        [NonAction]
        private async Task<List<FaceWrapper>> DetectFaces(Stream stream)
        {
            var faceServiceClient = new FaceServiceClient(Constant.OxfordFaceKey);
            var faces = await faceServiceClient.DetectAsync(stream, false, false,
                new FaceAttributeType[] {FaceAttributeType.Gender, FaceAttributeType.Age}).ConfigureAwait(false);

            return faces.Select(FaceWrapper.Parse).ToList();
        }
    }
}