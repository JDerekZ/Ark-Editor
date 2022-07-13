using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ark_EditorV2
{
    public partial class FrmArkMenu : Form
    {
        public FrmArkMenu()
        {
            InitializeComponent();
        }

        //buttons of menus Events
        private void btnBlogNotesProg_Click(object sender, EventArgs e)
        {
            FrmArKNotes frmArKNotes = new FrmArKNotes();
            frmArKNotes.ShowDialog();
            
        }

        private void btnPaintProg_Click(object sender, EventArgs e)
        {
            FrmArkPaint frmArkPaint = new FrmArkPaint();
            frmArkPaint.ShowDialog();
        }

    }
}
