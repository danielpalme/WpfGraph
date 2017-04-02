using System.Windows.Forms;

namespace Palmmedia.WpfGraph.UI.Interaction
{
    /// <summary>
    /// <see cref="IMessageHandler"/> implementation using message boxes.
    /// </summary>
    public class FormMessageHandler : IMessageHandler
    {
        /// <summary>
        /// Shows the given message in a message box.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Shows the given error in a message box.
        /// </summary>
        /// <param name="error">The error to show.</param>
        public void ShowError(string error)
        {
            MessageBox.Show(error, Properties.Resources.Error);
        }
    }
}
