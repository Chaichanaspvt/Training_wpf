using GalaSoft.MvvmLight.Ioc;

namespace HALCONMvvmExample.ServiceLocator
{
    public class ServiceLocator
    {
        public ServiceLocator()
        {
            SimpleIoc.Default.Register<ViewModel.MainViewModel>();
        }

        public ViewModel.MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ViewModel.MainViewModel>();
            }
        }
    }
}
