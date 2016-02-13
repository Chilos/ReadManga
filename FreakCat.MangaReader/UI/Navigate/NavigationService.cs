using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FreakCat.MangaReader.UI.Navigate
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;

        public Frame CurrentFrame => _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public Object NavigatedParametr { get; set; }
        public bool CanGoBack => _frame.CanGoBack;

        public void Navigate(Type sourcePageType)
        {
            _frame.Navigate(sourcePageType);
        }
        public void Navigate(Type sourcePageType, object parameter)
        {
            _frame.Navigate(sourcePageType, parameter);
        }

        public void Navigate(Type sourcePageType, Frame frame)
        {
            frame.Navigate(sourcePageType);
        }
        public void GoBack()
        {
            if(_frame.CanGoBack)
                _frame.GoBack();
            
        }
    }
}
