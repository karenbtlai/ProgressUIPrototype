using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace ProgressUIPrototype
{
    public class ProgressUI : RangeBase
    {
        public ProgressUI()
        {
            this.DefaultStyleKey = typeof(ProgressUI);
        }

        public ProgressUITemplateSettings templateSettings { get; } = new ProgressUITemplateSettings();

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            var value = Value / (Maximum - Minimum);
            Debug.Write("Setting Value: " + value);
            templateSettings.ProgressPosition = value;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            RegisterAndUpdateColors();
        }

        private void RegisterAndUpdateColors()
        {
            if (Foreground is SolidColorBrush)
            {
                var foreground = (Foreground as SolidColorBrush);
                templateSettings.ForegroundColor = foreground.Color;
                RegisterPropertyChangedCallback(ForegroundProperty, new Windows.UI.Xaml.DependencyPropertyChangedCallback(OnForegroundChanged));

                foreground.RegisterPropertyChangedCallback(SolidColorBrush.ColorProperty, new Windows.UI.Xaml.DependencyPropertyChangedCallback(OnForegroundChanged));
            }

            if (Background is SolidColorBrush)
            {
                var background = (Background as SolidColorBrush);
                templateSettings.BackgroundColor = background.Color;
                RegisterPropertyChangedCallback(BackgroundProperty,
                   new Windows.UI.Xaml.DependencyPropertyChangedCallback(OnBackgroundChanged));

                background.RegisterPropertyChangedCallback(SolidColorBrush.ColorProperty,
                    new Windows.UI.Xaml.DependencyPropertyChangedCallback(OnBackgroundChanged));
            }
        }

        private void OnForegroundChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (Foreground is SolidColorBrush)
            {
                templateSettings.ForegroundColor = (Foreground as SolidColorBrush).Color;
            }
        }

        private void OnBackgroundChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (Background is SolidColorBrush)
            {
                templateSettings.BackgroundColor = (Background as SolidColorBrush).Color;
            }
        }

        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(ProgressUI), new PropertyMetadata(false, new PropertyChangedCallback(OnIsIndeterminateChanged)));

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((ProgressUI)d, (bool)e.NewValue ? "Indeterminate" : "Normal", true);
        }

        public bool ShowPaused
        {
            get { return (bool)GetValue(ShowPausedProperty); }
            set { SetValue(ShowPausedProperty, value); }
        }

        public static readonly DependencyProperty ShowPausedProperty =
            DependencyProperty.Register("ShowPaused", typeof(bool), typeof(ProgressUI), new PropertyMetadata(false, new PropertyChangedCallback(OnShowPausedChanged)));

        private static void OnShowPausedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((ProgressUI)d, (bool)e.NewValue ? "ShowPaused" : "Normal", true);
        }

        public bool ShowError
        {
            get { return (bool)GetValue(ShowErrorProperty); }
            set { SetValue(ShowErrorProperty, value); }
        }

        public static readonly DependencyProperty ShowErrorProperty =
            DependencyProperty.Register("ShowPaused", typeof(bool), typeof(ProgressUI), new PropertyMetadata(false, new PropertyChangedCallback(OnShowErrorChanged)));

        private static void OnShowErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualStateManager.GoToState((ProgressUI)d, (bool)e.NewValue ? "ShowError" : "Normal", true);
        }

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public static readonly DependencyProperty StrokeWidthProperty = DependencyProperty.Register("StrokeWidth", typeof(float), typeof(ProgressUI), new PropertyMetadata((float)1.0));
    }
}
