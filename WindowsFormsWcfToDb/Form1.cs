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
            var count = service.InsertPerson(p);

            if (count == 1)
            {
                MessageBox.Show($"{count} запись добавлена в базу данных.", "Сообщение");
            }
            else if (count > 1)
            {
                MessageBox.Show($"{count} записи(-ей) добавлены в базу данных.", "Сообщение");
            }
            else
            {
                MessageBox.Show("Ни одна запись не добавлена в базу данных.", "Сообщение");
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

            if (count == 1)
            {
                MessageBox.Show($"{count} запись обновлена.", "Сообщение");
            }
            else if (count > 1)
            {                
                MessageBox.Show($"{count} записи(-ей) обновлены.", "Сообщение");
            }
            else
            {
                MessageBox.Show("Ни одна запись не обновлена.", "Сообщение");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Person p = new Person()
            {
                Id = Convert.ToInt32(txtId.Text)
            };
            Service1Client service = new Service1Client();
            
            var count = service.DeletePerson(p); //передаём в метод DeletePerson экземпляр класса 'p' и возвращаем колличество затрагиваемых строк
            var idCount = Convert.ToInt32(txtId.Text); //значение Id, запись которого удаляется

            if (count == 1)
            {                
                MessageBox.Show($"{count} запись со значением Id = {idCount} удалена.", "Сообщение");
            }
            else if (count > 1)
            {                
                MessageBox.Show($"{count} записи(-ей) со значением Id = {idCount} удалены.", "Сообщение");
            }
            else
            {
                MessageBox.Show($"Ни одна запись со значением Id = {idCount} не удалена.", "Сообщение");
            }            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<Person> _person = new List<Person>();
            Person p = new Person()
            {
                Id = Convert.ToInt32(txtId.Text)
            };
            Service1Client service = new Service1Client();
            _person.Add(service.GetPerson(p));
            dgvPeople.DataSource = _person;
            //проверка
        }
    }
}
