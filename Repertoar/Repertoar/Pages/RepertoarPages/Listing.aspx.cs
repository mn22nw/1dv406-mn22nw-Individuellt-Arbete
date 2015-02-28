using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        protected void exitbutton_OnClick(object sender, EventArgs e)
        {
            SuccessMessagePanel.Visible = false;
        }

        public IEnumerable<Material> MaterialListView_GetData()
        {
            return Service.GetSongs();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_DeleteSong(int MID)
        {
            try
            {
                Service.DeleteSong(MID);
                Page.SetTempData("SuccessMessage", "Låten togs bort.");
                Response.RedirectToRoute("ContactListing");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade när kontaktuppgiften skulle tas bort");
            }
        }

    }
}