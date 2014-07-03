using System.Collections.Specialized;
using System.Windows;

namespace MccTomskHelpers.Wpf.PushBinding
{
    public class PushBindingCollection : FreezableCollection<PushBinding>
    {
        public PushBindingCollection(FrameworkElement targetObject)
        {
            TargetObject = targetObject;
            ((INotifyCollectionChanged)this).CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PushBinding pushBinding in e.NewItems)
                {
                    pushBinding.SetupTargetBinding(TargetObject);
                }
            }
        }

        public FrameworkElement TargetObject
        {
            get;
            private set;
        }
    }
}
