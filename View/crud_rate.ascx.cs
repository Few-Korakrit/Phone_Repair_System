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
using System.EnterpriseServices;


namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_rate : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            GenerateNewID();
            ShowAuthors();
            showemployee();
            showcustomer();
        }
        private void GenerateNewID()
        {
            string Query = "SELECT TOP 1 rate_id FROM rate ORDER BY rate_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                rate_id.Value = "RATE0001";

            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(4)) + 1;
                string newid = "RATE" + nextId.ToString("D4");
                rate_id.Value = newid;
            }
        }
        private void showemployee()
        {
            string QueryddlBrand = "select * from Employee";
            DataTable dtBrand = Con.GetData(QueryddlBrand);
            if (dtBrand.Rows.Count > 0)
            {
                E_id.DataSource = dtBrand;
                E_id.DataTextField = "E_Name";
                E_id.DataValueField = "E_Id";
                E_id.DataBind();
            }
            //E_Id.Items.Insert(0, new ListItem("เลือกยี่ห้ออะไหล่", "0"));

        }
        private void showcustomer()
        {
            string QueryddlBrand = "select * from customer";
            DataTable dtBrand = Con.GetData(QueryddlBrand);
            if (dtBrand.Rows.Count > 0)
            {
                C_id.DataSource = dtBrand;
                C_id.DataTextField = "C_Name";
                C_id.DataValueField = "C_Id";
                C_id.DataBind();
            }
            //E_Id.Items.Insert(0, new ListItem("เลือกยี่ห้ออะไหล่", "0"));

        }

        private void ShowAuthors()
        {
            string Query = @"Select rate.*, Customer.C_Name, Employee.E_Name ,Customer.C_tel
                            from rate 
                            inner join Customer on Rate.C_ID = Customer.C_ID
                            inner join Employee on Rate.E_ID = Employee.E_ID";
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = @"Select rate.*, Customer.C_Name, Employee.E_Name ,Customer.C_tel
                            from rate 
                            inner join Customer on Rate.C_ID = Customer.C_ID
                            inner join Employee on Rate.E_ID = Employee.E_ID
                            where rate_id  LIKE  N'%{0}%' or Customer.C_Name  LIKE  N'%{0}%' or Customer.C_tel  LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = @"Select rate.*, Customer.C_Name, Employee.E_Name  ,Customer.C_tel
                            from rate 
                            inner join Customer on Rate.C_ID = Customer.C_ID
                            inner join Employee on Rate.E_ID = Employee.E_ID";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }

        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string Rate_ID = row.Cells[0].Text;
            string c_id = row.Cells[1].Text;
            string e_id = row.Cells[2].Text;
            string C_name = row.Cells[3].Text;
            string C_tel = row.Cells[4].Text;
            string E_name = row.Cells[5].Text;
            string Date = row.Cells[6].Text;
            string Rate_Broken = row.Cells[7].Text;
            string Rate_Total = row.Cells[8].Text;
            string Note = row.Cells[9].Text;
            string Status = row.Cells[10].Text;
           

            if (e.CommandName == "EditRow")
            {
                rate_id.Value = Rate_ID;
                //brand_Id.Items.Insert(0, new ListItem(B_name, B_id));
                E_id.SelectedValue = e_id;
                C_id.SelectedValue = c_id;
                //ddl_name_customer.Value = C_name;
                //tb_name_employee.Value = E_name;

                try
                {
                    DateTime Rate_Date = DateTime.ParseExact(Date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    tb_date.Value = Rate_Date.ToString("yyyy-MM-dd");
                }
                catch (FormatException ex)
                {
                    // Handle exception, maybe log the error or display a message
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }
                Broken.Value = Rate_Broken;
                tb_price.Text = Rate_Total;
                note.Value = Note;
                status.SelectedValue = Status;
                clean.Visible = true;
            }
            else if (e.CommandName == "print")
            {


                view_note.Text = Note;
                rateidprint.Text = Rate_ID;
                cname.Text = C_name;
                phone.Text = "0123456789";
                printbroken.Text = Rate_Broken;
                daterepair.Text = Date;
                rate_total.Text = Rate_Total;
                print_ename.Text = E_name;
                //ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                //Response.Write("<script>alert('Data inserted successfully')</script>");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "refresh", "location.reload();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "window.print();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM rate WHERE rate_id ='{0}'";
                Query = string.Format(Query, Rate_ID);
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
            E_id.SelectedValue = "";
            C_id.SelectedValue = "";
            tb_date.Value = "";
            Broken.Value = "";
            tb_price.Text = "";
            note.Value = "";
            status.SelectedValue = "";
            clean.Visible = true;

        }
        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                string Rate_ID = rate_id.Value;
                //string C_name = ddl_name_customer.Value;
                //string E_name = tb_name_employee.Value;
                string Rate_Date = tb_date.Value;
                string Rate_Broken = Broken.Value;
                string Rate_Total = tb_price.Text;
                string Note = note.Value;
                string Status = status.SelectedValue;
                string C_Id = C_id.SelectedValue;
                string E_Id = E_id.SelectedValue;


                if (rate_id.Value == "" || E_id.SelectedValue == "" || C_id.SelectedValue == "" || tb_date.Value == ""
                    || Broken.Value == "" || tb_price.Text == "" || note.Value == "" || status.SelectedValue == "")
                {
                    

                    string edit_id = "Select * from rate where Rate_ID = '{0}'";
                    edit_id = string.Format(edit_id, Rate_ID);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM rate WHERE rate_id ='{0}'";
                        Query = string.Format(Query, Rate_ID);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                    }
                }
                else
                {
                    string edit_id = "Select * from rate where Rate_ID = '{0}'";
                    edit_id = string.Format(edit_id, Rate_ID);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into rate VALUES ('{0}', '{1}', N'{2}', '{3}', N'{4}', N'{5}', '{6}', '{7}')";
                        Query = string.Format(Query, Rate_ID, Rate_Date, Rate_Broken, Rate_Total, Note, Status, C_Id, E_Id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  rate set Rate_Date =  '{0}', Rate_Broken = N'{1}',Rate_Total ='{2}',Note =N'{3}',Status =N'{4}',C_Id ='{5}',E_Id ='{6}' where Rate_ID = '{7}'";
                        Query = string.Format(Query,  Rate_Date, Rate_Broken, Rate_Total, Note, Status, C_Id, E_Id, Rate_ID);
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

        protected void cancel_Click(object sender, EventArgs e)
        {

            ClearInput();
            GenerateNewID();
            ShowAuthors();
        }

        protected void ClearInput()
        {
            rate_id.Value = "";
            E_id.SelectedValue = "";
            C_id.SelectedValue = "";
            tb_date.Value = "";
            Broken.Value = "";
            tb_price.Text = "";
            note.Value = "";
            status.SelectedValue = "";
        }
    }
}