using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class report_brand : System.Web.UI.UserControl
    {
        Models.Functions Con;

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAuthors();
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

        //protected void submit_search_Click(object sender, EventArgs e)
        //{
        //    string keyword = tb_search.Text.Trim();
        //    index.searchid = keyword;
        //    ShowAuthors(index.searchid);
        //}
        protected void btnExport_Click(object sender, EventArgs e)
        {
            // กำหนดค่า Response สำหรับไฟล์ Excel
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReportExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            //string startDateStr = txtStartDate.Text;
            //string endDateStr = txtEndDate.Text;


            string reportTitle = "รายงานข้อมูลยี่ห้อสินค้า"; // หัวข้อรายงาน
                                                             //DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
                                                             //DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
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
                    //hw.Write("<p style='margin: 0;'>ตั้งแต่วันที่: " + startDate.ToString("dd/MM/yyyy") + " ถึงวันที่: " + endDate.ToString("dd/MM/yyyy") + "</p>");
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