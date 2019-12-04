using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace MotoGPWeb1
{
    /// <summary>
    /// Interaction logic for Modifying.xaml
    /// </summary>
    public partial class Modifying : Window
    {
        MotoGPDBLinqToSQLClassDataContext db = new MotoGPDBLinqToSQLClassDataContext(
           Properties.Settings.Default.CreateMotoGPDb4ConnectionString);
        public Modifying()
        {
            InitializeComponent();
            if (db.DatabaseExists())
            {
                //MaxNum.Text = MaxWonCham();
                //MaxNumYear.Text = MaxYear();
                //BeginningNum1.Text = MinYear();
                //MaxNumMod.Text = MaxWonCham();
                ////BeginningNumMod1.Text = MinYear();
                //MaxNumYearMod.Text = MaxYear();
            }
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

        private void Back_Main(object sender, RoutedEventArgs e)
        {
            MainWindow foablak = new MainWindow();
            this.Close();
            foablak.Show();
        }

        private void ListingPage(object sender, RoutedEventArgs e)
        {
            this.Close();
            ListingWindow listazas = new ListingWindow();
            listazas.Show();
        }

        private void AddingPage(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Creating listaablak = new Creating();
            listaablak.Show();
        }

        private void ByName(object sender, RoutedEventArgs e)
        {

            try
            {
                MotoGPDB4 objCourse = db.MotoGPDB4s.Single(course => course.TeamName == TeamsName.Text);
                //Puts an entity from this table into a pending delete state and parameter is the entity which to be deleted.
               
                db.MotoGPDB4s.DeleteOnSubmit(objCourse);
                try
                {
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("Clicking 'Yes' deletes "+objCourse.TeamName+" from the database." + Environment.NewLine + "This will remove this record from your database." + Environment.NewLine + "Have you confirmed all data to be correct?", "DataSync", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        db.SubmitChanges();
                    }
                    

                    
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only existing teams can be deleted.", "Error", 0);
            }

        }

        private void WonCh(object sender, RoutedEventArgs e)
        {
            try
            {
                int minWonCh = int.Parse(BeginningNum.Text);
                int maxWonCh = int.Parse(MaxNum.Text);
                //MotoGPDB4 objCourse = db.MotoGPDB4s.Single(
                //    course => course.WonChamps>=minWonCh&& course.WonChamps<=maxWonCh);
                //db.MotoGPDB4s.DeleteOnSubmit(objCourse);
                //db.SubmitChanges();
                var deleteOrderDetails =
                    from details in db.MotoGPDB4s
                    where details.WonChamps >= minWonCh && details.WonChamps <= maxWonCh
                    select details;

                foreach (var detail in deleteOrderDetails)
                {
                    db.MotoGPDB4s.DeleteOnSubmit(detail);
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Won championships must be a number and greater than -1 end", "Error", 0);
            }
        }

        private void YearOfFound(object sender, RoutedEventArgs e)
        {
            try
            {
                int minYr = int.Parse(BeginningNum1.Text);
                int maxYr = int.Parse(MaxNumYear.Text);

                var deleteOrderDetails =
                    from details in db.MotoGPDB4s
                    where details.WonChamps >= minYr && details.WonChamps <= maxYr
                    select details;

                foreach (var detail in deleteOrderDetails)
                {
                    db.MotoGPDB4s.DeleteOnSubmit(detail);
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Years must be between 1949 and present year.", "Error", 0);
            }
        }

        private void Feedel(object sender, RoutedEventArgs e)
        {
            bool a = Fee1.IsChecked == true;
            try
            {
                int minYr = int.Parse(BeginningNum1.Text);
                int maxYr = int.Parse(MaxNumYear.Text);

                var deleteOrderDetails =
                    from details in db.MotoGPDB4s
                    where details.Fee == true
                    select details;

                foreach (var detail in deleteOrderDetails)
                {
                    db.MotoGPDB4s.DeleteOnSubmit(detail);
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something is wrong in fee button.", "Error", 0);
            }
        }

        private void TeamNameSet(object sender, RoutedEventArgs e)
        {
            try
            {
                
                MotoGPDB4 objCourse = db.MotoGPDB4s.Single(course => course.TeamName == TeamsNameMod.Text);
                
                string name = objCourse.TeamName;
                int yearof = objCourse.YearOfFound;
                int wonch = objCourse.WonChamps;
                bool a = objCourse.Fee;
                db.MotoGPDB4s.DeleteOnSubmit(objCourse);
                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
               
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong in submitting.", "Error", 0);
                }
                MotoGPDB4 datab = new MotoGPDB4();
                int maxPrice = db.MotoGPDB4s.Max(s => s.Id)+1;
                datab.Id = maxPrice;
                datab.TeamName = TeamNameModTo.Text;
                datab.WonChamps = wonch;
                datab.YearOfFound = yearof;
                datab.Fee = a;
                try
                {
                    db.MotoGPDB4s.InsertOnSubmit(datab);
                    db.SubmitChanges();
                }
                catch
                {
                    MessageBox.Show("Something is wrong in the insertion.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only existing teams can be deleted.", "Error",0, MessageBoxIcon.Warning);
            }
        }

        private void ChampChange(object sender, RoutedEventArgs e)
        {
            try
            {
                var changeDetails =
                    from details in db.MotoGPDB4s
                    where details.WonChamps==int.Parse(MaxNumMod.Text)
                    select details;

                foreach (var detail in changeDetails)
                {
                    detail.WonChamps = int.Parse(NewWonChamp.Text);
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Won championships must be a number and greater than -1 end", "Error", 0);
            }
        }

        private void YearChange(object sender, RoutedEventArgs e)
        {
            DateTime localDate = DateTime.Now;
            try
            {
                if (int.Parse(MaxNumYearMod.Text) < int.Parse(MinYear()) || int.Parse(MaxNumYearMod.Text) > int.Parse(MaxYear()))
                {
                    throw new Exception();
                }

                if (int.Parse(NewYearFSet.Text) < int.Parse(MinYear()) || int.Parse(NewYearFSet.Text) > localDate.Year)
                {
                    throw new Exception();
                }

                var changeDetails =
                    from details in db.MotoGPDB4s
                    where details.YearOfFound == int.Parse(MaxNumYearMod.Text)
                    select details;

                foreach (var detail in changeDetails)
                {
                    detail.YearOfFound = int.Parse(NewYearFSet.Text);
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Years must be between 1949 and the foundation of the youngest team, " +
                    "which was founded in "+ MaxYear(), "Error", 0);
            }
        }

        private void FeeChange(object sender, RoutedEventArgs e)
        {
            bool a = FeeSet1.IsChecked == true;
            bool b = FeeSet2.IsChecked == true;
            try
            {

                if (a == b)
                {
                    throw new Exception();
                }

                var deleteOrderDetails =
                    from details in db.MotoGPDB4s
                    where details.Fee == a
                    select details;

                foreach (var detail in deleteOrderDetails)
                {
                    detail.Fee = b;
                }

                try
                {
                    db.SubmitChanges();
                    MaxNum.Text = MaxWonCham();
                    MaxNumYear.Text = MaxYear();
                    BeginningNum1.Text = MinYear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something is wrong.", "Error", 0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something is wrong in fee button.", "Error", 0);
            }

        }
    }
}
