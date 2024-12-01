using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_customer : System.Web.UI.UserControl
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
            string Query = "SELECT TOP 1 C_id FROM customer ORDER BY C_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                tb_id_customer.Value = "B0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "C" + nextId.ToString("D4");
                tb_id_customer.Value = newid;
            }
        }

        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = "Select * from customer where C_name  LIKE  N'%{0}%' OR C_tel LIKE N'%{1}%' ";
                Query = string.Format(Query, keyword, keyword);
            }
            else
            {
                Query = "Select * from customer";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        protected void ClearInput()
        {
            ddl_name_customer.Value = "";
            ddl_name_email.Value = "";
            tb_address.Text = "";
            tb_tel.Value = "";
        }

        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string id = row.Cells[0].Text;
            string name = row.Cells[1].Text;
            string address = row.Cells[4].Text;
            string tel = row.Cells[2].Text;
            string e_mail = row.Cells[3].Text;

            if (e.CommandName == "EditRow")
            {
                tb_id_customer.Value = id;
                ddl_name_customer.Value = name;
                tb_tel.Value = tel;
                ddl_name_email.Value = e_mail;
                tb_address.Text = address;
                clean.Visible = true;

            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM customer WHERE C_ID ='{0}'";
                Query = string.Format(Query, id);
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
            ddl_name_customer.Value = "";
            ddl_name_email.Value = "";
            tb_address.Text = "";
            tb_tel.Value = "";

        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                string id = tb_id_customer.Value;
                string name = ddl_name_customer.Value;
                string tel = tb_tel.Value;
                string e_mail = ddl_name_email.Value;
                string address = tb_address.Text;

                if (tb_id_customer.Value == "" || ddl_name_customer.Value == "" || tb_tel.Value == "" || ddl_name_email.Value == ""
                    || tb_address.Text == "")
                {
                 

                    string edit_id = "Select * from customer where C_id = '{0}'";
                    edit_id = string.Format(edit_id, id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM customer WHERE C_ID ='{0}'";
                        Query = string.Format(Query, id);
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
                        string edit_id = "Select * from customer where C_id = '{0}'";
                        edit_id = string.Format(edit_id, id);
                        DataTable dt = Con.GetData(edit_id);

                        if (dt.Rows.Count == 0)
                        {
                            string Query = "insert into customer VALUES ('{0}', N'{1}', N'{2}', '{3}', N'{4}')";
                            Query = string.Format(Query, id, name, address, tel, e_mail);
                            Con.SetData(Query);
                            ErrMsg.CssClass = "text-primary";
                            ErrMsg.Text = "บันทึกสำเร็จ";
                        }
                        else
                        {
                            string Query = "update  customer set C_name =  N'{0}', C_address = N'{1}',C_tel ='{2}',C_email =N'{3}' where C_id = '{4}'";
                            Query = string.Format(Query, name, address, tel, e_mail, id);
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
                index.searchid = null;
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

    }
}