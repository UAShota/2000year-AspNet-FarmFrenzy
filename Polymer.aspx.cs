using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace xPolimer
{
    public partial class _Polymer : System.Web.UI.Page
    {
        private TGUI GUI;
        private TORA ORA;
        private String ActionModule;
        private String Config = "Config\\polymerGMC2.xml";
        private bool AcessPlane;
        private bool AcessTech;
        private const String _XMLCLASSHEAD = "bg_header";
        private const String _XMLCLASSCELL = "bg_cell";
        private const String _XMLCLASSCELLNOSEL = "bg_cellnosel";
        private const String _XMLDATAMAIN = "maintable";
        private const String _XMLDATAPROTO = "mainproto";
        private const String _XMLDATAPLAN = "inputplane";
        private const String _XMLDATAINNER = "inputinner";
        private const String _XMLDATATECH = "inputtechno";
        private const String _XMLDATAPARAM = "outputparam";
        private const String _XMLDATAROW = "row";
        private const String _XMLDATACELL = "cell";
        private const String _XMLDATAHEAD = "header";
        private const String _XMLDATACAPT = "caption";
        private const String _XMLDATALABEL = "label";
        private const String _XMLDATAConfig = "Config";
        private const String _XMLDATADESC = "description";
        private const String _XMLDATAMIN = "min";
        private const String _XMLDATAMAX = "max";
        private const String _XMLVALEMPTY = "";
        private const String _XMLVALZERO = "0";
        private const String _XMLVALVALUE = "value";
        private const String _XMLVALTYPE = "type";
        private const String _XMLVALARCHIVE = "int";
        private const String _XMLVALTYPETEXT = "text";
        private const String _XMLVALTAGSUM = "tagsum";
        private const String _XMLVALCALCID = "calcid";
        private const String _XMLVALCALCNAME = "calcname";
        private const String _SESSIONTAGINFO = "TagInfo";
        private const String _GUIBUTTONDEF = "bg_button";
        private const String _GUIBUTTONSEL = "bg_button_sel";
        private const String _SESSIONACCESSPLANE = "AccessPlane";
        private const String _SESSIONACCESSTECH = "AccessTech";

        protected void Page_Load(object sender, EventArgs e)
        {
            GUI = new TGUI();
            ORA = new TORA();

            // 2390 - xFramFrenzy :: Ввод расхода полимера ГМЦ-2
            // 2 - права на запись
            if (Session[_SESSIONACCESSPLANE] == null)
            {
                AcessPlane = GUI.GetUserRightForResource(2569) == 2;
                AcessTech = GUI.GetUserRightForResource(2570) == 2;
                Session.Add(_SESSIONACCESSPLANE, AcessPlane);
                Session.Add(_SESSIONACCESSTECH, AcessTech);
            }
            else
            {
                AcessPlane = Convert.ToBoolean(Session[_SESSIONACCESSPLANE]);
                AcessTech = Convert.ToBoolean(Session[_SESSIONACCESSTECH]);
            }

            if (!AcessPlane)
            {
                CPinputPlane.Visible = false;
                CPInputInner.Visible = false;
            }

            if (!AcessTech)
            {
                CPinputTech.Visible = false;
            }

            ActionModule = Request.QueryString.ToString();
            Config = System.AppDomain.CurrentDomain.BaseDirectory + Config;

            CPlabelResult.Visible = false;
            // влом писать цикл по количеству элементов в TagInfo, один хуй
            CPcommit0.Visible = false;
            CPcommit1.Visible = false;
            CPcommit2.Visible = false;    

            switch (ActionModule)
            {
                case _XMLVALEMPTY: Main_Load(); break;
            }
        }

        protected XDocument XML_Load(String FileName)
        {
            String DayNow;
            switch (ActionModule)
            {
                case _XMLDATAMAIN:
                case _XMLDATAPARAM:
                case _XMLDATAPROTO:
                case _XMLVALEMPTY:
                    DayNow = " за " + ddlYear.SelectedValue + ", " + GUI.MonthList[ddlMonth.SelectedIndex];
                    break;
                default:
                    DayNow = " за " + DateTime.Today.ToShortDateString();
                    break;
            }
            XDocument XDoc = XDocument.Load(Config);
            Page.Title = XDoc.Root.Attribute(_XMLDATADESC).Value + DayNow;

            return XDoc;
        }

        protected void ChangeButtonStyle(Button Selected)
        {
            if (Selected == CPmainLoad) 
            {
                CPmainLoad.CssClass = _GUIBUTTONSEL;
                CPprint.Enabled = true;
            }
            else CPmainLoad.CssClass = _GUIBUTTONDEF;
            if (Selected == CPinputPlane)
            {
                CPinputPlane.CssClass = _GUIBUTTONSEL;
                CPprint.Enabled = false;
            }
            else CPinputPlane.CssClass = _GUIBUTTONDEF;
            if (Selected == CPInputInner)
            {
                CPprint.Enabled = false;
                CPInputInner.CssClass = _GUIBUTTONSEL;
            }
            else CPInputInner.CssClass = _GUIBUTTONDEF;
            if (Selected == CPinputTech)
            {
                CPprint.Enabled = false;
                CPinputTech.CssClass = _GUIBUTTONSEL;
            }
            else CPinputTech.CssClass = _GUIBUTTONDEF;
            if (Selected == CPoutput)
            {
                CPprint.Enabled = true;
                CPoutput.CssClass = _GUIBUTTONSEL;
            }
            else CPoutput.CssClass = _GUIBUTTONDEF;
            if (Selected == CPprotocol)
            {
                CPprint.Enabled = false;
                CPprotocol.CssClass = _GUIBUTTONSEL;
            }
            else CPprotocol.CssClass = _GUIBUTTONDEF;
        }

        protected void Main_Load()
        {
            int iRowID;
            if (ddlYear.Items.Count == 0)
            {
                GUI.FillYear(ddlYear);
                GUI.FillMonth(ddlMonth);
            }

            CPpanelInput.Visible = false;
            CPpanelInputLabel.Visible = false;
            CPdateEdit.Visible = true;
            CPtableMain.Rows.Clear();
            CPtableMain.Width = Unit.Percentage(50);

            XDocument xmlDocument = XML_Load(Config);
            XElement xmlRoot = xmlDocument.Root.Element(_XMLDATAMAIN);

            foreach (XElement xmlRow in xmlRoot.Elements(_XMLDATAROW))
            {
                TableRow rowTable = new TableRow();
                if (xmlRow.Attribute(_XMLDATAHEAD) != null)
                {
                    rowTable.CssClass = _XMLCLASSHEAD;
                    rowTable.HorizontalAlign = HorizontalAlign.Center;
                    rowTable.TableSection = TableRowSection.TableHeader;
                }
                else rowTable.TableSection = TableRowSection.TableBody;
                iRowID = CPtableMain.Rows.Add(rowTable);

                foreach (XElement xmlCell in xmlRow.Elements(_XMLDATACELL))
                {
                    String ItemValue, ItemInt, ItemType;
                    int iItemTag, iItemInt, iItemYear, iItemMonth;
                    float fItemValue;

                    TableCell cellTable = new TableCell();
                    ItemValue = ItemInt = ItemType = _XMLVALEMPTY;

                    foreach (XElement xmlProp in xmlCell.Elements())
                    {
                        if (xmlProp.Name.ToString().Equals(_XMLVALVALUE)) ItemValue = xmlProp.Value;
                        else
                            if (xmlProp.Name.ToString().Equals(_XMLVALTYPE)) ItemType = xmlProp.Value;
                            else
                                if (xmlProp.Name.ToString().Equals(_XMLVALARCHIVE)) ItemInt = xmlProp.Value;

                        if (ItemType.Equals(_XMLVALTYPETEXT))
                            cellTable.CssClass = _XMLCLASSHEAD;
                        else
                            cellTable.CssClass = _XMLCLASSCELLNOSEL;
                    }
                    if (ItemType.Equals(_XMLVALVALUE))
                    {
                        iItemTag = Convert.ToInt32(ItemValue);
                        iItemInt = Convert.ToInt32(ItemInt);
                        iItemYear = Convert.ToInt32(ddlYear.SelectedValue);
                        iItemMonth = Convert.ToInt32(ddlMonth.SelectedValue);
                        fItemValue = ORA.GetTagValueMain(iItemTag, iItemInt, iItemYear, iItemMonth);
                        cellTable.Text = fItemValue.ToString();
                    }
                    else cellTable.Text = ItemValue;

                    if (ItemType.Equals(_XMLVALVALUE))
                    {
                        cellTable.ID = ItemValue;
                        cellTable.ToolTip = "ID: " + ItemValue.ToString();
                        cellTable.Attributes["onclick"] = "SelectCell(" + ItemValue + ")";
                        cellTable.Attributes["onMouseEnter"] = "FocusCell(" + ItemValue + ")";
                        cellTable.Attributes["onMouseLeave"] = "UnFocusCell(" + ItemValue + ")";
                    }

                    if (!ItemType.Equals(_XMLVALEMPTY))
                        CPtableMain.Rows[iRowID].Cells.Add(cellTable);
                }
            }
        }

        protected void Input_Load(String xmlFile, String XPath)
        {
            XDocument xmlDocument = XML_Load(xmlFile);
            XElement xmlRoot = xmlDocument.Root.Element(XPath);
            int TagCount = xmlRoot.Elements(_XMLDATAROW).Count();
            RangeValidator Validator = null;

            CPlabelTagCaption.Text = xmlRoot.Attribute(_XMLDATACAPT).Value;
            Engine.InputTagInfo TagInfo = new Engine.InputTagInfo();
            TagInfo.Info = new xPolimer.Engine.TagInfo[TagCount];            

            for (int Index = 0; Index < TagCount; Index++)
            {
                Validator = (RangeValidator)FindControl("CPvalidator" + Index.ToString());
                XElement xmlCell = xmlRoot.Elements(_XMLDATAROW).ElementAt(Index);              
                foreach (XElement xmlProp in xmlCell.Elements())
                {
                    switch (xmlProp.Name.ToString())
                    {
                        case _XMLVALVALUE:
                            TagInfo.Info[Index].ID = Convert.ToInt32(xmlProp.Value);
                            break;
                        case _XMLDATALABEL:
                            TagInfo.Info[Index].Label = xmlProp.Value;
                            break;
                        case _XMLDATAMIN: Validator.MinimumValue = xmlProp.Value; break;
                        case _XMLDATAMAX: Validator.MaximumValue = xmlProp.Value; break;
                        case _XMLVALARCHIVE: TagInfo.Info[Index].Int = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLVALTAGSUM: TagInfo.Info[Index].Sum = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLVALCALCID: TagInfo.Info[Index].CalcID = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLVALCALCNAME: TagInfo.Info[Index].CalcName = xmlProp.Value; break;
                    }
                }

                string Value;
                if (!ActionModule.Equals(_XMLDATAINNER))
                  Value = ORA.GetCurrentValue(TagInfo.Info[Index], DateTime.Today, true).ToString();
                else
                  Value = "0";

                ((Label)FindControl("CPlabelTag" + Index.ToString())).Text = TagInfo.Info[Index].Label;
                ((TextBox)FindControl("CPinputTag" + Index.ToString())).Text = Value;              
            }
            Session.Add(_SESSIONTAGINFO, TagInfo);
        }

        private void Output_Load()
        {
            if (ddlYear.Items.Count == 0)
            {
                GUI.FillYear(ddlYear);
                GUI.FillMonth(ddlMonth);
            }

            int iItemYear = Convert.ToInt32(ddlYear.SelectedValue);
            int iItemMonth = Convert.ToInt32(ddlMonth.SelectedValue);

            CPpanelInput.Visible = false;
            CPpanelInputLabel.Visible = false;
            CPdateEdit.Visible = true;
            CPtableMain.Rows.Clear();
            CPtableMain.Width = Unit.Percentage(100);

            XDocument xmlDocument = XML_Load(Config);
            XElement xmlRoot = xmlDocument.Root.Element(_XMLDATAPARAM);

            int TagCount = xmlRoot.Elements(_XMLDATACELL).Count();
            Engine.InputTagInfo TagInfo = new Engine.InputTagInfo();
            TagInfo.Info = new xPolimer.Engine.TagInfo[TagCount];    

            for (int Index = 0; Index < TagCount; Index++)
            {
                XElement xmlCell = xmlRoot.Elements(_XMLDATACELL).ElementAt(Index);
                foreach (XElement xmlProp in xmlCell.Elements())
                {
                    switch (xmlProp.Name.ToString())
                    {
                        case _XMLVALVALUE: TagInfo.Info[Index].ID = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLVALARCHIVE: TagInfo.Info[Index].Int = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLDATALABEL: TagInfo.Info[Index].Label = xmlProp.Value; break;
                    }
                }
            }
            ORA.GetTagValueOutput(TagInfo, iItemYear, iItemMonth, CPtableMain);
        }

        private void Plane_Load()
        {
            if (!AcessPlane) return;
            CPpanelInput.Visible = true;
            CPpanelInputLabel.Visible = true;
            CPdateEdit.Visible = false;
            CPsubmitData.Visible = true;
            GUI.FillMonthDated(CPlistDate);
            Input_Load(Config, _XMLDATAPLAN);
        }

        private void Inner_Load()
        {
            if (!AcessPlane) return;
            CPpanelInput.Visible = true;
            CPpanelInputLabel.Visible = true;
            CPdateEdit.Visible = false;
            CPsubmitData.Visible = true;
            GUI.FillDaySmena(CPlistDate, 0, 10);
            Input_Load(Config, _XMLDATAINNER);
        }

        private void Tech_Load()
        {
            if (!AcessTech) return;
            CPpanelInput.Visible = true;
            CPpanelInputLabel.Visible = true;
            CPdateEdit.Visible = false;
            CPsubmitData.Visible = true;
            GUI.FillDaySmena(CPlistDate, -10, 10);
            Input_Load(Config, _XMLDATATECH);
        }

        private void Proto_Load()
        {
            if (ddlYear.Items.Count == 0)
            {
                GUI.FillYear(ddlYear);
                GUI.FillMonth(ddlMonth);
            }

            int iItemYear = Convert.ToInt32(ddlYear.SelectedValue);
            int iItemMonth = Convert.ToInt32(ddlMonth.SelectedValue);

            CPpanelInput.Visible = false;
            CPpanelInputLabel.Visible = false;
            CPdateEdit.Visible = true;
            CPtableMain.Rows.Clear();

            XDocument xmlDocument = XML_Load(Config);
            XElement xmlRoot = xmlDocument.Root.Element(_XMLDATAPARAM);
            int TagCount = xmlRoot.Elements(_XMLDATACELL).Count();

            Engine.InputTagInfo TagInfo = new Engine.InputTagInfo();
            TagInfo.Info = new xPolimer.Engine.TagInfo[TagCount];            

            CPtableMain.Width = Unit.Percentage(100);

            for (int Index = 0; Index < TagCount; Index++)
            {
                XElement xmlCell = xmlRoot.Elements(_XMLDATACELL).ElementAt(Index);              
                foreach (XElement xmlProp in xmlCell.Elements())
                {
                    switch (xmlProp.Name.ToString())
                    {
                        case _XMLVALVALUE: TagInfo.Info[Index].ID = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLVALARCHIVE: TagInfo.Info[Index].Int = Convert.ToInt32(xmlProp.Value); break;
                        case _XMLDATALABEL: TagInfo.Info[Index].Label = xmlProp.Value; break;
                    }
                }
            }
            ORA.GetTagProtocol(TagInfo, iItemYear, iItemMonth, CPtableMain);
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ActionModule.ToString())
            {
                case _XMLDATAMAIN: Main_Load(); break;
                case _XMLDATAPARAM: Output_Load(); break;
                case _XMLDATAPROTO: Proto_Load(); break;
            }
        }

        protected void CPlistDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime WriteDate = Convert.ToDateTime(CPlistDate.Text);
            Engine.InputTagInfo TagInfo = (Engine.InputTagInfo)Session[_SESSIONTAGINFO];

            int Index = 0;
            foreach (Engine.TagInfo Info in TagInfo.Info)
            {
                ((TextBox)FindControl("CPinputTag" + Index.ToString())).Text = ORA.GetCurrentValue(Info, WriteDate, false).ToString();
                Index++;
            }
        }

        protected void CPsubmitData_Click(object sender, EventArgs e)
        {
            Engine.InputTagInfo TagInfo = (Engine.InputTagInfo)Session[_SESSIONTAGINFO];
            TORA.WriteType WriteType = TORA.WriteType.WriteNone;
            DateTime WriteDate = Convert.ToDateTime(CPlistDate.Text);
            int Value;

            switch (ActionModule)
            {
                case _XMLDATAPLAN: WriteType = TORA.WriteType.WriteMonth; break;
                case _XMLDATAINNER: WriteType = TORA.WriteType.WriteRests; break;
                case _XMLDATATECH: WriteType = TORA.WriteType.WriteCurrent; break;
            }
            try
            {
                int Index = 0;
                foreach (Engine.TagInfo Info in TagInfo.Info)
                {
                    Label LabelResult = ((Label)FindControl("CPcommit" + Index.ToString()));

                    Value = Convert.ToInt32(((TextBox)FindControl("CPinputTag" + Index.ToString())).Text);
                    LabelResult.Text = ORA.SetTagValueWrite(Info, Value, WriteType, WriteDate, CPlabelResult);
                    LabelResult.Visible = true;

                    Index++;
                }
            }
            finally
            {
                CPsubmitData.Visible = false;
                CPlabelResult.Visible = true;
            }
        }

        protected void CPcancelData_Click(object sender, EventArgs e)
        {
            CPsubmitData.Visible = true;
            switch (ActionModule)
            {
                case _XMLDATAPLAN: Plane_Load(); break;
                case _XMLDATAINNER: Inner_Load(); break;
                case _XMLDATATECH: Tech_Load(); break;
            }
        }

        protected void lbMain_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button) sender);
            Main_Load();
        }

        protected void CPinputPlane_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Plane_Load();
        }

        protected void CPInputInner_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Inner_Load();
        }

        protected void CPinputTech_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Tech_Load();
        }

        protected void CPoutput_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Output_Load();
        }

        protected void CPprotocol_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Proto_Load();
        }
    }
}
