<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_order.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.crud_order" %>



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
                                <asp:TextBox class="form-control w-75 font " ID="tb_search" placeholder="ค้นหาข้อมูลรายการอะไหล่" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search_order" OnClick="submit_search_Click" CssClass="col-sm-2 btn btn-outline-primary " runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลรายการอะไหล่</h2>
                        <form action="#">

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="d-grid gap-2 d-md-flex justify-content-md-lift">
                                        <asp:Label ID="lb_name_type_order" class="col-sm-2 col-form-label mt-2 " runat="server" Text="ประเภทอะไหล่"></asp:Label>
                                        <asp:DropDownList ID="type_id" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                            DataTextField="T_name" DataValueField="T_id" >
                                        </asp:DropDownList>

                                        <%--<input type="text" readonly class="form-control-plaintext  w-50" id="tb_id_type_order" runat="server">--%>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row  d-grid gap-2 d-md-flex justify-content-md-center">
            <asp:Label ID="lb_name_type" class="col-sm-2 col-form-label" runat="server" Text="ชื่อประเภทอะไหล่"></asp:Label>
            <asp:DropDownList ID="ddl_name_type" CssClass="form-control w-50" runat="server"></asp:DropDownList>
        </div>--%>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="brand_order" class="col-sm-2 col-form-label" runat="server" Text="ชื่อยี่ห้ออะไหล่"></asp:Label>
                                        <asp:DropDownList ID="brand_Id" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                            DataTextField="B_Name" DataValueField="B_Id" >
                                        </asp:DropDownList>
                                        <%--<input type="text" readonly class="form-control-plaintext  w-50" id="tb_id_brand_order" value="id_type">--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="Label1" class="col-sm-3 col-form-label" runat="server" Text="รหัสรายการอะไหล่"></asp:Label>
                                        <input type="text" readonly class="form-control font " id="tb_id_order" runat="server">
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="price" class="col-sm-3 col-form-label" runat="server" Text="ราคา"></asp:Label>
                                        <asp:TextBox class="form-control font" ID="tb_price" runat="server"></asp:TextBox>
                                        <asp:Label ID="bath" class="col-sm-3 col-form-label " runat="server" Text="บาท"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="name_brand_order" class="col-sm-3 col-form-label" runat="server" Text="ชื่อรายการอะไหล่"></asp:Label>
                                        <asp:TextBox class="form-control font" ID="ddl_name_brand_order" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="unit" class="col-sm-3 col-form-label" runat="server" Text="หน่วยนับ"></asp:Label>
                                        <asp:TextBox class="form-control font " ID="tb_unit" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                        <asp:Label ID="detail" class="col-sm-2 col-form-label" runat="server" Text="รายละเอียด"></asp:Label>
                                        <asp:TextBox class="form-control font" ID="tb_detail" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                                <asp:Button CssClass="btn btn-success font" ID="save_order" runat="server" Text="บันทึก" OnClick="save_order_Click" />
                                <asp:Button CssClass="btn btn-danger font" ID="cancel_order" runat="server" Text="ยกเลิก" OnClick="cancel_order_Click" />
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
            <asp:GridView ID="AuthorsList" CssClass="table table-light table-hover" OnRowCommand="AuthorsList_RowCommand" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="O_id" HeaderText="รหัสรายการอะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="O_name" HeaderText="รายการอะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="O_Price" HeaderText="ราคา"></asp:BoundField>
                    <asp:BoundField DataField="O_unit" HeaderText="หน่วยนับ"></asp:BoundField>
                    <asp:BoundField DataField="O_Description" HeaderText="รายละเอียด"></asp:BoundField>
                    <asp:BoundField DataField="T_name" HeaderText="ประเถทอะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="B_name" HeaderText="ยี่ห้ออะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="T_id"  HeaderText="รหัสประเภท"></asp:BoundField>
                    <asp:BoundField DataField="B_id"  HeaderText="รหัสยี่ห้ออะไหล่"></asp:BoundField>
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
<style>


</style>