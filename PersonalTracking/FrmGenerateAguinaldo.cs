using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTracking
{
    public partial class FrmGenerateAguinaldo : Form
    {
        public FrmGenerateAguinaldo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dpStart.Value > dpFinish.Value)
                MessageBox.Show("La fecha de inicio tiene que ser menor a la de fin");
            else if(dpStart.Value == dpFinish.Value)
                MessageBox.Show("La fecha de inicio tiene que ser diferente a la de fin");

            else
            {
                DialogResult result = MessageBox.Show("Estás seguro que quieres generar aguinaldos?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    BLL.SalaryBLL.UpdateSalary(dpStart.Value,dpFinish.Value);
                    MessageBox.Show("El aguinaldo fue actualizado!");
                }
            }
        
        }
    }
}
