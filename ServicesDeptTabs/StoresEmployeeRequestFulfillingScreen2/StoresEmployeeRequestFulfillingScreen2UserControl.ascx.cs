using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ServicesDeptTabs.StoresEmployeeRequestFulfillingScreen2
{
    public partial class StoresEmployeeRequestFulfillingScreen2UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlMeta metaEdgeIE = new HtmlMeta();
            metaEdgeIE.HttpEquiv = "X-UA-Compatible";
            metaEdgeIE.Content = "IE=EDGE";
            Page.Header.Controls.AddAt(0, metaEdgeIE);
        }
    }
}
