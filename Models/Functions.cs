using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;

namespace ระบบแจ้งซ่อมมือถือ.Models
{
    public class Functions
    {
        private SqlConnection Con;
        private SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string ConStr;

        // ตัวสร้างสำหรับการตั้งค่าการเชื่อมต่อ
        public Functions()
        {
            ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\new\ระบบแจ้งซ่อมมือถือ\App_Data\Database.mdf;Integrated Security=True; Connect Timeout = 30; Encrypt = False;";

            //ConStr = @" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = ""C:\Users\Lenovo\OneDrive\เดสก์ท็อป\ระบบแจ้งซ่อมโทรศัพท์มือถือร้าน PPM Service\ระบบแจ้งซ่อมมือถือ\App_Data\Database.mdf"";Integrated Security = True; Connect Timeout = 30; Encrypt = False;";
            Con = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = Con;
        }

        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConStr);
            sda.Fill(dt);
            return dt;
        }
        public int SetData(string Query)
        {
            int cnt = 0;
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            cmd.CommandText = Query;
            cnt = cmd.ExecuteNonQuery();
            Con.Close();
            return cnt;
        }
        public DataTable GetDataByQueryAndDateRange(string query, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable(); // สร้าง DataTable เพื่อเก็บผลลัพธ์

            using (SqlConnection conn = new SqlConnection(ConStr))  // ConStr คือ connection string ที่ใช้เชื่อมต่อกับฐานข้อมูล
            {
                // เปิด connection
                conn.Open();

                // สร้าง SqlCommand และตั้งค่า connection ให้กับ command
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // เพิ่ม Parameters สำหรับวันที่
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    // ใช้ SqlDataAdapter เพื่อดึงข้อมูลและใส่ลงใน DataTable
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);  // เติมข้อมูลใน DataTable
                    }
                }
            }

            return dt;  // คืนค่าผลลัพธ์ DataTable
        }



    }
}