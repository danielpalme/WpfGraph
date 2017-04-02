using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Palmmedia.WpfGraph.UI.ViewModels.Menu
{
    /// <summary>
    /// Base class for menu items.
    /// </summary>
    public abstract class MenuItemViewModel : ViewModelBase
    {
        /// <summary>
        /// The child menu items.
        /// </summary>
        private ObservableCollection<MenuItemViewModel> childMenuItems;

        /// <summary>
        /// The header.
        /// </summary>
        private string header;

        /// <summary>
        /// The command.
        /// </summary>
        private ICommand command;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="parentViewModel">The parent view model.</param>
        public MenuItemViewModel(MenuItemViewModel parentViewModel)
        {
            this.ParentViewModel = parentViewModel;
            this.childMenuItems = new ObservableCollection<MenuItemViewModel>();
        }

        /// <summary>
        /// Gets or sets the parent view model.
        /// </summary>
        public MenuItemViewModel ParentViewModel { get; set; }

        /// <summary>
        /// Gets the child menu items.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> ChildMenuItems
        {
            get
            {
                return this.childMenuItems;
            }
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                this.header = value;
                this.OnPropertyChanged("Header");
            }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return this.command;
            }

            set
            {
                this.command = value;
                this.OnPropertyChanged("Command");
            }
        }
    }
}
