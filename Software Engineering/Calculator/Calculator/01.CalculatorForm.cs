using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }
        decimal operandOne;
        decimal operandTwo;
        double result;
        string operation;
        string round;
        bool checkResult = true;   // example 1+2=3 if i press 2 -> 32 
        string fileName = @"C:\chunk1ty\C#\c# trening\c# trening\Folder for testing programs\Calculator\Result.txt";
        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text = "0";
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label1.Text=="")
            {
                return;
            }
            else
            {
                Clipboard.SetText(label1.Text);
                label1.Text = "";
            }
           
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label1.Text=="")
            {
                return;
            }
            else
            {
                Clipboard.SetText(label1.Text);   
            }
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text += Clipboard.GetText();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))            //01 -> only 1
            {
                label1.Text = "";
                label1.Text += '1';
                checkResult = true;
            }  
            else
            {
                label1.Text += '1';
                checkResult = true;
            }
                  
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '2';
                checkResult = true;
            } 
            else
            {
                label1.Text += '2';
                checkResult = true;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '3';
                checkResult = true;
            }
            else
            {
                label1.Text += '3';
                checkResult = true;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '4';
                checkResult = true;
            }
            else
            {
                label1.Text += '4';
                checkResult = true;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '5';
                checkResult = true;
            }
            else
            {
                label1.Text += '5';
                checkResult = true;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '6';
                checkResult = true;
            }
            else
            {
                label1.Text += '6';
                checkResult = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '7';
                checkResult = true;
            }
            else
            {
                label1.Text += '7';
                checkResult = true;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '8';
                checkResult = true;
            }
            else
            {
                label1.Text += '8';
                checkResult = true;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if ((checkResult == false) || (label1.Text == "0"))
            {
                label1.Text = "";
                label1.Text += '9';
                checkResult = true;
            }
            else
            {
                label1.Text += '9';
                checkResult = true;
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                label1.Text = " ";
                operation = "+";
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                label1.Text = "";
                operation = "-";
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                label1.Text = "";
                operation = "*";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                label1.Text = "";
                operation = "/";
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandTwo = decimal.Parse(label1.Text);
                if (operation == "+")
                {
                    label1.Text = Convert.ToString(operandOne + operandTwo);
                    checkResult = false;
                }
                else if (operation == "-")
                {
                    label1.Text = Convert.ToString(operandOne - operandTwo);
                    checkResult = false;
                }
                else if (operation == "*")
                {
                    label1.Text = Convert.ToString(operandOne * operandTwo);
                    checkResult = false;
                }
                else if (operation == "/")
                {
                    if (operandTwo == 0)
                    {
                        label1.Text = "Can not divide by zero";
                        checkResult = false;
                    }
                    else
                    {
                        label1.Text = Convert.ToString(operandOne / operandTwo);
                        checkResult = false;
                    }
                }
            }
  
            using (StreamWriter write = new StreamWriter(fileName,true))
            {
                string op1 = Convert.ToString(operandOne);
                string op2 = Convert.ToString(operandTwo);
                write.Write(op1);
                write.Write(" ");
                write.Write(operation);
                write.Write(" ");
                write.Write(op2);
                write.Write(" ");
                write.Write("=");
                write.Write(" ");
                write.Write(label1.Text);
                write.WriteLine();
                write.Close();
            }
        }
           

        private void button10_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                label1.Text = Convert.ToString(Math.Sqrt((double)operandOne));
                checkResult = false;
                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("√");
                    write.Write(op1);
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.WriteLine();
                    write.Close();
                }
            }   
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label1.Text != "") 
            {
                operandOne = decimal.Parse(label1.Text);
                if (operandOne == 0)
                {
                    label1.Text = "Can not divide by zero";
                    checkResult = false;
                }
                else
                { 
                    label1.Text = Convert.ToString(1 / operandOne);
                    checkResult = false;
                }
                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("1 / ");
                    write.Write(op1);
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.WriteLine();
                    write.Close();
                }     
            }
        }

        private void button44_Click(object sender, EventArgs e)
        { 
            if (label1.Text=="0")
            {
                label1.Text = label1.Text + ".";    
            }
            else if (checkResult == false)
            {
                label1.Text = "";
                label1.Text = label1.Text + "0";
                checkResult = true;
            }
            else
            {
                label1.Text += '0';
                checkResult = true;
            }
        }

        private void button47_Click(object sender, EventArgs e)
        { 
            if (label1.Text.Contains(".") || label1.Text == "")
            {
                return;
            }  
            label1.Text += ".";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (label1.Text.Contains('-'))
            {
                label1.Text = label1.Text.Remove(0, 1);
            }
            else
            {
                label1.Text = "-" + label1.Text;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                result = Math.Sin(Math.PI / 180 * (double)operandOne);
                round = Convert.ToString(Math.Round(result,10));
                label1.Text = round;
                checkResult = false;

                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("sin(");
                    write.Write(op1);
                    write.Write("°)");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.WriteLine();
                    write.Close();
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                result = Math.Cos(Math.PI / 180 * (double)operandOne);
                round = Convert.ToString(Math.Round(result, 10));
                label1.Text = round;
                checkResult = false;

                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("cos(");
                    write.Write(op1);
                    write.Write("°)");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.WriteLine();
                    write.Close();
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            double cosX;
            double checkCosX;
  
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                cosX = Math.Cos(Math.PI / 180 * (double)operandOne);
                checkCosX = Math.Round(cosX, 10);
                if (checkCosX==0)
                {
                    label1.Text = "Invalid input";
                    checkResult = false;
                }
                else
                {   
                    result = Math.Tan(Math.PI / 180 * (double)operandOne);
                    round = Convert.ToString(Math.Round(result, 10));
                    label1.Text = round;
                    checkResult = false;
                }

                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("tg(");
                    write.Write(op1);
                    write.Write("°)");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.WriteLine();
                    write.Close();
                }
            }   
        }

        private void button49_Click(object sender, EventArgs e)
        {
            double sinX;
            double cosX;
            int check = int.Parse(label1.Text);
            if ((label1.Text == "0") || (check % 180 == 0))
            {
                label1.Text = "Invalid input";
                checkResult = false;
            }
            else
            {
                if (label1.Text != "")
                {
                    operandOne = decimal.Parse(label1.Text);
                    sinX = Math.Sin(Math.PI / 180 * (double)operandOne);
                    cosX = Math.Cos(Math.PI / 180 * (double)operandOne);
                    result = cosX / sinX;
                    round = Convert.ToString(Math.Round(result, 10));
                    label1.Text = round;
                    checkResult = false;
                }
            }
            using (StreamWriter write = new StreamWriter(fileName, true))
            {
                string op1 = Convert.ToString(operandOne);
                write.Write("cotg(");
                write.Write(op1);
                write.Write("°)");
                write.Write(" ");
                write.Write("=");
                write.Write(" ");
                write.Write(label1.Text);
                write.WriteLine();
                write.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double degrees;
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                result = Math.Asin((double)operandOne);
                degrees = result* ( 180 / Math.PI );
                round = Convert.ToString(Math.Round(degrees, 5));
                label1.Text = round;
                checkResult = false;

                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("arcsin(");
                    write.Write(op1);
                    write.Write(")");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.Write("°");
                    write.WriteLine();
                    write.Close();
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            double degrees;
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                result = Math.Acos((double)operandOne);
                degrees = result * (180 / Math.PI);
                round = Convert.ToString(Math.Round(degrees, 5));
                label1.Text = round;
                checkResult = false;

                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("arccos(");
                    write.Write(op1);
                    write.Write(")");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.Write("°");
                    write.WriteLine();
                    write.Close();
                }
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            double degrees;
            if (label1.Text != "")
            {
                operandOne = decimal.Parse(label1.Text);
                result = Math.Atan((double)operandOne);
                degrees = result * (180 / Math.PI);
                round = Convert.ToString(Math.Round(degrees, 5));
                label1.Text = round;
                checkResult = false;
                using (StreamWriter write = new StreamWriter(fileName, true))
                {
                    string op1 = Convert.ToString(operandOne);
                    write.Write("arctg(");
                    write.Write(op1);
                    write.Write(")");
                    write.Write(" ");
                    write.Write("=");
                    write.Write(" ");
                    write.Write(label1.Text);
                    write.Write("°");
                    write.WriteLine();
                    write.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length>0)
            {
                label1.Text = label1.Text.Remove(label1.Text.Length - 1);  
            }     
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (label1.Text=="")
            {
                return;
            }
            else
            {
                Clipboard.SetText(label1.Text);
                label1.Text = "";
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (label1.Text=="")
            {
                return;
            }
            else
            {
                Clipboard.SetText(label1.Text);
            }     
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text += Clipboard.GetText();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text = "0";

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About showForm = new About();
            showForm.ShowDialog();
        }
    
    }
}
