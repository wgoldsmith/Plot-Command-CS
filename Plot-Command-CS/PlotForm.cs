//-----------------------------------------------------------------------
// <copyright file="PlotForm.cs" company="Goldsmith Engineering">
//     Copyright (c) Goldsmith Engineering. All rights reserved.
// </copyright>
// <author>Winston Goldsmith</author>
//-----------------------------------------------------------------------

namespace Plot_Command_CS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Initializes form, reads in data from XML file, and displays it on the form.
    /// If any radio buttons are changed, write any changes to the xml file, then call plot command.
    /// </summary>
    public partial class PlotForm : Form
    {
        /// <summary>
        /// A list to hold all the Panels on the form that show different colors based on what type of paper is selected
        /// </summary>
        private Dictionary<int, Panel> colorBoxList = new Dictionary<int, Panel>();

        /// <summary>
        /// A list to hold all the Panels on the form that hold all the controllers that must be placed dynamically based on the XML file
        /// </summary>
        private Dictionary<int, Panel> containerList = new Dictionary<int, Panel>();

        /// <summary>
        /// A list to hold all the Labels on the form that show name of each printer
        /// </summary>
        private Dictionary<int, Label> printerList = new Dictionary<int, Label>();

        /// <summary>
        /// A list to hold all the RadioButtons on the form for selecting the mylar option for that printer
        /// </summary>
        private Dictionary<int, RadioButton> mylarList = new Dictionary<int, RadioButton>();

        /// <summary>
        /// A list to hold all the RadioButtons on the form for selecting the paper option for that printer
        /// </summary>
        private Dictionary<int, RadioButton> paperList = new Dictionary<int, RadioButton>();

        /// <summary>
        /// A list to hold all the RadioButtons on the form for selecting the other option for that printer
        /// </summary>
        private Dictionary<int, RadioButton> otherList = new Dictionary<int, RadioButton>();

        /// <summary>
        /// A list to hold all the Labels on the form that show the name of the last person to change the option on that printer, 
        /// as well as the date and time it was changed
        /// </summary>
        private Dictionary<int, Label> ndtList = new Dictionary<int, Label>();

        /// <summary>
        /// A list to hold the starting status of the radio buttons
        /// </summary>
        private Dictionary<int, string> statusList = new Dictionary<int, string>();

        /// <summary>
        /// Initializes a new instance of the PlotForm class. Auto generated constructor for PlotForm
        /// </summary>
        public PlotForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Reads the plot printers xml file and creates controllers based on the information in the file
        /// </summary>
        private void ReadXML()
        {
            int key = 0;
            ///////////////////////////////////////////////////////////////////////               Change to new file location              //////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            using (XmlReader reader = XmlReader.Create(@"S:\C3D Config\Configuration\hgg_lsp\PlotCommand\plotprinters.xml"))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (reader.Name.Equals("printer"))
                            {
                                this.containerList.Add(key, new Panel());

                                this.printerList.Add(key, new Label());
                                this.printerList[key].Text = reader.GetAttribute("name");
                                this.printerList[key].Name = "printerLabel" + key;
                                this.printerList[key].AutoSize = false;
                                this.printerList[key].Size = new Size(100, 13);
                                this.printerList[key].AutoEllipsis = true;
                            }
                            else if (reader.Name.Equals("user"))
                            {
                                this.ndtList.Add(key, new Label());
                                this.ndtList[key].Text = "Last changed by: " + reader.ReadElementContentAsString();
                                this.ndtList[key].Name = "ntdLabel" + key;
                                this.ndtList[key].AutoSize = false;
                                this.ndtList[key].Size = new Size(306, 13);
                                this.ndtList[key].AutoEllipsis = true;
                            }

                            // month and hour are both preceded by " - "
                            else if (reader.Name.Equals("month") || reader.Name.Equals("hour"))
                            {
                                this.ndtList[key].Text = this.ndtList[key].Text + " - " + reader.ReadElementContentAsString();
                            }
                            // day and year are both preceded by "/"
                            else if (reader.Name.Equals("day") || reader.Name.Equals("year"))
                            {
                                this.ndtList[key].Text = this.ndtList[key].Text + "/" + reader.ReadElementContentAsString();
                            }
                            else if (reader.Name.Equals("minute"))
                            {
                                this.ndtList[key].Text = this.ndtList[key].Text + ":" + reader.ReadElementContentAsString();
                            }
                            else if (reader.Name.Equals("roll"))
                            {
                                this.colorBoxList.Add(key, new Panel());
                                this.colorBoxList[key].BorderStyle = BorderStyle.FixedSingle;
                                this.colorBoxList[key].Size = new Size(35, 25);
                                this.colorBoxList[key].Name = "colorBox" + key;

                                this.mylarList.Add(key, new RadioButton());
                                this.mylarList[key].Text = "Mylar";
                                this.mylarList[key].Name = "mylarButton" + key;
                                this.mylarList[key].AutoSize = true;
                                this.mylarList[key].MouseClick += mouseClick;

                                this.paperList.Add(key, new RadioButton());
                                this.paperList[key].Text = "Paper";
                                this.paperList[key].Name = "paperButton" + key;
                                this.paperList[key].AutoSize = true;
                                this.paperList[key].MouseClick += mouseClick;

                                this.otherList.Add(key, new RadioButton());
                                this.otherList[key].Text = "Other";
                                this.otherList[key].Name = "otherButton" + key;
                                this.otherList[key].AutoSize = true;
                                this.otherList[key].MouseClick += mouseClick;

                                SetRoll(reader.ReadElementContentAsString(), key);
                            }

                            break;

                        case XmlNodeType.EndElement:  // when it reaches the end of a printer, increase key by 1
                            if (reader.Name.Equals("printer"))
                            {
                                key++;
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Receives a key that is the printer options to change, then writes the username, current date and time, 
        /// and the roll type, then saves it.
        /// </summary>
        /// <param name="key">The number of the printer to write to.</param>
        private void WriteXML(int key)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"S:\C3D Config\Configuration\hgg_lsp\PlotCommand\plotprinters.xml");

            XmlNodeList users = doc.GetElementsByTagName("user");
            XmlNodeList dates = doc.GetElementsByTagName("date");
            XmlNodeList times = doc.GetElementsByTagName("time");
            XmlNodeList rolls = doc.GetElementsByTagName("roll");

            users.Item(key).InnerText = Environment.UserName; // user

            dates.Item(key).FirstChild.InnerText = DateTime.Now.Month.ToString().PadLeft(2, '0'); // month in the format mm with a leading 0 if necessary

            dates.Item(key).FirstChild.NextSibling.InnerText = DateTime.Now.Day.ToString().PadLeft(2, '0'); // day in the format dd with a leading 0 if necessary

            dates.Item(key).LastChild.InnerText = DateTime.Now.Year.ToString().Substring(2); // year. Only last 2 digits

            int hour = DateTime.Now.Hour;

            // if hour is greater than 12, subtract 12 to get the pm hour
            if (hour > 12) 
            {
                hour = hour - 12;
            }

            times.Item(key).FirstChild.InnerText = hour.ToString().PadLeft(2, '0'); // hour in the format hh with a leading 0 if necessary

            times.Item(key).LastChild.InnerText = DateTime.Now.Minute.ToString().PadLeft(2, '0'); // minute in the format mm with a leading 0 if necessary

            rolls.Item(key).InnerText = this.SearchOptions(key); // roll

            doc.Save(@"S:\C3D Config\Configuration\hgg_lsp\PlotCommand\plotprinters.xml");
        }

        /// <summary>
        /// Sets which radio buttons start as checked and adds the name to statusList
        /// </summary>
        /// <param name="roll">The name of the roll that was read from the XML file.</param>
        /// <param name="key">The number of the current printer.</param>
        private void SetRoll(string roll, int key)
        {
            if (roll.Equals("Mylar"))
            {
                this.colorBoxList[key].BackColor = Color.Red;
                this.mylarList[key].Checked = true;
                this.statusList[key] = "Mylar";
            }
            else if (roll.Equals("Paper"))
            {
                this.colorBoxList[key].BackColor = Color.White;
                this.paperList[key].Checked = true;
                this.statusList[key] = "Paper";
            }
            else if (roll.Equals("Other"))
            {
                this.colorBoxList[key].BackColor = Color.LimeGreen;
                this.otherList[key].Checked = true;
                this.statusList[key] = "Other";
            }
        }

        /// <summary>
        /// Places container panels in the group box and spaces each one based on the passed in spacer
        /// </summary>
        /// <param name="key">The key to the current container to be added.</param>
        /// <param name="spacer">The amount of space to be placed between each container.</param>
        private void PlaceContainerPanel(int key, int spacer)
        {
            // On the first entry the key will be 0, so container will be placed at 5, 15
            // On all others the container will be placed 41 spaces lower than the previous container
            this.containerList[key].Location = new Point(5, 20 + (key * spacer));
            this.containerList[key].Size = new Size(this.paperStatGroupBox.Width - 10, spacer);
            this.paperStatGroupBox.Controls.Add(this.containerList[key]);
        }

        /// <summary>
        /// Places all controllers in each container
        /// </summary>
        /// <param name="key">The key to the container and controls to be added.</param>
        private void PlaceInContainer(int key)
        {
            ////this.colorBoxList[key].Parent = this.containerList[key]; // also works
            this.colorBoxList[key].Location = new Point(0, 0);
            this.containerList[key].Controls.Add(this.colorBoxList[key]);

            this.printerList[key].Location = new Point(37, 6);
            this.containerList[key].Controls.Add(this.printerList[key]);

            this.mylarList[key].Location = new Point(142, 4);
            this.containerList[key].Controls.Add(this.mylarList[key]);

            this.paperList[key].Location = new Point(198, 4);
            this.containerList[key].Controls.Add(this.paperList[key]);

            this.otherList[key].Location = new Point(257, 4);
            this.containerList[key].Controls.Add(this.otherList[key]);

            this.ndtList[key].Location = new Point(2, 25);
            this.containerList[key].Controls.Add(this.ndtList[key]);
        }

        /// <summary>
        /// Runs when form is loaded. Loads all controllers and adds them to form based on what is in plot printer xml file.
        /// </summary>
        /// <param name="sender">Auto generated sender object by Visual Studio.</param>
        /// <param name="e">Auto generated EventArgs by Visual Studio.</param>
        private void PlotForm_Load(object sender, EventArgs e)
        {
            this.ReadXML();

            int spacer = 41; // the amount of space between each container panel

            // interate through container panels, place controllers inside, and place them in group box.
            // if there are no container panels, nothing will happen.
            for (int key = 0; key < this.containerList.Count; key++)
            {
                this.PlaceInContainer(key);

                this.PlaceContainerPanel(key, spacer);

                // -------------------------top spacer + (spacer * # of container boxes) + bottom spacer
                this.paperStatGroupBox.Height = 15 + (spacer * this.containerList.Count) + 10;
            }

            // -------------------------------------keep same X location,     top of groupBox         +    height = bottom of box. + 20 for some space between box and button.
            this.buttonOk.Location = new Point(this.buttonOk.Location.X, this.paperStatGroupBox.Location.Y + this.paperStatGroupBox.Height + 20);
            this.buttonCancel.Location = new Point(this.buttonCancel.Location.X, this.paperStatGroupBox.Location.Y + this.paperStatGroupBox.Height + 20);
        }

        /// <summary>
        /// Checks all radio buttons against statusList to see if any have been changed
        /// and change the xml file if any have.
        /// </summary>
        private void SetStatus()
        {
            // loop through all containers on form
            for (int key = 0; key < this.containerList.Count; key++)
            {
                // get the checked radio button in the current container
                string currBut = this.SearchOptions(key);

                // if the old checked radio button is not equal to the new checked radio button
                if (!this.statusList[key].Equals(currBut))
                {
                    this.WriteXML(key);
                }
            }
        }

        /// <summary>
        /// Check the container with the given key and return the text of the radio button
        /// that is selected in that container.
        /// </summary>
        /// <param name="key">Number of the container to be checked.</param>
        /// <returns>The text of the radio button that is selected in that container.</returns>
        private string SearchOptions(int key)
        {
            // loop through all buttons in current Panel
            foreach (RadioButton radBut in this.containerList[key].Controls.OfType<RadioButton>())
            {
                if (radBut.Checked)
                {
                    return radBut.Text;
                }
            }

            return "None";
        }

        /// <summary>
        /// When user clicks on a radio button, check to to see if it was a different one than the
        /// one that was already selected. If it was, update the color of the colorBox in that container.
        /// </summary>
        /// <param name="sender">Auto generated sender object by Visual Studio.</param>
        /// <param name="e">Auto generated EventArgs by Visual Studio.</param>
        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // loop through all Panels in container list
                for (int key = 0; key < this.containerList.Count; key++)
                {
                    if (this.SearchOptions(key).Equals("Mylar"))
                    {
                        this.colorBoxList[key].BackColor = Color.Red;
                    }
                    else if (this.SearchOptions(key).Equals("Paper"))
                    {
                        this.colorBoxList[key].BackColor = Color.White;
                    }
                    else if (this.SearchOptions(key).Equals("Other"))
                    {
                        this.colorBoxList[key].BackColor = Color.LimeGreen;
                    }
                }
            }
        }

        /// <summary>
        /// When user clicks OK button, calls SetStatus to see if any radio buttons were changed 
        /// and write them to xml file, then closes the form and calls plot
        /// </summary>
        /// <param name="sender">Auto generated sender object by Visual Studio.</param>
        /// <param name="e">Auto generated EventArgs by Visual Studio.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.SetStatus();
            this.Close();

            // after the form closes, redefine plot to original command, then call plot. Then undefine plot to be custom command again.
            Autodesk.AutoCAD.ApplicationServices.Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            doc.SendStringToExecute("REDEFINE PLOT ", true, false, false);
            doc.SendStringToExecute("PLOT ", true, false, false);
            doc.SendStringToExecute("UNDEFINE PLOT ", true, false, false);
        }

        /// <summary>
        /// When user clicks cancel button, close form.
        /// </summary>
        /// <param name="sender">Auto generated sender object by Visual Studio.</param>
        /// <param name="e">Auto generated EventArgs by Visual Studio.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}