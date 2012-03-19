using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BrickRed.Templates.SmallBusiness.ContactUsAddressWebPart;

namespace BrickRed.Templates.SmallBusiness
{
    class ContactUsAddressEditorPart : EditorPart
    {
        private Panel pnlAddressSettings;
        //private Panel pnlCommonSettings;
        private Panel pnlProperty;
        private Panel pnlPropertyName;
        private Panel pnlPropertyControl;
        private Label lblPropertyName;
        private TextBox txtCompanyName;
        private TextBox txtComapanyAddress;
        private HtmlGenericControl paragraph;
        

        public ContactUsAddressEditorPart(string webPartId)
        {
            this.ID = "ContactUsAddressEditorPart" + webPartId;
            this.Title = "BrickRed.SmallBusiness";
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //pnlCommonSettings = new Panel();
            pnlAddressSettings = new Panel();

            //Company Name
            pnlProperty = new Panel();
            pnlPropertyName = new Panel();
            pnlPropertyControl = new Panel();

            lblPropertyName = new Label();
            lblPropertyName.Text = "Comapny Name: <br/><br/>";
            pnlPropertyName.Controls.Add(lblPropertyName);
            pnlProperty.Controls.Add(pnlPropertyName);

            txtCompanyName = new TextBox();
            txtCompanyName.Width = Unit.Percentage(100);
            pnlPropertyControl.Controls.Add(txtCompanyName);
            pnlProperty.Controls.Add(pnlPropertyControl);

            paragraph = new HtmlGenericControl("p");
            paragraph.Controls.Add(pnlProperty);
            pnlAddressSettings.Controls.Add(paragraph);

            //Company Address
            pnlProperty = new Panel();
            pnlPropertyName = new Panel();
            pnlPropertyControl = new Panel();

            lblPropertyName = new Label();
            lblPropertyName.Text = "Company Address: <br/><br/>";
            pnlPropertyName.Controls.Add(lblPropertyName);
            pnlProperty.Controls.Add(pnlPropertyName);

            txtComapanyAddress = new TextBox();
            txtComapanyAddress.TextMode = TextBoxMode.MultiLine;
            txtComapanyAddress.Width = Unit.Percentage(100);
            pnlPropertyControl.Controls.Add(txtComapanyAddress);
            pnlProperty.Controls.Add(pnlPropertyControl);

            paragraph = new HtmlGenericControl("p");
            paragraph.Controls.Add(pnlProperty);
            pnlAddressSettings.Controls.Add(paragraph);

            this.Controls.Add(pnlAddressSettings);

        }

        public override bool ApplyChanges()
        {
            EnsureChildControls();

            ContactUsAddressWebPart.ContactUsAddressWebPart webPart = this.WebPartToEdit as ContactUsAddressWebPart.ContactUsAddressWebPart;

            if (webPart != null)
            {
                webPart.CompanyName = txtCompanyName.Text;
                webPart.CompanyAddress = txtComapanyAddress.Text;
            }

            return true;
        }

        public override void SyncChanges()
        {
            EnsureChildControls();
            ContactUsAddressWebPart.ContactUsAddressWebPart webPart = this.WebPartToEdit as ContactUsAddressWebPart.ContactUsAddressWebPart;

            if (webPart != null)
            {
                txtCompanyName.Text = webPart.CompanyName;
                txtComapanyAddress.Text = webPart.CompanyAddress;               
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
