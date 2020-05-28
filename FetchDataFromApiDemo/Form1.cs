using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FetchDataFromApiDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("");
            //HttpResponseMessage response = client.GetAsync("http://localhost:88/api/v2/students").Result;
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/products").Result;
            var emp = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            dataGridView1.DataSource = emp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            string plainText = txtName.Text.Trim() + ":" + txtPwd.Text.Trim();            
            var token = Encoding.UTF8.GetBytes(plainText);
            string access_token = Convert.ToBase64String(token);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", access_token);

            HttpResponseMessage response = client.GetAsync("http://localhost:55549/api/allemployees").Result;
            if (response.IsSuccessStatusCode)
            {
                var emp = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                dataGridView1.DataSource = emp;
            }            
        }
    }
}
