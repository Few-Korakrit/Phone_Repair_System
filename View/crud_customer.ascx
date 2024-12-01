<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_customer.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.crud_customer" %>


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
                                <asp:TextBox class="form-control w-75 font " ID="tb_search" placeholder="ค้นหาข้อมูลลูกค้า" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search" CssClass="col-sm-2 btn btn-outline-primary" OnClick="submit_search_Click" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลลูกค้า</h2>

                        <div class="row">
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="customer" class="col-sm-3 " runat="server" Text="รหัสลูกค้า"></asp:Label>
                                    <input type="text" readonly class="form-control font " id="tb_id_customer" runat="server">
                                </div>
                            </div>
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="lb_name_customer" class="col-sm-2 " runat="server" Text="ชื่อลูกค้า"></asp:Label>
                                    <input type="text" class="form-control  font" id="ddl_name_customer" runat="server">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="tel" class="col-sm-3 " runat="server" Text="โทรศัพท์มือถือ"></asp:Label>
                                    <input type="text" class="form-control  font" id="tb_tel" runat="server">

                                </div>
                            </div>
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="email" class="col-sm-2 " runat="server" Text="อีเมลล์"></asp:Label>
                                    <input type="email" class="form-control font " id="ddl_name_email" runat="server">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="address" class="col-sm-1 me-5 " runat="server" Text="ที่อยู่"></asp:Label>
                                    <asp:TextBox class="form-control font" ID="tb_address" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                            <asp:Button CssClass="btn btn-success font" ID="save" runat="server" Text="บันทึก" OnClick="save_Click" />
                            <asp:Button CssClass="btn btn-danger font" ID="cancel" runat="server" Text="ยกเลิก" OnClick="cancel_Click" />
                            <asp:Button CssClass="btn btn-secondary font" Visible="false" ID="clean" runat="server" Text="ล้างข้อมูล" OnClick="clean_Click" />
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-center">
                            <asp:Label ID="ErrMsg" CssClass="font" runat="server"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="col-2"></div>
            </div>
        </div>
        <div class="col-md-12 mt-3 container-fluid text-center">
            <asp:GridView ID="AuthorsList" CssClass="table table-light table-hover" OnRowCommand="AuthorsList_RowCommand" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="C_Id" HeaderText="รหัสลูกค้า"></asp:BoundField>
                    <asp:BoundField DataField="C_Name" HeaderText="ชื่อลูกค้า"></asp:BoundField>
                    <asp:BoundField DataField="C_Tel" HeaderText="โทรศัพท์มือถือ"></asp:BoundField>
                    <asp:BoundField DataField="C_Email" HeaderText="E-mail"></asp:BoundField>
                    <asp:BoundField DataField="C_Address" HeaderText="ที่อยู่"></asp:BoundField>
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
