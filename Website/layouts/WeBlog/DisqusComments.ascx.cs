using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Modules.WeBlog.Layouts;

namespace Sitecore.Modules.WeBlog.layouts.WeBlog
{
    public partial class DisqusComments : BaseEntrySublayout
    {
        protected string PageUrl
        {
            get
            {
                return HttpUtility.JavaScriptStringEncode(CurrentEntry.AbsoluteUrl);
            }
        }

        protected string PageId
        {
            get
            {
                return CurrentEntry.ID.ToShortID().ToString();
            }
        }

        public string DisqusHostName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}