<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_repair.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.crud_repair" %>


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
                                <asp:TextBox class="form-control w-75  font" ID="tb_search" placeholder="ค้นหาข้อมูลการแจ้งซ่อม" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search" OnClick="submit_search_Click" CssClass="col-sm-2 btn btn-outline-primary" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลการแจ้งซ่อม</h2>
                        <div class="row">
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="Label2" class="col-sm-3 " runat="server" Text="รหัสแจ้งซ่อม"></asp:Label>
                                    <input type="text" readonly class="form-control font" id="R_id" runat="server">
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="lb_rate_id" class="col-sm-3" runat="server" Text="รหัสใบประเมิน"></asp:Label>
                                    <asp:DropDownList ID="rateid" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                        DataTextField="rate_id" DataValueField="rate_id" OnSelectedIndexChanged="rateid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="lb_name_customer" class="col-sm-3 " runat="server" Text="ชื่อลูกค้า"></asp:Label>
                                    <input type="text" readonly visible="false" class="form-control " id="C_id" runat="server">
                                    <input type="text" readonly class="form-control font" id="C_name" runat="server">
                                    <%-- <asp:DropDownList ID="C_id" CssClass="form-control" runat="server" AutoPostBack="true"
                                        DataTextField="C_name" DataValueField="C_Id">
                                    </asp:DropDownList>--%>
                                </div>
                            </div>

                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="date" class="col-sm-3 col-form-label" runat="server" Text="วันที่แจ้งซ่อม"></asp:Label>
                                    <input type="date" class="form-control font" id="R_date" runat="server">
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="lb_Broken" class="col-sm-3 " runat="server" Text="อาการเสีย"></asp:Label>
                                    <input type="text" readonly class="form-control font" id="Broken" runat="server">
                                </div>
                            </div>
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="lb_status" class="col-sm-3 col-form-label " runat="server" Text="สถานะ"></asp:Label>
                                    <asp:DropDownList ID="R_status" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="เลือกรายการ" Value=""></asp:ListItem>
                                        <asp:ListItem Text="รอคิวซ่อม" Value="รอคิวซ่อม"></asp:ListItem>
                                        <asp:ListItem Text="อยู่ระหว่างซ่อม" Value="อยู่ระหว่างซ่อม"></asp:ListItem>
                                        <asp:ListItem Text="รออะไหล่" Value="รออะไหล่"></asp:ListItem>
                                        <asp:ListItem Text="ซ่อมเสร็จแล้ว" Value="ซ่อมเสร็จแล้ว"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <hr />

                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="name_brand_order" class="col-sm-3" runat="server" Text="ชื่อรายการอะไหล่"></asp:Label>
                                    <asp:DropDownList ID="order" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                        DataTextField="O_Name" DataValueField="O_ID" OnSelectedIndexChanged="order_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<asp:TextBox class="form-control" ID="order" runat="server"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="lb" class="col-sm-3 col-form-label" runat="server" Text="จำนวน"></asp:Label>
                                    <asp:TextBox class="form-control font" ID="R_quantity" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="price" class="col-sm-3 " runat="server" Text="ราคาอะไหล่"></asp:Label>
                                    <input type="text" readonly class="form-control font" id="R_total" runat="server">

                                    <asp:Label ID="bath" class="col-sm-3 col-form-label" runat="server" Text="บาท"></asp:Label>
                                </div>
                            </div>
                            <div class="col">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Button CssClass="btn btn-success font" ID="ordertocart" runat="server" Text="ยืนยัน" OnClick="ordertocart_Click" />
                                </div>
                            </div>
                        </div>
                        <br />

                        <asp:GridView ID="gvData" runat="server" CssClass="table table-light table-hover" OnRowCommand="gvData_RowCommand" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="O_id" HeaderText="#"></asp:BoundField>
                                <asp:BoundField DataField="O_name" HeaderText="รายการอะไหล่"></asp:BoundField>
                                <asp:BoundField DataField="O_Price" HeaderText="ราคาอะไหล่"></asp:BoundField>
                                <asp:BoundField DataField="cart_quantity" HeaderText="จำนวนอะไหล่"></asp:BoundField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" Visible="false" runat="server" Text="ลบ" CommandName="del" ControlStyle-CssClass="btn btn-danger font"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                        <hr />


                        <div class="row">
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="lbservice" class="col-sm-3 " runat="server" Text="ค่าบริการ"></asp:Label>
                                    <input type="text" class="form-control font" id="tbservice" runat="server">
                                    <asp:Label ID="Label3" class="col-sm-3 col-form-label" runat="server" Text="บาท"></asp:Label>
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="note" class="col-sm-3 col-form-label" runat="server" Text="หมายเหตุ"></asp:Label>
                                    <input type="text" readonly class="form-control font" id="rate_note" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                            <asp:Button CssClass="btn btn-success font" ID="Button1" runat="server" Text="บันทึก" OnClick="save_Click" />
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
                    <asp:BoundField DataField="R_id" HeaderText="รหัสแจ้งซ่อม"></asp:BoundField>
                    <asp:BoundField DataField="Rate_id" HeaderText="รหัสประเมินราคา"></asp:BoundField>
                    <asp:BoundField DataField="C_id" HeaderText="รหัสลูกค้า"></asp:BoundField>
                    <%--<asp:BoundField DataField="O_id" HeaderText="รหัสรายการ"></asp:BoundField>--%>
                    <asp:BoundField DataField="C_name" HeaderText="ชื่อลูกค้า"></asp:BoundField>
                    <asp:BoundField DataField="C_tel" HeaderText="เบอร์โทร"></asp:BoundField>
                    <asp:BoundField DataField="R_date" HeaderText="วันที่แจ้งซ่อม"></asp:BoundField>
                    <asp:BoundField DataField="rate_broken" HeaderText="อาการเสีย"></asp:BoundField>
                    <%--  <asp:BoundField DataField="O_name" HeaderText="ชื่อรายการอะไหล่"></asp:BoundField>
                    <asp:BoundField DataField="R_quantity" HeaderText="จำนวน"></asp:BoundField>--%>
                    <asp:BoundField DataField="R_total" HeaderText="ราคา"></asp:BoundField>
                    <asp:BoundField DataField="R_status" HeaderText="สถานะ"></asp:BoundField>


                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="แก้ไข" CommandName="EditRow" ControlStyle-CssClass="btn btn-warning font"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnprint" runat="server" Text="พิมพ์ใบแจ้งซ่อม" CommandName="print" ControlStyle-CssClass=" btn btn-dark font"
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
    <Triggers>
        <asp:PostBackTrigger ControlID="AuthorsList" />
        <asp:PostBackTrigger ControlID="gvData" />
    </Triggers>
