using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace CNET_TEST
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct DataHeader
    {
        public UInt16 flags;     // 标志位:0x01登录,0x02退出,0x03发送信息
        public UInt16 data_len;  // 负载长度(小于1000)
        
        public DataHeader(UInt16 flags, UInt16 data_len)
        {
            this.flags = flags;
            this.data_len = data_len;
        }
    };

    public partial class Form1 : Form
    {

        private bool connet_status = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                process_id_CB.Items.Add(new ComboxItem(p.ProcessName + "(" + p.Id + ")", p.Id));
            }
        }

        private void process_id_CB_Click(object sender, EventArgs e)
        {
            process_id_CB.Items.Clear();
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                process_id_CB.Items.Add(new ComboxItem(p.ProcessName + "(" + p.Id + ")", p.Id));
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // 发送消息
            if (connet_status)
            {
                SendMessage(0x03, input.Text);
                input.Text = "";
            }
            else
            {
                ShowMsg("未连接!");
            }
        }

        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Thread th;

        private void button2_Click(object sender, EventArgs e)
        {
            // 登录按钮
            if(textBox2.Text.Length == 0)
            {
                MessageBox.Show("请在左侧填写一个名称");
                return;
            }
            if(process_id_CB.Text.Length == 0)
            {
                MessageBox.Show("请先绑定一个进程");
                return;
            }
            if (!connet_status)
            {
                if (client == null)
                {
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                IPAddress ip = IPAddress.Parse("121.43.42.24");
                IPEndPoint point = new IPEndPoint(ip, int.Parse("1234"));
                try
                {
                    client.Connect(point);
                    ShowMsg("连接成功");
                    service_status.Text = "连接成功";
                    service_status.ForeColor = Color.Lime;
                    SendMessage(0x01, textBox2.Text);
                    ShowMsg("登录中..");
                    //连接成功后，就可以接收服务器发送的信息了
                    th = new Thread(new ThreadStart(delegate
                    {
                        Control.CheckForIllegalCrossThreadCalls = false;//添加这一句即可
                        ReceiveMsgThread();
                    }));
                    th.IsBackground = true;
                    th.Start();
                    connet_status = true;

                    process_id_CB.Enabled = false; // 进程列表
                    button2.Enabled = false;// 连接
                    button3.Enabled = true; // 退出
                    button1.Enabled = true; // 发送
                    input.Enabled = true;   // 输入框
                    textBox2.Enabled = false; // 用户名
                }
                catch (Exception ex)
                {
                    service_status.Text = "连接失败";
                    service_status.ForeColor = Color.Red;
                    ShowMsg("连接失败[" + ex.Message + "]");
                    connet_status = false;
                }
            }
            else
            {
                ShowMsg("已连接!请勿重复连接!");
            }
        }

        //接收服务器的消息线程
        void ReceiveMsgThread()
        {
            while (true)
            {
                if (!connet_status) break;
                
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int n = client.Receive(buffer);
                    UInt16 flag = BitConverter.ToUInt16(buffer, 0);
                    UInt16 data_len = BitConverter.ToUInt16(buffer, 2);
                    string data = Encoding.UTF8.GetString(buffer, 4, data_len);

                    if(flag == 0x01 || flag == 0x03)
                    {
                        //ShowMsg(client.RemoteEndPoint.ToString() + ":" + data);
                        ShowMsg(data);
                    }
                    else if (flag == 0x02)
                    {
                        ShowMsg(data);
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        client = null;
                        Application.ExitThread();
                        connet_status = false;
                        break;
                    }
                    else if (flag == 0x04)
                    {
                        ShowMsg(data);
                        KillProcess(((ComboxItem)process_id_CB.SelectedItem).Values);
                        SendMessage(0x03, "执行KILL进程命令");
                    }
                }
                catch(Exception ex)
                {
                    ShowMsg(ex.Message);
                    break;
                }
            }
            return; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(connet_status)
            {
                try
                {
                    SendMessage(0x02, "");
                    ShowMsg("退出中..");
                    service_status.Text = "未连接";
                    service_status.ForeColor = Color.Red;
                    connet_status = false;

                    process_id_CB.Enabled = true; // 进程列表
                    button2.Enabled = true;   // 连接
                    button3.Enabled = false;  // 退出
                    button1.Enabled = false;  // 发送
                    input.Enabled = false;    // 输入框
                    textBox2.Enabled = true; // 用户名
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message);
                }
            }
            else
            {
                ShowMsg("未连接!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
           if(process_id_CB.Text.Length == 0)
            {
                process_status.Text = "未绑定";
                process_status.ForeColor = Color.Red;
            }
            else
            {
                process_status.Text = "绑定成功:"+ process_id_CB.Text;
                process_status.ForeColor = Color.Lime;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F3:
                    if(process_id_CB.Text.Length == 0)
                    {
                        MessageBox.Show("未绑定进程ID");
                    }
                    else if (!connet_status)
                    {
                        MessageBox.Show("未连接到服务器");
                    }
                    else
                    {
                        // 发送杀死进程信号
                        //ShowMsg("发送KILL进程信号!");
                        SendMessage(0x04, "发送KILL进程信号!");
                    }
                    break;
                case Keys.Enter:
                    // 发送消息
                    if (connet_status)
                    {
                        SendMessage(0x03, input.Text);
                        input.Text = "";
                    }
                    else
                    {
                        ShowMsg("未连接!");
                    }
                    break;
            }
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名</param>
        private void KillProcess(int id)
        {
            System.Diagnostics.Process[] myproc = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process item in myproc)
            {
                if (item.Id == id)
                {
                    item.Kill();
                }
            }
        }

        /// <summary>
        /// 发送指定代码和字符串
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="s"></param>
        private void SendMessage(UInt16 flags, string s)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(s);
                DataHeader dataHeader = new DataHeader(flags, (UInt16)data.Length);
                byte[] header = StructToBytes(dataHeader);
                int len = header.Length + data.Length;
                byte[] buffer = new byte[len];
                buffer = header.Concat(data).ToArray();
                client.Send(buffer);
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }

        /// <summary>
        /// 结构体转化成byte[]
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static Byte[] StructToBytes(Object structure)
        {
            Int32 size = Marshal.SizeOf(structure);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                Byte[] bytes = new Byte[size];
                Marshal.Copy(buffer, bytes, 0, size);

                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 将字符串输出到屏幕
        /// </summary>
        void ShowMsg(string msg)
        {
            textBox1.AppendText(msg + "\r\n");
        }


    }

    public class ComboxItem
    {
        private string text;
        private int values;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public int Values
        {
            get { return this.values; }
            set { this.values = value; }
        }

        public ComboxItem(string _Text, int _Values)
        {
            Text = _Text;
            Values = _Values;
        }


        public override string ToString()
        {
            return Text;
        }
    }

}
