using System;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ProgressUIPrototype
{
    public sealed class LottieProgressUI : ProgressUI
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

        public static readonly DependencyProperty DeterminateAnimationSourceProperty = DependencyProperty.Register("DeterminateAnimationSource", typeof(IAnimatedVisualSource), typeof(ProgressUI), new PropertyMetadata(null, new PropertyChangedCallback(OnDeterminateAnimationSourceChanged)));

        public static void OnDeterminateAnimationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //AnimatedVisualPlayer player = new AnimatedVisualPlayer();
            
            //player.Source = e.NewValue as IAnimatedVisualSource;
            //_ = player.PlayAsync(0, 1, true);
        }

        public IAnimatedVisualSource IndeterminateAnimationSource
        {
            get { return (IAnimatedVisualSource)GetValue(IndeterminateAnimationSourceProperty); }
            set { SetValue(IndeterminateAnimationSourceProperty, value); }
        }

        public static readonly DependencyProperty IndeterminateAnimationSourceProperty = DependencyProperty.Register("IndeterminateAnimationSource", typeof(IAnimatedVisualSource), typeof(LottieProgressUI), new PropertyMetadata(null, new PropertyChangedCallback(OnIndeterminateAnimationSourceChanged)));

        public static void OnIndeterminateAnimationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //AnimatedVisualPlayer player = new AnimatedVisualPlayer();
            
            //player.Source = e.NewValue as IAnimatedVisualSource;
            //_ = player.PlayAsync(0, 1, true);
        }
    }
}
