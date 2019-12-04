using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MotoGPWeb1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MotoGPRSS mellett = new MotoGPRSS();
        public MainWindow()
        {
            InitializeComponent();
            mellett.Show();

        }

        private void Exit_page(object sender, RoutedEventArgs e)
        {
            mellett.Close();
            this.Close();
        }

        private void Add_data(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Creating listaablak = new Creating();
            listaablak.Show();
        }

        private void List_data(object sender, RoutedEventArgs e)
        {
            this.Close();
            ListingWindow listazas = new ListingWindow();
            listazas.Show();
            //this.Close();
            //mellett.Show();
            //Thread t = new Thread(new ThreadStart(SampleThread));
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            //while(t.IsAlive)
            //{
                
            //}
           
        }

        private void SampleThread()
        {
            MotoGPRSS mellett = new MotoGPRSS();
            ListingWindow listazas = new ListingWindow();
            listazas.Show();
            //this.Close();
            
            Thread.Sleep(100110);
        }

        private void Modifying_data(object sender, RoutedEventArgs e)
        {
            Modifying delta = new Modifying();
            this.Close();
            delta.Show();

        }
    }
}
