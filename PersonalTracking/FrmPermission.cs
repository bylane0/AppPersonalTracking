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
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TimeSpan PermissionDay;
        public bool isUpdate = false;
        public PermissionDetailDTO detail = new PermissionDetailDTO();
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
            if (isUpdate)
            {
                dpStart.Value = (DateTime)detail.StartDate;
                dpFinish.Value = (DateTime)detail.EndDate;
                txtDayAmount.Text = detail.PermissionDayAmount.ToString();
                txtExplanation.Text = detail.Explanation;
                txtUserNo.Text = detail.UserNo.ToString();

            }
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay=dpFinish.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void dpFinish_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpFinish.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("Cambie la fecha de inicio o la fecha de fin");
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("La cantidad de días tiene que ser positiva");
            else if(txtExplanation.Text.Trim() == "")
                MessageBox.Show("La explicación está vacía");
            else
            {
                PERMISSION permission = new PERMISSION();
                if (!isUpdate)
                {
                    permission.EmployeeID = UserStatic.EmployeeID;
                    permission.PermissionState = 1;
                    permission.PermissionStartDate = dpStart.Value.Date;
                    permission.PermissionEndDate = dpFinish.Value.Date;
                    permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                    permission.PermissionExplain = txtExplanation.Text;
                    PermissionBLL.AddPermission(permission);
                    MessageBox.Show("El permiso fue agregado!");
                    dpStart.Value = DateTime.Today;
                    dpFinish.Value = DateTime.Today;
                    txtDayAmount.Clear();
                    txtExplanation.Clear();
                }
                else if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("Estás seguro que quieres hacer los cambios?", "Warning", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        permission.ID = detail.PermissionID;
                        permission.PermissionExplain = txtExplanation.Text;
                        permission.PermissionStartDate = dpStart.Value;
                        permission.PermissionEndDate = dpFinish.Value;
                        permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                        PermissionBLL.UpdatePermission(permission);
                        MessageBox.Show("El permiso fue actualizado!");
                        this.Close();
                    }
                }

            }
        }
    }
}
