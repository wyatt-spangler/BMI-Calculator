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
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data;

namespace BMICalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [XmlRoot("BMI Calc", Namespace = "www.bmicalc.ninja")]
    public partial class MainWindow : Window
    {
        // Initialization
        public string FilePath = "C:/Temp/";
        public string FileName = "yourBMI.xml";

        public class Customer
        {
            [XmlAttribute("Last Name")]
            public string lastName { get; set; }
            [XmlAttribute("First Name")]
            public string firstName { get; set; }
            [XmlAttribute("Phone Number")]
            public string phoneNumber { get; set; }
            [XmlAttribute("Height")]
            public int heightInches { get; set; }
            [XmlAttribute("Weight")]
            public int weightLbs { get; set; }
            [XmlAttribute("Customer BMI")]
            public int custBMI { get; set; }
            [XmlAttribute("Status")]
            public string statusTitle { get; set; }
        }



        public MainWindow()
        {
            InitializeComponent();
            OnLoadStats();
        }
        #region Part 1 of lab. ClearBtn & ExitBtn
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            xPhoneBox.Text = "";
            xFirstNameBox.Text = "";
            xLastNameBox.Text = "";
            xHeightInchesBox.Text = "";
            xWeightLbsBox.Text = "";
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region Part 2 of lab. SubmitBtn & BMIFormula
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer customer1 = new Customer();

            customer1.firstName = xFirstNameBox.Text;
            customer1.lastName = xLastNameBox.Text;
            customer1.phoneNumber = xPhoneBox.Text;
            customer1.heightInches = Convert.ToInt32(xHeightInchesBox.Text);
            customer1.weightLbs = Convert.ToInt32(xWeightLbsBox.Text);

            customer1.custBMI = BMIFormula(customer1.weightLbs, customer1.heightInches);

            string yourBMIStatus = "NA";
            customer1.statusTitle = yourBMIStatus;



            xYourBMIResults.Text = $"{customer1.custBMI}";

            if (customer1.custBMI < 18.5)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are underweight.";
                customer1.statusTitle = "Underweight";
            }
            else if (customer1.custBMI < 24.9)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you have a normal body weight.";
                customer1.statusTitle = "Normal";
            }
            else if (customer1.custBMI < 29.9)
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are overweight.";
                customer1.statusTitle = "Overweight";
            }
            else
            {
                xBMIMessage.Text = "According to CDC.gov BMI Calculator you are obese.";
                customer1.statusTitle = "Obese";
            }

            TextWriter writer = new StreamWriter(FilePath + FileName);
            XmlSerializer ser = new XmlSerializer(typeof(Customer));

            ser.Serialize(writer, customer1);
            writer.Close();
        }

        public int BMIFormula(int weight, int height)
        {
            int bmi;
            bmi = 703 * weight / (height * height);
            return bmi;
        }
        #endregion

        #region Part 3 of lab. OnLoadStats
        private void OnLoadStats()
        {
            Customer cust = new Customer();

            XmlSerializer des = new XmlSerializer(typeof(Customer));
            using (XmlReader reader = XmlReader.Create(FilePath + FileName))
            {
                cust = (Customer)des.Deserialize(reader);

                xLastNameBox.Text = cust.lastName;
                xFirstNameBox.Text = cust.firstName;
                xPhoneBox.Text = cust.phoneNumber;
            }

            DataSet xmlData = new DataSet();
            xmlData.ReadXml(FilePath + FileName, XmlReadMode.Auto);
            xDataGrid.ItemsSource = xmlData.Tables[0].DefaultView;
        }
        #endregion
    }
}
