using System.Windows.Forms;

namespace Palmmedia.WpfGraph.UI.Interaction
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
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML (*.xml)|*.xml";
                openFileDialog.ShowDialog();

                return openFileDialog.FileName;
            }            
        }

        /// <summary>
        /// Gets the file name for saving.
        /// </summary>
        /// <returns>The name of the file to save.</returns>
        public string GetFileNameForSaving()
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML (*.xml)|*.xml";
                saveFileDialog.ShowDialog();

                return saveFileDialog.FileName;
            }
        }
    }
}
