using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ark_EditorV2
{
    public partial class FrmArKNotes : Form
    {

        string File;

        public FrmArKNotes()
        {
            InitializeComponent();
        }

        private void FrmArKNotes_Load(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            rtbTexArea.Clear();
            File = null;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text|*.text";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                File = ofd.FileName;
                using (StreamReader read = new StreamReader(File))
                {
                    rtbTexArea.Text = read.ReadToEnd();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "Texto|*.txt";
            if (File != null)
            {
                using (StreamWriter G = new StreamWriter(File))
                {
                    G.Write(rtbTexArea.Text);
                }
            }
            else
            {
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    File = SaveFile.FileName;
                    using (StreamWriter g = new StreamWriter(SaveFile.FileName))
                    {
                        g.Write(rtbTexArea.Text);

                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //Environment.Exit(0);
        }
        //End Section File

        private void btnUndo_Click(object sender, EventArgs e)
        {
            rtbTexArea.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            rtbTexArea.Redo();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            rtbTexArea.Copy();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            rtbTexArea.Cut();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            rtbTexArea.Paste();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            rtbTexArea.SelectAll();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            rtbTexArea.Clear();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                rtbTexArea.ForeColor = color.Color;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fuente = new FontDialog();

            if (fuente.ShowDialog() == DialogResult.OK)
            {
                rtbTexArea.Font = fuente.Font;
            }
        }
    }
}
