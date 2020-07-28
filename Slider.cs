using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WPFSlider
{
    class CustomSlider : UserControl
    {
        Grid MainGrid = new Grid();
        Grid TrackGrid = new Grid();

        Rectangle TrackBeforeThumb = new Rectangle()
        {
            Height = 4, RadiusX = 2, RadiusY = 2,
            Fill = new SolidColorBrush(Color.FromRgb(6, 176, 37)),
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        Rectangle TrackAfterThumb = new Rectangle()
        {
            Height = 4, RadiusX = 2, RadiusY = 2,
            Fill = new SolidColorBrush(Color.FromRgb(196, 196, 196)),
            HorizontalAlignment = HorizontalAlignment.Stretch
        };

        Ellipse Thumb = new Ellipse()
        {
            Width = 15, Height = 15,
            Fill = Brushes.White,
            Stroke = new SolidColorBrush(Color.FromRgb(6, 176, 37)),
            StrokeThickness = 3
        };

        double value;
        public double Value
        {
            get { return this.value; }
            set
            {
                this.value = Math.Max(0, Math.Min(1, value));

                MainGrid.ColumnDefinitions[0].Width =
                TrackGrid.ColumnDefinitions[0].Width =
                new GridLength(this.value, GridUnitType.Star);

                MainGrid.ColumnDefinitions[2].Width =
                TrackGrid.ColumnDefinitions[1].Width =
                new GridLength(1 - this.value, GridUnitType.Star);
            }
        }

        public CustomSlider()
        {
            InitializeGrids();

            MainGrid.Children.Add(TrackGrid);
            Grid.SetColumnSpan(TrackGrid, 3);
            MainGrid.Children.Add(Thumb);
            Grid.SetColumn(Thumb, 1);

            TrackGrid.Children.Add(TrackBeforeThumb);
            Grid.SetColumn(TrackBeforeThumb, 0);

            TrackGrid.Children.Add(TrackAfterThumb);
            Grid.SetColumn(TrackAfterThumb, 1);

            Content = MainGrid;

            InitializeMouseEvents();
        }

        void InitializeGrids()
        {
            // MainGrid setting 

            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = GridLength.Auto
            });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // TrackGrid setting 

            TrackGrid.ColumnDefinitions.Add(new ColumnDefinition());
            TrackGrid.ColumnDefinitions.Add(new ColumnDefinition());

            TrackGrid.Margin = new Thickness(Thumb.Width / 2 - 2);
        }

        void InitializeMouseEvents()
        {
            MouseLeftButtonDown += (object sender, MouseButtonEventArgs args) =>
            {
                Value = (args.GetPosition(this).X - Thumb.Width / 2) / (ActualWidth - Thumb.Width);
                args.MouseDevice.Capture(this);
            };

            MouseMove += (object sender, MouseEventArgs args) =>
            {
                if (IsMouseCaptured)
                {
                    Value = (args.GetPosition(this).X - Thumb.Width / 2) / (ActualWidth - Thumb.Width);
                }
            };

            MouseLeftButtonUp += (object sender, MouseButtonEventArgs args) =>
            {
                args.MouseDevice.Capture(null);
            };
        }
    }
}
