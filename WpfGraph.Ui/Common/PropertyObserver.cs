using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace Palmmedia.WpfGraph.Common
{
    /// <summary>
    /// Monitors the PropertyChanged event of an object that implements INotifyPropertyChanged,
    /// and executes callback methods (i.e. handlers) registered for properties of that object.
    /// </summary>
    /// <typeparam name="TPropertySource">The type of object to monitor for property changes.</typeparam>
    public class PropertyObserver<TPropertySource> : IWeakEventListener
        where TPropertySource : INotifyPropertyChanged
    {
        /// <summary>
        /// The reference to the monitored object.
        /// </summary>
        private readonly WeakReference propertySourceRef;

        /// <summary>
        /// Dictionary containing the handlers by property.
        /// </summary>
        private readonly Dictionary<string, Action<TPropertySource>> propertyNameToHandlerMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserver&lt;TPropertySource&gt;"/> class, which
        /// observes the 'propertySource' object for property changes.
        /// </summary>
        /// <param name="propertySource">The object to monitor for property changes.</param>
        public PropertyObserver(TPropertySource propertySource)
        {
            if (propertySource == null)
            {
                throw new ArgumentNullException("propertySource");
            }

            this.propertySourceRef = new WeakReference(propertySource);
            this.propertyNameToHandlerMap = new Dictionary<string, Action<TPropertySource>>();
        }

        /// <summary>
        /// Registers a callback to be invoked when the PropertyChanged event has been raised for the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <param name="handler">The callback to invoke when the property has changed.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> RegisterHandler(
            Expression<Func<TPropertySource, object>> expression,
            Action<TPropertySource> handler)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            string propertyName = GetPropertyName(expression);

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("'expression' did not provide a property name.");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            TPropertySource propertySource = this.GetPropertySource();
            if (propertySource != null)
            {
                this.propertyNameToHandlerMap[propertyName] = handler;
                PropertyChangedEventManager.AddListener(propertySource, this, propertyName);
            }

            return this;
        }

        /// <summary>
        /// Removes the callback associated with the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> UnregisterHandler(Expression<Func<TPropertySource, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            string propertyName = GetPropertyName(expression);

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("'expression' did not provide a property name.");
            }

             TPropertySource propertySource = this.GetPropertySource();
             if (propertySource != null)
             {
                 if (this.propertyNameToHandlerMap.ContainsKey(propertyName))
                 {
                     this.propertyNameToHandlerMap.Remove(propertyName);
                     PropertyChangedEventManager.RemoveListener(propertySource, this, propertyName);
                 }
             }

            return this;
        }

        /// <summary>
        /// Receives events from the centralized event manager.
        /// </summary>
        /// <param name="managerType">The type of the <see cref="T:System.Windows.WeakEventManager"/> calling this method.</param>
        /// <param name="sender">Object that originated the event.</param>
        /// <param name="e">Event data.</param>
        /// <returns>
        /// true if the listener handled the event. It is considered an error by the <see cref="T:System.Windows.WeakEventManager"/> handling in WPF to register a listener for an event that the listener does not handle. Regardless, the method should return false if it receives an event that it does not recognize or handle.
        /// </returns>
        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(PropertyChangedEventManager))
            {
                string propertyName = ((PropertyChangedEventArgs)e).PropertyName;
                TPropertySource propertySource = (TPropertySource)sender;

                if (string.IsNullOrEmpty(propertyName))
                {
                    // When the property name is empty, all properties are considered to be invalidated.
                    // Iterate over a copy of the list of handlers, in case a handler is registered by a callback.
                    foreach (var handler in this.propertyNameToHandlerMap.Values.ToArray())
                    {
                        handler(propertySource);
                    }

                    return true;
                }
                else
                {
                    Action<TPropertySource> handler;
                    if (this.propertyNameToHandlerMap.TryGetValue(propertyName, out handler))
                    {
                        handler(propertySource);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <returns>The name of the property if property exists, otherwise <c>null</c>.</returns>
        private static string GetPropertyName(Expression<Func<TPropertySource, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");

            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;

                return propertyInfo.Name;
            }

            return null;
        }

        /// <summary>
        /// Gets the monitored object.
        /// </summary>
        /// <returns>The monitored object.</returns>
        private TPropertySource GetPropertySource()
        {
            try
            {
                return (TPropertySource)this.propertySourceRef.Target;
            }
            catch 
            {
                return default(TPropertySource);
            }
        }
    }
}