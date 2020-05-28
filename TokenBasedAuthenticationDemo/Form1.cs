using Newtonsoft.Json;
using RestSharp;
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

namespace TokenBasedAuthenticationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            // Method 1
            var client = new RestClient("http://localhost:34984/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=password&username=Sambit&password=123pqr", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var jsonContent = response.Content;
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            string access_token = tok.AccessToken.ToString();
            client = new RestClient("http://localhost:34984/api/employee/getall");
            request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + access_token);
            response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var emp = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                dataGridView1.DataSource = emp;

            }
        }

    }
}
