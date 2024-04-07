using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using SomerenDAL;
using static System.Collections.Specialized.BitVector32;
using System.Text.RegularExpressions;
using System.Drawing.Text;
using System.Linq;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
            ShowCurrentPanel(pnlDashboard);
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
            ClearTextBoxesStudent();
        }


        private void FillTextBoxesStudent(Student student)
        {
            txtStudentName.Text = student.Name;
            txtStudentNumber.Text = student.Number;
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
        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewStudents.SelectedItems[0];
                Student selectedStudent = (Student)selectedItem.Tag;

                FillTextBoxesStudent(selectedStudent);
            }
        }

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
                Student selectedStudent = GetSelectedStudent(); // Check if a student                              // Get new student
                ModifyStudentProperties(selectedStudent);

                StudentService studentService = new StudentService();        // Add new drink
                studentService.AddStudent(selectedStudent);

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
                Student selectedStudent = GetSelectedStudent();
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButtons.YesNo);   // Get confirmation from the user

                if (answer == DialogResult.Yes)
                {
                    StudentService studentService = new StudentService();
                    studentService.DeleteStudent(selectedStudent);                    // Delete student
                    ShowStudentsPanel();                                             // Show the updated list 
                }
                else if (answer == DialogResult.No)
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
                Student student = GetSelectedStudent();
                ListViewItem selectedItem = listViewStudents.SelectedItems[0];     // Get the selected student from the list view
                Student selectedStudent = (Student)selectedItem.Tag;

                ModifyStudentProperties(selectedStudent);

                StudentService studentService = new StudentService();
                studentService.ModifyStudent(selectedStudent);

                ShowStudentsPanel();                        // Show updated list view 
                ClearTextBoxesStudent();

                MessageBox.Show("Student modified");       // Show confirmation
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                 // Throw exception
            }
        }

        private Student GetSelectedStudent()
        {
            ListViewItem selectedItem;

            if (listViewStudents.SelectedItems.Count == 0)
            {
                MessageBox.Show("No student selected.");
                return null;
            }
            else
            {
                selectedItem = listViewStudents.SelectedItems[0];     // Get the selected student from the list view
                Student selectedStudent = (Student)selectedItem.Tag;
            }

            return (Student)selectedItem.Tag;   // get the selected student
        }

        private void ClearTextBoxesStudent()
        {
            txtStudentName.Text = null;
            txtStudentNumber.Text = null;
            txtStudentClass.Text = null;
            txtStudentTelephoneNumber.Text = null;
        }


        // Activity participants

        private void activityParticipantsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowParticipantsPanel();
        }
        private void ShowParticipantsPanel()
        {
            //show Participants
            ShowCurrentPanel(pnlParticipants);
            ConfigureActivitySelectionControls();

            try
            {
                //get and display activities
                List<Activity> activities = GetActivity();
                DisplayActivities(activities);


            }
            catch (Exception e)
            {

                MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
            }
        }

        private void ConfigureActivitySelectionControls()
        {
            // Enable row selection to only be able to select one row at a time
            // Assuming listViewActivities is the correct control; adjust if it was meant to be another control
            listViewActivity.FullRowSelect = true;
            listViewActivity.MultiSelect = false;
            // Assuming listViewStudentsWhoAreParticipating and listViewStudentsWhoAreNotParticipating are correct; adjust if necessary
            listViewParticipants.FullRowSelect = true;
            listViewParticipants.MultiSelect = false;

            listViewNotParticipating.FullRowSelect = true;
            listViewNotParticipating.MultiSelect = false;


        }


        private List<Activity> GetActivity()
        {
            ActivityService activityService = new ActivityService();
            List<Activity> activities = activityService.GetActivities();
            return activities;
        }

        private void DisplayActivities(List<Activity> activities)
        {
            listViewActivity.View = View.Details;
            listViewActivity.Items.Clear();

            foreach (Activity activity in activities)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                   activity.Id.ToString(),
                   activity.Name
                });

                item.Tag = activity;

                listViewActivity.Items.Add(item);
            }
        }


        private void listViewActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefillParticipantListViews();

        }

        private void RefillParticipantListViews()
        {
            if (listViewActivity.SelectedItems.Count == 0)
                return;

            ListViewItem selectedItem = listViewActivity.SelectedItems[0];
            Activity activity = (Activity)selectedItem.Tag;

            StudentDao studentDao = new StudentDao();

            try
            {
                List<Student> participatingStudents = studentDao.GetParticipants(activity?.Id ?? 0); // Use null-conditional operator for safety
                List<Student> nonParticipatingStudents = GetNonParticipants(activity?.Id ?? 0); // Use null-conditional operator for safety

                DisplayParticipants(activity.Id, participatingStudents);
                DisplayNonStudentParticipants(activity.Id, nonParticipatingStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayParticipants(int activityId, List<Student> students)
        {
            // Clear the list view for participating students
            listViewParticipants.Items.Clear();
            listViewParticipants.View = View.Details;

            if (listViewParticipants.Columns.Count == 0)
            {
                listViewParticipants.Columns.AddRange(new[]
                {
            new ColumnHeader { Text = "Id", Width = 130 },
            new ColumnHeader { Text = "Name", Width = 100 },
            new ColumnHeader { Text = "Class", Width = 100 }
        });
            }

            // Add a column for Activity ID
            if (!listViewParticipants.Columns.Cast<ColumnHeader>().Any(c => c.Text == "Activity ID"))
            {
                listViewParticipants.Columns.Add("Activity ID", 100);
            }

            // List all students who are participating
            foreach (Student student in students)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
            student.Id.ToString(),
            student.Name,
            student.Class,
            activityId.ToString() // Add the activity ID as a column value
                });
                item.Tag = student;
                listViewParticipants.Items.Add(item);
            }
        }

        private List<Student> GetNonParticipants(int activityNumber)
        {
            List<Student> nonParticipantStudents = new List<Student>();
            try
            {
                StudentDao studentDao = new StudentDao();
                // Assuming GetStudents() fetches all students
                List<Student> allStudents = GetStudents(); // Make sure you have this method implemented

                // Assuming GetParticipants(int activityNumber) correctly fetches participating students for the given activity
                List<Student> participatingStudents = studentDao.GetParticipants(activityNumber); // Assuming this method retrieves participants

                // Filter out the students who are not participating in the given activity
                foreach (Student student in allStudents)
                {
                    if (!participatingStudents.Any(s => s.Id == student.Id))
                    {
                        nonParticipantStudents.Add(student);
                    }
                }
            }

            catch (Exception e)
            {

                MessageBox.Show("Something went wrong while trying to get the participants from the database" + e);
            }
            return nonParticipantStudents;
        }
        // Display all Stuudents Participating

        private void DisplayNonStudentParticipants(int activityId, List<Student> nonParticipants)
        {
            // Clear the list view for non-participating students
            listViewNotParticipating.Items.Clear();
            listViewNotParticipating.View = View.Details;

            // Define columns if not already defined
            if (listViewNotParticipating.Columns.Count == 0)
            {
                listViewNotParticipating.Columns.AddRange(new[]
                {
            new ColumnHeader { Text = "Id", Width = 130 },
            new ColumnHeader { Text = "Name", Width = 100 },
            new ColumnHeader { Text = "Class", Width = 100 },
            new ColumnHeader { Text = "Activity ID", Width = 100 } // Add column for Activity ID
        });
            }

            // Populate the list view with the non-participating students
            foreach (Student nonParticipant in nonParticipants)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
            nonParticipant.Id.ToString(),
            nonParticipant.Name,
            nonParticipant.Class,
            activityId.ToString() // Add the activity ID as a column value
                });
                item.Tag = nonParticipant;
                listViewNotParticipating.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Method for deleting students from an activity
            // Check if both a student and an activity are selected
            if (listViewActivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select activity.");
                return; // Exit the method early
            }
            else if (listViewParticipants.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a student from the participants list fïrst.");
                return; // Exit the method early
            }

            // Retrieve the selected activity and student
            ListViewItem selectedActivity = listViewActivity.SelectedItems[0];
            Activity activity = (Activity)selectedActivity.Tag;
            int activityId = activity.Id; // Get the activity ID

            ListViewItem selectedParticipant = listViewParticipants.SelectedItems[0];
            Student student = (Student)selectedParticipant.Tag;
            int studentId = student.Id; // Get the student ID

            // Proceed with deleting the participant

            if (MessageBox.Show($"Are you sure that you wish to remove this participant from the activity {activity.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ParticipantsDAO participantDao = new ParticipantsDAO();
                participantDao.DeleteParticipants(studentId, activityId);
                // Update the UI
                RefillParticipantListViews();
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            // //Method for adding students to an activity
            // Check if both a student and an activity are selected
            if (listViewActivity.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select activity.");
                return; // Exit the method early
            }
            else if (listViewNotParticipating.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select student.");
                return; // Exit the method early
            }

            // Retrieve the selected activity and student
            ListViewItem selectedActivity = listViewActivity.SelectedItems[0];
            Activity activity = (Activity)selectedActivity.Tag;
            int activityId = activity.Id; // Get the activity ID

            ListViewItem selectedParticipant = listViewNotParticipating.SelectedItems[0];
            Student student = (Student)selectedParticipant.Tag;
            int studentId = student.Id; // Get the student ID

            // Proceed with deleting the participant
            ParticipantsDAO participantDao = new ParticipantsDAO();
            participantDao.AddParticpants(studentId, activityId);

            // Update the UI
            RefillParticipantListViews();
        }
    }
}