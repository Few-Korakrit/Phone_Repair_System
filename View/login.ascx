<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.login" %>
  <div class="container "style="height:360px">
      <div class="row mt-5 mb-100 " >
          <div class="col-3"></div>
          <div class="col-6">
                  <div class="mb-3">
                      <%--<label for="exampleInputEmail1" class="form-label">Email address</label>--%>
                      <input type="text" class="form-control font" id="user" aria-describedby="emailHelp" placeholder="ชื่อผู้ใช้" runat="server">
                  </div>
                  <div class="mb-3">
                      <%--<label for="exampleInputPassword1" class="form-label">Password</label>--%>
                      <input type="password" class="form-control font" id="password" placeholder="รหัสผ่าน" runat="server">
                  </div>
                 
                  <div class="d-grid gap-2 col-6 mx-auto">
                      <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary font" OnClick="Loginbtn_Click" Text="ยืนยัน" />
                      <%--<asp:Button Text="เข้าสู่ระบบ" ID="loginbtn" OnClick="loginbtn_Click" class="btn btn-primary" runat="server" />--%>
                  </div>
              <div class="mb-3 mt-3">
                      <asp:Label ID="alerts"  runat="server" Text=""></asp:Label>
              </div>
          </div>
          <div class="col-3"></div>
      </div>
  </div>
