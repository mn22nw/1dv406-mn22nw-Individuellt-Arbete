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
            //Om genomförd handling lyckades av klienten och meddelande finns så visas det
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

        protected void MaterialListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            var label = e.Item.FindControl("InstrumentNameLabel") as Label;
            if (label != null)
            {
                // Typomvandlar e.Item.DataItem så att primärnyckelns värde kan hämtas och...
                var material = (Material)e.Item.DataItem;

                // ...som sedan kan användas för att hämta ett instrumentobjekt...
                var instrument = Service.GetInstruments()
                    .Single(instr => instr.InstrumentID == material.InstrumentID);

                // ...så att en beskrivning av instrument kan presenteras; ex: instrument:piano
                label.Text = String.Format(label.Text, instrument.Namn);
            }

        }
    }
}