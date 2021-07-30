using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using HalconDotNet;
using Camera_Manger_ns;
using HEngine_Manager_ns;
using ToolBase_Manager;
using System.Windows.Media;
using Microsoft.Win32;


namespace WPF_MVVM_Halcon.ViewModels
{
    public class MainViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private HObject _DisplayImage;
        private HObject _DisplayRegion;
        private string _Name_btn_live_mode = "Live Mode";


        //Class Camera Manager
        Camera_Manager Camera;
        private HTuple _Camera_List;
        private HTuple _FGHandle;
        //Thread Camera
        BackgroundWorker CameraWorker = new BackgroundWorker();
       
        BackgroundWorker ThresoldWorker;

        //Class Fillter 
        ImageFillter_class Fillter;
        bool onFillter = false;
        
        
        //Btn color
        private Brush _color_btn_find_camera = Brushes.LightGray;
        private Brush _color_btn_connect_camera = Brushes.LightGray;
        private Brush _Status_Color = Brushes.Red;

        //HEngine 
        public string Path_Proc;
        HEngine_Manager CallEngine = new HEngine_Manager();
        bool _usEngine = false;

        /// <summary>
        /// use DisplayImage for display on HSmartWindow
        /// </summary>
        public HObject DisplayImage
        {
            get { return _DisplayImage; }
            set
            {
                _DisplayImage = value;
                NotifyPropertyChanged("DisplayImage");
                DisplayRegion = null;
            }
        }
        public HObject DisplayRegion
        {
            get { return _DisplayRegion; }
            set { _DisplayRegion = value; NotifyPropertyChanged("DisplayRegion"); }
        }
        private string _btn_Name_Main_Progarm = "RUN HDevEngine";

        public string btn_Name_Main_Progarm
        {
            get { return _btn_Name_Main_Progarm ; }
            set { _btn_Name_Main_Progarm = value; }
        }
        private bool _waitng_debug;

        public bool waitng_debug
        {
            get { return _waitng_debug; }
            set { _waitng_debug = value; }
        }



        public HTuple Camera_List
        {
            get { return _Camera_List; }
            set { _Camera_List = value; }
        }

        
        //Button Color
        public Brush color_btn_find_camera
        {
            get { return _color_btn_find_camera; }
            set {
                _color_btn_find_camera = value;
                NotifyPropertyChanged("color_btn_find_camera");
            }
        }
        public Brush color_btn_connect_camera
        {
            get { return _color_btn_connect_camera; }
            set { _color_btn_connect_camera = value;
                NotifyPropertyChanged("color_btn_connect_camera");
            }
        }
        //Color status
        

        public Brush Status_Color
        {
            get { return _Status_Color; }
            set { _Status_Color = value; }
        }

        /// <summary>
        /// Button Name
        /// </summary>



        public string Name_btn_live_mode
        {
            get { return _Name_btn_live_mode; }
            set
            {
              _Name_btn_live_mode = value;
              NotifyPropertyChanged("Name_btn_live_mode");
            }
        }

        public int MinThreshold
        {
            get { return (int)GetValue(MinThresholdProperty); }
            set { SetValue(MinThresholdProperty, value); }
        }
        public static readonly DependencyProperty MinThresholdProperty =
            DependencyProperty.Register("MinThreshold", typeof(int), typeof(MainViewModel), 
                new PropertyMetadata(0, new PropertyChangedCallback(OnThresholdValueChanged)));
        public int MaxThreshold
        {
            get { return (int)GetValue(MaxThresholdProperty); }
            set { SetValue(MaxThresholdProperty, value); }
        }
        public static readonly DependencyProperty MaxThresholdProperty =
            DependencyProperty.Register("MaxThreshold", typeof(int),
                typeof(MainViewModel), new PropertyMetadata(255, new PropertyChangedCallback(OnThresholdValueChanged)));

        private string _Filter_image;
        public string Filter_image
        {
            get { return  (string)GetValue(fillterProperty); }
            set {SetValue(fillterProperty, value);
                _Filter_image = value;
            }
        }
        public static readonly DependencyProperty fillterProperty = DependencyProperty.Register("Filter_image", typeof(string),
                typeof(MainViewModel), new PropertyMetadata( new PropertyChangedCallback(OnfilterChanged)));

