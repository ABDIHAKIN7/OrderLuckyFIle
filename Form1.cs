using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LuckyCafe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;

            if (name == "")
            {
                MessageBox.Show("Enter Customer Name");
                return;
            }

            int quantity;

            try
            {
                quantity = Convert.ToInt32(txtQuantity.Text);
            }
            catch
            {
                MessageBox.Show("Enter Correct Quantity");
                return;
            }

            if (quantity < 1)
            {
                MessageBox.Show("Quantity must be 1 or more");
                return;
            }
            string drink = "";
            double price = 0;

            if (rbTea.Checked)
            {
                drink = "Tea";
            }
            else if (rbCoffee.Checked)
            {
                drink = "Coffee";
            }
            else if (rbJuice.Checked)
            {
                drink = "Juice";
            }
            else
            {
                MessageBox.Show("Choose a Drink");
                return;
            }

            switch (drink)
            {
                case "Tea":
                    price = 1;
                    break;

                case "Coffee":
                    price = 2;
                    break;

                case "Juice":
                    price = 3;
                    break;
            }

            lstOrders.Items.Clear();

            Random random = new Random();

            bool freeCookie = false;

            int luckynumber = 0;

            for (int i = 1; i <= quantity; i++)
            {
                int lucky = random.Next(1, 51);

                lstOrders.Items.Add(
                    i + ") " + name + " - " +
                    drink + " - Lucky: " + lucky +
                    " - " + price.ToString("c2")
                );

                if (lucky == 7 )
                {
                    freeCookie = true;
                    luckynumber = lucky;

                }
                else if (lucky == 17)
                {
                    freeCookie = true;
                    luckynumber = lucky;
                }
            }

            double total = price * quantity;

            if (chkStudent.Checked)
            {
                lstOrders.Items.Add("\n\n\n");
                lstOrders.Items.Add("Member  Discount 10%");
                lstOrders.Items.Add("\n");
                lstOrders.Items.Add("Total Before Discount:" + total.ToString("c2") );
                lstOrders.Items.Add("\n");
                total = total - (total * 10 / 100);
                lstOrders.Items.Add("Total After Discount:" + total.ToString("c2"));
                lstOrders.Items.Add("\n\n");
            }
            lblTotal.Text = total.ToString("c2");

            if (freeCookie == true)
            {
                lblLucky.Text = "You got a Free Cookie!";
                lstOrders.Items.Add(" Lucky Message: You got Free Cookie!( Lucky" + luckynumber + ")");
            }
            else
            {
                lblLucky.Text = "No prize this time.";
                lstOrders.Items.Add("No prize this time" );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstOrders.Items.Count == 0)

            {

                MessageBox.Show("No order to save.");

                return;

            }

            StreamWriter Orderfile = File.AppendText("orders.txt");

            for (int i=0; i<lstOrders.Items.Count; i++)

            {

                Orderfile.WriteLine(lstOrders.Items[i]. ToString());

            }

            Orderfile.WriteLine("Total Price: " + lblTotal.Text);

            Orderfile.WriteLine("Lucky Message: " + lblLucky.Text);

            Orderfile.Close();

            MessageBox.Show("Order saved.");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("orders.txt") == false)

            {

                MessageBox.Show("No file found.");
                return;
            }

            lstOrders.Items.Clear();

            StreamReader Orderfile = File.OpenText("orders.txt");

            while (Orderfile.EndOfStream == false)

            {
                lstOrders.Items.Add(Orderfile.ReadLine());

            }
            Orderfile.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();

            txtQuantity.Clear();

            lstOrders.Items.Clear();

            lblTotal.Text = "";

            lblLucky.Text = "";

            chkStudent.Checked = false;

            rbTea.Checked = false;

            rbCoffee.Checked = false;

            rbJuice.Checked = false;
        }
    }
    }

