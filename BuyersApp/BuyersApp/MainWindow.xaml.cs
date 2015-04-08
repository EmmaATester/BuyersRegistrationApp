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

namespace BuyersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Page> m_Pages = new List<Page> { new FirstPage(), new ProductDetails() };
        private int m_CurrentPage = 0;

        public MainWindow()
        {
            InitializeComponent();
            BackButton.IsEnabled = false;
            PageFrame.Navigate(m_Pages[m_CurrentPage]);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_CurrentPage + 1 == m_Pages.Count)
            {
                // Run thing
                Close();
            }
            else
            {
                m_CurrentPage++;
                PageFrame.Navigate(m_Pages[m_CurrentPage]);
                BackButton.IsEnabled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentPage--;
            if (m_CurrentPage == 0)
            {
                BackButton.IsEnabled = false;
            }
            PageFrame.Navigate(m_Pages[m_CurrentPage]);

        }
    }
}
