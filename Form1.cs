using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ConvCEMRims
{
    public partial class Form1 : Form
    {
        string CurrentDirectory = (Environment.CurrentDirectory);
        int indx_start = 1;
        int indx_shit = 0;
        int indx_revshit = 4;
        int indx_shit2 = 0;
        int indx_revshit2 = 4;


        int useles;
        int shit;
        int at;
        int start;
        int offile;
        byte[] rimshit = new byte[200000];
        byte filever = 0xFB;
        int useles2;
        int shit2;
        int at2;
        int start2;
        int offile2;
        byte[] rimshit2 = new byte[200000];






        public Form1()
        {
            InitializeComponent();
            cem_List();
        }
        private void cem_List()
        {
            if (indx_start == 1)
            {
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(CurrentDirectory, "*.cem");
                foreach (string file in files)
                {
                    listBox1.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
                indx_start = 2;

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file = CurrentDirectory + "\\" + listBox1.SelectedItem.ToString() + ".cem";
            string name = CurrentDirectory + "\\SPK\\" + listBox1.SelectedItem.ToString() + ".spk";
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);


                BinaryReader rd = new BinaryReader(fs);
                
                    
                    useles = rd.ReadInt32();
                    shit = rd.ReadInt32();
                    at = rd.ReadInt32();
                    start = rd.ReadInt32();
                    offile = rd.ReadInt32();
                    while (rd.BaseStream.Position < rd.BaseStream.Length)
                    {
                        rimshit[rd.BaseStream.Position] = rd.ReadByte();
                        indx_shit += 1;
                    }
                    rd.Close();

                
                
                fs.Close();
                

            }
            catch (IOException)
            { }

            string srs = "LFSSPK\0";
            byte[] boshe = Encoding.ASCII.GetBytes(srs);
            BinaryWriter file2 = new BinaryWriter(File.Open(name, FileMode.Create));
            file2.Write(boshe);
            file2.Write(filever);
            while (indx_revshit < indx_shit+20)
            {
                file2.Write(rimshit[indx_revshit]);
                indx_revshit += 1;
            }
            file2.Close();
            indx_shit = 0;
            indx_revshit = 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string[] filesZ = Directory.GetFiles(CurrentDirectory, "*.cem");
            foreach (string file_ in filesZ)
            {

                string fileZ = CurrentDirectory + "\\" + Path.GetFileNameWithoutExtension(file_).ToString() + ".cem";
                string nameZ = CurrentDirectory + "\\SPK\\" + Path.GetFileNameWithoutExtension(file_).ToString() + ".spk";
                try
                {
                    FileStream fsZ = new FileStream(fileZ, FileMode.Open, FileAccess.Read);

                    BinaryReader rdZ = new BinaryReader(fsZ);


                    useles2 = rdZ.ReadInt32();
                    shit2 = rdZ.ReadInt32();
                    at2 = rdZ.ReadInt32();
                    start2 = rdZ.ReadInt32();
                    offile2 = rdZ.ReadInt32();
                    while (rdZ.BaseStream.Position < rdZ.BaseStream.Length)
                    {
                        rimshit2[rdZ.BaseStream.Position] = rdZ.ReadByte();
                        indx_shit2 += 1;
                    }
                    rdZ.Close();



                    fsZ.Close();


                }
                catch (IOException)
                { }

                string srs = "LFSSPK\0";
                byte[] boshe = Encoding.ASCII.GetBytes(srs);
                BinaryWriter file2Z = new BinaryWriter(File.Open(nameZ, FileMode.Create));
                file2Z.Write(boshe);
                file2Z.Write(filever);
                while (indx_revshit2 < indx_shit2 + 20)
                {
                    file2Z.Write(rimshit2[indx_revshit2]);
                    indx_revshit2 += 1;
                }
                file2Z.Close();
                indx_shit2 = 0;
                indx_revshit2 = 4;


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://podfolio.eu/lfs-mods");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            indx_start = 1;
            cem_List();
        }
    }
}
