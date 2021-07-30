using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
namespace Camera_Manger_ns
{
    
    public class Camera_Manager
    {
        /// <summary>
        /// Camera Manager for connect camera and operater it
        /// function 
        /// ############
        /// 1 .Connect_Camera use for connect camera by halcon library and return FGHandle(Handle Variable)
        /// 
        /// </summary>
        private HTuple _FGHandle;
        private HTuple _List_Camera;
        private HTuple _info_Camera;
        public HTuple FGHandle
        {
            get { return _FGHandle; }
            set { _FGHandle = value; }
        }
        public HTuple List_Camera
        {
            get { return _List_Camera; }
            set { _List_Camera = value; }
        }
        

        public HTuple MyProperty
        {
            get { return _info_Camera; }
            set { _info_Camera = value; }
        }


        public void Connect_Camera(HTuple type,HTuple _List_Camera ,out HTuple  _FGHandle)
        {

            HOperatorSet.OpenFramegrabber(type, 0, 0, 0, 0, 0, 0,
                "progressive", -1, "default", -1, "false", "default",
                _List_Camera, 0, -1,out _FGHandle);
        }
        
        public void Check_list_Camera(out HTuple _List_Camera)
        {
            HOperatorSet.InfoFramegrabber("GigEVision2", "info_boards", out _info_Camera, out _List_Camera);
        }
        /// <summary>
        /// for get image from camera 
        /// </summary>
        /// <param name="_FGHandle"></param> Framegrabber use ViewModel for handle
        /// <param name="_Image"></param> Image outout
        public void Get_Image(HTuple _FGHandle, out HObject _Image)
        {
            HOperatorSet.GrabImage(out _Image, _FGHandle);
        }

    }

    


}
