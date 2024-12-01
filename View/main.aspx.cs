using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ.View
{
    public partial class index : System.Web.UI.Page
    {

        public static string searchtype;
        public static string searchid;
        public static string txtStartDate;
        public static string txtEndDate;

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Bypass the rendering check to allow GridView export.
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string userid = login.userid;
            string Uname = login.Uname;
           
            string part = Request.QueryString["page"];
            if (userid == "")
            {
               
                LoadUserControl("login.ascx");
            }
            else
            {

                if (part == "login")
                {
                    LoadUserControl("login.ascx");
                }
                else if (part == "type")
                {
                    LoadUserControl("crud_type.ascx");
                }
                else if (part == "brand")
                {
                    LoadUserControl("crud_brand.ascx");
                }
                else if (part == "order")
                {
                    LoadUserControl("crud_order.ascx");
                }
                else if (part == "employee")
                {
                    LoadUserControl("crud_employee.ascx");
                }
                else if (part == "customer")
                {
                    LoadUserControl("crud_customer.ascx");
                }
                else if (part == "report_dataemployee")
                {
                    LoadUserControl("report_dataemployee.ascx");
                }
                else if (part == "report_rate")
                {
                    LoadUserControl("report_rate.ascx");
                }
                else if (part == "report_repair")
                {
                    LoadUserControl("report_repair.ascx");
                }
                else if (part == "report_payment")
                {
                    LoadUserControl("report_payment.ascx");
                }
                else if (part == "crud_rate")
                {
                    LoadUserControl("crud_rate.ascx");
                }
                else if (part == "report_employee")
                {
                    LoadUserControl("report_employee.ascx");
                } 
                else if (part == "report_customer")
                {
                    LoadUserControl("report_customer.ascx");
                }
                else if (part == "crud_repair")
                {
                    LoadUserControl("crud_repair.ascx");
                }
                else if (part == "crud_admin")
                {
                    LoadUserControl("crud_admin.ascx");
                }
                else if (part == "crud_payment")
                {
                    LoadUserControl("crud_payment.ascx");
                }
                else if (part == "logout")
                {
                    login.userid = "";
                    login.Uname = "";
                    LoadUserControl("login.ascx");
                    //LoadUserControl("main.aspx");

                }
                else
                {
                }
                //}

            }
        }

        private void LoadUserControl(string controlPath)
        {
            // ล้างเนื้อหาของ PlaceHolder ก่อน
            PlaceHolder1.Controls.Clear();

            Control userControl = LoadControl(controlPath);
            PlaceHolder1.Controls.Add(userControl);
        }




        //protected void LoadPart1(object sender, EventArgs e)
        //{
        //    LoadUserControl("../View/login.ascx");
        //}
        //private void LoadUserControl(string controlPath)
        //{
        //    // ล้างเนื้อหาของ PlaceHolder ก่อนโหลด Control ใหม่
        //    PlaceHolder1.Controls.Clear();

        //    // โหลด User Control ตาม path ที่ระบุ
        //    Control userControl = LoadControl(controlPath);
        //    PlaceHolder1.Controls.Add(userControl);
        //}
    }
}