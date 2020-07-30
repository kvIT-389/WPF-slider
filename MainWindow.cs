using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace WPFSlider
{
    class MainWindow : Window
    {
        Grid MainGrid = new Grid();

        CustomSlider Slider = new CustomSlider()
        {
            Margin = new Thickness(7),
            Cursor = Cursors.Hand,
            Value = 0.37
        };

        Label PercentageLabel = new Label()
        {
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 16
        };

        public MainWindow()
        {
            Width = 450;
            Height = 125;
            Title = "Custom slider";

            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new GridLength(60)
            });

            MainGrid.Children.Add(Slider);

            MainGrid.Children.Add(PercentageLabel);
            Grid.SetColumn(PercentageLabel, 1);

            Content = MainGrid;

            Slider.MouseLeftButtonDown += (object sender, MouseButtonEventArgs args) => 
                { UpdateLabelContent(); };

            Slider.MouseMove += (object sender, MouseEventArgs args) => 
                { UpdateLabelContent(); };

            UpdateLabelContent();
        }

        void UpdateLabelContent()
        {
            PercentageLabel.Content = $"{Slider.Value:p1}";
        }
    }
}
