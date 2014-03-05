/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBEmu
{
    public partial class GBEmuWindow : Form
    {
        public GBEmuWindow()
        {
            InitializeComponent();
        }

        private void GBEmuWindow_Load(object sender, EventArgs e)
        {
            GPU.setDisplay(ref display);
            main.reset();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            main.frame();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Executing command at address: " + Z80.Registers.pc.ToString("X") + "(" + MMU.readByte(Z80.Registers.pc).ToString("X") + ")";
            Z80.step();
            GPU.step();
            printRegisters();
        }

        private void printRegisters()
        {
            textBox1.Text += ("\r\nCPU Registers:");
            textBox1.Text += ("\r\nA : " + Z80.Registers.a.ToString("X"));
            textBox1.Text += ("\r\nB : " + Z80.Registers.b.ToString("X"));
            textBox1.Text += ("\r\nC : " + Z80.Registers.c.ToString("X"));
            textBox1.Text += ("\r\nD : " + Z80.Registers.d.ToString("X"));
            textBox1.Text += ("\r\nE : " + Z80.Registers.e.ToString("X"));
            textBox1.Text += ("\r\nH : " + Z80.Registers.h.ToString("X"));
            textBox1.Text += ("\r\nL : " + Z80.Registers.l.ToString("X"));
            textBox1.Text += ("\r\nPC : " + Z80.Registers.pc.ToString("X"));
            textBox1.Text += ("\r\nSP : " + Z80.Registers.sp.ToString("X"));
            textBox1.Text += ("\r\nF (Decimal) : " + Z80.Registers.f.ToString("X"));
            textBox1.Text += ("\r\nClock m : " + Z80.Clock.m.ToString("X"));
            textBox1.Text += ("\r\nClock t : " + Z80.Clock.t.ToString("X"));
        }

        private void btnStepN_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            while (Z80.Registers.pc != Convert.ToInt16(txtStepN.Text))
            {
                Z80.step();
                GPU.step();
            }
            printRegisters();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            main.reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            for (int i = 0; i < Convert.ToInt16(txtStepN2.Text); i++)
            {
                Z80.step();
                GPU.step();
            }
            Z80.step();
            GPU.step();
            printRegisters();
        }
    }
}
*/