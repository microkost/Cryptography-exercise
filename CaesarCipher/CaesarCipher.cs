using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaesarCipher
{
    public partial class CaesarCipher : Form
    {
        public static List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public CaesarCipher()
        {
            InitializeComponent();
            numericUpDown1.Maximum = alphabet.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e) //caesar
        {
            textBox2.Text = ShiftCrypt(textBox1.Text.ToLower(), (int)numericUpDown1.Value);
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ShiftDeCrypt(textBox2.Text.ToLower(), (int)numericUpDown1.Value);
            textBox2.Text = "";
        }

        public static string ShiftCrypt(string input, int shift)  //CAESAR
        {
            if (input == null || input.Length == 0)
                return "";

            string output = "";
            foreach (char c in input)
            {
                if (c == ' ') //remove spaces
                    continue;

                if (!alphabet.Contains(c)) //remove numbers etc
                {
                    return "String contains non letter characters!";
                }

                output += moveInAlphabet(c, shift); //add new char on shifted position
            }

            return output;
        }

        public static string ShiftDeCrypt(string input, int shift)
        {
            return ShiftCrypt(input, shift * (-1));
        }

        public static char moveInAlphabet(char c, int shift) //returning shifted letter for all functions
        {
            int position = alphabet.IndexOf(c); //actual position of char
            shift += alphabet.Count; //remove negative
            return alphabet[(position + shift) % alphabet.Count];
        }

    }
}
