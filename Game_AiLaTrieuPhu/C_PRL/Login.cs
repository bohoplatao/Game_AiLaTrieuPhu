using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Game_AiLaTrieuPhu.C_PRL
{
    public partial class Login : Form
    {
        SoundPlayer musicbgr = new SoundPlayer("C:\\Users\\admin\\OneDrive\\Máy tính\\Anh_ALTP\\Assets\\altp_Background.wav");
        public Login()
        {
            InitializeComponent();
            musicbgr.Play();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            GameForm game = new GameForm();
            game.Show();
            this.Hide();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
