using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_type : System.Web.UI.UserControl
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
            string Query = "SELECT TOP 1 T_id FROM Type ORDER BY T_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                tb_id_type.Value = "T0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "T" + nextId.ToString("D4");
                tb_id_type.Value = newid;
                tb_name_type.Text = "";
            }
        }

        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = "Select * from Type where T_name  LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = "Select * from Type";
            }

            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }




        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchtype); // เรียกดูค่า keyword ใน Session 
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string id = row.Cells[0].Text;
            string name = row.Cells[1].Text;


            if (e.CommandName == "EditRow")
            {
                tb_id_type.Value = id;
                tb_name_type.Text = name;
                clean.Visible = true;

            }
            else if (e.CommandName == "DeleteRow")
            {

                string Query = "DELETE FROM Type WHERE T_ID ='{0}'";
                Query = string.Format(Query, id);
                Con.SetData(Query);
                ErrMsg.CssClass = "text-primary";
                ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                //ShowAuthors();
                GenerateNewID();
            }
            index.searchtype = null;
            ShowAuthors(index.searchtype);

        }

        protected void clean_Click(object sender, EventArgs e)
        {
            clean.Visible = true;
            tb_name_type.Text = "";
        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_id_type.Value == "" || tb_name_type.Text == "")
                {
                  

                    string T_id = tb_id_type.Value;
                    string T_name = tb_name_type.Text;
                    string edit_id = "Select * from Type where T_id = '{0}'";
                    edit_id = string.Format(edit_id, T_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM Type WHERE T_ID ='{0}'";
                        Query = string.Format(Query, T_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        //ShowAuthors();
                        GenerateNewID();

                    }
                }
                else
                {

                    string T_id = tb_id_type.Value;
                    string T_name = tb_name_type.Text;
                    string edit_id = "Select * from Type where T_id = '{0}'";
                    edit_id = string.Format(edit_id, T_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into type VALUES ('{0}', N'{1}')";
                        Query = string.Format(Query, T_id, T_name);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                        GenerateNewID();
                        ShowAuthors();
                    }
                    else
                    {
                        string Query = "update  type set T_name =  N'{0}' where T_id = '{1}'";
                        Query = string.Format(Query, T_name, T_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";

                    }

                }
                GenerateNewID();
                ShowAuthors();
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }

        }

        protected void submit_search_Click(object sender, EventArgs e)
        {

            string keyword = tb_search.Text.Trim(); // รับค่าค้นหาจาก TextBox
            index.searchtype = keyword;
            ShowAuthors(index.searchtype); // เรียกฟังก์ชัน ShowData โดยส่งค่า keyword ไปค้นหา
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            GenerateNewID();
            ShowAuthors();
        }

       
    }

}