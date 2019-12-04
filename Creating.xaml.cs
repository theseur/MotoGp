using System;
using System.Collections.Generic;
using System.Data.Linq;
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
using System.ComponentModel;

namespace MotoGPWeb1
{
    /// <summary>
    /// Interaction logic for Creating.xaml
    /// </summary>
    public partial class Creating : Window
    {
        MotoGPDBLinqToSQLClassDataContext db = new MotoGPDBLinqToSQLClassDataContext(
            Properties.Settings.Default.CreateMotoGPDb4ConnectionString);

        public Creating()
        {
            InitializeComponent();
            //MotoGPRSS mellett = new MotoGPRSS();
            //mellett.Show();
        }

        private void Back_Main(object sender, RoutedEventArgs e)
        {
            MainWindow foablak = new MainWindow();
            this.Close();
            foablak.Show();
        }

        private void YearContol(object sender, TextChangedEventArgs e)
        {
            DateTime localDate = DateTime.Now;
            if(InputYear.Text.Length>3)
            {
                try
                {
                    if (int.Parse(InputYear.Text) < 1948 || int.Parse(InputYear.Text) > localDate.Year)
                    { throw new Exception(); }
                }
                catch (Exception)
                {
                    MessageBox.Show("Years must be between 1949 and present year", "Error", 0);
                }
            }      
        }

        //private void ChampContol(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (int.Parse(Won_Champs.Text)<0)
        //        { throw new Exception(); }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Won Championshps must be greater than -1", "Error", 0);
        //    }
        //}

        private void Submitting_data(object sender, RoutedEventArgs e)
        {

            try
            {
                if (int.Parse(Won_Champs.Text) < 0)
                { throw new Exception(); }
            }
            catch (Exception)
            {
                MessageBox.Show("Won Championshps must be greater than -1", "Error", 0);
            }
            MotoGPDB4 dastab = new MotoGPDB4();
                 if(db.DatabaseExists())
            {
                bool b = Paid.IsChecked == true;
                try
                {
                    MotoGPDB4 custQuery =
    (from custs in db.MotoGPDB4s
     orderby custs.Id descending
     select custs)
    .First();
                   
                    dastab.Id = custQuery.Id+1;
                    dastab.TeamName = TeamNameInput.Text;
                    dastab.YearOfFound = int.Parse(InputYear.Text);
                    dastab.WonChamps = int.Parse(Won_Champs.Text);
                    dastab.Fee = b;
                    db.MotoGPDB4s.InsertOnSubmit(dastab);
                    db.SubmitChanges();
                    TeamNameInput.Text = "";
                    InputYear.Text = "";
                    Won_Champs.Text = "";
                    Paid.IsChecked = false;
                }
             
                catch(Exception)
                {
                    MessageBox.Show("Teamname cannot be empty", "Error", 0);
                }
            }
        }

        private void Listing_page(object sender, RoutedEventArgs e)
        {
            ListingWindow listazas = new ListingWindow();
            this.Close();
            listazas.Show();
        }
    }
}
