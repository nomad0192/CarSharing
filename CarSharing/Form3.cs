﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarSharing
{
    public partial class Form3 : Form
    {
        Form2 f2;
        Form4 f4;
        //Form1 f1;
        public SqlConnection con { get; set; }
        public bool closing = true;
        String connectionString = @"Data Source=" + Program.serverName + "Initial Catalog=" + Program.bdName + ";" +
                  "Integrated Security=True";

        public Form3()
        {
            InitializeComponent();
            f2 = new Form2();
            textBox3.Visible = false;
            label3.Visible = false;

            textBox2.UseSystemPasswordChar = true;
            label5.Visible = false;

           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            f4 = new Form4();
            f4.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            String insertValueLogin = textBox1.Text;
            String insertValuePass = textBox2.Text;
            String insertValueKod = textBox3.Text;
            if (checkBox1.CheckState == CheckState.Checked)
            {
                try
                {

                    con = new SqlConnection(connectionString);
                    con.Open();
                    string idAdminSelect = "SELECT idAdmin FROM AutDate Where Login = '" + insertValueLogin + "' AND Password = '" + insertValuePass + " '";
                    SqlCommand idAdmin = new SqlCommand(idAdminSelect, con);
                    Int32 idAdminInt = (Int32)(idAdmin).ExecuteScalar();
                    MessageBox.Show(Convert.ToString(idAdminInt));

                    string kodAdminSelect = "SELECT idAdmin FROM Admins Where KodAdmin = '" + insertValueKod + " '";
                    SqlCommand kodAdmin = new SqlCommand(kodAdminSelect, con);
                    Int32 kodAdminInt = (Int32)(kodAdmin).ExecuteScalar();
                    MessageBox.Show(Convert.ToString(kodAdminInt));
                    MessageBox.Show("Успешно");
                    Program.adminOrUser = true;
                    closing = false;
                    this.Close();
                }
                catch(Exception ex)
                {
                    label5.Visible = true;
                    label5.Text = "Совпадений не найдено";
                }
            }
            else
            {
                try
                {
                    
                    con = new SqlConnection(connectionString);
                    con.Open();
                    string idUserSelect = "SELECT idUser FROM AutDate Where Login = '" + insertValueLogin + "' AND Password = '" + insertValuePass + " '";
                    SqlCommand idUser = new SqlCommand(idUserSelect, con);
                    Int32 idUserInt = (Int32)(idUser).ExecuteScalar();
                    

                    string statusUserSelect = "SELECT StatusPodtverzdeniya FROM Polzovatel Where IdUser = '" + idUserInt + " '";
                    SqlCommand statusUser = new SqlCommand(statusUserSelect, con);
                    bool userStatus = (bool)(statusUser).ExecuteScalar();
                    con.Close();
                    Program.idUser = idUserInt;
                    if(userStatus == true)
                    {
                        MessageBox.Show("еее роцк");
                        Program.adminOrUser = false;
                        closing = false;
                        Program.idUser = idUserInt;
                        this.Close();
                        
                        
                    }
                    else if(userStatus == false)
                    {
                        closing = false;
                        Program.confirmUserOrNo = true;
                        Program.adminOrUser = false;
                        Program.idUser = idUserInt;
                        this.Close();
                        
                    }


                }
                catch (Exception ex)
                {
                    label5.Visible = true;
                }


            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox3.Visible = true;
                label3.Visible = true;
            }
            else
            {
                textBox3.Visible = false;
                label3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (closing == true)
            {
                Application.Exit();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
