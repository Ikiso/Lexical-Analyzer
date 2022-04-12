using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
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

        public void FollowThisLink()
        {
            var url = "https://coub.com/view/31h9qx";
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void Execute(string str)
        {
            Lexema.Scan(str);
            Parser.Prog();
        }
        public string Message()
        {
            return Lexema.Message(Lexema.State, Lexema.ErroreCode) + Environment.NewLine + Parser.Message(Parser.ErroreCode);
        }
    }
}
