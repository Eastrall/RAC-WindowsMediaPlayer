using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

/*--------------------------------------------------------
 * DragDropBehavior.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 10/01/2015 20:47:40
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.MVVM
{
    public class DragDropBehaviour
    {
        public static readonly DependencyProperty DragEnterCommandProperty =
            DependencyProperty.RegisterAttached("DragEnterCommand", typeof(ICommand), typeof(DragDropBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(DragEnterCommandChanged)));

        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand", typeof(ICommand), typeof(DragDropBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(DropCommandChanged)));

        private static void DragEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.DragEnter += element_DragEnter;
        }

        private static void DropCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.Drop += element_Drop;
        }

        static void element_DragEnter(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetDragEnterCommand(element);

            command.Execute(e);
        }


        static void element_Drop(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetDropCommand(element);

            command.Execute(e);
        }

        public static void SetDragEnterCommand(UIElement element, ICommand value)
        {
            element.SetValue(DragEnterCommandProperty, value);
        }

        public static ICommand GetDragEnterCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DragEnterCommandProperty);
        }


        public static void SetDropCommand(UIElement element, ICommand value)
        {
            element.SetValue(DropCommandProperty, value);
        }

        public static ICommand GetDropCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DropCommandProperty);
        }
    }
}
