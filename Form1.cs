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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(client != null)
            {
                try
                {
                    string s = input.Text;
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    DataHeader dataHeader = new DataHeader(0x03, (UInt16)data.Length);
                    byte[] header = StructToBytes(dataHeader);
                    //ShowMsg(dataHeader.flags + " " + dataHeader.data_len);
                    int len = header.Length + data.Length;
                    byte[] buffer = new byte[len];
                    //Buffer.BlockCopy(header, 0, buffer, 0, header.Length);
                    //Buffer.BlockCopy(data, 0, buffer, header.Length, data.Length);
                    buffer = header.Concat(data).ToArray();
                    client.Send(buffer);
                }
                catch(Exception ex)
                {
                    ShowMsg(ex.Message);
                }
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
            if(client == null)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            IPAddress ip = IPAddress.Parse("121.43.42.24");
            IPEndPoint point = new IPEndPoint(ip, int.Parse("1234"));
            try
            {
                client.Connect(point);
                ShowMsg("连接成功");
                DataHeader data = new DataHeader(0x01, 0);
                byte[] buffer = StructToBytes(data);
                client.Send(buffer);
                ShowMsg("登录中..");
                //连接成功后，就可以接收服务器发送的信息了
                th = new Thread(new ThreadStart(delegate
                {
                    Control.CheckForIllegalCrossThreadCalls = false;//添加这一句即可
                    ReceiveMsg();
                }));
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                ShowMsg("连接失败[" + ex.Message + "]");
            }

        }

        //接收服务器的消息
        void ReceiveMsg()
        {
            while (true)
            {
                if (client == null) break;
                
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int n = client.Receive(buffer);
                    UInt16 flag = BitConverter.ToUInt16(buffer, 0);
                    UInt16 data_len = BitConverter.ToUInt16(buffer, 2);
                    string data = Encoding.UTF8.GetString(buffer, 4, data_len);

                    if(flag == 0x01 || flag == 0x03)
                    {
                        ShowMsg(client.RemoteEndPoint.ToString() + ":" + data);
                    }
                    else if (flag == 0x02)
                    {
                        ShowMsg(client.RemoteEndPoint.ToString() + ":" + data);
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        client = null;
                        Application.ExitThread();
                        break;
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
        /// byte[]转化成结构体
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="strcutType"></param>
        /// <returns></returns>
        public static Object BytesToStruct(Byte[] bytes, Type strcutType)
        {
            Int32 size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);

                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        void ShowMsg(string msg)
        {
            textBox1.AppendText(msg + "\r\n");
        }

        public int IntToBitConverter(byte[] bytes)
        {
            int temp = BitConverter.ToInt32(bytes, 0);
            return temp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(client != null)
            {
                try
                {
                    DataHeader data = new DataHeader(0x02, 0);
                    byte[] buffer = StructToBytes(data);
                    client.Send(buffer);
                    ShowMsg("退出中..");
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
    }

}
