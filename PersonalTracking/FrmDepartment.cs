using System;
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
namespace PersonalTracking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim() == "")
            {
                MessageBox.Show("No se aceptan campos vacíos");
            }
            else
            {
                DEPARTMENT department = new DEPARTMENT();
                if(!isUpdate)
                {
                    department.DepartmentName = txtDepartment.Text;
                    BLL.DepartmentBLL.AddDepartment(department);
                    MessageBox.Show("El departamento fue creado!");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Estás seguro que quieres hacer los cambios?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        department.ID = detail.ID;
                        department.DepartmentName = txtDepartment.Text;
                        DepartmentBLL.UpdateDepartment(department);
                        MessageBox.Show("El departamento fue actualizado!");
                        this.Close();
                    }
                }
            }
         
        }
        public bool isUpdate = false;
        public DEPARTMENT detail = new DEPARTMENT();
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtDepartment.Text = detail.DepartmentName;

        }
    }
}
