using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ
{
    public partial class report_datatype : System.Web.UI.UserControl
    {
        Models.Functions Con;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
            ShowAuthors();
        }

        private void ShowAuthors()
        {
            string Query = "Select * from employee";
            AuthorsList.DataSource = Con.GetData(Query);
            AuthorsList.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string startDateStr = txtStartDate.Text;
            string endDateStr = txtEndDate.Text;

            DateTime startDate = DateTime.ParseExact(startDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;
            DateTime endDate = DateTime.ParseExact(endDateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Date;


            string Query = @"SELECT * FROM employee  where E_Date BETWEEN  @StartDate AND @EndDate";

            //string Query = @"SELECT * FROM employee  where E_Date BETWEEN   '%{0}%' and  '%{1}%'";
            //Query = string.Format(Query, startDate, endDate);

            DataTable dt = Con.GetDataByQueryAndDateRange(Query, startDate, endDate);


            //string Query = @"SELECT * FROM employee  where E_Date BETWEEN '2024-10-03' and  '2024-10-05'";
            //Query = string.Format(Query);
            //DataTable dt = Con.GetData(Query);
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


        protected void btnExport_Click(object sender, EventArgs e)
        {
            // ตั้งค่า Response สำหรับไฟล์ Excel
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            // Disable ViewState for GridView during export
            AuthorsList.EnableViewState = false;

            // Create a StringWriter to capture HTML content
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // Disable paging so that all rows are exported
                    AuthorsList.AllowPaging = false;
                    

                    // Render GridView control to HtmlTextWriter
                    AuthorsList.RenderControl(hw);

                    // Write the rendered content to the response
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

       

    }







}