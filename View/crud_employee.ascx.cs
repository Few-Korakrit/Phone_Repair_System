using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud__employee : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            GenerateNewID();
            ShowAuthors();
        }
        private void GenerateNewID()
        {
            string Query = "SELECT TOP 1 E_id FROM employee ORDER BY E_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                tb_id_employee.Value = "E0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "E" + nextId.ToString("D4");
                tb_id_employee.Value = newid;
            }
        }


        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = "Select * from employee where E_name  LIKE  N'%{0}%' or E_tel LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = "Select * from employee";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }
        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid);
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
                tb_id_employee.Value = E_ID;
                ddl_name_employee.Value = E_Name;
                tb_number.Value = E_number;
                tb_address.Value = E_address;
                clean.Visible = true;

                try
                {
                    DateTime Rate_Date = DateTime.ParseExact(Date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    tb_date.Value = Rate_Date.ToString("yyyy-MM-dd");
                }
                catch (FormatException ex)
                {
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }
                tb_tel.Value = E_Tel;
                tb_status.Value = E_status;
                tb_user.Value = E_user;
                tb_password.Value = E_Password;

            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM employee WHERE E_ID ='{0}'";
                Query = string.Format(Query, E_ID);
                Con.SetData(Query);
                ErrMsg.CssClass = "text-primary";
                ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                ShowAuthors();
                GenerateNewID();
            }
            index.searchid = null;
            ShowAuthors(index.searchid);
        }

        protected void submit_search_Click(object sender, EventArgs e)
        {
            string keyword = tb_search.Text.Trim();
            index.searchid = keyword;
            ShowAuthors(index.searchid);
        }

        protected void clean_Click(object sender, EventArgs e)
        {
            clean.Visible = true;
            ddl_name_employee.Value = "";
            tb_number.Value = "";
            tb_address.Value = "";
            tb_date.Value = "";
            tb_tel.Value = "";
            tb_status.Value = "";
            tb_user.Value = "";
            tb_password.Value = "";
        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                string E_ID = tb_id_employee.Value;
                string E_Name = ddl_name_employee.Value;
                string E_number = tb_number.Value;
                string E_date = tb_date.Value;
                string E_address = tb_address.Value;
                string E_Tel = tb_tel.Value;
                string E_status = tb_status.Value;
                string E_user = tb_user.Value;
                string E_Password = tb_password.Value;

                if (tb_id_employee.Value == "" || ddl_name_employee.Value == "" || tb_number.Value == "" || tb_address.Value == ""
                    || tb_date.Value == "" || tb_tel.Value == "" || tb_status.Value == "" || tb_user.Value == "" || tb_password.Value == "")
                {
                    

                   
                    string edit_id = "Select * from employee where E_id = '{0}'";
                    edit_id = string.Format(edit_id, E_ID);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM employee WHERE E_ID ='{0}'";
                        Query = string.Format(Query, E_ID);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                    }
                }
                else
                {

                    string input = tb_tel.Value;

                    if (input.Length == 10 && input.All(char.IsDigit)) // ตรวจสอบว่ามีเฉพาะตัวเลขและไม่เกิน 10 หลัก
                    {
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
                    else
                    {
                        ErrMsg.Text = "กรุณากรอกโทรศัพท์มือถือเป็นตัวเลขจำนวน 10 หลัก";
                        ErrMsg.CssClass = "text-danger";
                    }
                   


                }
                ClearInput();
                GenerateNewID();
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
            GenerateNewID();
            ShowAuthors();
        }

        protected void ClearInput()
        {
            ddl_name_employee.Value = "";
            tb_number.Value = "";
            tb_address.Value = "";
            tb_date.Value = "";
            tb_tel.Value = "";
            tb_status.Value = "";
            tb_user.Value = "";
            tb_password.Value = "";
        }

    }
}