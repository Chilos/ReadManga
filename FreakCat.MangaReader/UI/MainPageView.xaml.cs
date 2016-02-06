using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FreakCat.MangaReader.UI.Navigate;
using FreakCat.MangaReader.ViewModel;
using GalaSoft.MvvmLight.Ioc;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FreakCat.MangaReader.UI
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPageView : Page
    {
        public MainPageView()
        {
            this.InitializeComponent();
            DataContext = new ViewModelLocator(CurrentFrame).MainPage;
        }
    }
}
