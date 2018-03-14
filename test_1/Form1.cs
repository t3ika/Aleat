using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace test_1
{
    public partial class Form1 : MetroForm
    {

        int nbTotal;

        private ClassLibrary1.ClassSQL1 _cla1 = new ClassLibrary1.ClassSQL1();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var data = _cla1.GetMain();
            listBox10.DataSource = data;
            listBox10.DisplayMember = "na";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            listBox1.Items.Add(metroTextBox1.Text);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            nbTotal = listBox1.Items.Count;
            int compt1;
            for (compt1 = 0; compt1 < nbTotal; compt1++)
            {
                int nbRestant = listBox1.Items.Count;
                Random alea = new Random();
                int nbAlea = alea.Next(nbRestant);
                if (compt1 < 2)
                {
                    listBox2.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 1 && compt1 < 4)
                {
                    listBox3.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 3 && compt1 < 6)
                {
                    listBox4.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 5 && compt1 < 8)
                {
                    listBox5.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 7 && compt1 < 10)
                {
                    listBox6.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 9 && compt1 < 12)
                {
                    listBox7.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 11 && compt1 < 14)
                {
                    listBox8.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else if (compt1 > 13 && compt1 < 16)
                {
                    listBox9.Items.Add(listBox1.Items[nbAlea]);
                    listBox1.Items.Remove(listBox1.Items[nbAlea]);
                }
                else
                {
                    MessageBox.Show("ee");
                }


            }

            
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                sql_datamodel dBase = new sql_datamodel();
                listBox10.DataSource = dBase.mains.ToList();
                listBox10.DisplayMember = "na";
            }
            catch (Exception ez)
            {
                MessageBox.Show(ez.ToString());
            }
                
        }
    }
}
