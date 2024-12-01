using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class report_payment : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAuthors();
        }
        protected void cancel_Click(object sender, EventArgs e)
        {

            txtStartDate.Text = "";
            txtEndDate.Text = "";
            index.txtStartDate = "";
            index.txtEndDate = "";
            ShowAuthors();

        }
        private void ShowAuthors()
        {
          
            string Query;
            if (!string.IsNullOrEmpty(index.txtStartDate))
            {
                string startDateStr = index.txtStartDate;
                string endDateStr = index.txtEndDate;



                DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
                DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;


                Query = @"select Payment.P_id,Payment.P_date,Customer.C_Name,Customer.C_Tel,Customer.C_Email,
                            Repair.R_id,Repair.R_date,Repair.R_status,Repair.R_total,rate_total,rate.note
                            from Payment
                            inner join Repair on Payment.R_id = Repair.R_id                     
                            inner join rate on Repair.Rate_id = rate.Rate_id
                            inner join Customer on Repair.C_id = Customer.C_ID
                            where R_status = N'ซ่อมเสร็จแล้ว' and P_Date BETWEEN  @StartDate AND @EndDate";
                //string Query = @"SELECT * FROM employee  where E_Date BETWEEN   '%{0}%' and  '%{1}%'";
                //Query = string.Format(Query, startDate, endDate);

                DataTable dt = Con.GetDataByQueryAndDateRange(Query, startDate, endDate);
                // ตรวจสอบว่ามีคอลัมน์ 'rate_total' และ 'R_total' อยู่ใน DataTable หรือไม่
                if (dt.Columns.Contains("rate_total") && dt.Columns.Contains("R_total"))
                {
                    // เพิ่มคอลัมน์ใหม่สำหรับเก็บผลรวม
                    dt.Columns.Add("total", typeof(decimal));

                    // คำนวณผลรวมราคาและใส่ในคอลัมน์ "รวม"
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal rateTotal = Convert.ToDecimal(row["rate_total"]);
                        decimal partTotal = Convert.ToDecimal(row["R_total"]);
                        row["total"] = rateTotal - partTotal;
                    }
                }
                else
                {
                    // แสดงข้อความ error ถ้าหากไม่พบคอลัมน์ rate_total หรือ R_total
                    throw new Exception("Column 'rate_total' or 'R_total' does not exist in the DataTable.");
                }

                if (dt.Rows.Count > 0)
                {
                    AuthorsList.DataSource = dt;
                    AuthorsList.DataBind();
                }
                else
                {
                    AuthorsList.DataSource = null;
                    AuthorsList.DataBind();
                }


            }
            else
            {
                Query = @"select Payment.P_id,Payment.P_date,Customer.C_Name,Customer.C_Tel,Customer.C_Email,
                            Repair.R_id,Repair.R_date,Repair.R_status,Repair.R_total,rate_total,rate.note
                            from Payment
                            inner join Repair on Payment.R_id = Repair.R_id                     
                            inner join rate on Repair.Rate_id = rate.Rate_id
                            inner join Customer on Repair.C_id = Customer.C_ID
                            where R_status = N'ซ่อมเสร็จแล้ว'";

                // ดึงข้อมูลจากฐานข้อมูล
                DataTable dt = Con.GetData(Query);

                // ตรวจสอบว่ามีคอลัมน์ 'rate_total' และ 'R_total' อยู่ใน DataTable หรือไม่
                if (dt.Columns.Contains("rate_total") && dt.Columns.Contains("R_total"))
                {
                    // เพิ่มคอลัมน์ใหม่สำหรับเก็บผลรวม
                    dt.Columns.Add("total", typeof(decimal));

                    // คำนวณผลรวมราคาและใส่ในคอลัมน์ "รวม"
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal rateTotal = Convert.ToDecimal(row["rate_total"]);
                        decimal partTotal = Convert.ToDecimal(row["R_total"]);
                        row["total"] = rateTotal - partTotal;
                    }
                }
                else
                {
                    // แสดงข้อความ error ถ้าหากไม่พบคอลัมน์ rate_total หรือ R_total
                    throw new Exception("Column 'rate_total' or 'R_total' does not exist in the DataTable.");
                }

                // Bind DataTable ให้กับ GridView
                AuthorsList.DataSource = dt;
                AuthorsList.DataBind();


            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            index.txtStartDate = txtStartDate.Text;
            index.txtEndDate = txtEndDate.Text;
            ShowAuthors();



        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // กำหนดค่า Response สำหรับไฟล์ Excel
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            string startDateStr = txtStartDate.Text;
            string endDateStr = txtEndDate.Text;


            string reportTitle = "รายงานประจำวัน"; // หัวข้อรายงาน

          


            // ปิดการทำงานของ ViewState ในการ Export
            AuthorsList.EnableViewState = false;

            // ใช้ StringWriter เพื่อเก็บ HTML เนื้อหา
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // เพิ่มหัวข้อรายงานและช่วงวันที่ลงในเนื้อหา
                    hw.Write("<div style='text-align:center; font-weight:bold;'>");
                    hw.Write("<h3 style='margin: 0;'>" + reportTitle + "</h3>");
                    if (startDateStr != "" && endDateStr != "")
                    {
                        DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
                        DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
                        hw.Write("<p style='margin: 0;'>ตั้งแต่วันที่: " + startDate.ToString("dd/MM/yyyy") + " ถึงวันที่: " + endDate.ToString("dd/MM/yyyy") + "</p>");
                    }

                    hw.Write("</div><br/>");

                    // ปิดการแบ่งหน้าเพื่อให้แสดงข้อมูลครบ
                    AuthorsList.AllowPaging = false;

                    // Render GridView ลงใน HtmlTextWriter
                    AuthorsList.RenderControl(hw);

                    // ส่งข้อมูลที่เขียนลงใน Response
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }




    }
}