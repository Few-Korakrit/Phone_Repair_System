<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_brand.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.crud_brand" %>


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
                                <asp:TextBox class="form-control w-75 font " ID="tb_search" placeholder="ค้นหาข้อมูลยี่ห้ออะไหล่"  runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search" CssClass="col-sm-2 btn btn-outline-primary" OnClick="submit_search_Click" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลยี่ห้ออะไหล่</h2>
                        <form action="#">
                            <%-- <div class="row  d-grid gap-2 d-md-flex justify-content-md-center">
            <asp:Label ID="lb_id_type" class="col-sm-2 col-form-label " runat="server" Text="รหัสประเภทอะไหล่"></asp:Label>
            <input type="text" readonly class="form-control-plaintext  w-50" id="tb_id_type" value="id_type">
        </div>
        <div class="row  d-grid gap-2 d-md-flex justify-content-md-center">
            <asp:Label ID="lb_name_type" class="col-sm-2 col-form-label" runat="server" Text="ชื่อประเภทอะไหล่"></asp:Label>
            <asp:DropDownList ID="ddl_name_type" CssClass="form-control w-50" runat="server"></asp:DropDownList>
        </div>--%>
                            <div class="row  d-grid gap-2 d-md-flex justify-content-md-center">
                                <asp:Label ID="brand" class="col-sm-2 col-form-label" runat="server" Text="รหัสยี่ห้ออะไหล่"></asp:Label>
                                <div class="col">
                                    <input type="text" readonly class="form-control-plaintext  w-50" id="tb_id_brand" value="" runat="server">
                                </div>
                            </div>
                            <div class="row  d-grid gap-2 d-md-flex justify-content-md-center">
                                <asp:Label ID="name_brand" class="col-sm-2 col-form-label" runat="server" Text="ชื่อยี่ห้ออะไหล่"></asp:Label>
                                <div class="col">
                                    <asp:TextBox class="form-control font " ID="tb_name_brand" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddl_name_brand" CssClass="form-control  w-50" runat="server"></asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                                <asp:Button CssClass="btn btn-success font " ID="save" runat="server" Text="บันทึก" OnClick="save_Click" />
                                <asp:Button CssClass="btn btn-danger font " ID="cancel" runat="server" Text="ยกเลิก" OnClick="cancel_Click" />
                                <asp:Button CssClass="btn btn-secondary font " Visible="false" ID="clean" runat="server" Text="ล้างข้อมูล" OnClick="clean_Click" />
                            </div>

                            <div class="d-grid gap-2 d-md-flex justify-content-md-center">
                                <asp:Label ID="ErrMsg" runat="server"></asp:Label>
                            </div>

                        </form>
                    </div>
                </div>
                <div class="col-2"></div>
            </div>
        </div>


        <div class="col-md-8 mt-3 container-fluid text-center">
            <asp:GridView ID="AuthorsList" CssClass="table table-light table-hover" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" OnRowCommand="AuthorsList_RowCommand" EnableTheming="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="B_id" HeaderText="รหัสยี่ห้ออะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="B_name" HeaderText="ชื่อยี่ห้ออะไหล่"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="แก้ไข" CommandName="EditRow" ControlStyle-CssClass="btn btn-warning font " 
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField>
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
