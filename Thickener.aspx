<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thickener.aspx.cs" Inherits="xPolimer._Thickener"
    EnableSessionState="True" Culture="auto" UICulture="auto" %>

<html>
<head runat="server">
    <meta http-equiv='content-type' content='text/html; charset=windows-1251' />
    <title></title>
    <link href="themes/style.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="themes/print.css" rel="stylesheet" type="text/css" media="print" />

    <script src="engine/module_js.js" type="text/javascript"></script>

</head>
<body onload="ClearCookieArray();">
    <form id="fmMain" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table class="fixedTable">
                <tr>
                    <td>
                        <div align="center" class="bg_panel">
                            <asp:Button ID="CPmainLoad" runat="server" BorderStyle="None" OnClick="lbMain_Click"
                                PostBackUrl="Thickener.aspx?maintable" Text="Параметры" CssClass="bg_button_sel" />
                            <asp:Button ID="CPInputInner" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPInputInner_Click" PostBackUrl="Thickener.aspx?inputinner"
                                Text="Ввод" />
                            <asp:Button ID="CPprotocol" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPprotocol_Click" PostBackUrl="Thickener.aspx?mainproto"
                                Text="Протокол" />
                            <asp:Button ID="CPgraphics" runat="server" BorderStyle="None" CssClass="bg_button"
                                Enabled="False" EnableTheming="True" onmousedown="OpenWindow(9)" Text="Графики" />
                            <asp:Button ID="CPprint" runat="server" BorderStyle="None" CssClass="bg_button" EnableTheming="True"
                                onmousedown="print()" Text="Печать" />
                        </div>
                        <br />
                        <asp:Panel ID="CPdateEdit" runat="server" Height="30px" HorizontalAlign="Center">
                            <div class="bg_panelsub" style="height: 50px; width: 300px">
                                &nbsp;
                                <asp:DropDownList ID="CPlistDate" runat="server" AutoPostBack="True" CssClass="comboBox"
                                    OnSelectedIndexChanged="CPlistDate_SelectedIndexChanged" OnChange="ClearCookieArray();">
                                </asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelEditor" runat="server" Height="16px" Visible="False">
                            <asp:TextBox ID="CPinputTag0" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator0" runat="server" ControlToValidate="CPinputTag0"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag1" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator1" runat="server" ControlToValidate="CPinputTag1"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag2" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator2" runat="server" ControlToValidate="CPinputTag2"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag3" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator3" runat="server" ControlToValidate="CPinputTag3"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag4" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator4" runat="server" ControlToValidate="CPinputTag4"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag5" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator5" runat="server" ControlToValidate="CPinputTag5"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag6" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator6" runat="server" ControlToValidate="CPinputTag6"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag7" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator7" runat="server" ControlToValidate="CPinputTag7"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag8" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator8" runat="server" ControlToValidate="CPinputTag8"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag9" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator9" runat="server" ControlToValidate="CPinputTag9"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag10" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator10" runat="server" ControlToValidate="CPinputTag10"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>
                            <asp:TextBox ID="CPinputTag11" runat="server" CssClass="bg_edit_dark" MaxLength="4"></asp:TextBox>
                            <asp:RangeValidator ID="CPvalidator11" runat="server" ControlToValidate="CPinputTag11"
                                ErrorMessage="!" ForeColor="White" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                Type="Double"></asp:RangeValidator>                                
                        </asp:Panel>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td height="100%" align="center">
                        <div class="fixedDiv">
                            <asp:Table ID="CPtableMain" runat="server" BorderStyle="Double" CellPadding="0" CellSpacing="0"
                                EnableTheming="True" GridLines="Both" HorizontalAlign="Center" Width="100%">
                            </asp:Table>
                            <asp:Panel ID="PanelPostData" runat="server" CssClass="bg_panel" HorizontalAlign="Center"
                                Visible="False">
                                <asp:Label ID="CPtimeRemaining0" runat="server" CssClass="bg_panelsub" Height="48px"
                                    Width="300px">Ввод осуществляется только за текущий четный час</asp:Label>
                                <asp:Label ID="CPtimeRemainingStart" runat="server" CssClass="bg_panelsub" Height="48px"
                                    Width="300px"></asp:Label>
                                <asp:Label ID="CPtimeRemainingEnd" runat="server" CssClass="bg_panelsub" Height="48px"
                                    Width="300px"></asp:Label>
                                <br />
                                <asp:Button ID="CPsubmitData" runat="server" BorderStyle="None" CssClass="bg_button"
                                    OnClick="CPsubmitData_Click" OnClientClick="CPsubmitData.value='Расчет...'" Text="Принять" />
                                <asp:Button ID="CPcancelData" runat="server" BorderStyle="None" CssClass="bg_button"
                                    OnClick="CPcancelData_Click" Text="Отменить" />
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel"
                DisplayAfter="3">
                <ProgressTemplate>
                    <center>
                        <img alt="Загрузка" src="themes/ajax-loader.gif"> </img>
                    </center>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
