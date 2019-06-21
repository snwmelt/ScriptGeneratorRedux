namespace DCR_Script_Generator
{
    partial class ScriptGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptGenerator));
            this.label1 = new System.Windows.Forms.Label();
            this.txtStudyID = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEnvironment = new System.Windows.Forms.ComboBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkWindAuth = new System.Windows.Forms.CheckBox();
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTestServer = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnGetParams = new System.Windows.Forms.Button();
            this.cmbUpdateType = new System.Windows.Forms.ComboBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.rtbStudyVerifications = new System.Windows.Forms.RichTextBox();
            this.btnGenerateScript = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.fastTextOutput = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastTextOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the Study ID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtStudyID
            // 
            this.txtStudyID.Location = new System.Drawing.Point(169, 92);
            this.txtStudyID.Name = "txtStudyID";
            this.txtStudyID.Size = new System.Drawing.Size(138, 20);
            this.txtStudyID.TabIndex = 1;
            this.txtStudyID.Text = "442";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(35, 675);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(116, 675);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Enter Environment";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cmbEnvironment
            // 
            this.cmbEnvironment.FormattingEnabled = true;
            this.cmbEnvironment.Items.AddRange(new object[] {
            "Build",
            "Test",
            "UAT",
            "Live"});
            this.cmbEnvironment.Location = new System.Drawing.Point(169, 66);
            this.cmbEnvironment.Name = "cmbEnvironment";
            this.cmbEnvironment.Size = new System.Drawing.Size(138, 21);
            this.cmbEnvironment.TabIndex = 11;
            this.cmbEnvironment.Text = "Build";
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Items.AddRange(new object[] {
            "Berlin",
            "Legacy",
            "<Custom>"});
            this.cmbServer.Location = new System.Drawing.Point(169, 40);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(138, 21);
            this.cmbServer.TabIndex = 12;
            this.cmbServer.Text = "Berlin";
            this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.cmbServer_SelectedIndexChanged);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(97, 16);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(182, 20);
            this.txtServer.TabIndex = 13;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(97, 66);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(182, 20);
            this.txtUsername.TabIndex = 14;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(97, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(182, 20);
            this.txtPassword.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Select the Server";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkWindAuth);
            this.groupBox1.Controls.Add(this.txtSecurity);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnTestServer);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Location = new System.Drawing.Point(377, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 152);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Details";
            // 
            // chkWindAuth
            // 
            this.chkWindAuth.AutoSize = true;
            this.chkWindAuth.Location = new System.Drawing.Point(149, 127);
            this.chkWindAuth.Name = "chkWindAuth";
            this.chkWindAuth.Size = new System.Drawing.Size(121, 17);
            this.chkWindAuth.TabIndex = 22;
            this.chkWindAuth.Text = "Use Windows Login";
            this.chkWindAuth.UseVisualStyleBackColor = true;
            this.chkWindAuth.CheckedChanged += new System.EventHandler(this.chkWindAuth_CheckedChanged);
            // 
            // txtSecurity
            // 
            this.txtSecurity.Location = new System.Drawing.Point(97, 40);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.Size = new System.Drawing.Size(182, 20);
            this.txtSecurity.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Security";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // btnTestServer
            // 
            this.btnTestServer.Location = new System.Drawing.Point(16, 123);
            this.btnTestServer.Name = "btnTestServer";
            this.btnTestServer.Size = new System.Drawing.Size(117, 23);
            this.btnTestServer.TabIndex = 19;
            this.btnTestServer.Text = "Test Connection";
            this.btnTestServer.UseVisualStyleBackColor = true;
            this.btnTestServer.Click += new System.EventHandler(this.btnTestServer_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(13, 95);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 18;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(13, 69);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 17;
            this.lblUsername.Text = "Username";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Server";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnGetParams);
            this.groupBox2.Controls.Add(this.cmbUpdateType);
            this.groupBox2.Controls.Add(this.btnVerify);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbEnvironment);
            this.groupBox2.Controls.Add(this.cmbServer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtStudyID);
            this.groupBox2.Location = new System.Drawing.Point(35, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 150);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enter Study Details";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Enter the Update Type";
            // 
            // btnGetParams
            // 
            this.btnGetParams.Location = new System.Drawing.Point(9, 121);
            this.btnGetParams.Name = "btnGetParams";
            this.btnGetParams.Size = new System.Drawing.Size(106, 23);
            this.btnGetParams.TabIndex = 18;
            this.btnGetParams.Text = "Get Parameters";
            this.btnGetParams.UseVisualStyleBackColor = true;
            this.btnGetParams.Click += new System.EventHandler(this.btnGetParams_Click);
            // 
            // cmbUpdateType
            // 
            this.cmbUpdateType.FormattingEnabled = true;
            this.cmbUpdateType.Location = new System.Drawing.Point(169, 13);
            this.cmbUpdateType.Name = "cmbUpdateType";
            this.cmbUpdateType.Size = new System.Drawing.Size(138, 21);
            this.cmbUpdateType.TabIndex = 19;
            this.cmbUpdateType.Text = "<Please Select>";
            this.cmbUpdateType.SelectedIndexChanged += new System.EventHandler(this.cmbUpdateType_SelectedIndexChanged);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(169, 121);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 17;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // rtbStudyVerifications
            // 
            this.rtbStudyVerifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStudyVerifications.Location = new System.Drawing.Point(35, 170);
            this.rtbStudyVerifications.Name = "rtbStudyVerifications";
            this.rtbStudyVerifications.Size = new System.Drawing.Size(633, 308);
            this.rtbStudyVerifications.TabIndex = 5;
            this.rtbStudyVerifications.Text = "";
            // 
            // btnGenerateScript
            // 
            this.btnGenerateScript.Enabled = false;
            this.btnGenerateScript.Location = new System.Drawing.Point(35, 484);
            this.btnGenerateScript.Name = "btnGenerateScript";
            this.btnGenerateScript.Size = new System.Drawing.Size(100, 23);
            this.btnGenerateScript.TabIndex = 8;
            this.btnGenerateScript.Text = "Generate Script";
            this.btnGenerateScript.UseVisualStyleBackColor = true;
            this.btnGenerateScript.Click += new System.EventHandler(this.btnGenerateScript_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(550, 675);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 23);
            this.btnCopy.TabIndex = 22;
            this.btnCopy.Text = "Copy to Clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // fastTextOutput
            // 
            this.fastTextOutput.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastTextOutput.AutoIndentCharsPatterns = "";
            this.fastTextOutput.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastTextOutput.BackBrush = null;
            this.fastTextOutput.CharHeight = 14;
            this.fastTextOutput.CharWidth = 8;
            this.fastTextOutput.CommentPrefix = "--";
            this.fastTextOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastTextOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastTextOutput.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastTextOutput.IsReplaceMode = false;
            this.fastTextOutput.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastTextOutput.LeftBracket = '(';
            this.fastTextOutput.Location = new System.Drawing.Point(35, 513);
            this.fastTextOutput.Name = "fastTextOutput";
            this.fastTextOutput.Paddings = new System.Windows.Forms.Padding(0);
            this.fastTextOutput.RightBracket = ')';
            this.fastTextOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastTextOutput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastTextOutput.ServiceColors")));
            this.fastTextOutput.Size = new System.Drawing.Size(633, 131);
            this.fastTextOutput.TabIndex = 23;
            this.fastTextOutput.Zoom = 100;
            // 
            // ScriptGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 716);
            this.Controls.Add(this.fastTextOutput);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnGenerateScript);
            this.Controls.Add(this.rtbStudyVerifications);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScriptGenerator";
            this.Text = "Script Generator";
            this.Load += new System.EventHandler(this.Delete_Site_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastTextOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStudyID;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEnvironment;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnTestServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbUpdateType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGenerateScript;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnGetParams;
        private System.Windows.Forms.RichTextBox rtbStudyVerifications;
        private System.Windows.Forms.CheckBox chkWindAuth;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private FastColoredTextBoxNS.FastColoredTextBox fastTextOutput;
    }
}

