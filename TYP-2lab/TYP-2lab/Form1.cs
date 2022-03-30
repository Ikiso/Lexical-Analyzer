using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace TYP_2lab
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public partial class Form1 : Form
    {
        public static Table Tables = new Table();
        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = @"Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = @"Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listBoxForServerWord.Items.AddRange(Tables.ItemValuesTableSeveredWord().ToArray());
            listBoxRazdelitelei.Items.AddRange(Tables.ItemTableRazdeliteli().ToArray());
        }

        // Меню стрип - действия //

        /// <summary>
        /// Выполнить анализ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformAnAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();

            var action = new Actions();
            var table = new Lexema(Tables);

            action.Execute(textBoxCode.Text);

            textBoxReusltMessage.Text = action.Message();
            textBoxCodeResult.Text = @"";

            foreach (var x in Tables.Lexemes)
            {
                textBoxCodeResult.Text += x;
            }

            listBoxIndificate.Items.AddRange(Tables.ItemTableIndificate());
            listBoxDigit.Items.AddRange(Tables.ItemTableDigit());
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var action = new Actions();
            textBoxCode.Text = action.FileOpen(sender, e);
        }

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var action = new Actions();
            action.FileSave(textBoxCode.Text);
        }

        private void CleareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxCode.Clear();
            Clear();
        }

        public void Clear()
        {
            foreach (var x in Tables.Lexemes.ToArray())
            {
                Tables.Lexemes.Remove(x);
            }

            foreach (var x in Tables.TableDigit.ToArray())
            {
                Tables.TableDigit.Remove(x);
            }

            foreach (var x in Tables.TableInfdificate.ToArray())
            {
                Tables.TableInfdificate.Remove(x);
            }

            listBoxDigit.Items.Clear();
            listBoxIndificate.Items.Clear();
            textBoxCodeResult.Clear();
            textBoxReusltMessage.Clear();
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}