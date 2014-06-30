using System;
using System.Windows;
using System.Windows.Media;

namespace WpfInfrastructure.Extensions
{
    public static class ExVisualTreeHelper
    {
        public static T FindParent<T>(DependencyObject childDependencyObject) where T: DependencyObject
        {
            var parentType = typeof (T);

            var parentDependencyObject = childDependencyObject;
            while ((parentDependencyObject = VisualTreeHelper.GetParent(parentDependencyObject)) != null)
            {
                if (parentDependencyObject.GetType() == parentType)
                    return (T)parentDependencyObject;
            }
            return null;
        }
    }
}