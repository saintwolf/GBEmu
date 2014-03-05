using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBEmu
{
    class GBEmu
    {
        

    /*    
        [STAThread]
        static void Main()
        {
            // Load Emulator Window
            StartLoadWindowThread();

            Console.WriteLine("GBEMU DEV by Saintwolf");

            Console.WriteLine("Resetting Gameboy");
            reset();

            Console.WriteLine("Loading ROM...");
            MMU.load("C:\\GBROMS\\Pokemon Blue.gb");


            bool running = true;

            while (running)
            {
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

                    case ConsoleKey.X:
                        running = false;
                        Console.WriteLine("Shutting Down...");
                        break;

                    case ConsoleKey.J:
                        while (Z80.Registers.pc != 0x6A)
                        {
                            Z80.step();
                            GPU.step();
                        }
                        break;

                    case ConsoleKey.N:
                        for (int i = 0; i < 1000; i++)
                        {
                            frame();
                        }
                        break;

                    case ConsoleKey.R:
                        Z80.reset();
                        break;
                }

            }
        }
        */
        public static void reset()
        {
            GPU.reset();
            MMU.reset();
            Z80.reset();
        }

        public static void frame()
        {Thread.Sleep(2);
            int fclk = Z80.Clock.t + 70224;
            do
            {
                try
                {
                    Z80.step();
                    GPU.step();
                }
                catch (Exception e)
                {
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
                }
            } while (Z80.Clock.t < fclk);
        }

        private static void StartLoadWindowThread()
        {
            Thread t = new Thread(new ThreadStart(LoadWindow));
            t.Start();
        }

        private static void LoadWindow()
        {
        //    Application.Run(new GBEmuWindow());
        }

        
    }
}
