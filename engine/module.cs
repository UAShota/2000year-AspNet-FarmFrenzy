using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace xPolimer.Engine
{
    public struct TagInfo
    {
        public int ID;
        public int Int;
        public int Sum;
        public int CalcID;
        public String CalcName;
        public String Label;
    }

    public struct InputTagInfo
    {
        public TagInfo[] Info;
        public String Caption;
    }
}
