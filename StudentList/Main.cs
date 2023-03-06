using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;


namespace StudentList
{
    public partial class Main : Form
    {
        public string _filePath =
               Path.Combine(Environment.CurrentDirectory, "students.txt");

        private FileHelper<List<Student>> fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);
        public Main()
        {
            InitializeComponent();

            RefreshDiary();

            SetColumnHeader();
        }

        private void RefreshDiary()
        {
            var students = fileHelper.DeserializeFromFIle();

            dgvDiary.DataSource = students;
        }


        private void SetColumnHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Uwagi";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Technologia";
            dgvDiary.Columns[6].HeaderText = "Fizyka";
            dgvDiary.Columns[7].HeaderText = "Język Polski";
            dgvDiary.Columns[8].HeaderText = "Język Obcy";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.ShowDialog();



        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Prosze zaznacz uczna którego dane chcesz edytowac");

            }

            var addEditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Prosze zaznacz uczna którego dane chcesz usunąc");
                return;
            }
            var selectedStudent = dgvDiary.SelectedRows[0];

            string selected = (selectedStudent.Cells[1].Value.ToString() + " " +
                selectedStudent.Cells[2].Value.ToString()).Trim();

            var confirmDelete =
                MessageBox.Show($"Czy na pewno chcesz usunąc uczna{selected}",
                "Usuwanie ucznia", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value.ToString()));
                RefreshDiary();
            }


        }
        private void DeleteStudent(int id)
        {
            var students = fileHelper.DeserializeFromFIle();
            students.RemoveAll(x => x.Id == id);
            fileHelper.SerializeToFile(students);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void dgvDiary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
