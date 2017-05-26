using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// PicturesHandler 的摘要说明
    /// </summary>
    public class PicturesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "xbm image/x-xbitmap";
            GenerateQRcode gqr = new WebApplication1.GenerateQRcode();
            Bitmap bt = gqr.Create_ImgCode("http:www.baidu.com", 60);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bt.Save(stream, ImageFormat.Bmp);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            context.Response.BinaryWrite(bytes);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}