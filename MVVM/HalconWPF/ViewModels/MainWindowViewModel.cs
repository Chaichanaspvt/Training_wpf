using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using HalconDotNet;
using HalconWPF.Models;
using System.Windows.Input;
using System.ComponentModel;

namespace HalconWPF.ViewModels
{
    public class MainWindowViewModel : Screen 
    {
        /// <summary>
        /// use propfull for s
        /// </summary>
        private string  _fristName = "SP Vision";
        private HObject _DisplayImage;
        private BindableCollection<ReadImageModel> _Image = new BindableCollection<ReadImageModel>();
        private ReadImageModel _selcetImage;
        private HObject _Region;
        private HObject _connectedRegions;

        public string FristName
        {
            get { return _fristName; }
            set 
            {
                _fristName = value;
                //Need to include Screen => update FristName
                NotifyOfPropertyChange(() => FristName);
                
            }
        }

       

        public HObject DisplayImage
        {
            //retun _DisplayImage to DisplayImage
            get { return _DisplayImage; }
            set 
            { 
                _DisplayImage = value;
                NotifyOfPropertyChange(() => DisplayImage);
            }
        }

       
        public BindableCollection<ReadImageModel> Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        

        public ReadImageModel SelectedImage
        {
            get { return _selcetImage; }
            set { 
                _selcetImage = value;
                NotifyOfPropertyChange(() => SelectedImage);
            }
        }
       

        public HObject Region
        {
            get { return _Region; }
            set { _Region = value;
                NotifyOfPropertyChange(() => Region);
            }
        }


        public HObject ConnectedRegions
        {
            get { return _connectedRegions; }
            set
            { 
                _connectedRegions = value;
                NotifyOfPropertyChange(() => ConnectedRegions);
            }
            
        }



        public void ReadImage()
        {
            HObject Imagehandle, R, C;
            // @"C:/Users/Public/Documents/MVTec/HALCON-20.11-Steady/examples/images/barcode/25industrial/25industrial01.png"
            HOperatorSet.ReadImage(out Imagehandle, @"printer_chip/printer_chip_01.png");
            //DisplayImage = new HImage(@"printer_chip/printer_chip_01.png");
            thredsholdwpf_ns.thredsholdwpf_pn.ResourcePath = @"H:\01-Projects\02-GVBPRO_New_Version\Training_wpf\halcon\thredsholdwpf_pn\res_thredsholdwpf_pn";
            thredsholdwpf_ns.thredsholdwpf_pn.threshold_wpf(Imagehandle, out R,out C);
            Region = R;
            ConnectedRegions = C;
            DisplayImage = Imagehandle;
        }
 

    }
}
