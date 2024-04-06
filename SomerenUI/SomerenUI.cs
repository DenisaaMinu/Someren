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

                currentPanel.Show();
            }
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
                ListViewItem li = new ListViewItem(student.Name);
                li.SubItems.Add(student.Number);
                li.SubItems.Add(student.TelephoneNumber);
                li.SubItems.Add(student.Class);
                //test
                li.Tag = student;   // link student object to listview item
                listViewStudents.Items.Add(li);
            }
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
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
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
        private void activitySupervisorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Activity participants

        private void activityParticipantsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowParticipantsPanel();
        }
        private void ShowParticipantsPanel()
        {
            //show Participants
            ShowCurrentPanel(pnlSupervisors);
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
    private void activitySupervisorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowSupervisorsPanel();
    }
    private void ShowSupervisorsPanel()
    {
        ShowCurrentPanel(pnlSupervisors);

        try
        {
            List<Activity> activities = GetActivity();
            DisplayActivities(activities);

            List<Teacher> supervisors = GetSupervisors();
            DisplaySupervisors(supervisors);

        }
        catch (Exception ex)
        {
            MessageBox.Show("Something went wrong while loading the data: " + ex.Message);
        }
    }
    private List<Teacher> GetSupervisors()
    {
        TeacherService teacherService = new TeacherService();
        List<Teacher> supervisors = teacherService.GetSupervisors();
        return supervisors;
    }
    private void DisplaySupervisors(List<Teacher> supervisors)
    {
        listViewSupervisors.Items.Clear();

        foreach (Teacher teacher in supervisors)
        {
            ListViewItem item = new ListViewItem(teacher.FirstName);
            item.SubItems.Add(teacher.LastName);

            item.Tag = teacher;

            listViewSupervisors.Items.Add(item);
        }
    }
    private List<Teacher> GetActivitySupervisors(int activityId)
    {
        ActivityService activityService = new ActivityService();
        List<Teacher> activitySupervisors = activityService.GetActivitySupervisors(activityId);
        return activitySupervisors;
    }
    private void DisplayActivitySupervisors(List<Teacher> activitySupervisors)
    {
        listViewActivitySupervisors.Items.Clear();

        foreach (Teacher teacher in activitySupervisors)
        {
            ListViewItem item = new ListViewItem(teacher.FirstName);
            item.SubItems.Add(teacher.LastName);

            item.Tag = teacher;

            listViewSupervisors.Items.Add(item);
        }
    }
    private List<Teacher> GetActivitySupervisors(int activityId)
    {
        ActivityService activityService = new ActivityService();
        List<Teacher> activitySupervisors = activityService.GetActivitySupervisors(activityId);
        return activitySupervisors;
    }
    private void DisplayActivitySupervisors(List<Teacher> activitySupervisors)
    {
        listViewActivitySupervisors.Items.Clear();

        foreach (Teacher teacher in activitySupervisors)
        {
            ListViewItem item = new ListViewItem(teacher.FirstName);
            item.SubItems.Add(teacher.LastName);

            item.Tag = teacher;

            listViewSupervisors.Items.Add(item);
        }
    }
}
