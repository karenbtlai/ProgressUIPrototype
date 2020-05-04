using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ProgressUIPrototype
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void indeterminateCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (IsIndeterminateToggleProgressUI.IsIndeterminate)
            {
                IsIndeterminateToggleProgressUI.AnimationSource = new IndeterminateRing();
            }
            else
            {
                IsIndeterminateToggleProgressUI.AnimationSource = new DeterminateRing();
            }
        }

        private void HandleCustomStateToggle(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Name == "ShowSuccess")
            {
                CustomStateToggleProgressUI.IsLooping = false;
                CustomStateToggleProgressUI.AnimationSource = new LoadingSuccessState();
            }
            else if (rb.Name == "ShowError")
            {
                CustomStateToggleProgressUI.IsLooping = false;
                CustomStateToggleProgressUI.AnimationSource = new LoadingErrorState();
            }
            else
            {
                CustomStateToggleProgressUI.IsLooping = true;
                CustomStateToggleProgressUI.AnimationSource = new LoadingGenericState();
            }
        }
    }

    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, String language)
        {
            var colorBrush = value as SolidColorBrush;
            return colorBrush.Color;
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
