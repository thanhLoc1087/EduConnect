using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for GradeUC.xaml
    /// </summary>
    public partial class GradeUC : UserControl
    {
        public GradeUC()
        {
            InitializeComponent();
        }
        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Grid grid = sender as Grid;
            ScrollViewer scv = grid.Parent as ScrollViewer;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

    }
}
