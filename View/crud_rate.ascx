<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="crud_rate.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.crud_rate" %>


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
                                <asp:TextBox class="form-control w-75 font " ID="tb_search" placeholder="ค้นหาข้อมูลการประเมินราคา" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="submit_search" OnClick="submit_search_Click" CssClass="col-sm-2 btn btn-outline-primary" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">ข้อมูลการประเมินราคา</h2>
                        <div class="row">
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="lb_rate_id" class="col-sm-3 " runat="server" Text="รหัสใบประเมิน"></asp:Label>
                                    <input type="text" readonly class="form-control font" id="rate_id" runat="server">
                                </div>
                            </div>
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="Label1" class="col-sm-3 col-form-label " runat="server" Text="ชื่อพนักงาน"></asp:Label>
                                    <asp:DropDownList ID="E_id" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                        DataTextField="E_name" DataValueField="E_Id">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="lb_name_customer" class="col-sm-3 " runat="server" Text="ชื่อลูกค้า"></asp:Label>
                                    <asp:DropDownList ID="C_id" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true"
                                        DataTextField="C_name" DataValueField="C_Id">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="date" class="col-sm-3 col-form-label" runat="server" Text="วันที่ประเมินราคา"></asp:Label>
                                    <input type="date" class="form-control font " id="tb_date" runat="server">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="lb_status" class="col-sm-3 col-form-label" runat="server" Text="สถานะ"></asp:Label>
                                    <%--<input type="text" class="form-control " id="status" runat="server">--%>
                                    <asp:DropDownList ID="status" CssClass="form-control dropdrown font" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="รอยืนยัน" Value="รอยืนยัน"></asp:ListItem>
                                        <asp:ListItem Text="ยกเลิก" Value="ยกเลิก"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-lift mt-2">
                                    <asp:Label ID="price" class="col-sm-3 col-form-label" runat="server" Text="ราคา ณ วันประเมิน (บาท)"></asp:Label>
                                    <asp:TextBox class="form-control font" ID="tb_price" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                                    <asp:Label ID="lb_Broken" class="col-sm-3 " runat="server" Text="อาการเสีย"></asp:Label>
                                    <input type="text" class="form-control font " id="Broken" runat="server">
                                </div>
                            </div>
                            <div class="col">
                                <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                    <asp:Label ID="lb_note" class="col-sm-3 col-form-label" runat="server" Text="หมายเหตุ"></asp:Label>
                                    <input type="text" class="form-control font" id="note" runat="server">
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
            <asp:GridView ID="AuthorsList" Visible="true" CssClass="table table-light table-hover" OnRowCommand="AuthorsList_RowCommand" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Rate_ID" HeaderText="รหัสประเมินราคา"></asp:BoundField>
                    <asp:BoundField DataField="C_id" HeaderText="รหัสลูกค้า"></asp:BoundField>
                    <asp:BoundField DataField="E_id" HeaderText="รหัสพนักงาน"></asp:BoundField>
                    <asp:BoundField DataField="C_name" HeaderText="ชื่อลูกค้า"></asp:BoundField>
                    <asp:BoundField DataField="C_tel" HeaderText="เบอร์โทร"></asp:BoundField>
                    <asp:BoundField DataField="E_name" HeaderText="ชื่อพนักงาน"></asp:BoundField>
                    <asp:BoundField DataField="Rate_Date" HeaderText="วันที่"></asp:BoundField>
                    <asp:BoundField DataField="Rate_Broken" HeaderText="อาการเสีย"></asp:BoundField>
                    <asp:BoundField DataField="Rate_Total" HeaderText="ราคารวม"></asp:BoundField>
                    <asp:BoundField DataField="Note" HeaderText="หมายเหตุ"></asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="สถานะ"></asp:BoundField>


                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="แก้ไข" CommandName="EditRow" ControlStyle-CssClass="btn btn-warning font"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnprint" runat="server" Text="พิมพ์ใบประเมิน" CommandName="print" ControlStyle-CssClass=" btn btn-dark font"
                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--                    <asp:TemplateField>
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
    <h2 class="text-center">ประเมินราคา</h2>
    <p class="text-center">
        PPM Service ศูนย์ซ่อมมือถือครบวงจร<br>
        1/3 ซ.30กันยา ต.ในเมือง อ.เมือง จ.นครราชสีมา
    </p>
    <div class="divider"></div>
  
    <div class="info-row">
        <p>
            <span class="strong-text">รหัสใบประเมินราคา:</span>
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
            <span class="strong-text">ราคา ณ วันที่ประเมิน:</span>
            <asp:Label ID="rate_total" runat="server" Text=""></asp:Label>
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
