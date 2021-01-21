/**
 * Author: Zhiping Yu
 * Date:   November 22 2020
 * Purpose: This form is created for user to screen tags from html file. 
 *          
 *          
 * I, Zhiping Yu, 000822513 certify that this material is my original work.  
 *  No other person's work has been used without due acknowledgement.
 * 
 */

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

namespace PartB
{
    /// <summary>
    /// This class opens a html file and read data from it. Then, extract html tags from the file and store
    /// them into a stack of string. Finally print them in the same order and format as they were in the file previouly.
    /// </summary>
    public partial class Form1 : Form
    {
        // a stack of string which is used to store tags read from a html file
        private Stack<string> Tags { get; set; } = new Stack<string>();
        private int countOpenTag; // number of opening tags
        private int countCloseTag; // number of closing tags
        /// <summary>
        /// Constructor without parameters, set up the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Enable and clear the listbox storing the tags. Make sure only html file can be opened.
        /// if user forget to open any files, providing a error message in a message box. Otherwise,
        /// load the file. If file cannot be loaded, throw an exception to stop the application.
        /// If the file is loaded successfully, read the content in the file one line and one line.
        /// Search for the tags in the file based on the html tags features and assign them into three different
        /// different groups, which are opening, closing and non-container tags. After finding the tags, store them
        /// into the stack of string. 
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tagsLB.Items.Clear();
            tagsLB.Enabled = false;
            // message shown to indicate if a file loaded or not
            messageLbl.Text = "No File Loaded";
            // define the type of files to be loaded
            openFileDialog1.Filter = "HTML Files|*.html";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            // set up open file dialogue title
            openFileDialog1.Title = "Select a HTML file to open";
            // user does not select any file and show message to reminder the user
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("You did not select a file!!!","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            messageLbl.Text = "Loaded: " + openFileDialog1.SafeFileName;
            // catch the exception when opening file fails
            try
            {
                // define a stream from the file to the application
                StreamReader sr = File.OpenText(openFileDialog1.FileName);

                // check if the file has been read completely
                while (!sr.EndOfStream)
                {
                    int numSpace = 0; // number of space in front of the first character in a line
                    string position = ""; // the content before the string which will be in the stack
                    string originalText = sr.ReadLine().ToLower();

                    // find how many space before the first character in the line
                    if (originalText.Contains('<'))
                    {
                        for (int i = 0; i < originalText.Length; i++)
                        {
                            if (originalText[i] == ' ')
                            {
                                numSpace += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                         position = new string(' ', numSpace); // store the number of space into the string 
                    }

                    // check while the string in the line contains the <>, loop the original string
                    while(originalText.Contains('<') && originalText.Contains('>'))
                    {
                        int openIndex = originalText.IndexOf('<'); 
                        int closeIndex = originalText.IndexOf('>');
                        // extract the string which has <> from position of < to position of >
                        string tempText = originalText.Substring(openIndex, closeIndex - openIndex + 1);

                        // if the substring with spaces inside it
                        if(tempText.Contains(' '))
                        {
                            int blankIndex = tempText.IndexOf(' '); // find the position of the first space
                            tempText = tempText.Substring(0, blankIndex)+">"; // delete the conten started from first blank space, and concatenate string > to the left part.
                            if(tempText == "<img>") // if the tag is <img>
                            {
                                string finalText = position+ "Found non-container tag: " + tempText;
                                Tags.Push(finalText);
                            }
                            else // if the tag is not <img>
                            {
                                countOpenTag++;
                                string finalText = position + "Found opening tag: " + tempText;
                                Tags.Push(finalText);
                            }


                        }
                        // if the substring without spaces inside it
                        else
                        {
                            // if the substring has no / inside it
                            if (!tempText.Contains('/')) 
                            {
                                if(tempText== "<br>" || tempText== "<hr>") // if the tags are <br> or <hr>
                                {
                                    string finalText = position + "Found non-container tag:" + tempText;
                                    Tags.Push(finalText);
                                }
                                else // if the tag is not <br> or <hr>
                                {
                                    countOpenTag++;
                                    string finalText = position + "Found opening tag: " + tempText;
                                    Tags.Push(finalText);
                                }
                            }
                            // if the substring has / inside it 
                            else
                            {
                                countCloseTag++;
                                string finalText = position + "Found closing tag: " + tempText;
                                Tags.Push(finalText);
                            }
                        }
                        if (closeIndex == originalText.Length - 1)
                        {
                            break;
                        }
                        else
                        {
                            originalText = originalText.Substring(closeIndex + 1);
                        }

                    }

                }
            }
            catch(Exception ex)
            {
                messageLbl.Text = "File cannot be found, due to " + $"{ex.Message}";
            }
            // enable the checkTags menu
            checkTagsToolStripMenuItem.Enabled = true;

        }
        /// <summary>
        ///  Enable and clear the listbox which is used to hold the tags. Then, create an array of string.
        ///  Use that string to store the tags from stack in reverse order. After that, add the tags into the 
        ///  listbox. If the number of opening tags is equal to the number of closing tags, show the file
        ///  has the balanced tags. Otherwise, indicate that the file has unbalanced tags.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void checkTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tagsLB.Enabled = true;
            // clear the listbox
            tagsLB.Items.Clear();
            // decide if the tags are blanced or not
            if (countCloseTag != countOpenTag)
            {
                messageLbl.Text = openFileDialog1.SafeFileName + " does not have balanced tags";
            }
            else
            {
                messageLbl.Text = openFileDialog1.SafeFileName + " has balanced tags";
            }
            // create a new array of string and length of it is same as the stack
            string[] arr = new string[Tags.Count];

            // shift items in stack to the array in reverse order

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                arr[i] = Tags.Pop();
            }
            // add the whole array to the listbox
            tagsLB.Items.AddRange(arr);
            // deactive the checkTags menu
            checkTagsToolStripMenuItem.Enabled = false;

        }

        /// <summary>
        /// Exit the program when cliking the exit menu
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
