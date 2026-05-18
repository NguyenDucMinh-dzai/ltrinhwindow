using System;
using System.Windows.Forms;

namespace MusicBox_QuanLyThanhVien
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // SỬA: Thêm dòng này để chạy Form1 khi bấm Start
            Application.Run(new Form1());
        }
    }
}