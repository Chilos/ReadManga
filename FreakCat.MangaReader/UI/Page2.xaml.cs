using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FreakCat.MangaReader.UI.Navigate;
using FreakCat.MangaReader.ViewModel;


namespace FreakCat.MangaReader.UI
{

    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new Page2ViewModel(new NavigationService(this.Frame) {NavigatedParametr = e.Parameter});
        }
    }
}
