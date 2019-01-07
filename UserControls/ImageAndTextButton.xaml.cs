using System.Windows;
using System.Windows.Controls;

namespace P2PBoardGames.UserControls
{
    /// <summary>
    /// Interaction logic for ImageAndTextButton.xaml
    /// </summary>
    public partial class ImageAndTextButton : UserControl
    {
        public ImageAndTextButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty PathToImageProperty =
            DependencyProperty.Register("PathToImage", typeof(string), typeof(ImageAndTextButton));

        public static readonly DependencyProperty BodyProperty =
            DependencyProperty.Register("Body", typeof(string), typeof(ImageAndTextButton));

        public string PathToImage
        {
            get { return (string)GetValue(PathToImageProperty); }
            set { SetValue(PathToImageProperty, value); }
        }

        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }
    }
}
