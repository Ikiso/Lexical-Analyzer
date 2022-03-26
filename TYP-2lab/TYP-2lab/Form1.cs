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
            var table = new Table();

            listBoxForServerWord.Items.AddRange(table.ItemValuesTableSeveredWord().ToArray());
            listBoxRazdelitelei.Items.AddRange(table.ItemTableRazdeliteli().ToArray());
        }

        // Меню стрип - действия //

        /// <summary>
        /// Выполнить анализ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformAnAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var action = new Actions();
            var table = new Lexema(Tables);

            Clear();

            action.Execute(textBoxCode.Text);

            textBoxReusltMessage.Text = action.Message();
            textBoxCodeResult.Text = @" ";

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
            Actions action = new Actions();
            action.FileSave(textBoxCode.Text);
        }

        private void CleareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxCode.Clear();
            Clear();
        }

        public void Clear()
        {
            Tables.Lexemes.Clear();
            Tables.TableInfdificate.Clear();
            Tables.TableDigit.Clear();

            listBoxDigit.Items.Clear();
            listBoxIndificate.Items.Clear();
            textBoxCodeResult.Clear();
            textBoxReusltMessage.Clear();
        }
    }
}