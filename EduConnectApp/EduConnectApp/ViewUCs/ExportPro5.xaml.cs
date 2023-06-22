using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EduConnectApp.ViewUCs
{
    /// <summary>
    /// Interaction logic for ExportPro5.xaml
    /// </summary>
    public partial class ExportPro5 : UserControl
    {
        public ExportPro5()
        {
            InitializeComponent();
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            scroll.ScrollToVerticalOffset(0);
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "Delivery Invoice");
                MessageBox.Show("Xuất thành công!");
            }
        }

        private void dtg_Scoretable_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            DataGrid dtg = sender as DataGrid;
            ScrollViewer scv = FindParentScrollViewer(dtg);
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        public static ScrollViewer FindParentScrollViewer(DependencyObject child)
        {
            // Check if the child object is null
            if (child == null)
                return null;

            // Check if the current object is a ScrollViewer
            if (child is ScrollViewer scrollViewer)
                return scrollViewer;

            // Get the parent of the child object
            var parent = VisualTreeHelper.GetParent(child);

            // Recursive call to find the ScrollViewer
            return FindParentScrollViewer(parent);
        }
    }
}
