using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace ToolBase_Manager
{
    public class ImageFillter_class
    {
        /// <summary>
        /// Input Image 
        /// </summary>
        private HObject _Imageinput;

        public HObject Imageinput
        {
            get { return _Imageinput; }
            set { _Imageinput = value; }
        }
        private void threshold(HObject Imageinput, out HObject Region, HTuple Min, HTuple Max)
        {
            HOperatorSet.Threshold(Imageinput, out Region, Min, Max);
        }
        private void dilation_circle(HObject Region, out HObject RegionDilation)
        {

            HOperatorSet.DilationCircle(Region, out RegionDilation, 3.5);
        }
        private void erosion_circle(HObject Imageout, out HObject RegionErosion)
        {
            HOperatorSet.ErosionCircle(Imageout, out RegionErosion,3.5);

        }
        public void fillter_Main(HObject RegionInput,ref HObject RegionOutput, string type_filler)
        {
            
            if(type_filler == "Dilation")
            {
                dilation_circle(RegionInput,out RegionOutput);
            }
            if(type_filler == "Erosion")
            {
                erosion_circle(RegionOutput, out RegionOutput);
            }

        }


    }
}
