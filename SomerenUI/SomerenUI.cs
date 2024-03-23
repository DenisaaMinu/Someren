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

        // showing the current panel
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
            // show dashboard
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
                // throw exception
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
                ListViewItem li = CreateStudentLiItem(student);
                listViewStudents.Items.Add(li);
            }
        }

        private ListViewItem CreateStudentLiItem(Student student)
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
                // throw exception
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
                ListViewItem li = CreateTeacherLiItem(teacher);
                listViewTeachers.Items.Add(li);
            }
        }

        private static ListViewItem CreateTeacherLiItem(Teacher teacher)
        {
            ListViewItem li = new ListViewItem(teacher.FirstName);
            li.SubItems.Add(teacher.LastName);
            li.SubItems.Add(teacher.TelephoneNumber);
            li.SubItems.Add(teacher.Age.ToString());

            // test
            li.Tag = teacher;   // link teacher object to listview item
            return li;
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
                // throw exception
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
                ListViewItem li = CreateRoomLiItem(room);
                listViewRooms.Items.Add(li);
            }
        }

        private static ListViewItem CreateRoomLiItem(Room room)
        {
            ListViewItem li = new ListViewItem(room.Building.ToString());
            li.SubItems.Add(room.Number.ToString());
            li.SubItems.Add(room.Size.ToString());
            li.SubItems.Add(room.Capacity.ToString());
            li.SubItems.Add(room.Type.ToString());

            // test
            li.Tag = room;   // link room object to listview item
            return li;
        }

        private void roomsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowRoomsPanel();
        }


        // Drink supplies
        private void ShowDrinkSuppliesPanel()
        {
            // show drinks
            ShowCurrentPanel(pnlDrinks);

            try
            {
                // get and display all drinks
                List<Drink> drinks = GetDrinks();
                DisplayDrinkSupplies(drinks);
            }
            catch (Exception e)
            {
                // throw exception
                MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
            }
        }

        private List<Drink> GetDrinks()
        {
            DrinkService drinkService = new DrinkService();
            List<Drink> drinks = drinkService.GetDrinks();
            return drinks;
        }

        private void DisplayDrinkSupplies(List<Drink> drinks)
        {
            // clear the listview before filling it
            listViewDrinks.Items.Clear();

            foreach (Drink drink in drinks)
            {
                ListViewItem li = CreateDrinkSuppliesLiItem(drink);
                listViewDrinks.Items.Add(li);
            }
        }
        private static ListViewItem CreateDrinkSuppliesLiItem(Drink drink)
        {
            string isAlcoholic;
            if (drink.IsAlcoholic)
                isAlcoholic = "Yes";
            else
                isAlcoholic = "No";

            ListViewItem li = new ListViewItem(drink.Name);
            li.SubItems.Add(drink.VATRate.ToString());
            li.SubItems.Add(drink.Price.ToString());
            li.SubItems.Add(drink.Stock.ToString());
            li.SubItems.Add(drink.StockStatus);
            li.SubItems.Add(isAlcoholic);
            li.SubItems.Add(drink.NumberOfDrinksSold.ToString());

            // test
            li.Tag = drink;   // link drink object to listview item
            return li;
        }

        private void drinkSuppliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDrinkSuppliesPanel();
        }

        private void listViewDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDrinks.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewDrinks.SelectedItems[0];
                Drink selectedDrink = (Drink)selectedItem.Tag;

                MessageBox.Show($"Drink {selectedDrink.Name} selected.");
                FillTextBoxes(selectedDrink);
            }
        }

        private void FillTextBoxes(Drink selectedDrink)
        {
            txtName.Text = selectedDrink.Name;
            txtPrice.Text = selectedDrink.Price.ToString();
            txtVATRate.Text = selectedDrink.VATRate.ToString();
            txtStock.Text = selectedDrink.Stock.ToString();
            txtStockStatus.Text = selectedDrink.StockStatus.ToString();
            txtAlcoholic.Text = selectedDrink.IsAlcoholic.ToString();
        }

        private void ClearTextBoxes()
        {
            txtName.Text = null;
            txtPrice.Text = null;
            txtVATRate.Text = null;
            txtStock.Text = null;
            txtStockStatus.Text = null;
            txtAlcoholic.Text = null;
        }

        private void ModifyDrinkProperties(Drink selectedDrink)
        {
            selectedDrink.Name = txtName.Text;
            selectedDrink.VATRate = decimal.Parse(txtVATRate.Text);
            selectedDrink.Price = decimal.Parse(txtPrice.Text);
            selectedDrink.Stock = int.Parse(txtStock.Text);
            selectedDrink.StockStatus = txtStockStatus.Text;
            selectedDrink.IsAlcoholic = bool.Parse(txtAlcoholic.Text);
        }


        private void buttonAddDrink_Click(object sender, EventArgs e)
        {
            try
            {
                // Get new drink
                Drink drink = new Drink(txtName.Text, decimal.Parse(txtPrice.Text), decimal.Parse(txtVATRate.Text), int.Parse(txtStock.Text), bool.Parse(txtAlcoholic.Text));

                // Add new drink
                DrinkDao drinkDAO = new DrinkDao();
                drinkDAO.AddDrink(drink);

                // Show updated list view
                ShowDrinkSuppliesPanel();
                ClearTextBoxes();

                // Show confirmation
                MessageBox.Show("Drink added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDeleteDrink_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a drink has been selected
                if (listViewDrinks.SelectedItems.Count > 0)
                {
                    // Get te selected drink from the list view
                    ListViewItem selectedItem = listViewDrinks.SelectedItems[0];
                    Drink selectedDrink = (Drink)selectedItem.Tag;

                    // Delete item
                    DrinkDao drinkDAO = new DrinkDao();
                    drinkDAO.DeleteDrink(selectedDrink);

                    // Show updated list view 
                    ShowDrinkSuppliesPanel();
                    ClearTextBoxes();

                    // Show confirmation
                    MessageBox.Show("Drink deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void buttonModifyDrink_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a drink has been selected
                if (listViewDrinks.SelectedItems.Count > 0)
                {
                    // Get te selected drink from the list view
                    ListViewItem selectedItem = listViewDrinks.SelectedItems[0];
                    Drink selectedDrink = (Drink)selectedItem.Tag;

                    ModifyDrinkProperties(selectedDrink);

                    DrinkDao drinkDao = new DrinkDao();
                    drinkDao.ModifyDrink(selectedDrink);

                    // Show updated list view 
                    ShowDrinkSuppliesPanel();
                    ClearTextBoxes();

                    // Show confirmation
                    MessageBox.Show("Drink modified");
                }
                else
                {
                    MessageBox.Show("Choose a drink.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Ordering drinks 
        private void orderingDrinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDrinksOrderingPanel();
            ShowStudentsOrderingPanel();
        }

        private void ShowDrinksOrderingPanel()
        {
            //showing the ordering panel 
            ShowCurrentPanel(pnlOrdering);

            try
            {
                //get and display all drinks
                List<Drink> drinks = GetDrinks();
                DisplayOrderDrinks(drinks);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong with loading the drinks " + ex.Message);
            }
        }

        private static ListViewItem CreateOrderingDrinkLiItem(Drink drink)
        {
            ListViewItem li = new ListViewItem(drink.Name);
            li.SubItems.Add(drink.Price.ToString());
            li.SubItems.Add(drink.Stock.ToString());
            li.SubItems.Add(drink.IsAlcoholic.ToString());

            // test
            li.Tag = drink;   // link drink object to listview item
            return li;
        }

        private void DisplayOrderDrinks(List<Drink> drinks)
        {
            // clear the listview before filling it
            listViewOrderingDrinks.Items.Clear();

            foreach (Drink drink in drinks)
            {
                ListViewItem li = CreateOrderingDrinkLiItem(drink);
                listViewOrderingDrinks.Items.Add(li);
            }
        }


        //Students orders
        private void ShowStudentsOrderingPanel()
        {
            try
            {
                // get and display all students
                List<Student> students = GetStudents();
                DisplayStudentsOrdering(students);
            }
            catch (Exception e)
            {
                // throw exception
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }

        private void DisplayStudentsOrdering(List<Student> students)
        {
            // clear the listview before filling it
            listViewStudentsOrdering.Items.Clear();

            foreach (Student student in students)
            {
                ListViewItem li = new ListViewItem(student.Name);
                li.Tag = student;

                listViewStudentsOrdering.Items.Add(li);
            }
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            if (listViewOrderingDrinks.SelectedItems.Count > 0 && listViewStudentsOrdering.SelectedItems.Count > 0)
            {
                // Get the selected drink
                ListViewItem selectedDrink = listViewOrderingDrinks.SelectedItems[0];
                ListViewItem selectedStudent = listViewStudentsOrdering.SelectedItems[0];

                // Get the values necessary for placing an order
                double price = Convert.ToDouble(numericUpDownAmount.Value) * Convert.ToDouble(selectedDrink.SubItems[1].Text);
                int drinkId = ((Drink)selectedDrink.Tag).Id;
                int studentId = ((Student)selectedStudent.Tag).Id;
                int amount = Convert.ToInt32(numericUpDownAmount.Value);
                DateTime date = DateTime.Now;

                // Place order and Update drink supplies list
                OrderDao orderDao = new OrderDao();
                orderDao.PlaceOrder(drinkId, studentId, amount, price, date);

                orderDao.UpdateDrinkSupplies(drinkId, amount);

                // Show confirmation
                MessageBox.Show($"Student: {selectedStudent.Text}\nSelected drink: {selectedDrink.Text}\nPrice: {price.ToString("C")} ");

                // Clear section and reset values
                listViewOrderingDrinks.SelectedItems.Clear();
                numericUpDownAmount.Value = 1;
            }
        }


        // Revenue report

        private void revenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentPanel(pnlRevenue);
        }

        public void GenerateRevenueReport(DateTime startDate, DateTime endDate)
        {
            OrderDao orderDao = new OrderDao();
            int numberOfDrinksSold = orderDao.GetTotalDrinksSold(startDate, endDate);
            decimal totalTurnover = orderDao.GetTurnover(startDate, endDate);
            int numberOfCustomers = orderDao.GetNumberOfCustomers(startDate, endDate);

            lblSales.Text = $"Sales: \n{numberOfDrinksSold}";
            lblTurnover.Text = $"Turnover: \n{totalTurnover:C}";
            lblNumberOfCustomers.Text = $"Number of customers: \n{numberOfCustomers}";
        }

        private void buttonGenerateRevenue_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            if (startDate <= endDate && endDate <= DateTime.Now)
            {
                GenerateRevenueReport(startDate, endDate);
            }
            else
            {
                MessageBox.Show("Invalid date. Please select a valid date.");
            }
        }
    }
}