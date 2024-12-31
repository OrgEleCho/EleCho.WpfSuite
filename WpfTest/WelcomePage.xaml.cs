using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfTest
{
    /// <summary>
    /// WelcomePage.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class WelcomePage : Page
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentImageSource))]
        private int _currentImageSourceIndex;

        [ObservableProperty]
        private bool _transitionReverse;

        [ObservableProperty]
        private bool _popupOpen;

        public ObservableCollection<ImageSource> ImageSources { get; } = new()
        {
            new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/1.jpg")),
            new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/2.jpg")),
            new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/3.jpg")),
            new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/4.jpg")),
            new BitmapImage(new Uri("pack://application:,,,/WpfTest;component/Assets/Banners/5.jpg")),
        };

        public ImageSource? CurrentImageSource => ImageSources[CurrentImageSourceIndex];

        public WelcomePage()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void OnLayoutError(object sender, LayoutExceptionEventArgs e)
        {
            e.ThrowException = false;
        }

        [RelayCommand]
        public async void TogglePopup()
        {
            await Task.Yield();

            PopupOpen ^= true;
        }

        [RelayCommand]
        public void GoPrev()
        {
            TransitionReverse = true;
            var prevIndex = CurrentImageSourceIndex;
            if (prevIndex == 0)
                prevIndex = ImageSources.Count;

            prevIndex--;

            CurrentImageSourceIndex = prevIndex;
        }

        [RelayCommand]
        public void GoNext()
        {
            TransitionReverse = false;
            CurrentImageSourceIndex = (CurrentImageSourceIndex + 1) % ImageSources.Count;
        }
    }
}