        private static void OnfilterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
         
     
         
            MainViewModel vm = obj as MainViewModel;
            if (vm != null)
            {
                if (!vm.CameraWorker.IsBusy) {
                    if (vm.DisplayRegion != null)
                    {
                        List<String> data = vm.Filter_image.Split(':', ' ').ToList();
                        //remove system.windows.controls.comboboxitem:
                        if (data[2] != "None")
                        {
                            HObject RegionFillerHandle = null;
                            vm.Fillter = new ImageFillter_class();
                            vm.Fillter.fillter_Main(vm.DisplayRegion, ref RegionFillerHandle, data[2]);
                            vm.DisplayRegion = RegionFillerHandle;
                        }
                        else
                        {
                            vm.DisplayRegion = vm._DisplayRegion;
                        }

                    }
                }
            }
        }
        private static void OnThresholdValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            MainViewModel vm = obj as MainViewModel;
            if (vm != null)
            {
               
                    if (e.Property.Name == "MinThreshold" && (int)e.NewValue > vm.MaxThreshold)
                        vm.MaxThreshold = (int)e.NewValue;
                    if (e.Property.Name == "MaxThreshold" && (int)e.NewValue < vm.MinThreshold)
                        vm.MinThreshold = (int)e.NewValue;
                    if (vm.DisplayRegion != null && vm.DisplayImage != null)
                {
                    
                    HOperatorSet.Threshold(vm.DisplayImage, out vm._DisplayRegion, (double)vm.MinThreshold, (double)vm.MaxThreshold);
                    vm.DisplayRegion = vm._DisplayRegion;
                }
                       
                
            }
        }

        /// <summary>
        /// use for command  find List Camera,are connected.
        /// </summary>
        public MainCommand CheckListCamera { get; private set; }
        public MainCommand ConnectCamera { get;private set; }
        public MainCommand onSnapMode { get;private set; }
        public MainCommand onLiveMode { get;private set; }
        public MainCommand ApplyThreshold { get; set; }
        public MainCommand OpenImage { get; set; }
        public MainCommand CallHDevEng { get; set; }
        public MainCommand onServerDebug { get; set; }
        /// <summary>
        /// 1.CheckListCamera
        /// 2.ConnectCamera
        /// 3.onSnapMode
        /// 4.onLiveMode
        /// 5.ApplyThreshold
        /// 6.OpenImage File
        /// 7.Call HDevEngine 
        /// 8.On Sever Debug
        /// </summary>
        public MainViewModel()
        {
            // Define the functions that are called through the Commands assigned to
            // the buttons.
            // The buttons use for find Camera
            CheckListCamera = new MainCommand((obj) => Check_List_Camera());
            //Connect Camera
            ConnectCamera = new MainCommand((obj) => Connect_Camera());
            //On SnapMode
            onSnapMode = new MainCommand((obj) => Snap_Mode());
            //On Live Mode
            onLiveMode = new MainCommand((obj) => Live_Mode());
            // The Apply button is only enabled when an image was loaded
            ApplyThreshold = new MainCommand(
                (obj) => {
                    if (DisplayImage != null)
                    {
                        onFillter = true;
                        HOperatorSet.Threshold(DisplayImage, out _DisplayRegion, (double)MinThreshold, (double)MaxThreshold);
                        DisplayRegion = _DisplayRegion;
                      


                    }
                },
                (obj) => DisplayImage != null);
            OpenImage = new MainCommand(
                (obj) =>
                {
                    // Create OpenFileDialog
                    OpenFileDialog openFileDlg = new OpenFileDialog();

                    // Launch OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = openFileDlg.ShowDialog();
                    // Get the selected file name and display in a TextBox.
                    // Load content of file in a TextBlock
                    if (result == true)
                    {
                        string FileName = openFileDlg.FileName;
                        HOperatorSet.ReadImage(out _DisplayImage, FileName);
                        DisplayImage = _DisplayImage;
                        
                        
                    }

                });
            CallHDevEng = new MainCommand((obj) => Call_Engine());
            onServerDebug = new MainCommand((obj) => On_Sever_Debug());

        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Function progarm
        /// </summary>
        public void Check_List_Camera()
        {
            HTuple CameraListHandle;
            Camera = new Camera_Manager();
            Camera.Check_list_Camera(out CameraListHandle);
            Camera_List = CameraListHandle;
            color_btn_find_camera = Brushes.GreenYellow;


        }
        public void Connect_Camera()
        {
            
            try
            {
                Camera.Connect_Camera("GigEVision2", Camera_List, out _FGHandle);
                color_btn_connect_camera = Brushes.GreenYellow;
                // Create Thread Camera
                CameraWorker.DoWork += CameraWorker_Live_Mode_DoWork;
                CameraWorker.WorkerSupportsCancellation = true;
                // Create Image fillter Threshold Thread
                ThresoldWorker = new BackgroundWorker();
                ThresoldWorker.DoWork += ThresoldWorker_DoWork;






            }
            catch (Exception)
            {
                
            }
        }

        private void ThresoldWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Image_filler();
        }

        /// <summary>
        /// Camera work function 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraWorker_Live_Mode_DoWork(object sender, DoWorkEventArgs e)
        {
            HObject ImageHandle;
            while (true)
            {
                Camera.Get_Image(_FGHandle, out ImageHandle);
                DisplayImage = ImageHandle;
                if (onFillter) { ThresoldWorker.RunWorkerAsync(); }
                if (CameraWorker.CancellationPending)
                {
                    Console.WriteLine("Stop Live Mode ......");
                    break;
                }

            }
           
        }
        public void Snap_Mode()
        {
            if (!CameraWorker.IsBusy) {
                HObject ImageHandle;
                Camera.Get_Image(_FGHandle, out ImageHandle);
                DisplayImage = ImageHandle;
                if (onFillter) { ThresoldWorker.RunWorkerAsync(); }
                
            }

        }
        public void Live_Mode()
        {
          
            if (!CameraWorker.IsBusy)
            {
                //Start Thread
                CameraWorker.RunWorkerAsync();
                //Change Name Btn to Stop Live Mode
                Name_btn_live_mode = "Stop Live Mode";

            }
            else
            { //Please Alert Live mode on please stop it
                CameraWorker.CancelAsync();
                //Change Name Btn to Live Mode
                Name_btn_live_mode = "Live Mode";
            }
        }
        public void Image_filler()
        {
            this.Dispatcher.Invoke(() =>
            {
                HOperatorSet.Threshold(DisplayImage, out _DisplayRegion, (double)MinThreshold, (double)MaxThreshold);
                //remove system.windows.controls.comboboxitem: and ' '
                List<String> data = Filter_image.Split(':',' ').ToList();
                //remove system.windows.controls.comboboxitem:
                if (data[2] != "None")
                {
                    HObject RegionFillerHandle = null;
                    Fillter = new ImageFillter_class();
                    Fillter.fillter_Main(_DisplayRegion, ref RegionFillerHandle, data[2]);
                    DisplayRegion = RegionFillerHandle;
                }
                else
                {
                    DisplayRegion = _DisplayRegion;
                }
           });
        }
        public void Call_Engine()
        {
    
         
            if(!_usEngine)
            {
                CallEngine.Init();
            }
            CallEngine.wait_debug = waitng_debug;
            CallEngine.SetImage(DisplayImage);
            CallEngine.Proce();
            DisplayRegion = CallEngine.Regionout;
        }
        public void On_Sever_Debug()
        {
            CallEngine.onDebug_Sever();
        }
    }




    

    public class MainCommand : ICommand
    {
        private Action<object> _execute;

        private Predicate<object> _canExecute;
        private event EventHandler _canExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                _canExecuteChanged -= value;
            }
        }
        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            if (_canExecuteChanged != null)
                _canExecuteChanged.Invoke(this, EventArgs.Empty);
        }

        public MainCommand(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = ((obj) => true);
        }
        public MainCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            if (canExecute == null)
                throw new ArgumentNullException("canExecute");

            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
