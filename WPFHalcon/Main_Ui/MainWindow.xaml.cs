using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HalconDotNet;

namespace Main_Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(240, 222, 45));
        // Local iconic variables 

        HObject ho_Image;
        HObject ho_Region;
        HObject ho_ConnectedRegions;

        // Local control variables 
        HTuple hv_Height = new HTuple(), hv_Width = new HTuple();
        HTuple hv_WindowHandle = new HTuple();
        HTuple AcqHandle = new HTuple();
        public MainWindow()
        {
            InitializeComponent();
        }

  
    

        private void MainUi_Loaded(object sender, RoutedEventArgs e)
        {
            //initialprogarm camera
            hv_WindowHandle = Disp.HalconWindow;

            

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);

            Camera_Inital();

            HOperatorSet.SetColored(hv_WindowHandle, 12);

            // backgroundWorker inital
            //backgroundWorker_Proc.WorkerReportsProgress = true;
            //backgroundWorker_Proc.WorkerSupportsCancellation = true;
        }
        private void Camera_Inital()
        {
            string Image_Part = "printer_chip/";
            HOperatorSet.OpenFramegrabber("File", 1, 1, 0, 0, 0, 0, "default", -1, "default", -1, "false", Image_Part, "default", 1, -1, out AcqHandle);
        }
        private void Disp_Result()
        {
            HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
            HOperatorSet.DispObj(ho_ConnectedRegions, hv_WindowHandle);

            // Optional 1 
            // Try to comment and un-comment this commmand. and see what happens
            //HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //hSmartWindowControl_Display.HalconWindow.SetPart(0, 0, hv_Height.I - 1, hv_Width.I - 1);

            // Optional 2
            // Try to comment and un-comment this commmand. and see what happens
            Disp.SetFullImagePart(null);
        }
        private void Camera_acq_Image()
        {
            HOperatorSet.GrabImageAsync(out ho_Image, AcqHandle, -1);
        }

        private void Btn_open_Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //check toggle 
            if (Btn_Run.Toggle1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                Camera_acq_Image();
                proc_Image();
                Disp_Result();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                lb_cycletime.Content = "Cycle time 1 Image = " + elapsedMs.ToString() + " ms";
                Light.Fill = On;
            }
            else
            {
                Light.Fill = Off;
            }
        }

        private void proc_Image()
        {
            HOperatorSet.Threshold(ho_Image, out ho_Region, 128, 255);
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
        }

    }
}
