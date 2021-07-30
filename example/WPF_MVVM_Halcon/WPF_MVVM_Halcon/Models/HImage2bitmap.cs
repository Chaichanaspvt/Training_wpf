using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HalconDotNet;
using System.Drawing.Imaging;

namespace HalconWPF.Models
{
    public class HImage2bitmap
    {
        HImage Img;
        Image Imgbitmap;

        HTuple type, width, height;
        private HImage H_Img;

        public HImage ImageInput
        {
            get { return H_Img; }
            set { H_Img = value; }
        }
        private Image _Imageoutput;

        public Image Imageoutput
        {
            get { return _Imageoutput; }
            set { _Imageoutput = value; }
        }


        public void Convert2bitmap_grayscale()
        {
            IntPtr ptr = Img.GetImagePointer1(out type, out width, out height);
            Imgbitmap = new Bitmap(width / 4, height, width,PixelFormat.Format32bppArgb, ptr);

        }
    }

} 
