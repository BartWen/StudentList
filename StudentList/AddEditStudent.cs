﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StudentList
{
    public partial class AddEditStudent : Form
    {

        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        public AddEditStudent(int Id = 0)
        {
            InitializeComponent();

            _studentId = Id;

            
            GetStudentData();
            tbFirstName.Select();

        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytowanie danych ucznia";

                var students = _fileHelper.DeserializeFromFIle();
                var _student = students.FirstOrDefault(x => x.Id == _studentId);
                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym Id");
                FillTextBoxes();

            }
        }

        private void FillTextBoxes()
        {
            tbID.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbTechnology.Text = _student.Technology;
            tbPolishLang.Text = _student.PolishLang;
            tbForeignLang.Text = _student.ForeignLang;
            rtbComments.Text = _student.Comments;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AssignNewIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students.
                OrderByDescending(x => x.Id).FirstOrDefault();


            _studentId = studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            var students = _fileHelper.DeserializeFromFIle();

            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
            {
                AssignNewIdToNewStudent(students);
            }


            AddNewUserToList(students);

           _fileHelper.SerializeToFile(students);

            Close();

        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                ForeignLang = tbForeignLang.Text,
                Math = tbMath.Text,
                Physics = tbMath.Text,
                PolishLang = tbPolishLang.Text,
                Technology = tbTechnology.Text,

            };
        }


        private void AddEditStudent_Load(object sender, EventArgs e)
        {
            
        }

    }
}
