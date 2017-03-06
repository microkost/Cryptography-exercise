using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCiphers
{
    public partial class BasicCiphers : Form
    {
        public static List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static List<char> substitutedAlphabet;
        public static byte[] otpkey;

        public List<int> key;
        public BasicCiphers()
        {
            InitializeComponent();
            key = new List<int>();
            substitutedAlphabet = new List<char>(); //new List<char>();

            numericUpDown1.Maximum = alphabet.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e) //CAESAR
        {
            textBox2.Text = ShiftCrypt(textBox1.Text.ToLower(), (int)numericUpDown1.Value);
            textBox1.Text = "";
        }

        public static char moveInAlphabet(char c, int shift) //returning shifted letter for all functions
        {
            int position = alphabet.IndexOf(c); //actual position of char
            shift += alphabet.Count; //remove negative
            return alphabet[(position + shift) % alphabet.Count];

            //overkill version
            /*if (shift >= 0)
            {
                if (position <= ((alphabet.Count - 1) - shift)) //protection of end of alphabet
                {
                    position += shift; //if fits just shift
                }
                else
                {
                    int rest = (alphabet.Count - 1) - position; //magic math of new position //what about -1?
                    int shiftfromstart = shift - rest;
                    position = 0 + shiftfromstart;
                }
            }
            else
            {
                if (position + shift >= 0) //number - (-shift)
                {
                    position += shift;
                }
                else
                {
                    int rest = (-1) * shift - position;
                    position = (alphabet.Count - 1) - rest;
                }
            }

            return alphabet[position];
            */
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = ShiftDeCrypt(textBox2.Text.ToLower(), (int)numericUpDown1.Value);
            textBox2.Text = "";
        }

        public static string ShiftDeCrypt(string input, int shift)
        {
            return ShiftCrypt(input, shift * (-1));
        }


        private void button5_Click(object sender, EventArgs e) //crypt One Pad
        {
            Tuple<string, byte[]> crypt = OnePadCrypt(textBox1.Text);            
            otpkey = crypt.Item2;

            textBox2.Text = crypt.Item1;
            textBox3.Text = System.Text.Encoding.Default.GetString(crypt.Item2);
            textBox1.Text = "";
        }

        public static Tuple<string, byte[]> OnePadCrypt(string input, byte[] givenkey = null)
        {

            byte[] key;
            if (givenkey == null)
            {
                key = new byte[input.Length];

                Random r = new Random();
                r.NextBytes(key);
            }
            else
            {
                key = givenkey;
            }
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {

                output += (char)((byte)input[i] ^ key[i]);
            }

            return new Tuple<string, byte[]>(output, key);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = OnePadDeCrypt(textBox2.Text, otpkey);
            textBox2.Text = "";
        }

        public static string OnePadDeCrypt(string input, byte[] key)
        {
            Tuple<string, byte[]> decrypt = OnePadCrypt(input, key);
            return decrypt.Item1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (substitutedAlphabet.Count != 0 || substitutedAlphabet != null)
            {
                substitutedAlphabet.Clear();
            }

            var rnd = new Random();
            substitutedAlphabet = alphabet.OrderBy(item => rnd.Next()).ToList(); //mix alhabet

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (substitutedAlphabet.Count == 0 || substitutedAlphabet == null) //protection
                button8_Click(sender, e);

            textBox2.Text = DoSubstituteCipher(textBox1.Text.ToLower(), alphabet, substitutedAlphabet); //call crypting function
            textBox1.Text = "";
        }

        private static string DoSubstituteCipher(string input, List<char> alphabetSource, List<char> alphanetDestination)
        {
            if (input == null || input.Length == 0)
                return "Nothing to encrypt";

            if (alphabetSource.Count != alphanetDestination.Count)
                return "Alphabets are unequal, cant continue";

            string output = "";
            foreach (char c in input)
            {
                int originalindex = alphabetSource.IndexOf(c); //get location in alphabet
                if (originalindex < 0)
                    continue;

                output += alphanetDestination[originalindex]; //append letter on same position
            }

            return output;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (substitutedAlphabet.Count == 0 || substitutedAlphabet == null)
                button8_Click(sender, e);

            textBox1.Text = DoSubstituteCipher(textBox2.Text.ToLower(), substitutedAlphabet, alphabet); //call it with switched alphabets
            textBox2.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text.ToLower();

            if(textBox1.TextLength != textBox4.TextLength)
                return;

            string result = "";
            for(int i = 0; i< textBox1.TextLength; i++)
            {
                result += ShiftCrypt(message[i].ToString(), alphabet.IndexOf(textBox4.Text.ToLower()[i]));
            }

            textBox2.Text = result;
            textBox1.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string message = textBox2.Text.ToLower();

            if (textBox2.TextLength != textBox4.TextLength)
                return;

            string result = "";
            for (int i = 0; i < textBox2.TextLength; i++)
            {
                result += ShiftCrypt(message[i].ToString(), -alphabet.IndexOf(textBox4.Text.ToLower()[i]));
            }

            textBox1.Text = result;
            textBox2.Text = "";
        }
    }
}