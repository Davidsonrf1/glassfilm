using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm
{
    public partial class FrmCadSerial : Form
    {
        public static SerialPort GetSerialPort()
        {
            SerialPort port = new SerialPort();

            port.PortName = Program.Config["PlotterName"];
            port.BaudRate = int.Parse(Program.Config["PlotterCOMSpeed"]);

            if (Program.Config["PlotterCOMDataBits"] == "7")
            {
                port.DataBits = 7;
            }
            else
            {
                port.DataBits = 8;
            }

            port.Parity = Parity.None;
            if (Program.Config["PlotterCOMParity"] == "Par")
                port.Parity = Parity.Even;

            if (Program.Config["PlotterCOMParity"] == "Ímpar")
                port.Parity = Parity.Even;

            port.StopBits = StopBits.One;

            if (Program.Config["PlotterCOMStopBits"] == "2")
                port.StopBits = StopBits.One;

            port.Handshake = Handshake.None;
            if (Program.Config["PlotterCOMFollowControl"] == "XON/XOFF")
                port.Handshake = Handshake.XOnXOff;

            return port;
        }

        public void SetCBValue(ComboBox cb, string value)
        {
            cb.SelectedIndex = -1;

            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.Items[i].Equals(value))
                {
                    cb.SelectedIndex = i;
                }
            }
        }

        public FrmCadSerial()
        {
            InitializeComponent();

            SetCBValue(cbPorta, Program.Config["PlotterName"]);
            SetCBValue(cbSpeed, Program.Config["PlotterCOMSpeed"]);
            SetCBValue(cbBits, Program.Config["PlotterCOMDataBits"]);
            SetCBValue(cbParidade, Program.Config["PlotterCOMParity"]);
            SetCBValue(cbStopBits, Program.Config["PlotterCOMStopBits"]);
            SetCBValue(cbFollow, Program.Config["PlotterCOMFollowControl"]);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Program.Config["PlotterName"] = cbPorta.Text;
            Program.Config["PlotterCOMSpeed"] = cbSpeed.Text;
            Program.Config["PlotterCOMDataBits"] = cbBits.Text;
            Program.Config["PlotterCOMParity"] = cbParidade.Text;
            Program.Config["PlotterCOMStopBits"] = cbStopBits.Text;
            Program.Config["PlotterCOMFollowControl"] = cbFollow.Text;

            //MessageBox.Show("Configurações gravadas com sucesso", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
