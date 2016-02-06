using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace FreakCat.MangaReader.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private ICommand _catalogClick;
        private ICommand _page1Click;
        private ICommand _hamburgerCommand;
        private ICommand _onLoaded;
        private ICommand _goBack;
        private bool _isOpenPanel;
        private bool _btnCatalogChecked;
        private bool _btnAnotherChecked;
        private string _headerText;
        private Visibility _goBackVisible;


        private Page _frameContent;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                RaisePropertyChanged(() => HeaderText);
            }
        }

        public Visibility GoBackVisible
        {
            get { return _goBackVisible; }
            set
            {
                _goBackVisible = value;
                RaisePropertyChanged(() => GoBackVisible);
            }
        }

        public bool BtnCatalogChecked
        {
            get { return _btnCatalogChecked; }
            set
            {
                _btnCatalogChecked = value;
                RaisePropertyChanged(() => BtnCatalogChecked);
            }
        }

        public bool BtnAnotherChecked
        {
            get { return _btnAnotherChecked; }
            set
            {
                _btnAnotherChecked = value;
                RaisePropertyChanged(() => BtnAnotherChecked);
            }
        }

        public bool IsOpenPanel
        {
            get { return _isOpenPanel; }
            set
            {
                _isOpenPanel = value;
                RaisePropertyChanged(() => IsOpenPanel);
            }
        }

        public Page FrameContent
        {
            get { return _frameContent; }
            set
            {
                _frameContent = value;
                ChangeMenuBattons(_frameContent);
               
                RaisePropertyChanged(() => FrameContent);
            }
        }

        private void ChangeMenuBattons(Page frameContent)
        {
            if (frameContent is Page0)
            {
                HeaderText = "Page 0";
                BtnCatalogChecked = true;
            }

            if (frameContent is Page1)
            {
                HeaderText = "Page 1";
                BtnAnotherChecked = true;
            }
            if (frameContent is Page2)
            {
                HeaderText = "Page 2";
                BtnCatalogChecked = true;
                GoBackVisible = Visibility.Visible;
            }


        }

        private void ChangeGoBackVisible()
        {
            GoBackVisible = _navigationService.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        public ICommand GoBack
        {
            get
            {
                return _goBack ?? (_goBack = new RelayCommand(() =>
                {
                    _navigationService.GoBack();
                    ChangeGoBackVisible();
                }));
            }
        }

        public ICommand CatalogClick
        {
            get
            {
                return _catalogClick ?? (_catalogClick = new RelayCommand(() =>
                {
                    _navigationService.Navigate(typeof (Page0));
                    ChangeGoBackVisible();
                }));
            }
        }

        public ICommand Page1Click
        {
            get
            {
                return _page1Click ?? (_page1Click = new RelayCommand(() =>
                {
                    _navigationService.Navigate(typeof (Page1));
                    ChangeGoBackVisible();
                }));
            }
        }

        public ICommand OnLoaded
        {
            get
            {
                return _onLoaded ?? (_onLoaded = new RelayCommand(() =>
                {
                    BtnCatalogChecked = true;

                }));
            }
        }

        public ICommand HamburgerCommand
        {
            get
            {
                return _hamburgerCommand ?? (_hamburgerCommand = new RelayCommand(() =>
                {
                    IsOpenPanel = !IsOpenPanel;

                }));
            }
        }

    }
}
