using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using BoxInformation.Interfaces;
using BoxInformation.Presenter;


namespace BoxInformation
{
    public partial class ViewRecord : Page,  IView
    {
        BoxDetailsPresenter presenter;
        List<KeyValuePair<string, int>> boxData;


        protected void Page_Init(object sender, EventArgs e)
        {
            //test for null to save re-initializing every time
            if (presenter == null) presenter = new Presenter.BoxDetailsPresenter(this);
            
            ftbComments.EnableSsl = HttpContext.Current.Request.IsSecureConnection;
            ftbComments.SslUrl = "blank.htm";
            
            if (!Page.IsPostBack)
            {
                pnlUserView.Visible = false;
            }

            if (Request.QueryString["ID"] != null)
            {
                btnAddRecord.Visible = false;
                lblNumberOfBoxes.Visible = true;
                txtNumberOfBoxes.Visible = true;
                txtBoxLocation.Visible = true;
                btnAddBoxes.Visible = true;
                lblBoxLocation.Visible = true;
                presenter.GetRecordById(Request.QueryString["ID"]);
            }
            else
            {
                lblNumberOfBoxes.Visible = false;
                txtNumberOfBoxes.Visible = false;
                lblBoxLocation.Visible = false;
                txtBoxLocation.Visible = false;
                btnAddBoxes.Visible = false;
                btnDeleteRecord.Visible = false;
                btnUpdateRecord.Visible = false;
                lblCurrentFileName.Visible = false;
            }
            DrawBoxDetails();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void MandatoryFields()
        {

            String strProjNumb = txtClientNumber.Text;
            String strProjName = txtClientName.Text;
            String strProjLeader = txtClientLeader.Text;
            String strReviewDate = txtReviewDate.Text;

            if (strProjNumb == "")
            {
                lblErrorProjNumb.CssClass = "Mand";
                lblErrorProjNumb.Text = "This Field is Mandatory for Admin Users, Please Enter a Value";
            }

            if (strProjName == "")
            {
                lblErrorProjName.CssClass = "Mand";
                lblErrorProjName.Text = "This Field is Mandatory for Admin Users, Please Enter a Value";
            }


            if (strProjLeader == "")
            {
                lblErrorProjLeader.CssClass = "Mand";
                lblErrorProjLeader.Text = "This Field is Mandatory for Admin Users, Please Enter a Value";
            }


            if (strReviewDate != "")
            {
                try
                {
                    DateTime revDateDateTime = Convert.ToDateTime(strReviewDate);
                    txtReviewDate.Text = revDateDateTime.ToShortDateString();
                }
                catch
                {
                    //Will Just use the Default date.
                }
            }

            if (strReviewDate == "")
            {
                lblErrorReviewDate.CssClass = "Mand";
                lblErrorReviewDate.Text = "This Field is Mandatory for Admin Users, Please Enter a Value";
            }



        }


        #region IRecordView Members


        public bool SecureStorage
        {
            get { return cbxSecure.Checked; }
            set
            {
                cbxSecure.Checked = value;
                if (value)
                {
                    litSecure.Text = "Yes";
                }
                else
                {
                    litSecure.Text = "No";
                }
            }
        }


        public string Id
        {
            get { return Request.QueryString["ID"]; }
        }

        public DataSet searchResults
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string location
        {
            get { throw new NotImplementedException(); }
        }

        public string ClientNumber
        {
            get { return txtClientNumber.Text; }
            set {
                txtClientNumber.Text = value;
                litClientNumber.Text = value;
            }
        }


        public string ClientName
        {
            get { return txtClientName.Text; }
            set {
                txtClientName.Text = value;
                litClientName.Text = value;
            }
        }


        public string ClientPrincipal
        {
            get 
            {
                return txtClientLeader.Text; 
            }

            set
            {
                txtClientLeader.Text = value;
                litClientContact.Text = value;
            }
        }


        public DateTime? reviewDate
        {
            get 
            {
                DateTime? _date;
                DateTime _date2;
                
                if(DateTime.TryParse(txtReviewDate.Text, out _date2))
                {
                    _date = new DateTime?(_date2);
                }
                else
                {
                    _date = new DateTime?();
                }
                return _date;
            }

            set
            {
                txtReviewDate.Text = value.ToString();
                litReviewDate.Text = value.ToString();
            }
          
          
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string comments
        {
            get { return ftbComments.Text; }

            set {
                ftbComments.Text = value;
                litComments.Text = value;
            }
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public HttpPostedFile file
        {
            get { return fluDocument.PostedFile; }
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string fileName
        {
            get { return lblCurrentFileName.Text; }
            set {
                lblCurrentFileName.Text = value;
                litFileName.Text = "<a href='" + ConfigurationManager.AppSettings["FileServerLocation"] + value.ToString() + "' target='_blank'>View</a>";
                litDocumentFilename.Text = "<a href='" + ConfigurationManager.AppSettings["FileServerLocation"] + value.ToString() + "' target='_blank'>" + value.ToString() + "</a>";
            }
        }

        /// <summary>
        /// Gets the file2.
        /// </summary>
        /// <value>The file2.</value>
        public HttpPostedFile file2
        {
            get { return fluDocument2.PostedFile; }
        }

        /// <summary>
        /// Gets or sets the file name2.
        /// </summary>
        /// <value>The file name2.</value>
        public string fileName2
        {
            get { return lblCurrentFileName2.Text; }
            set
            {
                lblCurrentFileName2.Text = value;
                litFileName2.Text = "<a href='" + ConfigurationManager.AppSettings["FileServerLocation"] + value.ToString() + "'>View</a>";
                litDocumentFilename2.Text = "<a href='" + ConfigurationManager.AppSettings["FileServerLocation"] + value.ToString() + "'>" + value.ToString() + "</a>";
            }
        }

        /// <summary>
        /// Gets or sets the box details.
        /// </summary>
        /// <value>The box details.</value>
        public List<KeyValuePair<string, int>> boxDetails
        {

            get
            {
                return boxData;
            }

            set
            {
                boxData = value;
                DrawBoxDetails();
            }

        }

        /// <summary>
        /// DrawBoxDetails
        /// In order to create a dynamic list of locations and boxes we need to draw them based on the
        /// KeyValuePair list in Session (as BoxInformation).  This is called numerous times through the
        /// application.
        /// </summary>
        /// <returns>void</returns>
        private void DrawBoxDetails()
        {
            if (boxData == null)
            {
                boxData = new List<KeyValuePair<string, int>>();
            }

            pnlBoxDetails.Controls.Clear();

            Literal litHeader = new Literal();

            pnlBoxDetails.Controls.Add(litHeader);
            
            
            litHeader.ID = "litBoxesHeader";
            litHeader.Text = "<fieldset><legend>Box Details</legend><ol>";


            foreach (KeyValuePair<string, int> currentKVP in boxData)
            {

                if (pnlAddBoxes.FindControl("txt" + currentKVP.Key.ToString() + "Boxes") == null)
                {
                    Literal myStartLit = new Literal();
                    Label myOfficeLabel = new Label();
                    TextBox myBoxesTextBox = new TextBox();
                    Literal myEndLit = new Literal();
                    Button myChangeButton = new Button();

                    pnlBoxDetails.Controls.Add(myStartLit);
                    pnlBoxDetails.Controls.Add(myOfficeLabel);
                    pnlBoxDetails.Controls.Add(myBoxesTextBox);
                    pnlBoxDetails.Controls.Add(myChangeButton);
                    pnlBoxDetails.Controls.Add(myEndLit);

                    myStartLit.ID = "litStart" + currentKVP.Key.ToString();
                    myStartLit.Text = "<li>";

                    myOfficeLabel.ID = "lbl" + currentKVP.Key.ToString();
                    myOfficeLabel.Text = currentKVP.Key.ToString();

                    myBoxesTextBox.ID = "txt" + currentKVP.Key.ToString() + "Boxes";
                    myBoxesTextBox.Text = currentKVP.Value.ToString();
                    myBoxesTextBox.EnableViewState = true;
                    myBoxesTextBox.AutoPostBack = false;

                    myChangeButton.ID = "btn" + currentKVP.Key.ToString() + "Boxes";
                    myChangeButton.Text = "Update Box";
                    myChangeButton.Click += new EventHandler(myBoxesTextBox_TextChanged);

                    myEndLit.ID = "litEnd" + currentKVP.Key.ToString();
                    myEndLit.Text = "</li>";


                }
            }

            Literal litFooter = new Literal();

            pnlBoxDetails.Controls.Add(litFooter);

            litFooter.ID = "litBoxesFooter";
            litFooter.Text = "</ol></fieldset>";


        }


        /// <summary>
        /// myBoxesTextBox_TextChanged
        /// Event to handle changes in the dynamically created text boxes made during DrawBoxDetails()
        /// Cycles through the Text Boxes and finds which KVP we have edited.  Sets the new Key Value
        /// Pair to the new value.  If the value is set to zero it is removed.  The session is then
        /// updated with the new values
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        public void myBoxesTextBox_TextChanged(object sender, EventArgs e)
        {
            //Change the boxData list for the right box
            Button btnSender = (Button)sender;
            string txtID = btnSender.ID.Replace("btn", "txt");
            TextBox senderBox = (TextBox)FindControl(txtID);
            string officeID = senderBox.ID;
            officeID=officeID.Replace("txt", "");
            officeID=officeID.Replace("Boxes", "");
            int counter = 0;
            int indexToChange = -1;
            string Office = "";

            foreach (KeyValuePair<string, int> currentKVP in boxData)
            {
                if (currentKVP.Key.ToString() == officeID)
                {
                    indexToChange = counter;
                    Office = currentKVP.Key.ToString();
                }
                counter++;
            }

            if (indexToChange != -1)
            {
                if (int.Parse(senderBox.Text) == 0)
                {
                    boxData.RemoveAt(indexToChange);
                }
                else
                {
                    boxData[indexToChange] = new KeyValuePair<string, int>(Office, int.Parse(senderBox.Text));
                }
            }

            presenter.UpdateRecord();
            txtBoxLocation.Text = "";
            txtNumberOfBoxes.Text = "";
            presenter.GetRecordById(Request.QueryString["ID"]);
            DrawBoxDetails();

        }

        /// <summary>
        /// AddRecord_Click
        /// Event to handle changes the Add Record button.  Checks for Mandatory fields then calls the Presenter
        /// to add a record.  Currently passes a parameter of Client name, but this shouldn't be required.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        public void AddRecord_Click(object sender, EventArgs e)
        {
            //Call To Method To ensure Madatory Fields have been completed. HMS 14/07/09
            MandatoryFields();

            // Remove Client Name from this method call - DM 24 July 09
            presenter.AddRecord();
            txtBoxLocation.Text = "";
            txtNumberOfBoxes.Text = "";

        }

        /// <summary>
        /// UpdateRecord_Click
        /// Event to handle changes the Update Record button.  Checks for Mandatory fields then calls the Presenter
        /// to Update Record.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        protected void UpdateRecord_Click(object sender, EventArgs e)
        {
            //Call To Method To ensure Madatory Fields have been completed. HMS 14/07/09
            MandatoryFields();

            presenter.UpdateRecord();
            txtBoxLocation.Text = "";
            txtNumberOfBoxes.Text = "";
            presenter.GetRecordById(Request.QueryString["ID"]);
        }

        /// <summary>
        /// DeleteRecord_Click
        /// Event to handle changes the Delete Record button.Calls the Presenter to delete the current record.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        protected void DeleteRecord_Click(object sender, EventArgs e)
        {
            presenter.DeleteRecord();
        }
        #endregion

        protected void AddBoxes_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["ID"] != null)
            {
                if (txtBoxLocation.Text.Length > 1)
                {
                    if (boxData != null)
                    {
                        boxData.Add(new KeyValuePair<string, int>(txtBoxLocation.Text, int.Parse(txtNumberOfBoxes.Text)));
                    }
                    else
                    {
                        boxData = new List<KeyValuePair<string, int>>();
                        boxData.Add(new KeyValuePair<string, int>(txtBoxLocation.Text, int.Parse(txtNumberOfBoxes.Text)));
                    }
                }

                presenter.UpdateRecord();
                txtBoxLocation.Text = "";
                txtNumberOfBoxes.Text = "";

                presenter.GetRecordById(Request.QueryString["ID"]);
                DrawBoxDetails();
            }
            else
            {
                Response.Redirect("search.aspx");
            }

        }

        public void btnDeleteFile1_Click(object sender, EventArgs e)
        {
            // Remove File1 from Record
            presenter.DeleteManifest();
            lblCurrentFileName.Text = "";
            litFileName.Text = "";
            litDocumentFilename.Text = "";

        }

        public void btnDeleteFile2_Click(object sender, EventArgs e)
        {
            // Remove File1 from Record
            presenter.DeleteAgreement();
            lblCurrentFileName2.Text = "";
            litFileName2.Text = "";
            litDocumentFilename2.Text = "";

        }

    }
}
