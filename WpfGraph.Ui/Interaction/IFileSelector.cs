namespace Palmmedia.WpfGraph.UI.Interaction
{
    /// <summary>
    /// Interface to enable mocking of file selection dialogs in unit tests.
    /// </summary>
    public interface IFileSelector
    {
        /// <summary>
        /// Gets the file name for opening.
        /// </summary>
        /// <returns>The name of the file to open.</returns>
        string GetFileNameForOpening();

        /// <summary>
        /// Gets the file name for saving.
        /// </summary>
        /// <returns>The name of the file to save.</returns>
        string GetFileNameForSaving();
    }
}
