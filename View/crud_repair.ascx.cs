using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class crud_repair : System.Web.UI.UserControl
    {
        Models.Functions Con;



        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            GenerateNewID();
            showRate();
            showorder();
            ShowAuthors();
            //if (!IsPostBack)
            //{
            //    ShowAuthorsgvData();
            //}

            ShowAuthorsgvData();

        }

        private void GenerateNewID()
        {
            string Query = "SELECT TOP 1 r_id FROM repair ORDER BY r_id DESC";
            DataTable dt = Con.GetData(Query);
            if (dt == null)
            {
                R_id.Value = "R0001";
            }
            else
            {
                string lastId = dt.Rows[0][0].ToString();
                int nextId = int.Parse(lastId.Substring(1)) + 1;
                string newid = "R" + nextId.ToString("D4");
                R_id.Value = newid;
            }
        }
        private void showRate()
        {
            string Query = "select * from rate";
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                rateid.DataSource = data;
                rateid.DataTextField = "Rate_id";
                rateid.DataValueField = "Rate_id";
                rateid.DataBind();

                //C_name.Value = "";
            }
            rateid.Items.Insert(0, new ListItem("เลือกรายการ", "0"));


        }
        private void showorder()
        {
            string Query = "select * from TableOrder";
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                order.DataSource = data;
                order.DataTextField = "O_Name";
                order.DataValueField = "O_ID";
                order.DataBind();

                //C_name.Value = "";
            }
            order.Items.Insert(0, new ListItem("เลือกรายการ", "0"));


        }
        private void ShowAuthorsgvData()
        {
            string Query = @"Select TableOrder.O_name , TableOrder.O_Price, cart.cart_quantity ,TableOrder.O_id
                            from dbo.cart
                            inner join TableOrder   on cart.O_ID = TableOrder.O_ID
                            where r_id = '{0}' ";
            Query = string.Format(Query, R_id.Value);
            gvData.DataSource = Con.GetData(Query);
            gvData.DataBind();

        }


        private void ShowAuthors(string keyword = "")
        {
            string Query;
            if (!string.IsNullOrEmpty(keyword))
            {
                Query = @"Select Repair.* , Customer.C_Name ,rate.rate_broken ,Customer.C_tel
                            from Repair 
                            inner join Customer on Repair.C_ID = Customer.C_ID
                            inner join rate on Repair.rate_id = rate.rate_id
                            where r_id  LIKE  N'%{0}%' or Customer.C_Name  LIKE  N'%{0}%' or Customer.C_tel  LIKE  N'%{0}%'";
                Query = string.Format(Query, keyword);
            }
            else
            {
                Query = @"Select Repair.* , Customer.C_Name ,rate.rate_broken ,Customer.C_tel
                            from Repair 
                            inner join Customer on Repair.C_ID = Customer.C_ID
                            inner join rate on Repair.rate_id = rate.rate_id";
            }
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();


        }


        protected void rateid_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Query = @"Select rate.rate_broken, Customer.C_Name, Customer.C_id,rate.note
                            from rate 
                            inner join Customer on Rate.C_ID = Customer.C_ID
                            inner join Employee on Rate.E_ID = Employee.E_ID
                            where Rate_id = '{0}'";

            Query = string.Format(Query, rateid.SelectedValue);
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                string rate_broken = data.Rows[0][0].ToString();
                string C_namedata = data.Rows[0][1].ToString();
                string C_iddata = data.Rows[0][2].ToString();
                string note = data.Rows[0][3].ToString();

                Broken.Value = rate_broken;
                C_name.Value = C_namedata;
                C_id.Value = C_iddata;
                rate_note.Value = note;

            }
            ShowAuthorsgvData();

        }
        protected void order_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = @"Select o_id, o_name, o_price
                            from TableOrder  
                            where o_id = '{0}'";

            Query = string.Format(Query, order.SelectedValue);
            DataTable data = Con.GetData(Query);
            if (data.Rows.Count > 0)
            {
                //string o_id = data.Rows[0][0].ToString();
                //string o_name = data.Rows[0][1].ToString();
                string o_price = data.Rows[0][2].ToString();

                //R_quantity.Text = "1";
                R_total.Value = o_price;

            }
            ShowAuthorsgvData();


        }


        protected void AuthorsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ShowAuthors(index.searchid);
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = AuthorsList.Rows[rowIndex];
            string dtR_id = row.Cells[0].Text;
            string dtRate_id = row.Cells[1].Text;
            string dtC_id = row.Cells[2].Text;
            //string dtO_id = row.Cells[3].Text;
            string dtC_name = row.Cells[3].Text;
            string dtC_tel = row.Cells[4].Text;
            string GVR_date = row.Cells[5].Text;
            string rate_broken = row.Cells[6].Text;
            //string O_name = row.Cells[7].Text;
            //string dtR_quantity = row.Cells[8].Text;
            string dtR_total = row.Cells[7].Text;
            string dtR_status = row.Cells[8].Text;


            string Queryratetotal = @"Select rate.rate_total, rate.note,E_Name, repair.r_service from rate 
                                        inner join Employee on rate.E_ID = Employee.E_ID
                                        inner join repair on rate.rate_id = repair.rate_id
                                        where rate.Rate_id =  '{0}' and repair.R_id  =  '{1}'";
            Queryratetotal = string.Format(Queryratetotal, dtRate_id, dtR_id);
            DataTable data = Con.GetData(Queryratetotal);
            string dtratetotal = data.Rows[0][0].ToString();
            string rate_note = data.Rows[0][1].ToString();
            string e_name = data.Rows[0][2].ToString();
            string service = data.Rows[0][3].ToString();

            if (e.CommandName == "EditRow")
            {

                R_id.Value = dtR_id;
                rateid.SelectedValue = dtRate_id;
                C_id.Value = dtC_id;
                C_name.Value = dtC_name;
                tbservice.Value = service;

                try
                {
                    DateTime dtR_date = DateTime.ParseExact(GVR_date, "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    R_date.Value = dtR_date.ToString("yyyy-MM-dd");
                }
                catch (FormatException ex)
                {
                    // Handle exception, maybe log the error or display a message
                    ErrMsg.Text = "Date format is not valid: " + ex.Message;
                }
                //order.SelectedValue = dtO_id;
                R_status.SelectedValue = dtR_status;
                //R_quantity.Text = dtR_quantity;
                //R_total.Value = dtR_total;
                Broken.Value = rate_broken;
                clean.Visible = true;


            }
            else if (e.CommandName == "print")
            {

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
                        double dtorderPrice = Convert.ToDouble(orderPrice);
                        double dtcartQuantity = Convert.ToDouble(cartQuantity);
                        int order_Price = Convert.ToInt32(dtorderPrice);
                        int Quantity = Convert.ToInt32(dtcartQuantity);
                        int sum_orders = order_Price * Quantity;
                        // นำข้อมูลไปแสดงใน Label หรือใช้ประโยชน์
                        Label1.Text += $"สินค้า: {orderName}, ราคา: {orderPrice}฿, จำนวน: {cartQuantity},<br/>";
                        sum_order.Text += $"{sum_orders}฿<br/>";
                    }
                }
                else
                {
                    Label1.Text = "ไม่มีข้อมูล";
                }


                r_service.Text =  service + "฿";
                view_note.Text = rate_note;
                rateidprint.Text = dtRate_id;
                cname.Text = dtC_name;
                phone.Text = "0123456789";
                printbroken.Text = rate_broken;
                daterepair.Text = GVR_date;
                total.Text =  dtR_total + "฿" ;
                print_ename.Text = e_name;
                //ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                //Response.Write("<script>alert('Data inserted successfully')</script>");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "refresh", "location.reload();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "window.print();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string Query = "DELETE FROM repair WHERE r_id ='{0}'";
                Query = string.Format(Query, dtR_id);
                Con.SetData(Query);
                ErrMsg.CssClass = "text-primary";
                ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                ShowAuthors();
                GenerateNewID();
                ClearInput();
            }
            ShowAuthorsgvData();
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
            ClearInput();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                string dtR_id = R_id.Value;
                string dtR_date = R_date.Value;
                string dtR_service = R_total.Value;
                //string dtR_total = R_total.Value;
                string dtR_status = R_status.SelectedValue;
                string dtR_quantity = "0";
                //string dtR_quantity = R_quantity.Text;
                string dtrateid = rateid.SelectedValue;
                //string dtrateid = null;
                string C_Id = C_id.Value;
                string O_Id = order.SelectedValue;
                string dataR_service = tbservice.Value;
                int R_service = 0;
                if (dataR_service != "")
                {
                    R_service = Convert.ToInt32(dataR_service);
                }



                int order_total = 0;

                string sum_order = @"Select sum(cart_sum) as cart_sum
                                    from cart
                                    where cart.r_id ='{0}'";
                sum_order = string.Format(sum_order, dtR_id);
                DataTable dtsum_order = Con.GetData(sum_order);

                //sum_order = string.Format(sum_order); // ตรวจสอบให้แน่ใจว่า dtR_id มีค่า
                //DataTable datasum = Con.GetData(sum_order);

                if (dtsum_order.Rows.Count > 0) // ตรวจสอบว่ามีข้อมูลหรือไม่
                {
                    string cart_sum = dtsum_order.Rows[0][0].ToString();
                    //string cart_quantity = dtsum_order.Rows[0][1].ToString();
                    //double test00 = Convert.ToDouble(dtsum_order.Rows[0][1] ?? 0);
                    //string order_price2 = datasum.Rows[0][2].ToString();

                    double dtcart_sum = Convert.ToDouble(cart_sum);
                    //double sum_cart_quantity = Convert.ToDouble(cart_quantity);
                    //double price_quantity = Convert.ToDouble(datasum.Rows[0]["sum_quantity"] ?? 0);
                    order_total = Convert.ToInt32(dtcart_sum);

                }
                else
                {
                    Response.Write("ไม่มีข้อมูลที่ตรงกับเงื่อนไขในฐานข้อมูล");
                }
                int dtR_total = R_service + order_total;




                if (R_id.Value == "" || R_date.Value == "" || R_status.SelectedValue == ""
                   || tbservice.Value == "" || rateid.SelectedValue == "" || C_id.Value == "")
                {

                    string edit_id = "Select * from repair where R_id = '{0}'";
                    edit_id = string.Format(edit_id, dtR_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
                    }
                    else
                    {
                        string Query = "DELETE FROM repair WHERE r_id ='{0}'";
                        Query = string.Format(Query, dtR_id);
                        Con.SetData(Query);

                        string del_cart = "DELETE FROM cart WHERE R_id ='{0}' ";
                        del_cart = string.Format(del_cart, dtR_id);
                        Con.SetData(del_cart);


                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "ลบข้อมูลสำเร็จ";
                        ShowAuthors();
                        GenerateNewID();
                        ShowAuthorsgvData();
                        ClearInput();
                    }
                }
                else
                {
                    string edit_id = "Select * from repair where R_id = '{0}'";
                    edit_id = string.Format(edit_id, dtR_id);
                    DataTable dt = Con.GetData(edit_id);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into repair VALUES ('{0}', '{1}', '{2}', N'{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";
                        Query = string.Format(Query, dtR_id, dtR_date, dtR_total, dtR_status, dtR_quantity, dtrateid, C_Id, O_Id, R_service);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        string Query = "update  repair set R_date =  '{0}', R_total = '{1}',R_status = N'{2}',Rate_id ='{3}',C_Id ='{4}',O_Id='{5}',R_service = '{6}' where R_id = '{7}'";
                        Query = string.Format(Query, dtR_date, dtR_total, dtR_status, dtrateid, C_Id, O_Id, R_service, dtR_id);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                }
                index.searchid = null;
                GenerateNewID();
                ShowAuthors();
                ShowAuthorsgvData();
                ClearInput();


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
            ShowAuthorsgvData();

        }

        protected void ClearInput()
        {

            rateid.SelectedIndex = 0;
            C_id.Value = "";
            C_name.Value = "";
            R_date.Value = "";
            Broken.Value = "";
            R_status.SelectedIndex = 0;
            order.SelectedIndex = 0;
            R_quantity.Text = "";
            R_total.Value = "";
            tbservice.Value = "";
        }

        protected void ordertocart_Click(object sender, EventArgs e)
        {

            try
            {

                string dtR_id = R_id.Value;
                string dtr_total = R_total.Value;
                string dtR_quantity = R_quantity.Text;
                //string dtrateid = rateid.SelectedValue;
                string O_Id = order.SelectedValue;

                double sum_dtr_total = Convert.ToDouble(dtr_total);
                double sum_dtR_quantity = Convert.ToDouble(dtR_quantity);
                //double price_quantity = Convert.ToDouble(datasum.Rows[0]["sum_quantity"] ?? 0);
                int order_total = Convert.ToInt32(sum_dtr_total * sum_dtR_quantity);


                if (R_id.Value == "" || R_quantity.Text == "" || order.SelectedValue == "")
                {

                    ErrMsg.CssClass = "text-danger";
                    ErrMsg.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";

                }
                else
                {
                    string cart_row = "Select * from cart where R_id = N'{0}' and O_ID = N'{1}'";
                    cart_row = string.Format(cart_row, dtR_id, O_Id);
                    DataTable dt = Con.GetData(cart_row);

                    if (dt.Rows.Count == 0)
                    {
                        string Query = "insert into cart VALUES (N'{0}', N'{1}', '{2}', '{3}')";
                        Query = string.Format(Query, O_Id, dtR_id, dtR_quantity, order_total);
                        Con.SetData(Query);
                        ErrMsg.CssClass = "text-primary";
                        ErrMsg.Text = "บันทึกสำเร็จ";
                    }
                    else
                    {
                        ErrMsg.CssClass = "text-danger";
                        ErrMsg.Text = "มีรายการอยู่แล้ว";
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }

            order.SelectedIndex = 0;
            R_quantity.Text = "";
            R_total.Value = "";
            tbservice.Value = "";
            ShowAuthorsgvData();

        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvData.Rows[rowIndex];
            string r_id = R_id.Value;
            string O_id = row.Cells[0].Text;
            try
            {
                if (e.CommandName == "del")
                {
                    string Query = "DELETE FROM cart WHERE R_id ='{0}' and O_ID = '{1}' ";
                    Query = string.Format(Query, r_id, O_id);
                    Con.SetData(Query);
                }
            }
            catch (Exception Ex)
            {
                ErrMsg.Text = Ex.Message;
            }
            ErrMsg.CssClass = "text-primary";
            ErrMsg.Text = "ลบข้อมูลสำเร็จ";
            ShowAuthorsgvData();

        }

    }
}
