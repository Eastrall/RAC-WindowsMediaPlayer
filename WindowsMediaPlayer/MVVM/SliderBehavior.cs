using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

/*--------------------------------------------------------
 * SliderBehavior.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 08/01/2015 21:44:22
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.MVVM
{
    public class SliderBehavior : Behavior<MediaElement>
    {

        //une DP pour la position
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(TimeSpan), typeof(SliderBehavior),
                                        new PropertyMetadata((d, e) =>
                                                             ((SliderBehavior)d).PropertyChanged((TimeSpan)e.OldValue, (TimeSpan)e.NewValue)));
        // une autre pour le maximum
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(SliderBehavior), new PropertyMetadata(default(double)));

        // les proprietes qui vont avec

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set
            {
                SetValue(PositionProperty, value);
            }
        }

        private readonly Timer _timer = new Timer(1000);

        public void PropertyChanged(TimeSpan oldValue, TimeSpan newValue)
        {
            AssociatedObject.Position = newValue;
        }

        //Quand le behavior est utilise, on initialise tout.

        protected override void OnAttached()
        {
            base.OnAttached();
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
            Position = AssociatedObject.Position;
            AssociatedObject.MediaOpened += MediaOpened;
            _timer.Start();
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            Maximum = AssociatedObject.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void DispatcherTimerResync()
        {
            Position = AssociatedObject.Position;
        }

        //on utilise un timer pour changer la position toute les secondes.
        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(DispatcherTimerResync));
        }

        protected override void OnDetaching()
        {
            _timer.Stop();
            _timer.Elapsed -= _timer_Elapsed;
            base.OnDetaching();
        }
    }
}
