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
using System.Windows.Shapes;
using System.Data.Linq;
using System.Threading;

namespace MotoGPWeb1
{
    /// <summary>
    /// Interaction logic for ListingWindow.xaml
    /// </summary>
    public partial class ListingWindow : Window
    {
        MotoGPDBLinqToSQLClassDataContext db = new MotoGPDBLinqToSQLClassDataContext(
           Properties.Settings.Default.CreateMotoGPDb4ConnectionString);
        public ListingWindow()
        {
            InitializeComponent();
            if (db.DatabaseExists())
            {
                var q =
                from a in db.MotoGPDB4s
   
                select new { a.TeamName, a.YearOfFound, a.WonChamps, a.Fee};
                
                Listing.ItemsSource = q;
               
                //Listing.Columns[0].Header = "First Name";
                //Listing.Columns[1].Header = "Last Name";
            }
                
            //MotoGPRSS mellett = new MotoGPRSS();
            //mellett.Show();
        }

        private string MaxWonCham()
        {
            var maxProducts = (from p in db.MotoGPDB4s
                               select p).Max(p => p.WonChamps);
            return maxProducts.ToString();
        }

        private string MinYear()
        {
            var minYear = (from p in db.MotoGPDB4s
                           select p).Min(p => p.YearOfFound);
            return minYear.ToString();
        }

        private string MaxYear()
        {
            var maxYear = (from p in db.MotoGPDB4s
                           select p).Max(p => p.YearOfFound);
            return maxYear.ToString();
        }

        private void AddingtoPage(object sender, RoutedEventArgs e)
        {
            this.Close();
            Creating addTolist = new Creating();
            addTolist.Show();
        }

        private void Mainpage(object sender, RoutedEventArgs e)
        {
            MainWindow foablak = new MainWindow();
            this.Close();
            foablak.Show();
        }

        private void MaxWonChamp(object sender, RoutedEventArgs e)
        {
            WhereMaxChamp.Text = "";
            var maxProducts = (from p in db.MotoGPDB4s
                               select p).Max(p => p.WonChamps);
            WhereMaxChamp.Text=maxProducts.ToString();


        }
        private void SampleThread()
        {
            var maxProducts = from p in db.MotoGPDB4s
                              let maxPrice = db.MotoGPDB4s.Max(s => s.WonChamps)
                              where p.WonChamps == maxPrice
                              select p;

            ////foreach (var z in maxProducts)
            ////{
            ////    WhereMaxChamp.Invoke((Action)delegate
            ////    {
            ////        WhereMaxChamp.Text += z.TeamName + " " + z.WonChamps.ToString();
            ////    }
            ////    );
                    
            ////        //+= z.TeamName + " " + z.WonChamps.ToString();
            ////}
        }

        private void WonMinChamp(object sender, RoutedEventArgs e)
        {
            bool d = Year1.IsChecked == true;
            bool b = Won1.IsChecked == true;
            bool c = Fee1.IsChecked == true;
            bool f = TeaMNAme.IsChecked == true;
            MinWonChamp.Text = "";
            var minProducts = (from p in db.MotoGPDB4s

                               select p).Min(p => p.WonChamps);
            //var minProducts1 = (from h in db.MotoGPDB4s

            //                   select h).Where(h => h.WonChamps>1);
            var q =
      from a in db.MotoGPDB4s
      where a.WonChamps>1
      orderby a.WonChamps, a.TeamName
      select a;
            // select new { a.TeamName, a.YearOfFound };
            bool elso = true;

            foreach (var z in q)
            {
                if (elso)
                { elso = false; }
                else
                {
                    MinWonChamp.Text += "\n";
                }
                if(f)
                {
                    MinWonChamp.Text += z.TeamName+" ";
                }
                if(d)
                {
                    MinWonChamp.Text +=  " "  + z.YearOfFound.ToString() ;
                }
                if(b)
                {
                    MinWonChamp.Text +=" "+ z.WonChamps.ToString() + " " ;
                }
                if(c)
                {
                    if(z.Fee)
                    {
                        MinWonChamp.Text += " This team has paid the nomiation fee.";
                    }
                }
            }
        }

        private void TryButton(object sender, RoutedEventArgs e)
        {
       //     var r = (from b in db.MotoGPDB4s
       //              select b).Min(b => b.WonChamps);
       //     var q =
       //from a in db.MotoGPDB4s
       //where a.WonChamps.Equals(r)
       //orderby a.WonChamps, a.TeamName
       //select new { a.TeamName, a.YearOfFound };
            

       //     dataGridView1.ItemsSource = q;
        }

        private void Modyfying(object sender, RoutedEventArgs e)
        {
            Modifying delta = new Modifying();
            this.Close();
            delta.Show();
        }

        private void WhereMaxChamp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
