using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conexiune;
        OleDbCommand comanda;
        OleDbDataReader reader;
        DataTable dt;
        DataSet ds;

        private void cboRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            conexiune = new OleDbConnection();
            conexiune.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\barba\Desktop\C#-extra-apps\BookingApp\BookingSystem.accdb;Persist Security Info=False";
            conexiune.Open();

            comanda = new OleDbCommand();
            comanda.Connection = conexiune;
            comanda.CommandText = "SELECT Price FROM Rooms WHERE RoomType = ?";
            comanda.Parameters.Clear();
            comanda.Parameters.AddWithValue("RoomType", cboRooms.SelectedItem.ToString());

            reader = comanda.ExecuteReader();

            dt = new DataTable("Rooms");
            dt.Load(reader);

            ds = new DataSet();
            ds.Tables.Add(dt);

            txtpriceDB.Text = ds.Tables["Rooms"].Rows[0].ItemArray[0].ToString();

            conexiune.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;
            TimeSpan time = date2 - date1;
            int totalDays = (int)time.TotalDays;
            //MessageBox.Show(totalDays.ToString());
            txtAmount.Text = (int.Parse(txtnumberUSER.Text) * int.Parse(txtpriceDB.Text) * totalDays).ToString();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            txtcheck.Text = "Thank you for your booking, dear " + txtName.Text + "! You booked " + txtnumberUSER.Text + " " + cboRooms.SelectedItem.ToString() + " in our hotel from " + dateTimePicker1.Value.ToShortDateString() + " to " + dateTimePicker2.Value.ToShortDateString() + ".";
        }
    }
}
