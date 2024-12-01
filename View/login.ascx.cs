using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ระบบแจ้งซ่อมมือถือ
{
    public partial class login : System.Web.UI.UserControl
    {
        Models.Functions Con;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Con = new Models.Functions();
        }
        public static string userid = "";
        public static string Uname = "";
        public static string level = "";

        protected void Loginbtn_Click(object sender, EventArgs e)
        {

            if (user.Value == "" || password.Value == "")
            {
                //Response.Write("<script>alert('โปรดกรอกข้อมูล')</script>");
                alerts.CssClass = "text-danger";
                alerts.Text = "โปรดกรอกข้อมูลให้ครบถ้วน";
            }
            else
            {
                string Query = "Select e_id,e_name,e_level from Employee Where E_User ='{0}' and E_Password ='{1}' ";
                Query = string.Format(Query, user.Value, password.Value);
                DataTable dt = Con.GetData(Query);
                if (dt.Rows.Count == 0)
                {
                    //Response.Write("<script>alert('เข้าสู่ระบบไม่สำเร็จ')</script>");
                    //Response.Redirect("main.aspx?page=login");
                    alerts.CssClass = "text-danger";
                    alerts.Text = "โปรดตรวจสอบข้อมูลให้ถูกต้อง";

                }
                else
                {
                    alerts.Text = "เข้าสู่ระบบสำเร็จ";
                    //Response.Write("<script>alert('เข้าสู่ระบบสำเร็จ')</script>");
                    userid = dt.Rows[0][0].ToString();
                    Uname = dt.Rows[0][1].ToString();
                    level = dt.Rows[0][2].ToString();
                    //username = dt.Rows[0][0].ToString();
                    Response.Redirect("main.aspx");
                }
            }
        }
    }
}