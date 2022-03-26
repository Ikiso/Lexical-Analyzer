using System;
using System.IO;
using System.Windows.Forms;

namespace TYP_2lab
{
    public class Actions : IActions
    {
        private readonly Form1 _form1 = new Form1();
        public string FileOpen(object sender, EventArgs e)
        {
            if (_form1.openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return "";

            // получаем выбранный файл
            var filename = _form1.openFileDialog1.FileName;
            // читаем файл в строку
            var fileText = File.ReadAllText(filename);

            MessageBox.Show(@"Код программы записан");
            _form1.textBoxCode.Text = fileText;
            return _form1.textBoxCode.Text;
        }

        public void FileSave(string str)
        {
            if (_form1.saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // Сохранем в выбранный файл
            var filename = _form1.saveFileDialog1.FileName;
            // Сохраняем текст

            File.WriteAllText(filename, str);
            MessageBox.Show(@"Файл сохранён");
        }

        public void Execute(string str)
        {
            Lexema.Scan(str);
        }

        public string Message()
        {
            return Lexema.Message(Lexema.State, Lexema.ErroreCode);
        }
    }
}
