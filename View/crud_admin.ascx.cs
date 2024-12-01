using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_admin : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAuthors();
            Readonly();

        }
        private void ShowAuthors()
        {
            string Query = "Select * from employee where E_level = '1'";
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        private void Readonly()
        {
            tb_id_employee.Enabled = false;
            ddl_name_employee.Enabled = false;
            tb_number.Enabled = false;
            tb_address.Enabled = false;
            tb_date.Enabled = false;
            tb_tel.Enabled = false;
            tb_status.Enabled = false;
            tb_user.Enabled = false;
            tb_password.Enabled = false;
        }
        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string E_ID = row.Cells[0].Text;
            string E_Name = row.Cells[1].Text;
            string E_number = row.Cells[2].Text;
            string Date = row.Cells[3].Text;
            string E_address = row.Cells[4].Text;
            string E_Tel = row.Cells[5].Text;
            string E_status = row.Cells[6].Text;
            string E_user = row.Cells[7].Text;
            string E_Password = row.Cells[8].Text;

            if (e.CommandName == "EditRow")
            {

                ddl_name_employee.Enabled = true;
                tb_number.Enabled = true;
                tb_address.Enabled = true;
                tb_date.Enabled = true;
                tb_tel.Enabled = true;
                tb_status.Enabled = true;
                tb_user.Enabled = true;
                tb_password.Enabled = true;
                try
                {
                    tb_id_employee.Text = E_ID;
                    ddl_name_employee.Text = E_Name;
                    tb_number.Text = E_number;
                    tb_address.Text = E_address;
                    tb_tel.Text = E_Tel;
                    tb_status.Text = E_status;
                    tb_user.Text = E_user;
                    tb_password.Text = E_Password;

                    DateTime Rate_Date = DateTime.ParseExact(Date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    tb_date.Text   = Rate_Date.ToString("yyyy-MM-dd");
                }
                catch (FormatException ex)
                {
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }


            }

        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_id_employee.Text == "" || ddl_name_employee.Text == "" || tb_number.Text == "" || tb_address.Text == ""
                    || tb_date.Text == "" || tb_tel.Text == "" || tb_status.Text == "" || tb_user.Text == "" || tb_password.Text == "")
                {
                    ErrMsg.CssClass = "text-danger";
                    ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                }
                else
                {
                    string E_ID = tb_id_employee.Text;
                    string E_Name = ddl_name_employee.Text;
                    string E_number = tb_number.Text;
                    string E_date = tb_date.Text;
                    string E_address = tb_address.Text;
                    string E_Tel = tb_tel.Text;
                    string E_status = tb_status.Text;
                    string E_user = tb_user.Text;
                    string E_Password = tb_password.Text;
                    string edit_id = "Select * from employee where E_id = '{0}'";
                    edit_id = string.Format(edit_id, E_ID);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into employee VALUES ('{0}', N'{1}', '{2}', '{3}', N'{4}', '{5}', N'{6}', N'{7}', N'{8}',{9})";
                        Query = string.Format(Query, E_ID, E_Name, E_number, E_date, E_address, E_Tel, E_status, E_user, E_Password, "2");
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  employee set E_Name =  N'{0}', E_number = '{1}',E_date ='{2}',E_address =N'{3}',E_Tel ='{4}',E_status =N'{5}',E_user =N'{6}',E_Password =N'{7}' where E_id = '{8}'";
                        Query = string.Format(Query, E_Name, E_number, E_date, E_address, E_Tel, E_status, E_user, E_Password, E_ID);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }


                }
                ClearInput();
                ShowAuthors();
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {

            ClearInput();
            ShowAuthors();
        }

        protected void ClearInput()
        {
            ddl_name_employee.Text = "";
            tb_number.Text = "";
            tb_address.Text = "";
            tb_date.Text = "";
            tb_tel.Text = "";
            tb_status.Text = "";
            tb_user.Text = "";
            tb_password.Text = "";
        }
    }
}