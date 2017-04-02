using System;
using System.ComponentModel;

namespace Palmmedia.WpfGraph.UI.ViewModels
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {            
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// If <paramref name="disposeManagedResources"/> equals <c>True</c>, all managed resources should be disposed.
        /// Both managed and unmanaged resources should only get disposed if this method is executed for the first time.
        /// </summary>
        /// <param name="disposeManagedResources">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}