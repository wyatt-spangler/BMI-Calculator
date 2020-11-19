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

namespace BMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double BMI;
        double height;
        double weight;
        public class Customer
        {
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string phoneNumber { get; set; }
            public int heightInches { get; set; }
            public int weightLbs { get; set; }
            public int custBMI { get; set; }
            public string statusTitle { get; set; }

            
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Part 1
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

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            height = Convert.ToDouble(xHeightInchesBox.Text);
            weight = Convert.ToDouble(xWeightLbsBox.Text);

            BMI = (weight / Math.Pow(height, 2)) * 703;
            xYourBMIResults.Text = Convert.ToString(BMI);

            if (BMI < 18.5)
            {
                //under weight
                xBMIMessage.Text = "According to CDC.gov you are under weight.";
            }
            else if (BMI > 29.9)
            {
                //obese
                xBMIMessage.Text = "According to CDC.gov you are obese.";
            }
            else if (BMI > 24.9)
            {
                //over weight
                xBMIMessage.Text = "According to CDC.gov you are over weight.";
            }
            else
            {
                //normal
                xBMIMessage.Text = "According to CDC.gov you are the normal weight.";
            }
        }
    }
}
