using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_order : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            GenerateNewID();
            //Sessionauthors();
            ShowAuthors();
            showtype();
            showbrand();
            if (!IsPostBack)
            {
                
            }
           
        }
        private void GenerateNewID()
        {
            string Query = "SELECT TOP 1 O_id FROM TableOrder ORDER BY O_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                tb_id_order.Value = "O0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "O" + nextId.ToString("D4");
                tb_id_order.Value = newid;
            }
        }

   

      

        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = @"select TableOrder.*, Type.T_name, Brand.B_Name from TableOrder 
                            inner join Type on TableOrder.T_ID = Type.T_id 
                            inner join Brand on TableOrder.B_ID = Brand.B_ID
                            where O_Name  LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = @"select TableOrder.*, Type.T_name, Brand.B_Name from TableOrder 
                            inner join Type on TableOrder.T_ID = Type.T_id 
                            inner join Brand on TableOrder.B_ID = Brand.B_ID";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        private void  showtype()
        {
            string Queryddltype = "select * from type";
            Session["GridViewData"] = Con.GetData(Queryddltype);
            DataTable dttype = Session["GridViewData"] as DataTable;
            //DataTable dttype = Con.GetData(Queryddltype);
            if (dttype.Rows.Count > 0)
            {
                type_id.DataSource = dttype;
                type_id.DataTextField = "T_name";
                type_id.DataValueField = "T_Id";
                type_id.DataBind();
            }


            type_id.Items.Insert(0, new ListItem("เลือกประเภทอะไหล่", "0"));


        }

        private void showbrand()
        {
            string QueryddlBrand = "select * from Brand";
            DataTable dtBrand = Con.GetData(QueryddlBrand);
            if (dtBrand.Rows.Count > 0)
            {
                brand_Id.DataSource = dtBrand;
                brand_Id.DataTextField = "B_Name";
                brand_Id.DataValueField = "B_Id";
                brand_Id.DataBind();
            }
            brand_Id.Items.Insert(0, new ListItem("เลือกยี่ห้ออะไหล่", "0"));

        }
        

        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string O_id = row.Cells[0].Text;
            string O_name = row.Cells[1].Text;
            string O_Price = row.Cells[2].Text;
            string O_unit = row.Cells[3].Text;
            string O_Description = row.Cells[4].Text;
            string T_name = row.Cells[5].Text;
            string B_name = row.Cells[6].Text;
            string T_id = row.Cells[7].Text;
            string B_id = row.Cells[8].Text;

            if (e.CommandName == "EditRow")
            {
                tb_id_order.Value = O_id;
                ddl_name_brand_order.Text = O_name;
                tb_price.Text = O_Price;
                tb_unit.Text = O_unit;
                tb_detail.Text = O_Description;
                type_id.SelectedValue = T_id;
                brand_Id.SelectedValue = B_id;
                clean.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM TableOrder WHERE O_ID ='{0}'";
                Query = string.Format(Query, O_id);
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
            ddl_name_brand_order.Text = "";
            tb_price.Text = "";
            tb_unit.Text = "";
            tb_detail.Text = "";
            showtype();
            showbrand();
        }
        protected void save_order_Click(object sender, EventArgs e)
        {
            try
            {
                string O_id = tb_id_order.Value;
                string O_name = ddl_name_brand_order.Text;
                string O_Price = tb_price.Text;
                string O_unit = tb_unit.Text;
                string O_Description = tb_detail.Text;
                string T_id = type_id.SelectedValue;
                string B_id = brand_Id.SelectedValue;

                if (tb_id_order.Value == "" || ddl_name_brand_order.Text == "" || tb_price.Text == "" || tb_unit.Text == ""
                    || tb_detail.Text == "" || type_id.SelectedIndex == -1 || brand_Id.SelectedIndex == -1)
                {

                    string edit_id = "Select * from TableOrder where O_id = '{0}'";
                    edit_id = string.Format(edit_id, O_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";

                    }
                    else
                    {
                        string Query = "DELETE FROM TableOrder WHERE O_ID ='{0}'";
                        Query = string.Format(Query, O_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                    }
                }
                else
                {
                    string edit_id = "Select * from TableOrder where O_id = '{0}'";
                    edit_id = string.Format(edit_id, O_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into TableOrder VALUES ('{0}', N'{1}', N'{2}', N'{3}', N'{4}', '{5}', '{6}')";
                        Query = string.Format(Query, O_id, O_name, O_Price, O_unit, O_Description, T_id, B_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  TableOrder set O_name =  N'{0}', O_Price = '{1}',O_unit =N'{2}',O_Description =N'{3}',T_id =N'{4}',B_id =N'{5}' where O_id = '{6}'";
                        Query = string.Format(Query, O_name, O_Price, O_unit, O_Description, T_id, B_id, O_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
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
        private void ClearInput()
        {
            ddl_name_brand_order.Text = "";
            tb_price.Text = "";
            tb_unit.Text = "";
            tb_detail.Text = "";
            showtype();
            showbrand();
        }

        protected void cancel_order_Click(object sender, EventArgs e)
        {
            ClearInput();
            GenerateNewID();

        }
    }
}