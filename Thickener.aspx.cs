using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace xPolimer
{
    public partial class _Thickener : System.Web.UI.Page
    {
        private TGUI GUI;
        private TORA ORA;
        private String ActionModule;
        private String Config = "Config\\thickenerGMC2.xml";
        private bool AcessWrite;
        private const String _XMLCLASSHEAD = "bg_header";
        private const String _XMLCLASSCELL = "bg_cell";
        private const String _XMLCLASSCELLNOSEL = "bg_cellnosel";
        private const String _XMLDATAMAIN = "maintable";
        private const String _XMLDATAPROTO = "mainproto";
        private const String _XMLDATAINNER = "inputinner";
        private const String _XMLDATAROW = "row";
        private const String _XMLDATACELL = "cell";
        private const String _XMLDATAHEAD = "header";
        private const String _XMLDATACAPT = "caption";
        private const String _XMLDATACAPTEX = "captionex";
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
        private const String _SESSIONACCESSWRITE = "AccessWrite";
        private const String _GUIBUTTONDEF = "bg_button";
        private const String _GUISUBMITEND = "Готово!";
        private const String _GUICANCELBTN = "Отмена";
        private const String _GUIBUTTONSEL = "bg_button_sel";

        public String GetUserName2()
        {
            return HttpContext.Current.User.Identity.Name.Replace("PAZ\\", "").Replace("paz\\", "").Replace("AOK\\", "").Replace("aok\\", "");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GUI = new TGUI();
            ORA = new TORA();

            // 2389 - xFramFrenzy :: Ввод сгустителей ГМЦ-2
            // 2 - права на запись
            if (Session[_SESSIONACCESSWRITE] == null)
            {
                AcessWrite = GUI.GetUserRightForResource(2389) == 2;
                Session.Add(_SESSIONACCESSWRITE, AcessWrite);
            }
            else AcessWrite = Convert.ToBoolean(Session[_SESSIONACCESSWRITE]);

            if (!AcessWrite)
            {
                CPInputInner.Visible = false;
            }

            ActionModule = Request.QueryString.ToString();
            Config = System.AppDomain.CurrentDomain.BaseDirectory + Config;

            switch (ActionModule)
            {
                case _XMLVALEMPTY: Output_Load(); break;
            }
        }

        protected XDocument XML_Load(String FileName)
        {
            String DayNow;
            switch (ActionModule)
            {
                case _XMLDATAMAIN:
                case _XMLDATAPROTO:
                case _XMLVALEMPTY:
                    DayNow = " за " + CPlistDate.Text;
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
            if (Selected == CPInputInner)
            {
                CPprint.Enabled = false;
                CPInputInner.CssClass = _GUIBUTTONSEL;
            }
            else CPInputInner.CssClass = _GUIBUTTONDEF;
            if (Selected == CPprotocol)
            {
                CPprint.Enabled = false;
                CPprotocol.CssClass = _GUIBUTTONSEL;
            }
            else CPprotocol.CssClass = _GUIBUTTONDEF;
        }

        protected void Inner_Load(String xmlFile, String XPath)
        {
            XDocument xmlDocument = XML_Load(xmlFile);
            XElement xmlRoot = xmlDocument.Root.Element(XPath);
            int TagCount = xmlRoot.Elements(_XMLDATAROW).Count();
            RangeValidator Validator = null;

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
            }
            Session.Add(_SESSIONTAGINFO, TagInfo);
            ORA.GetTagThickenerInput(Page, TagInfo, CPtableMain);
        }

        private void Output_Load()
        {
            if (CPlistDate.Items.Count == 0)
            {
                GUI.FillDaySmena(CPlistDate, -31, 0);
            }

            CPdateEdit.Visible = true;
            PanelPostData.Visible = false;

            XDocument xmlDocument = XML_Load(Config);
            XElement xmlRoot = xmlDocument.Root.Element(_XMLDATAMAIN);

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
            ORA.GetTagThickenerOutput(Page, TagInfo, CPtableMain, CPlistDate.SelectedValue);
        }

        private void Proto_Load()
        {
            if (CPlistDate.Items.Count == 0)
            {
                GUI.FillDaySmena(CPlistDate, -31, 0);
            }

            CPdateEdit.Visible = true;
            PanelPostData.Visible = false;

            XDocument xmlDocument = XML_Load(Config);
            XElement xmlRoot = xmlDocument.Root.Element(_XMLDATAMAIN);
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
            ORA.GetTagProtocol(TagInfo, CPlistDate.SelectedValue, CPtableMain);
        }

        private void Inner_Load()
        {
            if (!AcessWrite) return;
            CPdateEdit.Visible = false;
            CPsubmitData.Visible = true;
            PanelPostData.Visible = true;
            CPcancelData.Text = _GUICANCELBTN;
            Inner_Load(Config, _XMLDATAINNER);
        }

        protected void lbMain_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Output_Load();
        }

        protected void CPInputInner_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Inner_Load();
        }

        protected void CPprotocol_Click(object sender, EventArgs e)
        {
            ChangeButtonStyle((System.Web.UI.WebControls.Button)sender);
            Proto_Load();
        }

        protected void CPsubmitData_Click(object sender, EventArgs e)
        {
            Engine.InputTagInfo TagInfo = (Engine.InputTagInfo)Session[_SESSIONTAGINFO];
            int TagIndex = 0;
            double TagValue;
            DateTime Date = DateTime.Now;

            try
            {
                foreach (Engine.TagInfo Info in TagInfo.Info)
                {
                    TagValue = Convert.ToDouble(((TextBox)FindControl("CPinputTag" + TagIndex.ToString())).Text);
                    ORA.SetTagValueWrite(Info, TagValue, TORA.WriteType.WriteDuoHour, Date, null);
                    TagIndex++;
                }
                Inner_Load();
            }
            finally
            {
                CPsubmitData.Visible = false;
                CPcancelData.Text = _GUISUBMITEND;
            }
        }

        protected void CPcancelData_Click(object sender, EventArgs e)
        {
            CPsubmitData.Visible = true;
            CPcancelData.Text = _GUICANCELBTN;
            switch (ActionModule)
            {
                case _XMLDATAINNER: Inner_Load(); break;
            }
        }

        protected void CPlistDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ActionModule)
            {
                case _XMLDATAMAIN: Output_Load(); break;
                case _XMLDATAPROTO: Proto_Load(); break;
            }
        }
    }
}
