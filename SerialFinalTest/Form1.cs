using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
 
namespace SerialFinalTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)//设置可以选择的串口号
            {
                comboBox1.Items.Add("COM" + i.ToString());
            }
            comboBox1.Text = "COM1";//设置串口号的默认值
            comboBox2.Text = "115200";//波特率默认值

            /********************非常重要************************/
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);

        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!radioButton1.Checked)//如果接受模式为字符模式
            {

                string str = serialPort1.ReadExisting(); //字符串方式读
                textBox1.AppendText(str);//添加内容
               
            }
            else//如果接受模式为数值接收
            {
                byte data;
                data = (byte)serialPort1.ReadByte();
                string str = Convert.ToString(data, 16).ToUpper();
                textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;//端口名字直接赋值过去
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text, 10);//波特率字符串转换成十进制
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("端口错误，请检查串口", "错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception err)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
