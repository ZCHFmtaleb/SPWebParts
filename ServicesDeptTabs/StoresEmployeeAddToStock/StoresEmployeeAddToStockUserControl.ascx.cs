using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace ServicesDeptTabs.StoresEmployeeAddToStock
{
    public partial class StoresEmployeeAddToStockUserControl : UserControl
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
