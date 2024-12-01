<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="report_rate.ascx.cs" Inherits="ระบบแจ้งซ่อมมือถือ.View.report_rate" %>

<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<!-- ใช้ UpdatePanel เพื่อป้องกันการรีเฟรชหน้าทั้งหมด -->


<%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
            <div class=" container-fluid">
                <h2 class=" text-center">รายงานข้อมูลประเมินราคา</h2>
                <div class="row">
                    <div class="col">
                        <div class=" d-grid gap-2 d-md-flex justify-content-md-end mt-2">
                            <label for="txtStartDate" class="col-sm-3">ตั้งแต่วันที่:</label>
                            <asp:TextBox ID="txtStartDate" class="form-control font" runat="server" />
                        </div>
                    </div>
                    <div class="col">
                        <div class="  d-grid gap-2 d-md-flex justify-content-md-start mt-2">
                            <label for="txtEndDate" class="col-sm-3">ถึงวันที่:</label>
                            <asp:TextBox ID="txtEndDate" class="form-control font" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="d-grid gap-2 mt-5 d-md-flex justify-content-md-center">
                <asp:Button ID="btnExport" runat="server" CssClass=" btn btn-secondary font" Text="ดาวน์โหลดเอกสาร" OnClick="btnExport_Click" />
                <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success font" Text="พิมพ์" OnClientClick="printGridView();" />
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success font" Text="ค้นหา" OnClick="btnSearch_Click" />
                <asp:Button CssClass="btn btn-danger font" ID="cancel" runat="server" Text="ยกเลิก" OnClick="cancel_Click" />

            </div>
            <div class="d-grid gap-2 d-md-flex justify-content-md-center">
               
                <asp:Label ID="ErrMsg" CssClass="font" runat="server"></asp:Label>
            </div>

            <div class="col-2"></div>
        </div>
    </div>
</div>
<div class="col-md-12 mt-3 container-fluid text-center">
    <asp:GridView ID="AuthorsList" CssClass="table table-light table-hover" AutoGenerateSelectButton="false" EnableSortingAndPagingCallbacks="false" runat="server" EnableTheming="False" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Rate_ID" HeaderText="รหัสประเมินราคา"></asp:BoundField>
            <asp:BoundField DataField="C_name" HeaderText="ชื่อลูกค้า"></asp:BoundField>
            <asp:BoundField DataField="E_name" HeaderText="ชื่อพนักงาน"></asp:BoundField>
            <asp:BoundField DataField="Rate_Date" HeaderText="วันที่"></asp:BoundField>
            <asp:BoundField DataField="Rate_Broken" HeaderText="อาการเสีย"></asp:BoundField>
            <asp:BoundField DataField="Rate_Total" HeaderText="ราคารวม"></asp:BoundField>
            <asp:BoundField DataField="Note" HeaderText="หมายเหตุ"></asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="สถานะ"></asp:BoundField>
        </Columns>
    </asp:GridView>
</div>

<script type="text/javascript">
    $(function () {
        $("#<%= txtStartDate.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });
        $("#<%= txtEndDate.ClientID %>").datepicker({ dateFormat: 'yy-mm-dd' });
    });


</script>
<script type="text/javascript">
    function printGridView() {
        var divToPrint = document.getElementById('<%= AuthorsList .ClientID %>'); // ดึง GridView ตาม ID
        var newWin = window.open('', 'Print-Window');

        newWin.document.open();
        newWin.document.write('<html><head><title>พิมพ์</title>');
        newWin.document.write('<style>');
        newWin.document.write('table { border-collapse: collapse; width: 100%; }');
        newWin.document.write('table, th, td { border: 1px solid black; text-align: left; padding: 8px; }');
        newWin.document.write('</style>');
        newWin.document.write('</head><body>');
        newWin.document.write(divToPrint.outerHTML); // เพิ่มเฉพาะ GridView ในหน้าใหม่
        newWin.document.write('</body></html>');
        newWin.document.close();

        newWin.print(); // เรียกคำสั่งพิมพ์
        newWin.close();
    }
</script>


<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
