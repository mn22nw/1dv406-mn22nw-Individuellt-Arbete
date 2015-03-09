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
            var songs = Service.GetSongs();
           
            //Lägg till ett nytt värde för dropdownlistan Instrument (så att användaren kan välja att visa alla Instrument)
            DropDownList instruments = (DropDownList)DropdownPanel.FindControl("ddlInstruments");
            ListItem li = new ListItem("Alla Instrument", "0");
            if (!(instruments.Items.Contains(li)))
            {
                instruments.Items.Insert(0, li);
            }

            if (!IsPostBack)
            {
                return songs;
            }
          
            if (IsPostBack)
            {
                var instrumentList = Service.GetInstruments() as List<Instrument>;

                instruments.SelectedValue = Request.Form[instruments.UniqueID];

                //Om värdet i dropdownlistan är ändrat från 0 har användaren valt att visa ett speciellt instrument
                if (Convert.ToInt32(instruments.SelectedValue) != 0)
                {
                    var songList = new List<Material>(100);
                    //Visa bara låtar för det instrumentet användaren har valt
                    foreach (Material material in songs)
                    {
                        if (material.InstrumentID == Convert.ToInt32(instruments.SelectedValue))
                        {
                            songList.Add(material);
                        }
                    }
                    songList.TrimExcess(); // krymper till det faktiskta antalet element som är utnyttjat 
                    return songList as IEnumerable<Material>;
                }
            }
            return songs;
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
        public IEnumerable<Instrument> InstrumentList_GetData()
        {
            return Service.GetInstruments();
        }
    }
}