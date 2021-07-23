using Caliburn.Micro;
using HalconWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HalconWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// use shortcut ctor for cretate public Bootstrapper
        /// </summary>
        public Bootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}
