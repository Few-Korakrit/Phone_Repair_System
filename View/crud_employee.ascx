<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_employee.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.crud__employee" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <div class=" container-fluid">
            <div class="row mt-5 mb-54">
                <div class="col-2"></div>
                <div class="col-8">

                    <div class="container-fluid ">
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="row col-md-4 d-grid gap-2 d-md-flex justify-content-md-end">
                                <asp:TextBox class="form-control w-75 font " ID="tb_search" placeholder="ค้นหาข้อมูลพนักงาน" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search_employee" OnClick="submit_search_Click" CssClass="col-sm-2  btn btn-outline-primary" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลพนักงาน</h2>
                        <form action="#">
                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                        <asp:Label ID="Label1" class="col-sm-3 " runat="server" Text="รหัสพนักงาน"></asp:Label>
                                        <input type="text" readonly class="form-control font " id="tb_id_employee" runat="server">
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                        <asp:Label ID="lb_name_employee" class="col-sm-3 " runat="server" Text="ชื่อพนักงาน"></asp:Label>
                                        <input type="text"  class="form-control font" id="ddl_name_employee" runat="server">
                                    </div>
                                </div>
                            </div>
     

                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                        <asp:Label ID="number" class="col-sm-3 col-form-label" runat="server" Text="บัตรประชาชน"></asp:Label>
                                        <input type="text"  class="form-control font" id="tb_number" runat="server">
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                        <asp:Label ID="tel" class="col-sm-3 col-form-label" runat="server" Text="โทรศัพท์มือถือ"></asp:Label>
                                        <input type="tel"  class="form-control font" id="tb_tel" runat="server">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                        <asp:Label ID="address" class="col-sm-1 me-5" runat="server" Text="ที่อยู่"></asp:Label>
                                        <input type="text"  class="form-control font " id="tb_address" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                        <asp:Label ID="date" class="col-sm-3 col-form-label" runat="server" Text="วันที่เริ่มทำงาน"></asp:Label>
                                        <input type="date" class="form-control font" id="tb_date" runat="server">
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                        <asp:Label ID="status" class="col-sm-3 col-form-label" runat="server" Text="สถานะ"></asp:Label>
                                        <input type="text"  class="form-control font" id="tb_status"  runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                        <asp:Label ID="user" class="col-sm-3 col-form-label" runat="server" Text="ชื่อเข้าสู่ระบบ"></asp:Label>
                                        <input type="text"  class="form-control font" id="tb_user" runat="server">
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                        <asp:Label ID="password" class="col-sm-3 col-form-label" runat="server" Text="รหัสผ่าน"></asp:Label>
                                        <input type="text"  class="form-control font" id="tb_password" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                                <asp:Button CssClass="btn btn-success font" ID="save" runat="server" Text="บันทึก" OnClick="save_Click" />
                                <asp:Button CssClass="btn btn-danger font"  ID="cancel" runat="server" Text="ยกเลิก" OnClick="cancel_Click" />
                                <asp:Button CssClass="btn btn-secondary font" Visible="false" ID="clean" runat="server" Text="ล้างข้อมูล" OnClick="clean_Click" />
                            </div>
                            <div class="d-grid gap-2 d-md-flex justify-content-md-center">
                                <asp:Label ID="ErrMsg" CssClass="font" runat="server"></asp:Label>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="col-2"></div>
            </div>
        </div>

        <div class="col-md-12 mt-3 container-fluid text-center">
            <asp:GridView ID="AuthorsList"  CssClass="table table-light table-hover"  OnRowCommand="AuthorsList_RowCommand"  AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="E_ID" HeaderText="รหัสพนักงาน"></asp:BoundField>
                    <asp:BoundField DataField="E_Name" HeaderText="ชื่อพนักงาน"></asp:BoundField>
                    <asp:BoundField DataField="E_number" HeaderText="บัตรประชาชน"></asp:BoundField>
                    <asp:BoundField DataField="E_date" HeaderText="เริ่มทำงาน"></asp:BoundField>
                    <asp:BoundField DataField="E_address" HeaderText="ที่อยู่"></asp:BoundField>
                    <asp:BoundField DataField="E_Tel" HeaderText="โทรศัพท์"></asp:BoundField>
                    <asp:BoundField DataField="E_status" HeaderText="สถานะ"></asp:BoundField>
                    <asp:BoundField DataField="E_user" HeaderText="ชื่อเข้าสู่ระบบ"></asp:BoundField>
                    <asp:BoundField DataField="E_Password" HeaderText="รหัสผ่าน"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="แก้ไข" CommandName="EditRow" ControlStyle-CssClass="btn btn-warning font"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="ลบ" CommandName="DeleteRow" ControlStyle-CssClass="btn btn-danger"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('คุณแน่ใจหรือไม่ว่าต้องการลบรายการนี้?');" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
