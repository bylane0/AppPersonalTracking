﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;
namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            
        }
   
        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO(); 
        public bool isUpdate= false;
        string imagePath = "";
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            comboFull = true; //mostrar todos los departamentos y posiciones
            if (isUpdate)
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtUserNo.Text = detail.UserNo.ToString();
                txtPassword.Text = detail.Password;
                chAdmin.Checked = Convert.ToBoolean(detail.isAdmin);
                txtAdress.Text = detail.Adress;
                dateTimePicker1.Value = Convert.ToDateTime(detail.BirthDay);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                txtSalary.Text = detail.Salary.ToString();
                imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                txtImagePath.Text = imagePath;
                pictureBox1.ImageLocation = imagePath;
                if (!UserStatic.isAdmin)
                {
                    chAdmin.Enabled = false;
                    txtUserNo.Enabled = false;
                    txtSalary.Enabled = false;
                    cmbDepartment.Enabled = false;
                    cmbPosition.Enabled = false;
                }
            }
        }
        bool comboFull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
         
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        string fileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("UserNo está vacío");
        
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password está vacío");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name está vacío");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname está vacío");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary está vacío");
            else if (cmbDepartment.Text.Trim() == "")
                MessageBox.Show("Department está vacío");
            else if (cmbPosition.Text.Trim() == "")
                MessageBox.Show("Position está vacío");
            else
            {
                if (!isUpdate)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                        MessageBox.Show("El 'UserNo' ya se encuentra en uso por otro usuario");
                    else
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.Name = txtName.Text;
                        employee.isAdmin = chAdmin.Checked;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.Surname = txtSurname.Text;
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Adress = txtAdress.Text;
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.ImagePath = fileName;
                        EmployeeBLL.AddEmployee(employee);
                        if (txtImagePath.Text.Trim() != "")
                        {
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        }

                        MessageBox.Show("El empleado fue creado!");
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        chAdmin.Checked = false;
                        txtName.Clear();
                        txtSurname.Clear();
                        txtSalary.Clear();
                        txtAdress.Clear();
                        txtImagePath.Clear();
                        pictureBox1.Image = null;
                        comboFull = false;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbPosition.SelectedIndex = -1;
                        comboFull = true;
                        dateTimePicker1.Value = DateTime.Today;
                    }
                   
                }
                else if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Estás seguro que quieres hacer los cambios?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        if(txtImagePath.Text != imagePath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                                File.Delete(@"images\\" + detail.ImagePath);
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            employee.ImagePath = fileName;
                        }
                        else
                        {
                            employee.ImagePath = detail.ImagePath;
                        }
                        employee.ID = detail.EmployeeID;
                        employee.UserNo = detail.UserNo;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.isAdmin = chAdmin.Checked;
                        employee.Password = txtPassword.Text;
                        employee.Adress = txtAdress.Text;
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        EmployeeBLL.UpdateEmployee(employee);
                        MessageBox.Show("El empleado fue actualizado!");
                        this.Close();

                    }
                }
                
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        bool isUnique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("El userNo está vacío");
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique) {
                    MessageBox.Show("Ya existe otro empleado con el mismo identificador");
                }
                else
                {
                    MessageBox.Show("El identificador se puede usar");
                }
            }
        }
    }
}
