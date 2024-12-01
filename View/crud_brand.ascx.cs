using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Web.UI.WebControls.WebParts;
using ระบบแจ้งซ่อมมือถือ.View;

namespace ระบบแจ้งซ่อมมือถือ
{
    public partial class crud_brand : System.Web.UI.UserControl
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
            string Query = "SELECT TOP 1 B_id FROM Brand ORDER BY B_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                tb_id_brand.Value = "B0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "B" + nextId.ToString("D4");
                tb_id_brand.Value = newid;
                tb_name_brand.Text = "";
            }
        }
      
        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = "Select * from Brand where B_name  LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);


            }
            else
            {
                Query = "Select * from Brand";
            }

            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        

        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid); 
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string id = row.Cells[0].Text;  
            string name = row.Cells[1].Text; 

            if (e.CommandName == "EditRow")
            {
                tb_id_brand.Value = id;
                tb_name_brand.Text = name;
                clean.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM brand WHERE B_ID ='{0}'";
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

        protected void cancel_Click(object sender, EventArgs e)
        {
            GenerateNewID();
            ShowAuthors();
        }

        protected void clean_Click(object sender, EventArgs e)
        {
            clean.Visible = true;
            tb_name_brand.Text = "";
        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_id_brand.Value == "" || tb_name_brand.Text == "")
                {
                    

                    string B_id = tb_id_brand.Value;
                    string B_name = tb_name_brand.Text;
                    string edit_id = "Select * from Brand where B_id = '{0}'";
                    edit_id = string.Format(edit_id, B_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM brand WHERE B_ID ='{0}'";
                        Query = string.Format(Query, B_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                    }

                }

                else
                {

                    string B_id = tb_id_brand.Value;
                    string B_name = tb_name_brand.Text;
                    string edit_id = "Select * from Brand where B_id = '{0}'";
                    edit_id = string.Format(edit_id, B_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into Brand VALUES ('{0}', N'{1}')";
                        Query = string.Format(Query, B_id, B_name);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  Brand set B_name =  N'{0}' where B_id = '{1}'";
                        Query = string.Format(Query, B_name, B_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                   

                }
                index.searchid = null;
                GenerateNewID();
                ShowAuthors();
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }

        }
    }
}