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

namespace Main_Ui
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(10, 10, 202, 18);
        Thickness RightSide = new Thickness(202, 10, 10, 18);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        private bool Toggled = false;
        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggle1 = false;
            Dot.Margin = LeftSide;

        }

        public bool Toggle1 { get => Toggled; set => Toggled = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!Toggled)
            {
                Back.Fill = On;
                Toggle1 = true;
                Dot.Margin = RightSide;
            }else
            {
                Back.Fill = Off;
                Toggle1 = false;
                Dot.Margin = LeftSide;
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggle1 = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Back.Fill = Off;
                Toggle1 = false;
                Dot.Margin = LeftSide;
            }
        }
    }
}
