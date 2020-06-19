using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace MidtermProjectGroup11
{
    public partial class frmClientStartGame : MaterialForm
    {
        public frmClientStartGame()
        {
            InitializeComponent();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

            Hide();
            using (frm_Map game = new frm_Map())
                game.ShowDialog();
            Show();
        }
    }
}
