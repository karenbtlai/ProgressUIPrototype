using System;
using System.Diagnostics;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace ProgressUIPrototype
{
    public class AnimatedProgressUI : Panel
    {
        private static AnimatedVisualPlayerProposed m_player;

        public AnimatedProgressUI()
        {
            m_player = new AnimatedVisualPlayerProposed();
            Children.Add(m_player);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement child in Children)
            {
                child.Measure(availableSize);
            }
            return m_player.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement child in Children)
            {
                child.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(AnimatedProgressUI), new PropertyMetadata(true, new PropertyChangedCallback(OnIsActiveChanged)));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;
            animatedProgressUI.UpdateStates();

        }

        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(AnimatedProgressUI), new PropertyMetadata(true, new PropertyChangedCallback(OnIsIndeterminateChanged)));

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;
            animatedProgressUI.UpdateStates();
        }

        public bool ShowPaused
        {
            get { return (bool)GetValue(ShowPausedProperty); }
            set { SetValue(ShowPausedProperty, value); }
        }

        public static readonly DependencyProperty ShowPausedProperty =
            DependencyProperty.Register("ShowPaused", typeof(bool), typeof(AnimatedProgressUI), new PropertyMetadata(false, new PropertyChangedCallback(OnShowPausedChanged)));

        private static void OnShowPausedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;
            animatedProgressUI.UpdateStates();
        }

        public bool ShowError
        {
            get { return (bool)GetValue(ShowErrorProperty); }
            set { SetValue(ShowErrorProperty, value); }
        }

        public static readonly DependencyProperty ShowErrorProperty =
            DependencyProperty.Register("ShowError", typeof(bool), typeof(AnimatedProgressUI), new PropertyMetadata(false, new PropertyChangedCallback(OnShowErrorChanged)));

        private static void OnShowErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;
            animatedProgressUI.UpdateStates();
        }

        public IAnimatedVisualSource AnimationSource
        {
            get { return (IAnimatedVisualSource)GetValue(AnimationSourceProperty); }
            set { SetValue(AnimationSourceProperty, value); }
        }

        public static readonly DependencyProperty AnimationSourceProperty =
            DependencyProperty.Register("AnimationSource", typeof(IAnimatedVisualSource), typeof(AnimatedProgressUI), new PropertyMetadata(true, new PropertyChangedCallback(OnAnimationSourceChanged)));

        private static void OnAnimationSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;

            animatedProgressUI.UpdateSource(e);
            animatedProgressUI.UpdateStates();
        }

        public double ProgressPosition
        {
            get { return (double)GetValue(ProgressPositionProperty); }
            set { SetValue(ProgressPositionProperty, value); }
        }

        public static readonly DependencyProperty ProgressPositionProperty =
            DependencyProperty.Register("ProgressPosition", typeof(double), typeof(AnimatedProgressUI), new PropertyMetadata(0, new PropertyChangedCallback(OnProgressPositionChanged)));

        private static void OnProgressPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animatedProgressUI = d as AnimatedProgressUI;
            if (!animatedProgressUI.IsIndeterminate)
            {
                animatedProgressUI.UpdateProgressPosition(animatedProgressUI.ProgressPosition);
            }
        }

        private void UpdateStates()
        {
            foreach (AnimatedVisualPlayerProposed child in Children)
            {
                if (this.IsActive)
                {
                    child.Opacity = 1;

                    if (this.ShowError)
                    {
                        child.Stop();
                    }
                    else if (this.ShowPaused)
                    {
                        child.Pause();
                    }
                    else if (this.IsIndeterminate)
                    {
                        _ = child.PlayAsync(0, 1, true);
                    }
                    else if (!this.IsIndeterminate)
                    {
                        child.SetProgress(ProgressPosition);
                    }
                }
                else
                {
                    child.Stop();
                    child.Opacity = 0;
                }
            }
        }

        private void UpdateSource(DependencyPropertyChangedEventArgs e)
        {
            foreach (AnimatedVisualPlayerProposed child in Children)
            {
                child.Source = e.NewValue as IAnimatedVisualSource;
            }
        }

        private void UpdateProgressPosition(double progressPosition)
        {
            foreach (AnimatedVisualPlayerProposed child in Children)
            {
                child.SetProgress(progressPosition);
            }
        }
    }
}
