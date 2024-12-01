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
    public partial class report_repair : System.Web.UI.UserControl
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

                Query = @"Select Repair.* , Customer.C_Name ,rate.rate_broken,tableorder.O_name
                            from Repair 
                            inner join Customer on Repair.C_ID = Customer.C_ID
                            inner join rate on Repair.rate_id = rate.rate_id
                            inner join tableorder on Repair.O_id = tableorder.O_id
                            where R_date BETWEEN  @StartDate AND @EndDate";
                DataTable dt = Con.GetDataByQueryAndDateRange(Query, startDate, endDate);
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
               Query = @"Select Repair.* , Customer.C_Name ,rate.rate_broken,tableorder.O_name
                            from Repair 
                            inner join Customer on Repair.C_ID = Customer.C_ID
                            inner join rate on Repair.rate_id = rate.rate_id
                            inner join tableorder on Repair.O_id = tableorder.O_id";
                AuthorsList.DataSource = Con.GetData(Query);
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
            DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
            DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
            // กำหนดหัวข้อรายงานและช่วงวันที่


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
                    hw.Write("<p style='margin: 0;'>ตั้งแต่วันที่: " + startDate.ToString("dd/MM/yyyy") + " ถึงวันที่: " + endDate.ToString("dd/MM/yyyy") + "</p>");
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