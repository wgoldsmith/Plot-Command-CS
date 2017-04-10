using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Plot_Command_CS
{
    public partial class PlotForm : Form
    {
        public PlotForm()
        {
            ReadXML();
            InitializeComponent();
        }

        private Dictionary<int, Panel> colorBoxList = new Dictionary<int, Panel>();
        private Dictionary<int, Label> printerList = new Dictionary<int, Label>();
        private Dictionary<int, RadioButton> mylarList = new Dictionary<int, RadioButton>();
        private Dictionary<int, RadioButton> paperList = new Dictionary<int, RadioButton>();
        private Dictionary<int, RadioButton> otherList = new Dictionary<int, RadioButton>();
        private Dictionary<int, Label> ndtList = new Dictionary<int, Label>();


        private void ReadXML()
        {
///////////////////////////////////////////////////////////////////////               Change to new file location              //////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            using (XmlReader reader = XmlReader.Create(@"C:\Users\wgoldsmith\Documents\Visual Studio 2015\Projects\Plot-Command-CS\Plot-Command-CS\plotprinters.xml"))
            {
                int key = 0;

                while (reader.Read())

                {

                    if (reader.IsStartElement())

                    {

                        //return only when you have START tag


                        switch (reader.Name)

                        {

                            case "printer":
                                key++;
                                printerList.Add(key, new Label());// reader.ReadString());
                                printerList[key].Text = reader.Value;
                                printerList[key].Location = new Point(5, 5);

                                printerList[key].AutoSize = true;
                                printerList[key].Name = "label1";

                                break;



 //                           case "Location":

  //                              Console.WriteLine("Your Location is : " + reader.ReadString());

//                                break;

                        }

                    }
                }
            }
        }
    }
}