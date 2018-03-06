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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                MessageBox.Show(nbAlea.ToString());

            }

            
        }
    }
}
