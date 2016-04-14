using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight.Ioc;

namespace FreakCat.MangaReader.ViewModel
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPage => SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public Page3ViewModel Page3 => SimpleIoc.Default.GetInstance<Page3ViewModel>();
        public Page2ViewModel Page2 => SimpleIoc.Default.GetInstance<Page2ViewModel>();
        public Page1ViewModel Page1 => SimpleIoc.Default.GetInstance<Page1ViewModel>();
        public Page0ViewModel Page0 => SimpleIoc.Default.GetInstance<Page0ViewModel>();

        public ViewModelLocator()
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Unregister<INavigationService>();
            var navigationService = new NavigationService((Frame)Window.Current.Content);
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<Page3ViewModel>();
            SimpleIoc.Default.Register<Page2ViewModel>();
            SimpleIoc.Default.Register<Page1ViewModel>();
            SimpleIoc.Default.Register<Page0ViewModel>();
        }

        public ViewModelLocator(Frame frame)
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Unregister<INavigationService>();
            var navigationService = new NavigationService(frame);
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<Page3ViewModel>();
            SimpleIoc.Default.Register<Page2ViewModel>();
            SimpleIoc.Default.Register<Page1ViewModel>();
            SimpleIoc.Default.Register<Page0ViewModel>();
        }

    }
}
