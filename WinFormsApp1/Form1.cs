using Microsoft.Win32;
using System.Diagnostics;
using System.Net.Sockets;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Font = new Font("dotum", 14);
            Size = new Size(230, 100);
            label1.Location = new Point(20, 20);
            button1.Location = new Point(110, 20);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            main_();
            int minutes = 5;
            timer1.Interval = minutes * 60 * 1000;
            /*
            net8 / visual studio 2022 ver 17.8
            2023. 11.
            */
        }

        int run = 0;

        void main_()
        {
            RegistryKey localmachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            var opensubkey = localmachine.OpenSubKey("software\\microsoft\\windows nt\\currentversion");
            int currentbuild = Convert.ToInt32(opensubkey.GetValue("currentbuild"));
            if (currentbuild < 19045)
            {
                MessageBox.Show("windows update 22H2" + Environment.NewLine + "윈도우 업데이트 필요");
            }
            localmachine.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string ip_list = "127.0.0.1";
            TcpClient tcpclient = new TcpClient();
            int port = 80;//포트 설정
            int second_want = 1;//초단위 설정
            TimeSpan timespan = TimeSpan.FromSeconds(second_want);
            if (tcpclient.ConnectAsync(ip_list, port).Wait(timespan))
            { }
            else
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "start.bat";
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.Close();
                    process.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("파일을 찾을수없음\n경로확인");
                    Application.Exit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (run == 0)
            {
                label1.Text = "작동중";
                timer1.Start();
                run = 1;
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "start.bat";
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.Close();
                    process.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("파일을 찾을수없음\n경로확인");
                    Application.Exit();
                }
            }
            else if (run == 1)
            {
                label1.Text = "대기";
                timer1.Stop();
                run = 0;
            }
        }
    }
}
