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

namespace NLog.Monitor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            MoveToLocation();
        }

        private void MoveToLocation()
        {
            var scrnw = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            var scrnh = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            scrnw -= ActualWidth;
            scrnh -= ActualHeight;
            Left = scrnw - 40;
            Top = scrnh;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainWindow_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            dynamic dc = this.DataContext;
            if (e.Delta < 0)
            {

                dc.NextCommand.Execute(null);
            }
            else
            {
                dc.PrevCommand.Execute(null);
            }
        }
    }
}
