using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace WebApplication1
{
    public class GenerateQRcode
    {

        #region 二维码生成  
        /// <summary>  
        /// 批量生成二维码图片  
        /// </summary>  
        private void Create_CodeImages(int imgSize)
        {
            try
            {
                DataSet myDataSet = new DataSet();
                DataTable dt = new DataTable("PictureList");
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("PictureName", typeof(string)), new DataColumn("DataUrl", typeof(string)) });
                {
                    DataRow drTemp1 = dt.NewRow();
                    drTemp1["PictureName"] = "BaiDu";
                    drTemp1["DataUrl"] = "http://www.baidu.com";
                    DataRow drTemp2 = dt.NewRow();
                    drTemp2["PictureName"] = "360";
                    drTemp2["DataUrl"] = "http://www.360.com";
                }


                myDataSet.Tables.Add(dt);
                if (myDataSet != null)
                {
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        //清空目录  
                        DeleteDir(currentPath);
                        foreach (DataRow dr in myDataSet.Tables[0].Rows)
                        {
                            if (dr[1] != null)
                            {
                                //生成图片  
                                Bitmap image = Create_ImgCode(dr[2].ToString(), imgSize);
                                //保存图片  
                                SaveImg(currentPath, image);
                            }
                        }
                        //打开文件夹  
                        Open_File(currentPath);
                        myDataSet = null;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //程序路径  
        readonly string currentPath = System.Configuration.ConfigurationManager.AppSettings["picPath"];  //Application.StartupPath + @"\BarCode_Images";

        /// <summary>  
        /// 保存图片  
        /// </summary>  
        /// <param name="strPath">保存路径</param>  
        /// <param name="img">图片</param>  
        public void SaveImg(string strPath, Bitmap img)
        {
            //保存图片到目录  
            if (Directory.Exists(strPath))
            {
                //文件名称  
                string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                //当前目录不存在，则创建  
                Directory.CreateDirectory(strPath);
            }
        }
        /// <summary>  
        /// 生成二维码图片  
        /// </summary>  
        /// <param name="codeNumber">要生成二维码的字符串</param>       
        /// <param name="size">大小尺寸</param>  
        /// <returns>二维码图片</returns>  
        public Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }
        /// <summary>  
        /// /打开指定目录  
        /// </summary>  
        /// <param name="path"></param>  
        public void Open_File(string path)
        {
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        /// <summary>  
        /// 删除目录下所有文件  
        /// </summary>  
        /// <param name="aimPath">路径</param>  
        public void DeleteDir(string aimPath)
        {
            try
            {
                //目录是否存在  
                if (Directory.Exists(aimPath))
                {
                    // 检查目标目录是否以目录分割字符结束如果不是则添加之  
                    if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                        aimPath += Path.DirectorySeparatorChar;
                    // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组  
                    // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法  
                    string[] fileList = Directory.GetFiles(aimPath);
                    //string[] fileList = Directory.GetFileSystemEntries(aimPath);  
                    // 遍历所有的文件和目录  
                    foreach (string file in fileList)
                    {
                        // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件  
                        if (Directory.Exists(file))
                        {
                            DeleteDir(aimPath + Path.GetFileName(file));
                        }
                        // 否则直接Delete文件  
                        else
                        {
                            File.Delete(aimPath + Path.GetFileName(file));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}