</asp:UpdatePanel>
<style>
    .container1 {
        width: 100%;
        max-width: 800px;
        margin: auto;
        border: 1px solid black;
        padding: 20px;
        display: none;
        ซ่อนจากหน้าจอปกติ
    }

    .text-center {
        text-align: center;
    }

    .info-row {
        display: flex; /* ใช้ flexbox สำหรับการจัดเรียงในแนวนอน */
        justify-content: space-between; /* เพิ่มช่องว่างระหว่างหัวข้อและเนื้อหา */
        align-items: center; /* จัดเรียงให้หัวข้อและเนื้อหาอยู่ตรงกลาง */
        margin-bottom: 10px;
    }

        .info-row p {
            margin: 0;
            flex: 1; /* เพิ่ม flex เพื่อให้หัวข้อและเนื้อหามีพื้นที่เหมาะสม */
            padding: 0 10px; /* เพิ่ม padding เล็กน้อยเพื่อไม่ให้หัวข้อและเนื้อหาชิดกันมากเกินไป */
        }

    .text-right {
        text-align: right;
    }

    .strong-text {
        font-weight: bold;
    }

    .divider {
        border-top: 1px solid black;
        margin: 10px 0;
    }

    @media print {
        body * {
            visibility: hidden;
        }

        .container1, .container1 * {
            visibility: visible; /* แสดงเฉพาะสิ่งที่จะพิมพ์ */
        }

        .container1 {
            position: absolute;
            display: block;
            left: 0;
            top: 0;
        }

        .btn-print {
            display: none;
        }
    }
</style>

<div class="container1 " id="print">
    <div class="text-right">
        <p>05/07/2567</p>
    </div>
    <h2 class="text-center">ใบแจ้งซ่อม</h2>
    <p class="text-center">
        PPM Service ศูนย์ซ่อมมือถือครบวงจร<br>
        1/3 ซ.30กันยา ต.ในเมือง อ.เมือง จ.นครราชสีมา
    </p>
    <div class="divider"></div>
    <div class="info-row">
        <p>
            <span class="strong-text">รหัสใบแจ้งซ่อม:</span>
            <asp:Label ID="rid" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">อ้างอิงใบประเมินราคา:</span>
            <asp:Label ID="rateidprint" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">ชื่อผู้ใช้:</span>
            <asp:Label ID="cname" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">โทรศัพท์มือถือ:</span>
            <asp:Label ID="phone" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">อาการเสีย:</span>
            <asp:Label ID="printbroken" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">ราคา อะไหล่:</span>
        </p>
    </div>     
    <div class="info-row">
        <p>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </p>
    </div>  

    <div class="info-row">
        <p>
            <span class="strong-text">ค่าบริการ:</span>
            <asp:Label ID="r_service" runat="server" Text=""></asp:Label>
        </p>
    </div> 
    <div class="info-row">
        <p>
            <span class="strong-text">ราคา ณ วันที่แจ้งซ่อม:</span>
            <asp:Label ID="total" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">วันส่งซ่อม:</span>
            <asp:Label ID="daterepair" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">หมายเหตุ:</span>
            <asp:Label ID="view_note" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <div class="info-row">
        <p>
            <span class="strong-text">พนักงาน:</span>
            <asp:Label ID="print_ename" runat="server" Text=""></asp:Label>
        </p>
    </div>

    <p>กรุณาติดต่อชื่อข้างต้นเพื่อนำใบแจ้งซ่อมเข้ารับสินค้าตามกำหนด หรือติดต่อสอบถามเพิ่มเติมได้ที่ศูนย์</p>
    <div class="divider"></div>
    <p class="text-center">PPM Service ยินดีให้บริการ</p>
</div>





