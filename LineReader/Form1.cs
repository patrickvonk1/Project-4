using DatabaseAssembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LineReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFile = new OpenFileDialog();
            oFile.Filter = "Text Files (*.txt)|*.txt";

            if (oFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(oFile.FileName))
                {
                    while (reader.Peek() > 0)
                    {
                        string line = reader.ReadLine();
                        string[] splittedLine = line.Split('*');
                        string lineText = splittedLine[0];
                        string lineType = splittedLine[1];//The filter name thingy
                        PickupLine newPickupLine = new PickupLine();
                        newPickupLine.Text = lineText;
                        //await App.Database.SavePickupLineAsync(newPickupLine);
                    }
                }
            }
        }
    }
}
