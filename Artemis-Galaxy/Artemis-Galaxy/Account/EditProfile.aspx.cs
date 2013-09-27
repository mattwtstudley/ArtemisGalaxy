using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Artemis_Galaxy.Account
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadTimeZonesIntoDropDown();
            }
        }

        private void loadTimeZonesIntoDropDown()
        {
            ddlTimezone.Items.Clear();
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                ddlTimezone.Items.Add(new ListItem(tz.DisplayName,tz.Id));
            }
        }
    }
}