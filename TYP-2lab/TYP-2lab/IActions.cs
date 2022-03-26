using System;

namespace TYP_2lab
{
    interface IActions
    {
        public string FileOpen(object sender, EventArgs e);
        public void FileSave(string str);
        public void Execute(string str);
    }
}
