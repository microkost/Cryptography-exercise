using System;
using System.Windows.Forms;

namespace MainGUI
{
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BasicCiphers.BasicCiphers f1 = new BasicCiphers.BasicCiphers();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RailFence.RailFence f2 = new RailFence.RailFence();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RowTransposition.RowTransposition f3 = new RowTransposition.RowTransposition();
            f3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CaesarCipher.CaesarCipher f4 = new CaesarCipher.CaesarCipher();          
            f4.Show();
        }
    }
}
