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
        private AnimatedVisualPlayerProposed m_player;

        public AnimatedProgressUI()
        {
            m_player = new AnimatedVisualPlayerProposed();
            m_player.Source = new DeterminateRing();
            Children.Add(m_player);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            m_player.Measure(availableSize);

            return m_player.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            m_player.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));

            return finalSize;
        }



        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(AnimatedProgressUI), new PropertyMetadata(true, new PropertyChangedCallback(OnIsActiveChanged)));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
