using System;
using System.Windows;

namespace WPFSlider
{
    public class App : Application
    {
        [STAThread]
        static void Main()
        {
            var app = new App();
            var window = new MainWindow();

            app.Run(window);
        }
    }
}
