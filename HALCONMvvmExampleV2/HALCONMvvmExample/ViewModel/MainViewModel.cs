using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALCONMvvmExample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private HObject _singleImage;
        private ObservableCollection<HDisplayObjectWPF> _multipleObjects;
        // Commands for the buttons in the View
        public RelayCommand LoadSingleImage { get; private set; }
        public RelayCommand LoadImageItem { get; private set; }
        public RelayCommand LoadRegionItem { get; private set; }
        public RelayCommand LoadMessageText { get; private set; }
        public RelayCommand ClearItems { get; private set; }
        public RelayCommand LoadObjects { get; private set; }

        // Property that can hold a single object for display in the View
        public HObject SingleImage
        {
            get { return _singleImage; }
            set => Set("SingleImage", ref _singleImage, value);
        }

        private HImage _imageItem;
        public HImage ImageItem
        {
            get { return _imageItem; }
            set => Set("ImageItem", ref _imageItem, value); 
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set => Set("MessageText", ref _messageText, value);
        }


        private HObject _regionItem;
        public HObject RegionItem
        {
            get { return _regionItem; }
            set => Set("RegionItem", ref _regionItem, value);
        }

        private string _regionColor;
        public string RegionColor
        {
            get { return _regionColor; }
            set => Set("RegionColor", ref _regionColor, value);
        }

        // Collection of Objects that can be displayed.
        // HDisplayObjectWPF is an abstract class, instances can either be
        // HIconicDisplayObjectWPF for Images, Regions or XLDs or 
        // HMessageDisplayObjectWPF for displaying a message in the
        // HSmartWindowControlWPF.
        // The View gets updated when the CollectionChanged Event gets triggered.
        public ObservableCollection<HDisplayObjectWPF> MultipleObjects
        {
            get { return _multipleObjects; }
            set { Set("MultipleObjects", ref _multipleObjects,  value); }
        }

        public MainViewModel()
        {
            MultipleObjects = new ObservableCollection<HDisplayObjectWPF>();
            // Define the functionality behind the commands.
            LoadSingleImage = new RelayCommand(
                // A single image is loaded and assigned to the SingleImage property.
                () => 
                {
                    SingleImage = new HImage("fabrik");
                }
                );
            // Commands for the SmartWindowControlWPF in the middle
            LoadImageItem = new RelayCommand(() => { ImageItem = new HImage("fabrik"); });
            LoadRegionItem = new RelayCommand(() => { RegionItem = new HImage("fabrik").Threshold(100.0, 255.0); RegionColor = "yellow"; } );
            LoadMessageText = new RelayCommand(() => { MessageText = "TextTextText"; } );
            ClearItems = new RelayCommand(() => { ImageItem = null; RegionItem = null; RegionColor = null;  MessageText = null; });

            LoadObjects = new RelayCommand(
                () =>
                {
                    // We first clear the collection of objects
                    MultipleObjects.Clear();
                    HImage img = new HImage("fabrik");
                    // Next we add the image itself
                    MultipleObjects.Add(new HIconicDisplayObjectWPF() { IconicObject = img });
                    // Then the first region without setting any properties. This means that the
                    // region will be displayed with the properties set in the HSmartWindowControlWPF
                    MultipleObjects.Add(new HIconicDisplayObjectWPF() { IconicObject = img.Threshold(128.0, 255.0) });
                    // For the second region we set the properties Draw and Color. These properties
                    // overwrite the corresponding properties of the HSmartWindowControlWPF when
                    // this object is displayed.
                    MultipleObjects.Add(new HIconicDisplayObjectWPF() { IconicObject = img.Threshold(75.0, 255.0), HDraw = "margin", HColor = "blue" });
                });
        }

    }
}
