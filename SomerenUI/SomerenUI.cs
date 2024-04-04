using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using SomerenDAL;
using static System.Collections.Specialized.BitVector32;
using System.Text.RegularExpressions;

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


        //Students
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

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewStudents.SelectedItems[0];
                Student selectedStudent = (Student)selectedItem.Tag;

                FillTextBoxesStudent(selectedStudent);
            }
        }

        private void FillTextBoxesStudent(Student student)
        {
            txtStudentName.Text = student.Name;
            txtStudentNumber.Text = student.Number.ToString();
            txtStudentTelephoneNumber.Text = student.PhoneNumber;
            txtStudentClass.Text = student.Class;
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
            li.SubItems.Add(student.PhoneNumber);
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
                FillTextBoxesDrink(selectedDrink);
            }
        }

        private void FillTextBoxesDrink(Drink selectedDrink)
        {
            txtName.Text = selectedDrink.Name;
            txtPrice.Text = selectedDrink.Price.ToString();
            txtVATRate.Text = selectedDrink.VATRate.ToString();
            txtStock.Text = selectedDrink.Stock.ToString();
            txtAlcoholic.Text = selectedDrink.IsAlcoholic.ToString();
        }

        private void ClearTextBoxesDrink()
        {
            txtName.Text = null;
            txtPrice.Text = null;
            txtVATRate.Text = null;
            txtStock.Text = null;
            txtAlcoholic.Text = null;
        }

        private void ModifyDrinkProperties(Drink selectedDrink)
        {
            selectedDrink.Name = txtName.Text;
            selectedDrink.VATRate = decimal.Parse(txtVATRate.Text);
            selectedDrink.Price = decimal.Parse(txtPrice.Text);
            selectedDrink.Stock = int.Parse(txtStock.Text);
            selectedDrink.IsAlcoholic = bool.Parse(txtAlcoholic.Text);
        }


        private void buttonAddDrink_Click(object sender, EventArgs e)
        {
            try
            {
                // Get new drink
                Drink drink = new Drink();
                ModifyDrinkProperties(drink);

                // Add new drink
                DrinkService drinkService = new DrinkService();
                drinkService.AddDrink(drink);

                // Show updated list view
                ShowDrinkSuppliesPanel();
                ClearTextBoxesDrink();

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
                    DrinkService drinkService = new DrinkService();
                    drinkService.DeleteDrink(selectedDrink);

                    // Show updated list view 
                    ShowDrinkSuppliesPanel();
                    ClearTextBoxesDrink();

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

                    DrinkService drinkService = new DrinkService();
                    drinkService.ModifyDrink(selectedDrink);

                    // Show updated list view 
                    ShowDrinkSuppliesPanel();
                    ClearTextBoxesDrink();

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


        //Student's orders
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
            // Get the selected drink
            Order order = GetOrderDetails();

            if (order != null)
            {
                // Place order and Update drink supplies list if the order is valid
                PlaceOrderAndUpdateDrinkSupplies(order);

                // Show confirmation
                MessageBox.Show($"Student: {listViewStudentsOrdering.SelectedItems[0]}\nSelected drink: {listViewStudentsOrdering.SelectedItems[0]}\nPrice: {order.Price.ToString("C")} ");

                // Clear section and reset values
                ClearSectionAndResetValues();
            }
            else
            {
                MessageBox.Show("Please select a drink and a student.");
            }
        }

        private static void PlaceOrderAndUpdateDrinkSupplies(Order order)
        {
            OrderService orderService = new OrderService();
            orderService.PlaceOrder(order);
            orderService.UpdateDrinkSupplies(order);
        }

        public Order GetOrderDetails()
        {
            if (listViewOrderingDrinks.SelectedItems.Count > 0 && listViewStudentsOrdering.SelectedItems.Count > 0)
            {
                ListViewItem selectedDrink = listViewOrderingDrinks.SelectedItems[0];                                          // Get the selected drink
                ListViewItem selectedStudent = listViewStudentsOrdering.SelectedItems[0];

                // Get values for placing an order
                int drinkId = ((Drink)selectedDrink.Tag).Id;
                int studentId = ((Student)selectedStudent.Tag).Id;
                decimal price = Convert.ToDecimal(numericUpDownAmount.Value) * Convert.ToDecimal(selectedDrink.SubItems[1].Text);
                int amount = Convert.ToInt32(numericUpDownAmount.Value);
                DateTime date = DateTime.Now;

                return new Order { DrinkId = drinkId, Amount = amount, Price = price, StudentId = studentId, Date = date };  // Return drink
            }
            return null;
        }

        public void ClearSectionAndResetValues()
        {
            listViewOrderingDrinks.SelectedItems.Clear();
            numericUpDownAmount.Value = 1;
        }


        // Revenue report

        private void revenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentPanel(pnlRevenue);
        }

        public void GenerateRevenueReport(DateTime startDate, DateTime endDate)
        {
            OrderService orderService = new OrderService();                                  // Get report values
            RevenueReport revenueReport = orderService.GetRevenueReport(startDate, endDate);

            lblSales.Text = $"Sales: \n{revenueReport.Sales}";                               // Fill labels with values
            lblTurnover.Text = $"Turnover: \n{revenueReport.Turnover:C}";
            lblNumberOfCustomers.Text = $"Number of customers: \n{revenueReport.NumberOfCustomers}";
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

        // Manage students
        private void ModifyStudentProperties(Student selectedStudent)
        {
            selectedStudent.Name = txtStudentName.Text;
            selectedStudent.Number = txtStudentNumber.Text;
            selectedStudent.PhoneNumber = txtStudentTelephoneNumber.Text;
            selectedStudent.Class = txtStudentClass.Text;
            selectedStudent.RoomId = 1;
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();                              // Get new student
                ModifyStudentProperties(student);

                StudentService studentService = new StudentService();        // Add new drink
                studentService.AddStudent(student);

                ShowStudentsPanel();                   // Show updated list view
                ClearTextBoxesStudent();

                MessageBox.Show("Student added");     // Show confirmation
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);         // Throw exception
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this student?");   // Get confirmation from the user

                ListViewItem selectedItem = listViewStudents.SelectedItems[0];
                Student selectedStudent = (Student)selectedItem.Tag;

                if (answer == DialogResult.OK)
                {
                    StudentService studentService = new StudentService();
                    studentService.DeleteStudent(selectedStudent);                          // Delete student
                    ShowStudentsPanel();                                             // Show the updated list 
                }
                else
                {
                    MessageBox.Show($"{selectedStudent.Name} won't be deleted.");  // Cancel deletion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                                      // Throw exception
            }
        }

        private void btnModifyStudent_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewStudents.SelectedItems.Count > 0)                           // Check if a student has been selected
                {
                    ListViewItem selectedItem = listViewStudents.SelectedItems[0];     // Get the selected student from the list view
                    Student selectedStudent = (Student)selectedItem.Tag;

                    ModifyStudentProperties(selectedStudent);

                    StudentService studentService = new StudentService();
                    studentService.ModifyStudent(selectedStudent);

                    ShowStudentsPanel();                        // Show updated list view 
                    ClearTextBoxesStudent();

                    MessageBox.Show("Student modified");       // Show confirmation
                }
                else
                {
                    MessageBox.Show("Choose a student.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                 // Throw exception
            }
        }

        private void ClearTextBoxesStudent()
        {
            txtStudentName.Text = null;
            txtStudentNumber.Text = null;
            txtStudentClass.Text = null;
            txtStudentTelephoneNumber.Text = null;
        }


        // Activity supervisors


        // Activity participants
        private void activityParticipantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // ShowParticipantsPanel();
        }

        
        
        
    }
}