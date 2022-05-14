using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace TYP_2lab
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public partial class Form1 : Form
    {
        public static Table Tables = new();
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
            Analyzer();
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

            foreach (var x in Tables.InfdificateType.ToArray())
            {
                Tables.InfdificateType.Remove(x);
            }

            foreach (var x in Tables.DigitTypes.ToArray())
            {
                Tables.DigitTypes.Remove(x);
            }

            listBoxDigit.Items.Clear();
            listBoxIndificate.Items.Clear();
            textBoxCodeResult.Clear();
            textBoxReusltMessage.Clear();
            textBoxPolis.Clear();
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            Analyzer();
        }

        public void Analyzer()
        {
            Clear();

            var action = new Actions();
            var table = new Lexema(Tables);
            var parser = new Parser(Tables);

            action.Execute(textBoxCode.Text);

            textBoxReusltMessage.Text = action.Message() + Environment.NewLine;
            textBoxCodeResult.Text = @"";

            foreach (var x in Tables.Lexemes.ToArray())
            {
                textBoxCodeResult.Text += @"(" + x.NumTable + @"," + x.NumSymbol + @")";
            }

            foreach (var x in Tables.InfdificateType.ToArray())
            {
                textBoxPolis.Text += x.Item + @"-" + x.Type + Environment.NewLine;
                foreach (var d in Tables.DigitTypes.ToArray())
                {
                    textBoxPolis.Text += d.Item + @"-" + d.Type + Environment.NewLine;
                }
            }

            listBoxIndificate.Items.AddRange(Tables.ItemTableIndificate());
            listBoxDigit.Items.AddRange(Tables.ItemTableDigit());
        }

        private void GoToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var action = new Actions();
            action.FollowThisLink();
        }

        private void listBoxForServerWord_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}