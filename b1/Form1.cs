using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace b1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool Require()
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Enter Id!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return false;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            return true;
        }
        private Person getPerson()
        {
            Person ps = new Person();
            ps.Id = Convert.ToInt32(txtId.Text);
            ps.Name = txtName.Text;
            return ps;
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Require())
            {
                ConnectPerson obj = new ConnectPerson();
                Person ps = getPerson();
                if (obj.addData(ps))
                    MessageBox.Show("Created!");
                EmptyBox();
                btnRead_Click(sender, e);
            }
        }
        private void EmptyBox()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ConnectPerson obj = new ConnectPerson();
            lblName.Text = obj.getRow();
            dataGridView1.DataSource = obj.getTable();
            Combobox(sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Require())
            {
                ConnectPerson obj = new ConnectPerson();
                Person ps = getPerson();
                if (obj.updateData(ps))
                    MessageBox.Show("Updated!");
                EmptyBox();
                btnRead_Click(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Enter Id!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
            }
            else
            {
                ConnectPerson obj = new ConnectPerson();
                Person ps = getPerson();
                if (obj.deleteData(ps))
                    MessageBox.Show("Deleted!");
                EmptyBox();
                btnRead_Click(sender, e);
            }
        }

        private void Combobox(object sender, EventArgs e)
        {
            cbbName.Items.Clear();
            ConnectPerson obj = new ConnectPerson();
            List<Person> lps = obj.getData();
            foreach (Person p in lps)
            {
                cbbName.Items.Add(p.Name);
                cbbName.SelectedIndex = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            EmptyBox();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            lblName.Text = string.Empty;
            cbbName.SelectedIndex = -1;
            cbbName.Items.Clear();
        }
    }
}
