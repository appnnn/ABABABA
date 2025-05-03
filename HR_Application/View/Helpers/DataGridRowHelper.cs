using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HR_Application.View.Helpers
{
    public static class DataGridRowHelper
    {
        public static readonly DependencyProperty RowNumberProperty =
            DependencyProperty.RegisterAttached("RowNumber", typeof(int), typeof(DataGridRowHelper), new PropertyMetadata(0));

        public static int GetRowNumber(DependencyObject obj)
        {
            return (int)obj.GetValue(RowNumberProperty);
        }

        public static void SetRowNumber(DependencyObject obj, int value)
        {
            obj.SetValue(RowNumberProperty, value);
        }
    }
}
