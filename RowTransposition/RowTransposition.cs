using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RowTransposition
{

    public partial class RowTransposition : Form
    {
        private int sizeOfAlign { get; set; }
        public List<string> linesOriginal;
        public List<string> linesPermutated;
        public List<int> KeyEncryption;
        public List<int> KeyDecryption;
        
        public RowTransposition()
        {
            InitializeComponent();
            linesOriginal = new List<String>();
            linesPermutated = new List<String>();
            KeyEncryption = new List<int>();
            KeyDecryption = new List<int>();
        }

        private void buttonAlign_Click(object sender, EventArgs e)
        {
            int countOfChars = textBox1.Text.Length;
            if (countOfChars == 0)
                return;

            if (linesOriginal.Count > 0)
                linesOriginal.Clear();

            if(linesPermutated.Count > 0)
                linesPermutated.Clear();

            textBox1.Text.Trim(); //remove spaces if againly reassigned           
            string input = "";
            foreach(char c in textBox1.Text)
            {
                if (c != ' ')
                    input += c;
            }
            textBox1.Text = input;

            sizeOfAlign = 5;
            double groupsNotRounded = (double)countOfChars / (double)sizeOfAlign;
            int groups = (int)Math.Ceiling(groupsNotRounded);

            string append = "";
            for (int i = 0; i < (groups*sizeOfAlign)-countOfChars; i++)
            {
                append += "Q";                
            }
            textBox1.Text += append;
            countOfChars = textBox1.TextLength; //update

            linesOriginal = textBox1.Text.Select((c, i) => new { Char = c, Index = i }).GroupBy(o => o.Index / 5).Select(g => new String(g.Select(o => o.Char).ToArray())).ToList();          

            string output = "";
            foreach (string l in linesOriginal)
            {
                output += l + ' ';
            }

            textBox1.Text = output;
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (linesOriginal.Count == 0)
                buttonAlign_Click(sender, e);

            linesPermutated.Clear(); //reset

            foreach(char c in textBoxEncKey.Text) //parsing numbers, checking number
            {
                KeyEncryption.Add((int)Char.GetNumericValue(c));
            }

            foreach (string lo in linesOriginal)
            {
                List<char> newline = new List<char>(sizeOfAlign);                
                for (int i = 0; i < sizeOfAlign; i++)
                {                    
                    newline.Add(lo[KeyEncryption[i] - 1]); //get value on position of key encryption and put it on first position in permutated string
                }
                linesPermutated.Add(string.Join("", newline.ToArray()));
            }
            //string codedmessage = string.Join("", linesPermutated.ToArray());

            string output = "";
            foreach (string l in linesPermutated)
            {
                output += l + ' ';
            }

            textBox2.Text = output;
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //needs to be aligned! Method is missing... Extend buttonAlign_Click()

            linesPermutated.Clear(); //remove what was inside
            linesOriginal.Clear(); //remove original message

            string[] crypted = textBox2.Text.Split(' '); //parsing from GUI
            foreach (string s in crypted)
            {
                if(s != "")
                    linesPermutated.Add(s);
            }            

            foreach (char c in textBoxDecKey.Text) //parsing keys
            {
                KeyDecryption.Add((int)Char.GetNumericValue(c));
            }

            foreach (string lo in linesPermutated) //final decoding
            {
                List<char> newline = new List<char>(sizeOfAlign);
                for (int i = 0; i < sizeOfAlign; i++)
                {                    
                    newline.Add(lo[KeyDecryption[i] - 1]); //get value on position specified by key and put in iterator position to result
                }
                linesOriginal.Add(string.Join("", newline.ToArray()));
            }

            string output = ""; 
            foreach (string l in linesOriginal)
            {
                output += l + ' '; //aligning
            }
            
            textBox1.Text = output;
            textBox2.Text = "";
        }
    }
}
