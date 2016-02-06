using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreakCat.MangaReader.UI.Navigate
{
    public interface INavigationService
    {
        Object NavigatedParametr { get; set; }
        bool CanGoBack { get; }
         void Navigate(Type sourcePageType);
         void Navigate(Type sourcePageType, object parameter);
         void GoBack();

    }
}
