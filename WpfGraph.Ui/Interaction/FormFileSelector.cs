using Microsoft.Win32;

namespace Palmmedia.WpfGraph.Ui.Interaction
{
    /// <summary>
    /// <see cref="IFileSelector"/> implementation using dialogs.
    /// </summary>
    public class FormFileSelector : IFileSelector
    {
        /// <summary>
        /// Gets the file name for opening.
        /// </summary>
        /// <returns>The name of the file to open.</returns>
        public string GetFileNameForOpening()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "XML (*.xml)|*.xml"
            };
            openFileDialog.ShowDialog();

            return openFileDialog.FileName;
        }

        /// <summary>
        /// Gets the file name for saving.
        /// </summary>
        /// <returns>The name of the file to save.</returns>
        public string GetFileNameForSaving()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "XML (*.xml)|*.xml"
            };
            saveFileDialog.ShowDialog();

            return saveFileDialog.FileName;
        }
    }
}
