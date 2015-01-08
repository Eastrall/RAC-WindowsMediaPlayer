using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

/*--------------------------------------------------------
 * Converter.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 08/01/2015 21:54:28
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.General
{
    public sealed class PositionToValueConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;

            try
            {
                _value = new TimeSpan(((MediaElement)parameter).Position.Hours, ((MediaElement)parameter).Position.Minutes, ((MediaElement)parameter).Position.Seconds);
            }
            catch (Exception)
            {
                _value = new TimeSpan(((TimeSpan)value).Hours, ((TimeSpan)value).Minutes, ((TimeSpan)value).Seconds);
            }
            return _value.TotalSeconds;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;

            try
            {
                _value = ((MediaElement)parameter).Position;
            }
            catch
            {
                _value = TimeSpan.FromSeconds((double)value);
            }
            return _value;
        }
    }

    public sealed class PositionToTextConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;
            String _time;

            try
            {
                _value = new TimeSpan(((MediaElement)parameter).Position.Hours, ((MediaElement)parameter).Position.Minutes, ((MediaElement)parameter).Position.Seconds);
            }
            catch (Exception)
            {
                _value = new TimeSpan(((TimeSpan)value).Hours, ((TimeSpan)value).Minutes, ((TimeSpan)value).Seconds);
            }
            if (_value.Hours != 0)
            {
                _time = String.Format("{0}:{1}:{2}", _value.Hours.ToString("D2"), _value.Minutes.ToString("D2"), _value.Seconds.ToString("D2"));
            }
            else
            {
                _time = String.Format("{0}:{1}", _value.Minutes.ToString("D2"), _value.Seconds.ToString("D2"));
            }
            return _time;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;

            try
            {
                _value = ((MediaElement)parameter).Position;
            }
            catch
            {
                _value = TimeSpan.FromSeconds((double)value);
            }
            return _value;
        }
    }

    public sealed class FloatToTextConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;
            String _time = "non";

            try
            {
                _value = TimeSpan.FromSeconds((Double)value);
            }
            catch (Exception)
            {
                return "UNDEFINED";
            }
            if (_value.Hours != 0)
            {
                _time = String.Format("{0}:{1}:{2}", _value.Hours.ToString("D2"), _value.Minutes.ToString("D2"), _value.Seconds.ToString("D2"));
            }
            else
            {
                _time = String.Format("{0}:{1}", _value.Minutes.ToString("D2"), _value.Seconds.ToString("D2"));
            }
            return _time;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            TimeSpan _value;

            try
            {
                _value = ((MediaElement)parameter).Position;
            }
            catch
            {
                _value = TimeSpan.FromSeconds((double)value);
            }
            return _value;
        }
    }
}
