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


using System.Xml;

namespace MotoGPWeb1
{
    /// <summary>
    /// Interaction logic for MotoGPRSS.xaml
    /// </summary>
    public partial class MotoGPRSS : Window
    {
        public MotoGPRSS()
        {
            //RSS.Text = "";
            InitializeComponent();
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load("http://www.motogp.com/en/news/rss");
            RSS.Text += ParseRssFile() + "\n";
            //Webes.Navigate("http://www.motogp.com/en/news/rss");


        }

        private string ParseRssFile()
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load("http://www.motogp.com/en/news/rss");

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();

            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                rssContent.Append( description+"\n\n");
            }

            // Return the string that contain the RSS items
          
            return rssContent.ToString();
        }
    }
}
