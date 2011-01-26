using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BoxInformation.Interfaces;


namespace BoxInformation
{
    public partial class ViewRecord : Page,  IView
    {
        Presenter.BoxDetailsPresenter _mBoxDetailsPresenter;
        List<KeyValuePair<string, int>> m_boxDetails;

        /// <summary>
        /// Page Init
        /// We are using this to create all of our dynamic controls for the page so that Viewstate will
        /// render correctly.  We also instanciate our Presenter in Init.  If the page has an "ID"
        /// parameter passed to it in the query string it will display as an EDIT form for the matching
        /// record, otherwise it displays as an ADD RECORD form.
        /// 
        /// Be aware of page render order and the dynamic creation of the Add Boxes controls.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        protected void Page_Init(object sender, EventArgs e)
        {
            //test for null to save re-initializing every time
            if (_mBoxDetailsPresenter == null) _mBoxDetailsPresenter = new Presenter.BoxDetailsPresenter(this);
            
            ftbComments.EnableSsl = HttpContext.Current.Request.IsSecureConnection;
            ftbComments.SslUrl = "blank.htm";
            //Page postback test to determin if we need to re-render the controls
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
                _mBoxDetailsPresenter.GetEntry(Request.QueryString["ID"]);
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

        /// <summary>
        /// Page Load
        /// Currently empty as we use Page Init extensively.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Mandatories the fields.
        /// </summary>
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

            //The Following will take a Value and Convert it to a Date. N.B. The Data comes
            //From a Text Box with no restrictions so this needs to be rewritten to accomodate all
            //the possibilites. For now, if it can't complete this then the Default date will be used / Added to the DB. HMS 15/07/09
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

        /// <summary>
        /// Gets or sets a value indicating whether [on microfilm].
        /// </summary>
        /// <value><c>true</c> if [on microfilm]; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
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

        /// <summary>
        /// Gets or sets the Client number.
        /// </summary>
        /// <value>The Client number.</value>
        public string ClientNumber
        {
            get { return txtClientNumber.Text; }
            set {
                txtClientNumber.Text = value;
                litClientNumber.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the Client.
        /// </summary>
        /// <value>The name of the Client.</value>
        public string ClientName
        {
            get { return txtClientName.Text; }
            set {
                txtClientName.Text = value;
                litClientName.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the Client leader.
        /// </summary>
        /// <value>The Client leader.</value>
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


        /// <summary>
        /// Gets or sets the review date.
        /// </summary>
        /// <value>The review date.</value>
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
                return m_boxDetails;
            }

            set
            {
                m_boxDetails = value;
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
            if (m_boxDetails == null)
            {
                m_boxDetails = new List<KeyValuePair<string, int>>();
            }

            pnlBoxDetails.Controls.Clear();

            Literal litHeader = new Literal();

            pnlBoxDetails.Controls.Add(litHeader);
            
            
            litHeader.ID = "litBoxesHeader";
            litHeader.Text = "<fieldset><legend>Box Details</legend><ol>";


            foreach (KeyValuePair<string, int> currentKVP in m_boxDetails)
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
            //Change the m_boxDetails list for the right box
            Button btnSender = (Button)sender;
            string txtID = btnSender.ID.Replace("btn", "txt");
            TextBox senderBox = (TextBox)FindControl(txtID);
            string officeID = senderBox.ID;
            officeID=officeID.Replace("txt", "");
            officeID=officeID.Replace("Boxes", "");
            int counter = 0;
            int indexToChange = -1;
            string Office = "";

            foreach (KeyValuePair<string, int> currentKVP in m_boxDetails)
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
                    m_boxDetails.RemoveAt(indexToChange);
                }
                else
                {
                    m_boxDetails[indexToChange] = new KeyValuePair<string, int>(Office, int.Parse(senderBox.Text));
                }
            }

            bool result = _mBoxDetailsPresenter.UpdateRecord();
            txtBoxLocation.Text = "";
            txtNumberOfBoxes.Text = "";
            _mBoxDetailsPresenter.GetEntry(Request.QueryString["ID"]);
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
            bool result = _mBoxDetailsPresenter.AddRecord(txtClientName.Text);
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

            bool result = _mBoxDetailsPresenter.UpdateRecord();
            txtBoxLocation.Text = "";
            txtNumberOfBoxes.Text = "";
            _mBoxDetailsPresenter.GetEntry(Request.QueryString["ID"]);
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
            bool result = _mBoxDetailsPresenter.DeleteRecord();
        }
        #endregion

        /// <summary>
        /// AddBoxes_Click
        /// Event to handle changes the Add Boxes button.  Updates the Boxes if the Key Value Pair List already has
        /// values in it, or creates a new list if it doesn't.  Then adds the list to Session.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        protected void AddBoxes_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["ID"] != null)
            {
                if (txtBoxLocation.Text.Length > 1)
                {
                    if (m_boxDetails != null)
                    {
                        m_boxDetails.Add(new KeyValuePair<string, int>(txtBoxLocation.Text, int.Parse(txtNumberOfBoxes.Text)));
                    }
                    else
                    {
                        m_boxDetails = new List<KeyValuePair<string, int>>();
                        m_boxDetails.Add(new KeyValuePair<string, int>(txtBoxLocation.Text, int.Parse(txtNumberOfBoxes.Text)));
                    }
                }

                bool result = _mBoxDetailsPresenter.UpdateRecord();
                txtBoxLocation.Text = "";
                txtNumberOfBoxes.Text = "";

                    _mBoxDetailsPresenter.GetEntry(Request.QueryString["ID"]);
                    DrawBoxDetails();
                }
                else
                {
                    Response.Redirect("search.aspx");
                }

        }

        /// <summary>
        /// btnDeleteFile1_Click
        /// Event to handle Deleting of File1 record.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        public void btnDeleteFile1_Click(object sender, EventArgs e)
        {
            // Remove File1 from Record
            _mBoxDetailsPresenter.DeleteFile1();
            lblCurrentFileName.Text = "";
            litFileName.Text = "";
            litDocumentFilename.Text = "";

        }

        /// <summary>
        /// btnDeleteFile2_Click
        /// Event to handle Deleting of File2 record.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        /// <returns>void</returns>
        public void btnDeleteFile2_Click(object sender, EventArgs e)
        {
            // Remove File1 from Record
            _mBoxDetailsPresenter.DeleteFile2();
            lblCurrentFileName2.Text = "";
            litFileName2.Text = "";
            litDocumentFilename2.Text = "";

        }

    }
}
