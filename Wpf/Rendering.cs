using System;
using System.Windows.Threading;

namespace MccTomskHelpers.Wpf
{
    public class Rendering
    {
         public static void WaitTillRenderingIsComplete()
         {
             //It is a hack to wait till WPF rendering is complete: http://www.jonathanantoine.com/2011/08/29/update-my-ui-now-how-to-wait-for-the-rendering-to-finish/
             Dispatcher.CurrentDispatcher.Invoke(new Action(() => { }), DispatcherPriority.ApplicationIdle, null);
         }
    }
}