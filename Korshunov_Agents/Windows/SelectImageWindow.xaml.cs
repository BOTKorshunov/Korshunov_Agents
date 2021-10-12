using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Korshunov_Agents.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectImageWindow.xaml
    /// </summary>
    public partial class SelectImageWindow : Window
    {
        private ImageSources _mainImageSources;
        public ImageSources mainImageSources
        { 
            get { return _mainImageSources; }
        }
        private List<ImageSources> imageSourcesList = new List<ImageSources>();
        private Border selectBrdImage = null;

        public SelectImageWindow(ImageSources mainImageSources)
        {
            InitializeComponent();

            _mainImageSources = mainImageSources;
            CreateImages();
        }
        public string[] FindImageFiles()
        {
            string[] files = Directory.GetFiles(@"..\..\Agents");

            List<string> imageFiles = new List<string>();
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".png")
                {
                    imageFiles.Add(file);
                }
            }

            files = imageFiles.ToArray();
            Array.Sort(files);
            return files;
        }
        public Border CreateBorder()
        {
            Border brdImage = new Border();
            brdImage.BorderBrush = Brushes.Black;
            brdImage.BorderThickness = new Thickness(5);
            brdImage.CornerRadius = new CornerRadius(5);
            brdImage.Width = 100;
            brdImage.Height = 100;
            brdImage.Margin = new Thickness(10, 0, 10, 0);
            return brdImage;
        }
        public void CreateImages()
        {
            spImages.Children.Clear();
            string[] imageFiles = FindImageFiles();

            int countImagesInStackPanel = 3;
            for (int i = 0; i < imageFiles.Count(); i += countImagesInStackPanel)
            {
                StackPanel spThreeImages = new StackPanel();
                spThreeImages.Orientation = Orientation.Horizontal;
                spThreeImages.HorizontalAlignment = HorizontalAlignment.Center;
                spThreeImages.Margin = new Thickness(0, 10, 0, 10);

                for (int j = 0; j < countImagesInStackPanel; j++)
                {
                    int index = i + j;

                    if (index == imageFiles.Count())
                    {
                        break;
                    }

                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(imageFiles[index], UriKind.Relative));
                    imageSourcesList.Add(new ImageSources(image, imageFiles[index]));

                    Border brdImage = CreateBorder();
                    brdImage.Child = image;
                    brdImage.MouseLeftButtonDown += BrdImage_MouseLeftButtonDown;

                    if (imageFiles[index] == mainImageSources.correctSource)
                    {
                        SelectImage(brdImage);
                    }

                    spThreeImages.Children.Add(brdImage);
                }

                spImages.Children.Add(spThreeImages);
            }

            Label label = new Label();
            label.Content = "+";
            label.FontSize = 25;
            label.FontWeight = FontWeights.Bold;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;

            Border brdNewImage = CreateBorder();
            brdNewImage.Child = label;
            brdNewImage.MouseLeftButtonDown += BrdNewImage_MouseLeftButtonDown;
            spImages.Children.Add(brdNewImage);
        }

        private void BrdNewImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png)|*.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                string[] files = FindImageFiles();
                fileInfo.CopyTo(@"..\..\Agents\agent_" + files.Count() + fileInfo.Extension);
                CreateImages();
            }
        }

        public void SelectImage(Border brdImage)
        {
            if (selectBrdImage != null)
            {
                selectBrdImage.BorderBrush = Brushes.Black;
            }
            selectBrdImage = brdImage;
            selectBrdImage.BorderBrush = Brushes.Red;

            Image image = (Image)selectBrdImage.Child;
            foreach (var imageSources in imageSourcesList)
            {
                if (imageSources.image == image)
                {
                    _mainImageSources = imageSources;
                    break;
                }
            }
        }

        private void BrdImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectImage((Border)sender);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }

    public class ImageSources
    {
        public Image image;
        public string correctSource;

        public ImageSources(Image image, string correctSource)
        {
            this.image = image;
            this.correctSource = correctSource;
        }
    }
}
