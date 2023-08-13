<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Polymer.aspx.cs" Inherits="xPolimer._Polymer"
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
                                PostBackUrl="Polymer.aspx?maintable" Text="Полимер" CssClass="bg_button_sel" />
                            <asp:Button ID="CPinputPlane" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPinputPlane_Click" PostBackUrl="Polymer.aspx?inputplane"
                                Text="План" />
                            <asp:Button ID="CPInputInner" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPInputInner_Click" PostBackUrl="Polymer.aspx?inputinner"
                                Text="Приход" />
                            <asp:Button ID="CPinputTech" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPinputTech_Click" PostBackUrl="Polymer.aspx?inputtechno"
                                Text="Наличие" />
                            <asp:Button ID="CPoutput" runat="server" BackColor="#FFCC66" BorderStyle="None" CssClass="bg_button"
                                OnClick="CPoutput_Click" PostBackUrl="Polymer.aspx?outputparam" Text="Параметры" />
                            <asp:Button ID="CPprotocol" runat="server" BackColor="#FFCC66" BorderStyle="None"
                                CssClass="bg_button" OnClick="CPprotocol_Click" PostBackUrl="Polymer.aspx?mainproto"
                                Text="Протокол" />
                            <asp:Button ID="CPgraphics" runat="server" BorderStyle="None" CssClass="bg_button"
                                Enabled="False" EnableTheming="True" onmousedown="OpenWindow(8)" Text="Графики" />
                            <asp:Button ID="CPprint" runat="server" BorderStyle="None" CssClass="bg_button" EnableTheming="True"
                                onmousedown="print()" Text="Печать" />
                        </div>
                        <br />
                        <asp:Panel ID="CPdateEdit" runat="server" Height="30px" HorizontalAlign="Center">
                            <div class="bg_panelsub" style="height: 50px; width: 300px">
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="comboBox"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" OnChange="ClearCookieArray();">
                                </asp:DropDownList>
                                &nbsp;
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="comboBox"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" OnChange="ClearCookieArray();">
                                </asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <table style="width: 0%;" align="center">
                            <tr>
                                <td>
                                    <asp:Panel ID="CPpanelInput" runat="server" Height="173px" Width="266px" Visible="False">
                                        <table align="center" class="bg_dialog" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td align="center" colspan="2" class="td_header">
                                                    <asp:Label ID="CPlabelTagCaption" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left">
                                                    <asp:Label ID="CPlabelTag0" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="CPinputTag0" runat="server" MaxLength="3" Width="97px" CssClass="bg_edit"
                                                        onkeydown="if (window.event.keyCode == 13) { CPinputTag1.focus(); return false; }"
                                                        onfocus="CPinputTag0.select()"></asp:TextBox>
                                                    <asp:RangeValidator ID="CPvalidator0" runat="server" ControlToValidate="CPinputTag0"
                                                        ErrorMessage="!" ForeColor="Black" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                                        Type="Integer"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left">
                                                    <asp:Label ID="CPlabelTag1" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="CPinputTag1" runat="server" MaxLength="3" Width="97px" CssClass="bg_edit"
                                                        onkeydown="if (window.event.keyCode == 13) { CPinputTag2.focus(); return false; }"
                                                        onfocus="CPinputTag1.select()"></asp:TextBox>
                                                    <asp:RangeValidator ID="CPvalidator1" runat="server" ControlToValidate="CPinputTag1"
                                                        ErrorMessage="!" ForeColor="Black" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                                        Type="Integer"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left">
                                                    <asp:Label ID="CPlabelTag2" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td class="td_right">
                                                    <asp:TextBox ID="CPinputTag2" runat="server" MaxLength="3" Width="97px" CssClass="bg_edit"
                                                        onkeydown="if (window.event.keyCode == 13) { CPsubmitData.focus(); return false; }"
                                                        onfocus="CPinputTag2.select()"></asp:TextBox>
                                                    <asp:RangeValidator ID="CPvalidator2" runat="server" ControlToValidate="CPinputTag2"
                                                        ErrorMessage="!" ForeColor="Black" MaximumValue="500" MinimumValue="0" SetFocusOnError="True"
                                                        Type="Integer"></asp:RangeValidator>
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td align="center" colspan="2">
                                                    &nbsp;<asp:DropDownList ID="CPlistDate" runat="server" CssClass="comboBox" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CPlistDate_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="CPsubmitData" runat="server" BorderStyle="None" CssClass="bg_button"
                                                        Text="Принять" OnClick="CPsubmitData_Click" OnClientClick="CPsubmitData.value='Расчет...'" />
                                                    <asp:Button ID="CPcancelData" runat="server" BorderStyle="None" CssClass="bg_button"
                                                        OnClick="CPcancelData_Click" Text="Отменить" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="CPcommit0" runat="server" Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="CPcommit1" runat="server" Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="CPcommit2" runat="server" Visible="False"></asp:Label>
                                                    <br />                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="CPlabelResult" runat="server" Text="Расчет" Visible="False"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Panel ID="CPpanelInputLabel" runat="server" Height="173px" Visible="False" Width="266px">
                                        <table align="center" cellpadding="0" cellspacing="0" class="bg_dialog" width="100%">
                                            <tr>
                                                <td align="center" class="td_header">
                                                    <asp:Label ID="CPlabelTagCaption0" runat="server" Text="Памятка"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_left">
                                                    &quot;План&quot; вводит старший
                                                    <br />
                                                    мастер в начале месяца.<br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="td_left">
                                                    &quot;Приход&quot; вводит старший
                                                    <br />
                                                    мастер по мере поступления полимера.<br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="td_left">
                                                    &quot;Наличие&quot; вводит оператор каждую смену.<br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="td_left">
                                                    &quot;Остаток&quot; рассчитывает
                                                    <br />
                                                    ЭВМ по мере ввода
                                                    <br />
                                                    &quot;План&quot; и &quot;Приход&quot;.<br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="100%" align="center">
                        <div class="fixedDiv">
                            <asp:Table ID="CPtableMain" runat="server" BorderStyle="Double" CellPadding="0" CellSpacing="0"
                                EnableTheming="True" GridLines="Both" HorizontalAlign="Center">
                            </asp:Table>
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
