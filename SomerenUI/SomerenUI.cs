using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using SomerenDAL;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        public void ShowCurrentPanel(Panel currentPanel)
        {
            foreach (Control control in Controls)
            {
                if (control is Panel)
                {
                    control.Hide();
                }
            }
            currentPanel.Show();
        }

        private void ShowDashboardPanel()
        {
            //  show dashboard
            ShowCurrentPanel(pnlDashboard);
        }

        private void dashboardToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            ShowDashboardPanel();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        //students
        private void ShowStudentsPanel()
        {
            // show students
            ShowCurrentPanel(pnlStudents);

            try
            {
                // get and display all students
                List<Student> students = GetStudents();
                DisplayStudents(students);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentsPanel();
        }

        private List<Student> GetStudents()
        {
            StudentService studentService = new StudentService();
            List<Student> students = studentService.GetStudents();
            return students;
        }

        private void DisplayStudents(List<Student> students)
        {
            // clear the listview before filling it
            listViewStudents.Items.Clear();

            foreach (Student student in students)
            {
                ListViewItem li = CreateStudentLItem(student);
                listViewStudents.Items.Add(li);
            }
        }

        private ListViewItem CreateStudentLItem(Student student)
        {
            ListViewItem li = new ListViewItem(student.Name);
            li.SubItems.Add(student.Number);
            li.SubItems.Add(student.TelephoneNumber);
            li.SubItems.Add(student.Class);
            //test
            li.Tag = student;   // link student object to listview item
            return li;
        }


        //teachers
        private void ShowTeachersPanel()
        {
            // show teachers
            ShowCurrentPanel(pnlTeachers);

            try
            {
                // get and display all teachers
                List<Teacher> teachers = GetTeachers();
                DisplayTeachers(teachers);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
            }
        }

        private List<Teacher> GetTeachers()
        {
            TeacherService teacherService = new TeacherService();
            List<Teacher> teachers = teacherService.GetTeachers();
            return teachers;
        }

        private void DisplayTeachers(List<Teacher> teachers)
        {
            // clear the listview before filling it
            listViewTeachers.Items.Clear();

            foreach (Teacher teacher in teachers)
            {
                ListViewItem li = new ListViewItem(teacher.FirstName);
                li.SubItems.Add(teacher.LastName);
                li.SubItems.Add(teacher.RoomId.ToString());
                li.SubItems.Add(teacher.TelephoneNumber);
                li.SubItems.Add(teacher.Age.ToString());

                li.Tag = teacher;   // link teacher object to listview item
                listViewTeachers.Items.Add(li);
            }
        }

        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTeachersPanel();
        }


        //rooms
        private void ShowRoomsPanel()
        {
            // show rooms
            ShowCurrentPanel(pnlRooms);

            try
            {
                // get and display all rooms
                List<Room> rooms = GetRooms();
                DisplayRooms(rooms);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
            }
        }

        private List<Room> GetRooms()
        {
            RoomService roomService = new RoomService();
            List<Room> rooms = roomService.GetRooms();
            return rooms;
        }

        private void DisplayRooms(List<Room> rooms)
        {
            // clear the listview before filling it
            listViewRooms.Items.Clear();

            foreach (Room room in rooms)
            {
                ListViewItem li = new ListViewItem(room.Id.ToString());
                li.SubItems.Add(room.Building);
                li.SubItems.Add(room.Number.ToString());
                li.SubItems.Add(room.Size.ToString());
                li.SubItems.Add(room.Capacity.ToString());
                li.SubItems.Add(room.Type.ToString());

                li.Tag = room;   // link room object to listview item
                listViewRooms.Items.Add(li);
            }
        }

        private void roomsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowRoomsPanel();
        }

        //drinks
        private void ShowDrinksPanel()
        {
            // show rooms
            ShowCurrentPanel(pnlDrinks);

            try
            {
                // get and display all drinks
                List<Drink> drinks = GetDrinks();
                DisplayDrinks(drinks);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
            }
        }

        private List<Drink> GetDrinks()
        {
            DrinkService drinkService = new DrinkService();
            List<Drink> drinks = drinkService.GetDrinks();
            return drinks;
        }

        private void DisplayDrinks(List<Drink> drinks)
        {
            // clear the listview before filling it
            listViewDrinks.Items.Clear();

            foreach (Drink drink in drinks)
            {
                ListViewItem li = new ListViewItem(drink.Id.ToString());
                li.SubItems.Add(drink.Name);
                li.SubItems.Add(drink.VATRate.ToString());
                li.SubItems.Add(drink.Price.ToString());
                li.SubItems.Add(drink.Stock.ToString());
                li.SubItems.Add(drink.StockStatus);

                li.Tag = drink;   // link drink object to listview item
                listViewDrinks.Items.Add(li);
            }
        }


        private void drinkSuppliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDrinksPanel();
        }

        private void listViewDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDrinks.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewDrinks.SelectedItems[0];
                Drink selectedDrink = (Drink)selectedItem.Tag;
                MessageBox.Show($"Drink {selectedDrink.Name} selected.");
            }
        }

        private void buttonAddDrink_Click(object sender, EventArgs e)
        {
            try
            {
                Drink drink = new Drink
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    VATRate = int.Parse(txtVATRate.Text),
                    Price = int.Parse(txtPrice.Text),
                    Stock = int.Parse(txtStock.Text),
                    StockStatus = txtStockStatus.Text
                };

                DrinkDao drinkDAO = new DrinkDao();
                drinkDAO.AddDrink(drink);
                MessageBox.Show("Drink added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void buttonDeleteDrink_Click(object sender, EventArgs e)
        {
            try
            {
                Drink drink = new Drink
                {
                    Id = int.Parse(txtId.Text)
                };

                DrinkDao drinkDAO = new DrinkDao();
                drinkDAO.DeleteDrink(drink);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonModifyDrink_Click(object sender, EventArgs e)
        {
            try
            {
                Drink drink = new Drink
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    VATRate = int.Parse(txtVATRate.Text),
                    Price = int.Parse(txtPrice.Text),
                    Stock = int.Parse(txtStock.Text),
                    StockStatus = txtStockStatus.Text
                };

                DrinkDao drinkDAO = new DrinkDao();
                drinkDAO.ModifyDrink(drink);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}