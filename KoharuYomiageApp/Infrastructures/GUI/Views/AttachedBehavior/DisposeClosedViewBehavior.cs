using System;
using System.Collections.Specialized;
using Prism.Common;
using Prism.Regions;

namespace KoharuYomiageApp.Infrastructures.GUI.Views.AttachedBehavior
{
    public class DisposeClosedViewsBehavior : RegionBehavior
    {
        protected override void OnAttach()
        {
            Region.Views.CollectionChanged += Views_CollectionChanged;
        }

        static void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action is not (NotifyCollectionChangedAction.Remove or NotifyCollectionChangedAction.Replace))
            {
                return;
            }

            foreach (var removedView in e.OldItems)
            {
                MvvmHelpers.ViewAndViewModelAction<IDisposable>(removedView, d => d.Dispose());
            }
        }
    }
}
