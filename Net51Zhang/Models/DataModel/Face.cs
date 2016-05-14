using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;

namespace Net51Zhang.Models.DataModel
{
    public class FaceWrapper
    {
        public string Age { get; set; }
        public string Gender { get; set; }
        public FaceRectangleWrapper Rectangle { get; set; }

        public class FaceRectangleWrapper
        {
            public int Height { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
            public int Width { get; set; }
        }

        public static FaceWrapper Parse(Face face)
        {
            if (face == null)
            {
                throw new ArgumentNullException("face");
            }

            FaceRectangleWrapper rectangle = new FaceRectangleWrapper()
            {
                Height = face.FaceRectangle.Height,
                Left = face.FaceRectangle.Left,
                Top = face.FaceRectangle.Top,
                Width = face.FaceRectangle.Width
            };

            FaceWrapper wrapper = new FaceWrapper()
            {
                Age = face.FaceAttributes.Age + "岁",
                Gender = "male".Equals(face.FaceAttributes.Gender, StringComparison.OrdinalIgnoreCase) ? "男" : "女",
                Rectangle = rectangle
            };

            return wrapper;
        }
    }
}