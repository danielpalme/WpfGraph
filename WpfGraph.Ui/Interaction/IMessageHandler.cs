namespace Palmmedia.WpfGraph.UI.Interaction
{
    /// <summary>
    /// Interface to enable mocking of message boxes in unit tests.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Shows the given message in a message box.
        /// </summary>
        /// <param name="message">The message to show.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the given error in a message box.
        /// </summary>
        /// <param name="error">The error to show.</param>
        void ShowError(string error);
    }
}
