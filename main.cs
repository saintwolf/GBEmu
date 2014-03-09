using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Input;
using SdlDotNet.Core;

namespace GBEmu
{
    class main
    {
        private const int width = 640;
        private const int height = 576;
        private Random rand = new Random();
        private Surface screen;

        private bool reset = true;

        public main()
        {
            Video.WindowIcon();
            screen = Video.SetVideoMode(width, height);
            GPU.setDisplay(screen);

        }

        private void KeyDown(object sender, KeyboardEventArgs e)
        {

        }

        private void Quit(Object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }

        private void Tick(object sender, TickEventArgs e)
        {
            /*
            for (int i = 0; i < 144; i++)
            {
                renderScan(i);
            }

                if (GPU.lineMode != 1)
                {
                    using (Surface s = new Surface(ResizeImage(GPU.fb, 4)))
                    {
                        screen.Blit(s);
                        s.Close();
                        screen.Update();
                    }
            }
             */
            
        }

        private void Go()
        {
            Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(this.KeyDown);
            Events.Quit += new EventHandler<QuitEventArgs>(this.Quit);
            Events.Tick += new EventHandler<TickEventArgs>(this.Tick);
            Events.Fps = 60;
            Events.Run();
        }

        public static void Main()
        {
            StartLoadGUIThread();

            bool reset = true;

            while (true)
            {
                
                //    Application.Run(new GBEmuWindow());
                if (reset)
                {
                    MMU.load("C:\\GBROMS\\hello.gb");
                    GBEmu.reset();
                    reset = false;
                }

                Console.WriteLine("Registers:");
                Console.WriteLine("A : " + Z80.Registers.a.ToString("X"));
                Console.WriteLine("B : " + Z80.Registers.b.ToString("X"));
                Console.WriteLine("C : " + Z80.Registers.c.ToString("X"));
                Console.WriteLine("D : " + Z80.Registers.d.ToString("X"));
                Console.WriteLine("E : " + Z80.Registers.e.ToString("X"));
                Console.WriteLine("H : " + Z80.Registers.h.ToString("X"));
                Console.WriteLine("L : " + Z80.Registers.l.ToString("X"));
                Console.WriteLine("PC : " + Z80.Registers.pc.ToString("X"));
                Console.WriteLine("SP : " + Z80.Registers.sp.ToString("X"));
                Console.WriteLine("F (Decimal) : " + Z80.Registers.f.ToString("X"));
                Console.WriteLine("Clock m : " + Z80.Clock.m.ToString("X"));
                Console.WriteLine("Clock t : " + Z80.Clock.t.ToString("X"));
                Console.Write("Press S to step, N to step 100 times, R to reset or X to exit: ");
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("Executing command at address: " + Z80.Registers.pc.ToString("X") + "(" + MMU.readByte(Z80.Registers.pc).ToString("X") + ")");
                        Z80.step();
                        GPU.step();
                        break;

                    case ConsoleKey.J:
                        Z80.step();
                        GPU.step();
                        while (Z80.Registers.pc != 0x100)
                        {
                            Z80.step();
                            GPU.step();
                        }
                        break;

                    case ConsoleKey.N:
                        for (int i = 0; i < 1000; i++)
                        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                            GBEmu.frame();
                        }
                        break;

                    case ConsoleKey.R:
                        Z80.reset();
                        break;
                }
            }
        }

        private static void StartLoadGUIThread()
        {
            Thread t = new Thread(new ThreadStart(LoadGUI));
            t.Start();
        }

        private static void LoadGUI()
        {
            main t = new main();
            t.Go();
        }

        private Bitmap ResizeImage(Bitmap originalImage, int factor)
        {
            using (Bitmap newImage = new Bitmap(originalImage.Width * factor, originalImage.Height * factor))
            {
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    for (int y = 0; y < originalImage.Height; y++)
                    {
                        for (int x = 0; x < originalImage.Width; x++)
                        {
                            using (SolidBrush brush = new SolidBrush(originalImage.GetPixel(x, y)))
                            {
                                g.FillRectangle(brush, new Rectangle(x * factor, y * factor, factor, factor));
                            }
                        }
                    }
                }
                return (Bitmap)newImage.Clone();
            }
        }

    }
}
