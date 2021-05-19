using PoEValuablesHelper.Poe;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PoEValuablesHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> ValuableItemsList { get; set; } = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SaveConfig(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
        }

        private async void CheckItems(object sender, RoutedEventArgs e)
        {
            var server = new PoeServer(
                Settings.Default.AccountName,
                Settings.Default.SessionId,
                Settings.Default.League);

            var tabs = await server.GetTabs();
            var requiredIndexes = tabs
                .Where(c => Settings.Default.TabNames.Contains(c.Name))
                .Select(c => c.Index)
                .ToList();
            var items = await server.GetItemsFromTabs(requiredIndexes);
            ValuableItemsList.Clear();
            foreach (var item in items.SelectMany(i => i.Value))
            {
                string label = (string.IsNullOrWhiteSpace(item.Name) ? "" : item.Name + " ") + item.FullTypeName;
                ValuableItemsList.Add(label);
            }
        }

        private void CopyItemNameToClipboard(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var result = (string)ValuableItems.SelectedItem;
            Clipboard.SetText(result);
        }
    }
}
