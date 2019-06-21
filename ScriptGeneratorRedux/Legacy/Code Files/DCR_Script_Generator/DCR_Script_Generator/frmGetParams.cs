using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCR_Script_Generator
{
    public partial class FormGetParams : Form
    {
        int counter = 0;

        public FormGetParams()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string currentItem = "123400075,123400076";
            string displayMessage = "";
            for (int i = 0; i < counter; i++)
            {
                Control x = this.Controls.Find("inputParamVal" + i.ToString(), true)[0];
                if (x.GetType() == typeof(TextBox)) currentItem = x.Text;
                if (x.GetType() == typeof(ComboBox)) currentItem = x.Text;


                DCR_Script_Generator.ScriptGenerator.InputParamItem inputParam = ScriptGenerator.updateParam.inputParamList[i];

                if (ScriptGenerator.validInputParam(ref displayMessage, inputParam, currentItem))
                {
                    if (ScriptGenerator.updateParam.inputParamList[i].inputType == "String")
                    {
                        ScriptGenerator.updateParam.inputParamList[i].value = ScriptGenerator.getQueryableList(currentItem);
                    }
                    else
                    {
                        ScriptGenerator.updateParam.inputParamList[i].value = currentItem;
                    }

                }
                else
                {
                    MessageBox.Show(displayMessage);
                    return;
                }
                
            }
            this.DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormGetParams_Load(object sender, EventArgs e)
        {
            int y = 22;
            int x = 253;

            int btnConfX = btnConfirm.Location.X;
            int btnConfY = btnConfirm.Location.Y;
            int btnCancelX = btnCancel.Location.X;
            int btnCancelY = btnCancel.Location.Y;
            int formHeight = this.Height;


            foreach (var paramItem in ScriptGenerator.updateParam.inputParamList)
            {

                if (paramItem.inputMethod == "Text")
                {
                    TextBox txt = new TextBox();
                    txt.Name = "inputParamVal" + counter.ToString();
                    //txt.Text = "123400075,123400076";
//                    txt.Text = "";
                    txt.Location = new Point(x, y);
                    txt.Width = 200;

                    Controls.Add(txt);

                }
                else if (paramItem.inputMethod == "List")
                {
                    ComboBox cmb = new ComboBox();
                    cmb.Name = "inputParamVal" + counter.ToString();
                    //                txt.Text = "123400075,123400076";
                    cmb.Text = "";
                    cmb.Location = new Point(x, y);
                    cmb.Width = 200;

                    paramItem.validItemsSQL = paramItem.validItemsSQL.Replace("{DESIGNDB}", ScriptGenerator.thisStudy.designDatabase);

                    //Only show a button if valid item sql is populated 
                    if (!String.IsNullOrWhiteSpace(paramItem.validItemsSQL) && paramItem.validItemsSQL.Contains('{')) // Has a parameter in it
                    {
                        Button btn = new Button();
                        btn.Name = "btnParamOption" + counter.ToString();
                        btn.Text = "Update List";
                        btn.Location = new Point(x + cmb.Width + 10, y);
                        btn.Width = 100;
                        btn.Click += new EventHandler(btnClick);
                        btn.Tag = counter;
                        Controls.Add(btn);

                    }
                    else if (!String.IsNullOrWhiteSpace(paramItem.validItemsSQL) && !paramItem.validItemsSQL.Contains('{'))
                    {
                        String errorMessage = "";
                        //Get a list of valid items based on the sql
                        List<String> theItems = ScriptGenerator.getValidItemList(ref errorMessage, paramItem.validItemsSQL);

                        foreach (var item in theItems)
                        {
                            cmb.Items.Add(item);
                        }

                    }
                    else
                    {
                        String[] validOptions = paramItem.validItems.Split(',');

                        foreach (string listItem in validOptions)
                        {
                            cmb.Items.Add(listItem);
                        }

                    }




                    Controls.Add(cmb);
                    

                }

                Label lbl = new Label();
                lbl.Name = "lblParam"+counter.ToString();
                lbl.Text = paramItem.inputText;
                lbl.Width = 300;
                lbl.Location = new Point(x-227,y);

                Controls.Add(lbl);

                btnConfirm.Location = new Point(btnConfX, btnConfY);
                btnCancel.Location = new Point(btnCancelX, btnCancelY);

                btnCancelY += 30;
                btnConfY += 30;
                y += 30;
                formHeight += 30;
                counter++;


                this.Height = formHeight;
//                lblParam1.Text = paramItem.inputText;
            }


        }

        private void btnClick(object sender, EventArgs e)
        {

            Button button = sender as Button;

            string errorMessage = "";

            string listSql = ScriptGenerator.updateParam.inputParamList[(int)button.Tag].validItemsSQL;

            //Go through all the parameters we have entered so far and pop in the search query
            string paramVal= "";
            for (int i = 0; i < counter; i++)
            {
                Control x = this.Controls.Find("inputParamVal" + i.ToString(), true)[0];
                if (x.GetType() == typeof(TextBox)) paramVal = x.Text;
                if (x.GetType() == typeof(ComboBox)) paramVal = x.Text;


                DCR_Script_Generator.ScriptGenerator.InputParamItem inputParam = ScriptGenerator.updateParam.inputParamList[i];

                if (ScriptGenerator.updateParam.inputParamList[i].inputType == "String")
                {
                    paramVal = ScriptGenerator.getQueryableList(paramVal);
                }
             
                listSql = listSql.Replace('{' + inputParam.paramName + '}', paramVal);

            }

            listSql = listSql.Replace("{DESIGNDB}", ScriptGenerator.thisStudy.designDatabase);

            //Get a list of valid items based on the sql
            List<String> theItems = ScriptGenerator.getValidItemList(ref errorMessage,listSql);
            
            Control ctrl = this.Controls.Find("inputParamVal" + button.Tag.ToString(), true)[0];
            ComboBox cmb = ctrl as ComboBox;
            cmb.Items.Clear();

            if (theItems.Count == 0)
            {
                MessageBox.Show("No records returned");
            }
            else
            {
                foreach (var x in theItems)
                {
                  cmb.Items.Add(x);
                }

            }


        }
    }
}
