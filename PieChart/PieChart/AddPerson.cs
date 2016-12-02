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
    public partial class AddPerson : Form
    {
        public static List<Person> listOfPerson = new List<Person>();
        private Person person;
        public AddPerson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.ToString();
            name = name.Substring(36);
            string salary = this.textBox2.ToString();
            salary = salary.Substring(36);

            if (name != string.Empty && salary != string.Empty)
            {
                person = new Person(name, int.Parse(salary));
                listOfPerson.Add(person);
            }
            else
            {
                throw  new ArgumentNullException("The name or age can not be empty");
            }

            this.textBox1.Clear();
            this.textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
