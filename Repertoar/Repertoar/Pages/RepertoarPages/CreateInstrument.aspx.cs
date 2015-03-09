using Repertoar.App_GlobalResourses;
using Repertoar.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Repertoar.Pages.RepertoarPages
{
    public partial class CreateInstrument : System.Web.UI.Page
    {
        #region Service-objekt
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Om genomförd handling lyckades av klienten och meddelande finns så visas det
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }


        public void InstrumentFormView_InsertInstrument(Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //*SPARAR DET NYA INSTRUMENTET  - IFALL ALLT ÄR VALIDERAT OCH OKEJ*//
                    instrument.InstrumentID = Service.SaveInstrument(instrument);

                    //sätter meddelande till klienten
                    Page.SetTempData("SuccessMessage", "Instrumentet: " + instrument.Namn + " har lagts till.");

                    //Omdirigerar klienten tillbaka till listan
                    Response.RedirectToRoute("Default", false);
                    Context.ApplicationInstance.CompleteRequest();
                }

                catch (Exception ex)
                {
                    //sätter felmeddelande till klienten
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }

        }
    }
}