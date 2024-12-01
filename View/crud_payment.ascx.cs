using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_payment : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            GenerateNewID();
            ShowAuthors();
            showRepair();
        }
        private void GenerateNewID()
        {
            string Query = "SELECT TOP 1 P_id FROM payment ORDER BY P_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                P_id.Value = "P0001";
            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "P" + nextId.ToString("D4");
                P_id.Value = newid;
            }
        }

        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = @"select Payment.P_id,Payment.P_date,Customer.C_Name,Customer.C_Tel,Customer.C_Email,
                            Repair.R_id,Repair.R_date,Repair.R_status,Repair.R_total,rate_total,rate.note
                            from Payment
                            inner join Repair on Payment.R_id = Repair.R_id
                            inner join rate on Repair.Rate_id = rate.Rate_id
                            inner join Customer on Repair.C_id = Customer.C_ID
                            where P_id  LIKE  N'%{0}%' or Customer.C_name  LIKE  N'%{0}%' or Customer.C_Tel  LIKE  N'%{0}%'
                            where R_status = N'ซ่อมเสร็จแล้ว'
";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = @"select Payment.P_id,Payment.P_date,Customer.C_Name,Customer.C_Tel,Customer.C_Email,
                            Repair.R_id,Repair.R_date,Repair.R_status,Repair.R_total,rate_total,rate.note
                            from Payment
                            inner join Repair on Payment.R_id = Repair.R_id                     
                            inner join rate on Repair.Rate_id = rate.Rate_id
                            inner join Customer on Repair.C_id = Customer.C_ID
                            where R_status = N'ซ่อมเสร็จแล้ว'
                            ";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }
        private void showRepair()
        {
            string Query = "select * from repair  where R_status = N'ซ่อมเสร็จแล้ว'";
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                Repairid.DataSource = data;
                Repairid.DataTextField = "r_id";
                Repairid.DataValueField = "r_id";
                Repairid.DataBind();

                //C_name.Value = "";
            }
            Repairid.Items.Insert(0, new ListItem("เลือกรายการ", "0"));

        }

        protected void Repairid_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Query = @"Select Repair.*, Customer.C_Name,Customer.C_Tel,Customer.C_Email, rate.rate_broken,rate.note
                            from Repair 
                            inner join Customer on Repair.C_ID = Customer.C_ID
                            inner join rate on Repair.rate_id = rate.rate_id
                            where Repair.r_id = '{0}'";

            Query = string.Format(Query, Repairid.SelectedValue);
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                string dtp_date = data.Rows[0][0].ToString();
                string dtP_id = data.Rows[0][1].ToString();
                string dtR_id = data.Rows[0][2].ToString();
                string dtC_name = data.Rows[0][9].ToString();
                string dtC_Tel = data.Rows[0][10].ToString();
                string dtC_Email = data.Rows[0][11].ToString();
                string gvR_date = data.Rows[0][1].ToString();
                string dtR_status = data.Rows[0][3].ToString();
                string dtR_total = data.Rows[0][2].ToString();
                //string dtrate_broken = data.Rows[0][11].ToString();
                string note = data.Rows[0][13].ToString();
                try
                {
                    C_name.Value = dtC_name;
                    C_tel.Value = dtC_Tel;
                    C_email.Value = dtC_Email;
                    DateTime dtR_date = DateTime.ParseExact(gvR_date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    R_date.Value = dtR_date.ToString("yyyy-MM-dd");
                    //o_Name.Value = dtO_Name;
                    rate_note.Value = note;
                    //R_status.SelectedValue = dtR_status;
                    R_total.Value = dtR_total;
                }
                catch (FormatException ex)
                {
                    // Handle exception, maybe log the error or display a message
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }

            }
        }

        //protected void R_status_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // การเลือกใหม่ใน DropDownList R_status จะถูกจัดการที่นี่
        //    string selectedStatus = R_status.SelectedValue;

        //    // คุณสามารถจัดการกับค่าที่ผู้ใช้เลือกใหม่ที่นี่
        //}
        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ShowAuthors(index.searchid);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string gvp_date = row.Cells[0].Text;
            string dtP_id = row.Cells[1].Text;
            string dtR_id = row.Cells[2].Text;
            string dtC_name = row.Cells[3].Text;
            string dtC_Tel = row.Cells[4].Text;
            string dtC_Email = row.Cells[5].Text;
            string gvR_date = row.Cells[6].Text;
            //string dtO_Name = row.Cells[7].Text;
            string rate_note = row.Cells[7].Text;
            string dtR_status = row.Cells[8].Text;
            string dtR_total = row.Cells[9].Text;

            string Queryratetotal = @"Select E_Name, rate_broken from Repair
                                    inner join Rate on Repair.Rate_id = rate.Rate_ID
                                    inner join Employee on rate.E_ID = Employee.E_ID
                                    where R_id =  '{0}'";
            Queryratetotal = string.Format(Queryratetotal, dtR_id);
            DataTable data = Con.GetData(Queryratetotal);

            string e_name = data.Rows[0][0].ToString();
            string dtrate_broken = data.Rows[0][1].ToString();

            if (e.CommandName == "EditRow")
            {
                try
                {
                    DateTime dtP_date = DateTime.ParseExact(gvp_date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    p_date.Value = dtP_date.ToString("yyyy-MM-dd");
                    P_id.Value = dtP_id;
                    Repairid.SelectedValue = dtR_id;
                    C_name.Value = dtC_name;
                    C_tel.Value = dtC_Tel;
                    C_email.Value = dtC_Email;
                    DateTime dtR_date = DateTime.ParseExact(gvR_date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    R_date.Value = dtR_date.ToString("yyyy-MM-dd");
                    //o_Name.Value = dtO_Name;
                    //R_status.SelectedValue = dtR_status;
                    R_total.Value = dtR_total;
                    clean.Visible = true;
                }
                catch (FormatException ex)
                {
                    // Handle exception, maybe log the error or display a message
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }
            }
            else if (e.CommandName == "print")
            {
                rid.Text = dtR_id;
                pdate.Text = gvp_date;
                printcname.Text = dtC_name;
                printphon.Text = dtC_Tel;
                view_note.Text = rate_note;
                print_ename.Text = e_name;
                rate_broken.Text = dtrate_broken;


                string Query = @" SELECT TableOrder.O_name, TableOrder.O_Price, cart.cart_quantity, TableOrder.O_id
                                FROM dbo.cart
                                 INNER JOIN TableOrder ON cart.O_ID = TableOrder.O_ID
                                 WHERE r_id = '{0}'";

                Query = string.Format(Query, dtR_id); // ใส่ค่า R_id ลงใน Query
                DataTable dt1 = Con.GetData(Query); // ดึงข้อมูลจากฐานข้อมูล

                if (dt1.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลใน DataTable หรือไม่
                {
                    foreach (DataRow dataRow in dt1.Rows) // วนลูปทีละแถว
                    {
                        string orderName = dataRow["O_name"].ToString();       // ชื่อสินค้า
                        string orderPrice = dataRow["O_Price"].ToString();     // ราคา
                        string cartQuantity = dataRow["cart_quantity"].ToString(); // จำนวนสินค้า

                        // นำข้อมูลไปแสดงใน Label หรือใช้ประโยชน์
                        Label1.Text += $"สินค้า: {orderName}, ราคา: {orderPrice}, จำนวน: {cartQuantity},<br/>";
                    }
                }
                else
                {
                    Label1.Text = "ไม่มีข้อมูล";
                }



                string dtservice = @"Select R_service  from repair 
                                        where  repair.R_id  =  '{0}'";
                dtservice = string.Format(dtservice, dtR_id);
                DataTable data_service = Con.GetData(dtservice);
                string dtservice_row = data_service.Rows[0][0].ToString();

                r_service.Text = "฿" + dtservice_row;
                //order.Text = dtO_Name;
                //Ptotal.Text = "฿"+ dtR_total;
                sumtotle.Text = "฿" + dtR_total;
                //rateidprint.Text = dtRate_id;
                //cname.Text = dtC_name;
                //phone.Text = "";
                //printbroken.Text = "";
                ////ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                ////Response.Write("<script>alert('Data inserted successfully')</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "window.print();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM payment WHERE P_id ='{0}'";
                Query = string.Format(Query, dtP_id);
                Con.SetData(Query);
                ErrMsg.CssClass = "text-primary";
                ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                ShowAuthors();
                GenerateNewID();
                ClearInput();
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
            Repairid.SelectedIndex = 0;
            C_name.Value = "";
            C_tel.Value = "";
            C_email.Value = "";
            R_date.Value = "";
            p_date.Value = "";
            //o_Name.Value = "";
            R_total.Value = "";
        }


        protected void save_Click(object sender, EventArgs e)
        {
            try
            {

                string gvp_date = p_date.Value;
                string dtP_id = P_id.Value;
                string dtR_id = Repairid.SelectedValue;
                string dtC_name = C_name.Value;
                string dtC_Tel = C_tel.Value;
                string dtC_Email = C_email.Value;
                string gvR_date = R_date.Value;
                //string dtO_Name = o_Name.Value;
                //string dtR_status = R_status.SelectedValue;
                string dtR_total = R_total.Value;
                //DateTime Date = DateTime.ParseExact(gvp_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;


                if (P_id.Value == "" || Repairid.SelectedValue == "" || C_name.Value == "" || C_tel.Value == "" || C_email.Value == "" || R_date.Value == ""
                    || p_date.Value == "" /*|| R_status.SelectedValue == ""*/ || /*o_Name.Value == "" ||*/ R_total.Value == "")
                {



                    string edit_id = "Select * from payment where p_id = '{0}'";
                    edit_id = string.Format(edit_id, dtP_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM payment WHERE P_id ='{0}'";
                        Query = string.Format(Query, dtP_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                        ClearInput();
                    }
                }
                else
                {


                    string edit_id = "Select * from payment where p_id = '{0}'";
                    edit_id = string.Format(edit_id, dtP_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {

                        string Query = "insert into payment VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')";
                        Query = string.Format(Query, dtP_id, gvp_date, dtR_total, dtR_id, "NULL");
                        Con.SetData(Query);
                        //string Queryrepair = "update  repair set R_status =N'{0}' where R_id = '{1}'";
                        //Queryrepair = string.Format(Queryrepair, dtR_status, dtR_id);
                        //Con.SetData(Queryrepair);

                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  payment set p_date ='{0}' where p_id = '{1}'";
                        Query = string.Format(Query, gvp_date, dtP_id);
                        Con.SetData(Query);
                        //string Queryrepair = "update  repair set R_status =N'{0}' where R_id = '{1}'";
                        //Queryrepair = string.Format(Queryrepair, dtR_status, dtR_id);
                        //Con.SetData(Queryrepair);
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
            Repairid.SelectedIndex = 0;
            C_name.Value = "";
            C_tel.Value = "";
            C_email.Value = "";
            R_date.Value = "";
            p_date.Value = "";
            //o_Name.Value = "";
            R_total.Value = "";
        }

    }
}