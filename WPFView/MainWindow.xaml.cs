using System.Windows;
using System.Windows.Controls;
using WPFModelView;

namespace WPFView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PersonViewModel();
          
        }
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PersonViewModel viewModel && SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortingTag = selectedItem.Tag.ToString();
                viewModel.SortPersons(sortingTag);
            }
        }
  
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PersonViewModel viewModel && FilterComboBox.SelectedItem != null)
            {
                viewModel.ApplyCountryFilter(FilterComboBox.SelectedItem.ToString());
            }
        }
    }
}
