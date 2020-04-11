using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsWcfToDb.ServiceReference1;

namespace WindowsFormsWcfToDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Person p = new Person()
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };

            Service1Client service = new Service1Client();

            if(service.InsertPerson(p) == 1)
            {
                MessageBox.Show("Запись добавлена в базу данных");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Person p = new Person()
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };

            Service1Client service = new Service1Client();
            var count = service.UpdatePerson(p);

            if(count == 1)
            {
                MessageBox.Show($"{count} запись обновлена");
            }
            else if(count > 1)
            {                
                MessageBox.Show($"{count} записи/записей обновлены");
            }
            else
            {
                MessageBox.Show("Ни одна запись не обновлена");
            }
        }
    }
}
