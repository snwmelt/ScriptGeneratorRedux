using ClinPhone.Connections;
using ClinPhone.Instances;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace DCR_Script_Generator
{
    public partial class ScriptGenerator : Form
    {
//        Study thisStudy;
        public static StudyDetail thisStudy;

        //Connecting
        public static QueryableConnection connection;
        InstanceService instanceService;
        Instance instance;
        List<Study> studies;


        Update_Queries updateQueryObj;
        DCR_Script dcrScript;
        public static Update_param updateParam;
        String scriptXmlText;


        public ScriptGenerator()
        {
            InitializeComponent();
            resetForm();
            initialiseQuery();
            txtPassword.PasswordChar = '*';

            populateServerDetails();
            populateValidScripts();

        }

        private void populateValidScripts()
        {
            var fileNames = Directory.GetFiles("Scripts", "*", SearchOption.AllDirectories).Select(f => Path.GetFileNameWithoutExtension(f));

            foreach (String x in fileNames)
            {
                cmbUpdateType.Items.Add(x);

            }
                        
        }

        private bool setUpParams()
        {
            String searchString = "Scripts/" + cmbUpdateType.Text + ".xml";
            try
            {
                //Read in the xml
                System.IO.StreamReader objReader;
                objReader = new System.IO.StreamReader(searchString);
                scriptXmlText = objReader.ReadToEnd();
                objReader.Close();
            }
            catch
            {
                MessageBox.Show("Error reading: "+searchString);
                return false;
            }


            //Convert scriptXmlText to dcrParams object
            TextReader txtReader = new StringReader(scriptXmlText);

            //This creates xml based on the QueryList object
            XmlSerializer serializer = new XmlSerializer(typeof(DCR_Script));

            //Sets up the entire script    
            dcrScript = (DCR_Script)serializer.Deserialize(txtReader);

            txtReader.Close();

            //Set up the updateParam to be refreshed
            updateParam = dcrScript.updateParamList.First();

            string displayMessage = "";
            //Check up study and initialise
            if (validStudy(ref displayMessage, txtStudyID.Text))
            {
                if (updateParam.inputParamList.Count == 0)
                {
                    rtbStudyVerifications.Text = "Parameters: N/A";
                    btnVerify.Enabled = true;
                }
                else
                {
                    FormGetParams frmGetParams = new FormGetParams();
                    if (frmGetParams.ShowDialog() == DialogResult.OK)
                    {
                        string paramText = "Parameters:\n=========";

                        foreach (var x in updateParam.inputParamList)
                        {
                            paramText += "\n" + x.paramDesc + ": " + x.value;
                        }
                        rtbStudyVerifications.Text = paramText;
                        btnVerify.Enabled = true;
                    }

                }
            }
            else
            {
                MessageBox.Show(displayMessage);
            }


            return true;
        }

        private void initialiseQuery()
        {
            updateQueryObj = new Update_Queries();
            dcrScript = new DCR_Script();
       }


        #region notused
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
            initialiseQuery();
        }

        private void resetForm()
        {
            fastTextOutput.Text = "";
            btnGenerateScript.Enabled = false;
            btnVerify.Enabled = false;
            rtbStudyVerifications.Text = "<Parameters Not Set>";
            txtStudyID.Text = "";
            cmbUpdateType.Text = "<Please Select>";
            scriptXmlText = "";
        }

        /// <summary>
        /// Do the Script
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateScript_Click(object sender, EventArgs e)
        {
            string theScript = "";

            theScript = "--Run:";
            theScript += "\nUSE SECURITY ";
            theScript += "\nSELECT  S.StudyID, "+thisStudy.dataDatabaseField+", DesignDatabaseName, Servers.Name ServerName FROM Studies s \nINNER JOIN Servers ON Servers.ServerID = S.ServerID \nINNER JOIN Versions ON Versions.VersionID = "+thisStudy.versionIDField+" \nWHERE s.StudyID =" + thisStudy.studyID;
            theScript += "\n--Verify that " + thisStudy.dataDatabaseField + " is set to " + thisStudy.dataDatabase + ", DesignDatabaseName is set to " + thisStudy.designDatabase + " and serverName is set to " + thisStudy.server;
            theScript += "\n--Take a Screenshot";
            theScript += "\n\n--Run the following on the "+thisStudy.server+" server";

            List<Query> preZeroChecks = updateQueryObj.querys.Where(p => p.preRecordsReturned == "0" && p.includeSql == "true").ToList();
            //Dont bother doing the post checks and executions if it should stay same
            List<Query> prePosChecks = updateQueryObj.querys.Where(p => !String.IsNullOrWhiteSpace(p.preCheckSql) && p.includeSql == "true").ToList();
            prePosChecks = prePosChecks.Where(p => int.Parse(p.preRecordsReturned) > 0 ).ToList();

            List<Query> executionQuery = updateQueryObj.querys.Where(p => !String.IsNullOrWhiteSpace(p.updateSql) && p.includeSql == "true").ToList();            
//            executionQuery = executionQuery.Where(p => int.Parse(p.updateCount) > 0 && p.preRecordsReturned != p.postRecordsReturned).ToList();
            executionQuery = executionQuery.Where(p => int.Parse(p.updateCount) > 0).ToList();

            List<FreeText> preFreeText = updateQueryObj.freeText.Where(p => !String.IsNullOrWhiteSpace(p.preFreeText) && p.includeSql == "true").ToList();
            List<FreeText> updateFreeText = updateQueryObj.freeText.Where(p => !String.IsNullOrWhiteSpace(p.updateFreeText) && p.includeSql == "true").ToList();
            List<FreeText> endFreeText = updateQueryObj.freeText.Where(p => !String.IsNullOrWhiteSpace(p.endFreeText) && p.includeSql == "true").ToList();


            //If no pre checks then assume either the query is wrong or we need to add in the query ourselves, in which case still use the USE statement
            if (prePosChecks.Count == 0 && preZeroChecks.Count == 0)
            {
                theScript += "\n\n--Run";
                theScript += "\nUSE " + thisStudy.dataDatabase;
            }           
            else
            {
                //PreChecks
                foreach (Query theQuery in prePosChecks)
                {
                    theScript += "\n\n--Run";
                    theScript += "\nUSE " + thisStudy.dataDatabase;
                    theScript += "\n" + theQuery.preCheckSql;
                    theScript += "\n--" + theQuery.preCheckText + "  Take Screenshot";
                }

                if (preZeroChecks.Count > 0)
                {
                    theScript += "\n\n--Run";
                    foreach (Query theQuery in preZeroChecks)
                    {
                        theScript += "\nUSE " + thisStudy.dataDatabase;
                        theScript += "\n" + theQuery.preCheckSql;
                    }
                    theScript += "\n--Verify that zero records are returned. Take Screenshot";

                }

            }


            //Pre Free Text
            foreach (FreeText theText in preFreeText)
            {
                theScript += "\n\n" + theText.preFreeText;
            }

            //Execution
            theScript += "\n\n--Run";
            theScript += "\nBEGIN TRANSACTION";
            theScript += "\nDECLARE @Rows INT = 0";
            theScript += "\nDECLARE @TotalRows INT = 0\n";

            int totalUpdated = 0;
            theScript += "\nUSE " + thisStudy.dataDatabase;
            foreach (Query theQuery in executionQuery)
            {
                theScript += "\n\n" + theQuery.updateSql;              
                theScript += "\nSET @Rows = @@RowCount";
                theScript += "\nSET @TotalRows += @Rows";
                theScript += "\nSELECT @Rows as \"Rows Affected\"";

                totalUpdated += int.Parse(theQuery.updateCount);

            }

            //Pre Free Text
            foreach (FreeText theText in updateFreeText)
            {
                theScript +=  "\n\n"+theText.updateFreeText;
            }
            //If updating zero records then we have to assume that no actual query is used and its free text
            theScript += "\n\nSELECT @TotalRows AS \"Total Rows Affected\"";
            theScript += "\n--Verify that the following number of records are updated: " + (totalUpdated > 0 ? totalUpdated.ToString() : "<INSERT VALUE HERE>") + ". Take Screenshot";

            
            theScript += getPostExecutionScript();
            theScript += "\n\n--If there are no errors COMMIT else ROLLBACK. Take a screenshot.";

            theScript += getPostExecutionScript();

            //End Free Text
            foreach (FreeText theText in endFreeText)
            {
                theScript += "\n\n" + theText.endFreeText + "\n";
            }

            fastTextOutput.Text = theScript;
        }

        /// <summary>
        /// Get Post Execution Script
        /// </summary>
        /// <returns> script of checks</returns>
        private string getPostExecutionScript()
        {
            //Post execution checks
            string postExecutionScript = "";

            //split in two so we filter out only those we need to include else it will crash due to number not being filled in
            List<Query> postZeroChecks = updateQueryObj.querys.Where(p => p.postRecordsReturned == "0" && p.includeSql == "true").ToList();
//            postZeroChecks = postZeroChecks.Where(p => p.preRecordsReturned != p.postRecordsReturned && int.Parse(p.updateCount) > 0).ToList();
            postZeroChecks = postZeroChecks.Where(p => int.Parse(p.updateCount) > 0).ToList();

            List<Query> postPosChecks = updateQueryObj.querys.Where(p => !String.IsNullOrWhiteSpace(p.postCheckSql) && p.includeSql == "true").ToList();
//            postPosChecks = postPosChecks.Where(p => int.Parse(p.postRecordsReturned) > 0 && p.preRecordsReturned != p.postRecordsReturned && int.Parse(p.updateCount) > 0 && p.includeSql == "true").ToList();
            postPosChecks = postPosChecks.Where(p => int.Parse(p.postRecordsReturned) > 0  && int.Parse(p.updateCount) > 0 && p.includeSql == "true").ToList();

            List<FreeText> postFreeText = updateQueryObj.freeText.Where(p => !String.IsNullOrWhiteSpace(p.postFreeText) && p.includeSql == "true").ToList();

            if (postPosChecks.Count == 0 && postZeroChecks.Count == 0)
            {
                postExecutionScript += "\n\n--Run";
                postExecutionScript += "\nUSE " + thisStudy.dataDatabase;
            }
            else
            {
                foreach (Query theQuery in postPosChecks)
                {
                    postExecutionScript += "\n\n--Run";
                    postExecutionScript += "\nUSE " + thisStudy.dataDatabase;
                    postExecutionScript += "\n" + theQuery.postCheckSql;
                    postExecutionScript += "\n--" + theQuery.postCheckText + " Take Screenshot";

                }

                if (postZeroChecks.Count > 0)
                {
                    postExecutionScript += "\n\n--Run";
                    postExecutionScript += "\nUSE " + thisStudy.dataDatabase;
                    foreach (Query theQuery in postZeroChecks)
                    {
                        postExecutionScript += "\n" + theQuery.postCheckSql;
                    }
                    postExecutionScript += "\n--Verify that zero records are returned. Take Screenshot";

                }

            }

            //Post Free Text
            foreach (FreeText theText in postFreeText)
            {
                postExecutionScript += "\n\n" + theText.postFreeText ;
            }


            return postExecutionScript;
        }

        /// <summary>
        /// Get List into "'XXXXX','XXXXX'" format for use in queryes
        /// </summary>
        /// <param name="searchItems"></param>
        /// <returns></returns>
        public static string getQueryableList(String searchItems)
        {
            String returnList = "";

            String[] searchItemList = searchItems.Split(',');
            foreach (string item in searchItemList)
            {
                if (String.IsNullOrEmpty(returnList))
                {
                    returnList = "'" + item + "'";
                }
                else
                {
                    returnList += ",'" + item + "'";
                }
            }
            return returnList;
        }

        /// <summary>
        /// Check if study is valid or not
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="studyIDString"></param>
        /// <returns></returns>
        bool validStudy(ref string errorMessage, string studyIDString)
        {
            int studyID;

            if (String.IsNullOrEmpty(studyIDString))
            {
                errorMessage += "Study ID cannot be empty\n";
                return false;
            }   
         
            if (!int.TryParse(studyIDString, out studyID))
            {
                errorMessage += "Study ID is not numeric\n";
                return false;
            }
    


            try
            {
                Study x =  null;
                x = studies.SingleOrDefault(s => s.Id == studyID);

                if (x == null)
                {
                    errorMessage += "Study not found\n";
                    return false;
                }
                else
                {
                     thisStudy = new StudyDetail(x, cmbEnvironment.Text);
                }


            }
            catch
            {
                errorMessage += "Error Searching for Study\n";
                return false;
            }




            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theSql"></param>
        /// <returns></returns>
        public static List<String> getValidItemList(ref string errorMessage, string theSql)
        {
            List<String> theList = new List<String>();

            //Connecting
            connection = new QueryableConnection();
            connection.ServerName = thisStudy.server;
            connection.DatabaseName = thisStudy.dataDatabase;

            //Running Query 
            var x = connection.GetDataTable(theSql);

            if (x.Rows.Count == 0)
            {
                errorMessage += "No Valid Items Found";
                return theList;
            }
            else
            {
                   
                foreach (DataRow row in x.Rows)
                {
                   theList.Add(row["Item"].ToString());
                }
            }

            return theList;
        }
        /// <summary>
        /// Check if a valid site or not
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="siteIDString"></param>
        /// <returns></returns>
        public static bool validInputParam(ref string errorMessage, InputParamItem inputParam, string inputItem)
        {

            if (String.IsNullOrEmpty(inputItem))
            {
                errorMessage += "A Parameter cannot be empty\n";
                return false;
            }   


            //ADD IN CHECK FOR SITE EXISTING  

            //Connecting
            connection = new QueryableConnection();
            connection.ServerName = thisStudy.server;
            connection.DatabaseName = thisStudy.dataDatabase;

            String[] inputItems = inputItem.Split(',');
//            string siteList = "";

//            siteList = getQueryableList(siteIDString);

            foreach (String currItem in inputItems)
            {

                string checkQuery = inputParam.sqlValidation.Replace("{CSV_ITEM}",currItem);
                checkQuery = checkQuery.Replace("{DESIGNDB}", thisStudy.designDatabase);

                //If the lists are populated use this, else query this
                if (!string.IsNullOrWhiteSpace(inputParam.validItems))
                {
                    List<String> checkListItems = inputParam.validItems.Split(',').ToList();
                    if (!checkListItems.Contains(currItem))
                    {
                        errorMessage += inputParam.paramDesc + ": " + currItem + " not found \n";
                        return false;
                    }
                }
                else
                {
                    //Only run if we have validation 
                    if (!String.IsNullOrWhiteSpace(checkQuery)){
                        //Running Query 
                        var x = connection.GetDataTable(checkQuery);

                        if (x.Rows.Count == 0)
                        {
                            errorMessage += inputParam.paramDesc + ": " + currItem + " not found \n";
                            return false;
                        }
                    }

                }


            }

            return true;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateServerDetails();
        }

        private void populateServerDetails()
        {
            txtServer.Text = cmbServer.Text;
            if (cmbServer.Text == "Berlin")
            {
                txtServer.Text = "BER-FLXSQL-002c";              
                txtUsername.Text = "ReadOnly";
                txtPassword.Text = "D3xterous";                
                txtSecurity.Text = "Security";
                chkWindAuth.Checked = false;
            }
            else if (cmbServer.Text == "Legacy"){
                txtServer.Text = "BER-FLXSQL-092C\\LEGPRODCP4";
                txtUsername.Text = "ReadOnly";
                txtPassword.Text = "";
                txtSecurity.Text = "Security";
                chkWindAuth.Checked = true;
            }
            else
            {
                txtServer.Text = "NOT001926\\SQLEXPRESS";
                txtUsername.Text = "ReadOnly";
                txtPassword.Text = "ClinIntel001";
                txtSecurity.Text = "SecurityR29";
                chkWindAuth.Checked = false;
            }
        }

        private void btnTestServer_Click(object sender, EventArgs e)
        {
            if (connectToServer())
            {
                MessageBox.Show("Connection Succesful");
            }
            else
            {
                MessageBox.Show("Unable to Connect to Server");

            }
        }

        private bool connectToServer()
        {
            try
            {
                connection = new QueryableConnection();
                connection.ServerName = txtServer.Text;
                connection.DatabaseName = txtSecurity.Text;


                var password = new SecureString();
                foreach (char x in txtPassword.Text)
                {
                    password.AppendChar(x);
                }
                password.MakeReadOnly();

                if (!chkWindAuth.Checked)
                {
                    connection.SqlCredential = new SqlCredential(txtUsername.Text, password);
                }

                instanceService = new InstanceService();
                instance = instanceService.GetInstance(connection);
                return true;

            }
            catch
            {
                return false;
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Delete_Site_Load(object sender, EventArgs e)
        {
          
        }

        private void cmbUpdateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            btnVerify.Enabled = false;
            btnGenerateScript.Enabled = false;
            fastTextOutput.Text = "";
            rtbStudyVerifications.Text = "<Parameters Not Set>";

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(fastTextOutput.Text);
        }

/*        
        [XmlRoot("DCR_Updates")]
        public class DCRUpdates
        {
            [XmlElement("DCR_Update")]
            public List<DCRUpdate> dcrUpdates { get; set; }
        }
        */
        public class Update_Queries
        {
            [XmlElement("updateType")]
            public string updateType { get; set; }

            [XmlElement("Query")]
            public List<Query> querys { get; set; }

            [XmlElement("FreeText")]
            public List<FreeText> freeText { get; set; }
             
        }

        public class FreeText
        {
            [XmlElement("includeSql")]
            public string includeSql { get; set; }
            [XmlElement("preFreeText")]
            public string preFreeText { get; set; }
            [XmlElement("updateFreeText")]
            public string updateFreeText { get; set; }
            [XmlElement("postFreeText")]
            public string postFreeText { get; set; }
            [XmlElement("endFreeText")]
            public string endFreeText { get; set; }

        }


        public class Query
        {

            [XmlElement("includeSql")]
            public string includeSql { get; set; }

            [XmlElement("preCheckSql")]
            public string preCheckSql { get; set; }
            [XmlElement("preCheckText")]
            public string preCheckText { get; set; }
            [XmlElement("preRecordsReturned")]
            public string preRecordsReturned { get; set; }

            [XmlElement("updateSql")]
            public string updateSql { get; set; }
            [XmlElement("updateCount")]
            public string updateCount { get; set; }
           
            [XmlElement("postCheckSql")]
            public string postCheckSql { get; set; }
            [XmlElement("postCheckText")]
            public string postCheckText { get; set; }
            [XmlElement("postRecordsReturned")]
            public string postRecordsReturned { get; set; }

        }

        

        [XmlRoot("dcr_script")]
        public class DCR_Script
        {
            [XmlElement("update_param")]
            public List<Update_param> updateParamList { get; set; }

            [XmlElement("update_queries")]
            public List<Update_Queries> updateQueries { get; set; }

        }

        public class Update_param
        {
            [XmlElement("updateType")]
            public string updateType { get; set; }

            [XmlElement("description")]
            public string description { get; set; }

            [XmlElement("inputParam")]
            public List<InputParamItem> inputParamList { get; set; }

            [XmlElement("Param")]
            public List<ParamItem> paramList { get; set; }
        }

        public class InputParamItem
        {
            [XmlElement("inputText")]
            public string inputText { get; set; }

            [XmlElement("paramName")]
            public string paramName { get; set; }

            [XmlElement("paramDesc")]
            public string paramDesc { get; set; }

            [XmlElement("sqlValidation")]
            public string sqlValidation { get; set; }

            [XmlElement("inputType")]
            public string inputType { get; set; }

            [XmlElement("inputMethod")]
            public string inputMethod { get; set; }

            [XmlElement("validItems")]
            public string validItems { get; set; }

            [XmlElement("validItemsSQL")]
            public string validItemsSQL { get; set; }

            public string value;

        }

        public class ParamItem
        {
            [XmlElement("query")]
            public string query { get; set; }

            [XmlElement("requiredTables")]
            public string requiredTables { get; set; }
        }


        private void btnVerify_Click(object sender, EventArgs e)
        {
            
                       //reset 
            fastTextOutput.Text = "";
            btnGenerateScript.Enabled = false;
            initialiseQuery();

            if (!studiesSetUp()) return;

            String displayMessage =rtbStudyVerifications.Text+ "\n";

            if (!updateTypeSelected()) return;

            bool isValid = true;
            //Check up study and initialise
            if (!validStudy(ref displayMessage, txtStudyID.Text))
            {
                rtbStudyVerifications.Text = displayMessage;
                return;
            }


            //Convert Update_Param object to string so we can replace all the placeholders
            //We will turn the string back into object after
            XmlSerializer xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(Update_param));

            string paramXml = "";

            using (var sww = new System.IO.StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, updateParam);
                    paramXml = sww.ToString(); // Your XML
                }
            }

            //Read in the list of queries        
            string queriesXml = scriptXmlText;

            foreach (var x in updateParam.inputParamList)
            {
                paramXml = paramXml.Replace('{'+x.paramName+'}', x.value);
                queriesXml = queriesXml.Replace('{' + x.paramName + '}', x.value);
            }
            
            paramXml = paramXml.Replace("{DESIGNDB}", thisStudy.designDatabase);
            queriesXml = queriesXml.Replace("{DESIGNDB}", thisStudy.designDatabase);


            //Convert paramXml to dcrParams object
            TextReader txtReader = new StringReader(paramXml);

            //This creates xml based on the QueryList object
            XmlSerializer serializer = new XmlSerializer(typeof(Update_param));
            
            updateParam = (Update_param)serializer.Deserialize(txtReader);

            txtReader.Close();

            if (updateParam.paramList.Count == 0)
            {
                rtbStudyVerifications.Text += "\n\nReturned Settings: N/A";
            }
            else
            {
                //Connecting
                connection = new QueryableConnection();
                connection.ServerName = thisStudy.server;
                connection.DatabaseName = thisStudy.dataDatabase;

                String paramSql = "";
                foreach (ParamItem x in updateParam.paramList)
                {
                    //If we need to check for a table existing first
                    if (!String.IsNullOrEmpty(x.requiredTables))
                    {
                        String tableList = getQueryableList(x.requiredTables);
                        string reqTablesSql = "Select * from Information_schema.tables where Table_Name in (" + tableList + ")";
                        var output = connection.GetDataTable(reqTablesSql);

                        if (output.Rows.Count == 0)
                        {
                            displayMessage += "Not all of the following tables are found: " + x.requiredTables + "\n";
                            continue;
                        }


                    }

                    //If we can ... set up the query
                    if (String.IsNullOrEmpty(paramSql))
                    {
                        paramSql = x.query;

                    }
                    else
                    {
                        paramSql = x.query + " UNION " + paramSql;

                    }

                }

                if (paramSql.ToUpper().Contains("INSERT ") || paramSql.ToUpper().Contains("UPDATE ") || paramSql.ToUpper().Contains("DELETE "))
                {
                    MessageBox.Show("Params.xml contains either 'INSERT ', 'UPDATE ' or 'DELETE '. This isnt allowed.");
                    return;
                }

                try
                {
                    //Get all the parameters
                    var paramOutput = connection.GetDataTable(paramSql);
                    if (displayMessage != "") displayMessage += "\n";
                    displayMessage += "Returned Settings\n==============";
                    foreach (DataRow row in paramOutput.Rows)
                    {
                        string param = row["Param"].ToString();
                        string desc = row["Desc"].ToString();
                        string val = row["Val"].ToString();

                        displayMessage += "\n" + desc + ": " + val;
                        //  var siteID = row["SiteID"].ToString();

                        queriesXml = queriesXml.Replace('{' + param.ToUpper() + '}', val);
                    }

                    rtbStudyVerifications.Text = displayMessage;
                }
                catch
                {
                    rtbStudyVerifications.Text = "Error runnning parameters sql: \n" + paramSql;
                    return;
                }
            }


            TextReader queryReader = new StringReader(queriesXml);

            //This creates xml based on the QueryList object
            XmlSerializer queriesSerializer = new XmlSerializer(typeof(DCR_Script));

            DCR_Script dcrUpdates;
            dcrUpdates = (DCR_Script)queriesSerializer.Deserialize(queryReader);


            updateQueryObj = dcrUpdates.updateQueries.First(); 
            queryReader.Close();

            if (isValid)btnGenerateScript.Enabled = true;


        }

        private bool studiesSetUp()
        {
            try
            {

                if (connectToServer())
                {
                    studies = new List<ClinPhone.Instances.Study>();
                    foreach (var c in instance.Clients)
                    {
                        foreach (var p in c.Programmes)
                        {
                            studies.AddRange(p.Studies);
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Unable to Connect to Server");
                    return false;
                }
            }
            catch (System.Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            return false;
        }

        private bool updateTypeSelected()
        {
            if (cmbUpdateType.Text == "<Please Select>")
            {
                MessageBox.Show("Please select your update type");
                return false;
            }
            return true;
        }

        private void btnGetParams_Click(object sender, EventArgs e)
        {
            btnGenerateScript.Enabled = false;
            if (!updateTypeSelected()) return;
            if (!studiesSetUp()) return;

            //Set up the Update_Params and DCR_Script object
            if (!setUpParams()) return;
        }

        private void txtSite_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void chkWindAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWindAuth.Checked)
            {
                txtPassword.Visible = false;
                txtUsername.Visible = false;
                lblPassword.Visible = false;
                lblUsername.Visible = false;
            }
            else
            {
                txtPassword.Visible = true;
                txtUsername.Visible = true;
                lblPassword.Visible = true;
                lblUsername.Visible = true;

            }
        }



    }

    public class StudyDetail
    {
        public string server = "";
        public string dataDatabase = "";
        public string designDatabase = "";
        public string studyID = "";
        public string environment = "";
        public string versionIDField = "";
        public string dataDatabaseField = "";
        public StudyDetail(Study thisStudy, string environment)
        {
            studyID = thisStudy.Id.ToString();
            server = thisStudy.Programme.Server.ToString();
            switch (environment.ToUpper()){
                case "BUILD":
                    dataDatabase = thisStudy.Build.DataDatabaseName;
                    designDatabase = thisStudy.Build.DesignDatabaseName;
                    environment = "Build";
                    versionIDField = "BuildVersionID";
                    dataDatabaseField = "BuildDataDatabaseName";
                    break;
                 case "TEST":
                    dataDatabase = thisStudy.Test.DataDatabaseName;
                    designDatabase = thisStudy.Test.DesignDatabaseName;
                    environment = "Test";
                    versionIDField = "TestVersionID";
                    dataDatabaseField = "TestDataDatabaseName";
                    break;
                 case "UAT":
                    dataDatabase = thisStudy.Uat.DataDatabaseName;
                    designDatabase = thisStudy.Uat.DesignDatabaseName;
                    environment = "UAT";
                    versionIDField = "UATVersionID";
                    dataDatabaseField = "UATDataDatabaseName";
                    break;
                 case "LIVE":
                    dataDatabase = thisStudy.Live.DataDatabaseName;
                    designDatabase = thisStudy.Live.DesignDatabaseName;
                    environment = "Live";
                    versionIDField = "LiveVersionID";
                    dataDatabaseField = "DatabaseName";

                    break;

                default :
                    dataDatabase = "ERROR";
                    designDatabase = "ERROR";
                    break;
            }

        }
    }
    

        /// <summary>
    /// 
    /// </summary>
    public class DCRQueryItem
    {
        public String table = "";
        public String updateType = "";
        public String inclusionSql = ""; //Positive results means include
        public bool includeSql = true; // Default to include
        
        // SET TO "IGNORE" and it will not be used
        public String preCheckSql = ""; // The double check sql
        public String preCheckText = "";
        public String preRecordsReturned = "";

        public String updateCount = ""; // Sql to work out how many records should be updated!
        public String updateSql = ""; // The sql to update this value

        public String postCheckSql = ""; // The double check sql
        public String postCheckText = "";
        public String postRecordsReturned = "";



        public void setUpQueryForStudy(string siteList, StudyDetail thisStudy, QueryableConnection connection)
        {
            this.updateSql = setUpQuery(this.updateSql, siteList, thisStudy, connection);
            this.inclusionSql = setUpQuery(this.inclusionSql, siteList, thisStudy, connection);
        }

        private string setUpQuery(string inputString, string inputList, StudyDetail thisStudy, QueryableConnection connection)
        {


            if (this.updateType == "DELETE_SITE")
            {
                string siteIDSQL = @"DECLARE @SiteIDs VARCHAR(MAX)
                                SELECT @SiteIDs = COALESCE(@SiteIDs + ',','')+isnull(Convert(varchar(max),SIteID),'N/A')
                                FROM Sites WHERE CentreID in (" + inputList + @")
                                SELECT @SiteIDs as SiteIDList";

                var dataTable = connection.GetDataTable(siteIDSQL);
                string siteIDList = dataTable.Rows[0]["SiteIDList"].ToString();

                inputString = inputString.Replace("{CENTREID_CSV}", inputList);
                inputString = inputString.Replace("{SITEID_CSV}", siteIDList);
            }

            if (this.updateType == "DELETE_PATIENT")
            {

                string patIdSql = @"DECLARE @PatientIDs VARCHAR(MAX)
                                    SELECT @PatientIDs = COALESCE(@PatientIDs + ',','')+isnull(Convert(varchar(max),PatientID),'N/A')
                                    FROM Patients WHERE PatientCode in (" + inputList + @")
                                    SELECT @PatientIDs as PatientIDList";

                var dataTable = connection.GetDataTable(patIdSql);
                string patIDList = dataTable.Rows[0]["PatientIDList"].ToString();

                inputString = inputString.Replace("{PATIENTCODE_CSV}", inputList);
                inputString = inputString.Replace("{PATIENTID_CSV}", patIDList);
            }




            inputString = inputString.Replace("{DESIGNDB}", thisStudy.designDatabase);
            return inputString;

        }

    }


    
 
}
