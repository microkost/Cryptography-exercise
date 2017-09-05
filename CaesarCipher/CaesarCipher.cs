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
        public static List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private int numberOfBruteIterations { get; set; }
        public CaesarCipher()
        {
            InitializeComponent();

            numericUpDown1.Maximum = alphabet.Count - 1; //shift feature
            textBoxAplhabet.Text = String.Join(", ", alphabet); //showing alphabet to user only

            numberOfBruteIterations = 1000; //brute force calculation
            numericUpDown2.Value = numberOfBruteIterations;
        }

        private void button1_Click(object sender, EventArgs e) //caesar button
        {
            textBox2.Text = ShiftCrypt(textBox1.Text, (int)numericUpDown1.Value);
            textBox1.Text = "";

            textBoxAplhabetShift.Text = ShiftCrypt(String.Join(", ", alphabet), (int)numericUpDown1.Value); //showing shifted alphabet to user only
        }

        private void button2_Click(object sender, EventArgs e) //caesar button
        {
            textBox1.Text = ShiftDeCrypt(textBox2.Text, (int)numericUpDown1.Value);
            textBox2.Text = "";

            textBoxAplhabetShift.Text = ShiftCrypt(String.Join(", ", alphabet), (int)numericUpDown1.Value); //showing shifted alphabet to user only
        }

        public static string ShiftCrypt(string input, int shift)  //CAESAR
        {
            if (input == null || input.Length == 0)
                return "";

            string output = "";
            foreach (char c in input)
            {
                if (c == ' ') //remove spaces
                {
                    output += c; //dont shift spaces
                    continue;
                }

                if (!alphabet.Contains(c)) //remove numbers and special characters etc
                {
                    output += c; //dont shift other characters
                    continue;
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

        //----------------------------------------------------------------BRUTE-FORCE

        public List<String> bruteForceVariants = new List<String>();
        private void button3_Click(object sender, EventArgs e)
        {            
            richTextBox1.Text = ""; //reset ofoutput window
           
            for (int i = 0; i < numberOfBruteIterations; i++) //make a list of possible variants
            {
                bruteForceVariants.Add(ShiftCrypt(textBox3.Text, i));
            }
                      
            for (int i = 0; i < numberOfBruteIterations; i++) //printing and searching for matchies
            {
                string insertText = String.Format(("Key {0}:\t {1},{2}"), i, bruteForceVariants[i], Environment.NewLine); //line output form

                if((textBox4.Text == null || textBox4.Text == "")) //when help word not provided
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                }
                else if (bruteForceVariants[i].ToLower().Contains(textBox4.Text.ToLower())) //when match founded, highlight every matchi in lower case!
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                }
                else
                {
                    richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                }

                richTextBox1.AppendText(insertText);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            numberOfBruteIterations = textBox3.Text.Length;
            numericUpDown2.Value = numberOfBruteIterations;
        }
    }
}
