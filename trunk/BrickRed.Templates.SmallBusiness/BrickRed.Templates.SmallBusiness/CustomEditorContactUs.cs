using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BrickRed.Templates.SmallBusiness
{
    class CustomEditorContactUs : EditorPart
    {
        TextBox tbUserEmail;
        RadioButtonList rdoSendMailTo;

        public override void SyncChanges()
        {
            EnsureChildControls();
            BrickRed.Templates.SmallBusiness.ContactUsWebPart.ContactUsWebPart ThisWebPart = (BrickRed.Templates.SmallBusiness.ContactUsWebPart.ContactUsWebPart)WebPartToEdit;
            tbUserEmail.Text = ThisWebPart.userEmail;
        }

        //To Ensure web part property values reflect what is set by the userin the custom editor controls
        public override bool ApplyChanges()
        {
            EnsureChildControls();
            BrickRed.Templates.SmallBusiness.ContactUsWebPart.ContactUsWebPart ThisWebPart = (BrickRed.Templates.SmallBusiness.ContactUsWebPart.ContactUsWebPart)WebPartToEdit;
            ThisWebPart.userEmail = tbUserEmail.Text;
            return true;
        }

       
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Panel panUserEmail = new Panel();
            rdoSendMailTo = new RadioButtonList();
            rdoSendMailTo.Items.Add(new ListItem("Send Mail To Admins", "Admin"));
            rdoSendMailTo.Items.Add(new ListItem("Send Mail To Users", "Users"));
            rdoSendMailTo.Items.FindByValue("Admin").Selected = true;
            rdoSendMailTo.AutoPostBack = false;
            panUserEmail.Controls.Add(rdoSendMailTo);
            Label labUserEmail = new Label();
            labUserEmail.Text = "Users Email:";
            tbUserEmail = new TextBox();
            panUserEmail.Controls.Add(labUserEmail);
            panUserEmail.Controls.Add(new LiteralControl("<br>"));
            panUserEmail.Controls.Add(tbUserEmail);
            this.Controls.Add(panUserEmail);
        }
    }
}
