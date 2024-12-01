<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="report_customer.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.report_customer" %>

<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<script type="text/javascript">

    function PrintBill() {
        var PGrid = document.getElementById('<%=AuthorsList.ClientID%>');
        PGrid.bordr = 0;
        var PWin = window.open('', 'PrintGrid', 'left=100,top=100,width=1024,height=768,toolbar=0,scrollbars=1,status=0,resizable=1');
        PWin.document.write(PGrid.outerHTML);
        PWin.document.close();
        PWin.focus();
        PWin.print();
        PWin.close();
    }

</script>

        <div class=" container-fluid">
            <div class="row mt-5 mb-54">
                <div class="col-2"></div>
                <div class="col-8">

                    <%--<div class="container-fluid ">--%>
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="row col-md-4 d-grid gap-2 d-md-flex justify-content-md-end">
                            </div>
                        </div>
                    </div>

                    <div class=" container-fluid">
                        <h2 class=" text-center">รายงานข้อมูลลูกค้า</h2>
                        <form action="#">
                            <div class="row">
                             <%--   <div class="col">
                                    <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                                        <asp:TextBox class="form-control w-75 " ID="tb_search" placeholder="ค้นหาข้อมูลประเภทอะไหล่" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="submit_search_employee" OnClick="submit_search_Click" CssClass="col-sm-2  btn btn-outline-primary" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                </div>--%>
                                <div class="col">
                                    <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                                        <asp:Button ID="btnExport" runat="server" CssClass=" btn btn-secondary font" Text="ดาวน์โหลดเอกสาร" OnClick="btnExport_Click" />
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success font"  Text="พิมพ์" OnClientClick="printGridView();" />
                                        <%--<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="ค้นหา" OnClick="btnSearch_Click" />--%>
                                    </div> 
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="col-2"></div>
            </div>
        </div>
<div class="col-md-12 mt-3 container-fluid text-center">
    <asp:GridView ID="AuthorsList" CssClass="table table-light table-hover" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="C_Id" HeaderText="รหัสลูกค้า"></asp:BoundField>
            <asp:BoundField DataField="C_Name" HeaderText="ชื่อลูกค้า"></asp:BoundField>
            <asp:BoundField DataField="C_Tel" HeaderText="โทรศัพท์มือถือ"></asp:BoundField>
            <asp:BoundField DataField="C_Email" HeaderText="E-mail"></asp:BoundField>
            <asp:BoundField DataField="C_Address" HeaderText="ที่อยู่"></asp:BoundField>
           
        </Columns>
    </asp:GridView>
</div>


<script type="text/javascript">
    function printGridView() {
        // ดึง GridView ตาม ID
        var divToPrint = document.getElementById('<%= AuthorsList.ClientID %>');

        // คำนวณยอดรวม
        //var rows = divToPrint.getElementsByTagName('tr');
        //var total = 0;
        //for (var i = 1; i < rows.length; i++) { // ข้ามหัวตาราง
        //    var cells = rows[i].getElementsByTagName('td');
        //    if (cells.length > 0) {
        //        var value = parseFloat(cells[5]?.innerText || 0); // สมมติว่าคอลัมน์ราคาอยู่ index 5
        //        total += value;
        //    }
        //}

        // เปิดหน้าต่างใหม่สำหรับพิมพ์
        var newWin = window.open('', 'Print-Window');
        newWin.document.open();

        // เขียน HTML สำหรับการพิมพ์
        newWin.document.write('<html><head>');
        newWin.document.write('<style>');
        newWin.document.write('body { font-family: Arial, sans-serif; font-size: 12px; }');
        newWin.document.write('table { border-collapse: collapse; width: 100%; }');
        newWin.document.write('table, th, td { border: 1px solid black; text-align: left; padding: 8px; }');
        newWin.document.write('h1 { text-align: center; font-size: 20px; margin-bottom: 5px; }');
        newWin.document.write('.header-info { display: flex; justify-content: space-between; font-size: 10px; margin-bottom: 10px; }');
        newWin.document.write('</style>');
        newWin.document.write('</head><body>');

        // เพิ่มหัวข้อรายงาน
        newWin.document.write('<h1>รายงานข้อมูลลูกค้า</h1>');

        // เพิ่มวันที่และยอดรวมให้อยู่บรรทัดเดียวกัน
        newWin.document.write('<div class="header-info">');
        newWin.document.write('<h2>วันที่พิมพ์: ' + new Date().toLocaleDateString('th-TH') + '</h2>');
        //newWin.document.write('<h2>รวมยอดราคา: ' + total.toFixed(2) + ' บาท</h2>');
        newWin.document.write('</div>');

        // เพิ่ม GridView
        newWin.document.write(divToPrint.outerHTML);

        newWin.document.write('</body></html>');
        newWin.document.close();

        // สั่งพิมพ์
        newWin.print();
        newWin.close();
    }
</script>