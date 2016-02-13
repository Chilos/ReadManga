using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FreakCat.MangaReader.UI.Navigate
{
    public interface INavigationService
    {
        Object NavigatedParametr { get; set; }
        Frame CurrentFrame { get;}
        bool CanGoBack { get; }
         void Navigate(Type sourcePageType);
         void Navigate(Type sourcePageType, object parameter);
        void Navigate(Type sourcePageType, Frame frame);
         void GoBack();

    }
}
