using System;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ProgressUIPrototype
{
    public class LottieProgressUI : ProgressUI
    {
        public LottieProgressUI()
        {
            this.DefaultStyleKey = typeof(LottieProgressUI);
        }

        public IAnimatedVisualSource DeterminateAnimationSource
        {
            get { return (IAnimatedVisualSource)GetValue(DeterminateAnimationSourceProperty); }
            set { SetValue(DeterminateAnimationSourceProperty, value); }
        }

        public static readonly DependencyProperty DeterminateAnimationSourceProperty = DependencyProperty.Register("Source", typeof(IAnimatedVisualSource), typeof(LottieProgressUI), new PropertyMetadata(null, new PropertyChangedCallback(OnDeterminateAnimationSourceChanged)));

        public static void OnDeterminateAnimationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimatedVisualPlayer).Source = e.NewValue as IAnimatedVisualSource;
        }
    }

    //public IAnimatedVisualSource DeterminateAnimation { get; set; }
    //Public IAnimatedVisualSource IndeterminateAnimation { get; set; }
}
