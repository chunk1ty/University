using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PieChart.PieChartFolder;

namespace PieChart
{
    public partial class PieChart : Form
    {
        public PieChart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AddPerson.listOfPerson.Count > 0)
            {
                AddPerson.listOfPerson = new List<Person>();
            }
            AddPerson person = new AddPerson();
            person.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < AddPerson.listOfPerson.Count; i++)
            {
                this.chart1.Series["Salary"].Points.AddXY(AddPerson.listOfPerson[i].Name,AddPerson.listOfPerson[i].Salary);
                this.chart1.Series["SalaryForSecondLegend"].Points.AddXY(AddPerson.listOfPerson[i].Salary, AddPerson.listOfPerson[i].Salary);
            }
        }
    }
}
