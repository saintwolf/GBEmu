using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SdlDotNet.Graphics;

namespace GBEmu
{
    class GPU
    {
        private enum ModeFlags
        {
            HBlank = 0,
            VBlank = 1,
            OAMRead = 2, // Searching OAM ram
            OAMWrite = 3 // Writing OAM data to display driver
        }

        private static Surface display = null;

        // LCD Control 0xFF40
        public static bool LcdEnable = false; // FALSE=OFF TRUE=ON
        public static bool WindowTileMapSelect = false; // FALSE=9800-9BFF TRUE=9C00-9FFF
        public static bool WindowEnable = false; // FALSE=OFF TRUE=ON
        public static bool TileDataSelect = false; // FALSE=8800-97FF TRUE=8000-8FFF
        public static bool BgTileMapSelect = false; // FALSE=9800-9BFF TRUE=9C00-9FFF
        public static bool ObjSize = false; // FALSE=8x8 TRUE=8x16
        public static bool ObjEnable = false; // FALSE=OFF TRUE=ON
        public static bool BgEnable = false; // FALSE=OFF TRUE=ON

        public static int lineMode = 0;
        public static int modeClock = 0;
        public static int curLine = 0; // The current line being drawn/scanned
        public static int yScrl;
        public static int xScrl;
        public static int curScan;
        public static int raster;
        //public static UInt16 bgTileBase;
        //public static UInt16 bgMapBase;
        public static int[] scanRow;

        public static System.Drawing.Color[] pal = new System.Drawing.Color[4];

        public static byte[] vram = new byte[0x2000];
        public static byte[] oam = new byte[0xA0];

        public static byte[] lineBuffer = new byte[160];

        static GPU()
        {
            pal[0] = Color.Black;
            pal[1] = Color.LightGray;
            pal[2] = Color.Gray;
            pal[3] = Color.Black;
        }

        public static void setDisplay(Surface d)
        {
            GPU.display = d;
        }

        public static void reset()
        {
            GPU.curLine = 0;
            GPU.curScan = 0;
            GPU.lineMode = 2;
            GPU.modeClock = 0;
            GPU.yScrl = 0;
            GPU.xScrl = 0;
            GPU.raster = 0;
        }

        public unsafe static void step()
        {
            GPU.modeClock += Z80.Registers.m;
            switch (GPU.lineMode)
            {
                // OAM read mode, scanline active
                case 2:
                    if (GPU.modeClock >= 20)
                    {
                        // Enter scanline mode 3
                        GPU.modeClock = 0;
                        GPU.lineMode = 3;
                    }
                    break;

                // VRAM read mode, scanline active
                case 3:

                        if (GPU.modeClock >= 172)
                        {
                            // Enter hblank
                            GPU.modeClock = 0;
                            GPU.lineMode = 0;


                            GPU.renderScan();
                            UInt32 color;
                            UInt32* pixels = (UInt32*)display.Pixels;
                            for (int i = 0; i < 160; i++)
                            {
                                color = (UInt32)(pal[lineBuffer[i]].ToArgb());
                                pixels[(curLine * display.Width) + i] = color;
                            }
                        }

                    break;

                // HBlank
                // After the last HBlank, push the framebuffer to the display
                case 0:
                    if (GPU.modeClock >= 51)
                    {
                        // End of hblank for last scanline; render screen
                        if (GPU.curLine == 143)
                        {
                            // Enter VBlank
                            GPU.lineMode = 1;

                            display.Update();

                        }
                        //display.Invoke(new UpdateDisplayDelegate(updateDisplay));

                        else
                        {
                            GPU.lineMode = 2;
                        }

                        GPU.curLine++;
                        GPU.curScan += 640;
                        GPU.modeClock = 0;
                    }
                    break;

                // VBlank (10 lines)
                case 1:
                    if (GPU.modeClock >= 114)
                    {
                        GPU.modeClock = 0;
                        GPU.curLine++;

                        if (GPU.curLine > 153)
                        {
                            // Restart scanning modes
                            GPU.lineMode = 2;
                            GPU.curLine = 0;
                            GPU.curScan = 0;
                        }
                    }
                    break;
            }
        }

        public static byte readByte(UInt16 addr)
        {
            switch (addr)
            {
                // LCD Control
                case 0xFF40:
                    int value = GPU.LcdEnable ? 0x80 : 0x0;
                    value += GPU.WindowTileMapSelect ? 0x40 : 0x0;
                    value += GPU.WindowEnable ? 0x20 : 0x0;
                    value += GPU.TileDataSelect ? 0x10 : 0x0;
                    value += GPU.BgTileMapSelect ? 0x08 : 0x0;
                    value += GPU.ObjSize ? 0x04 : 0x0;
                    value += GPU.ObjEnable ? 0x02 : 0x0;
                    value += GPU.BgEnable ? 0x01 : 0x0;
                    return (byte)value;

                // LCD Status
                case 0xFF41:
                    return (byte)((GPU.curLine == GPU.raster ? 4 : 0) | GPU.lineMode);

                // Scroll Y
                case 0xFF42:
                    return (byte)GPU.yScrl;

                // Scroll X
                case 0xFF43:
                    return (byte)GPU.xScrl;

                // Current scanline
                case 0xFF44:
                    return (byte)GPU.curLine;

                case 0xFF45:
                    return (byte)GPU.raster;

                case 0xFF47:
                    return 1;
            }
            return 0;
        }

        public static void writeByte(UInt16 addr, byte val)
        {
            switch (addr)
            {
                // LCD Control
                case 0xFF40:
                    GPU.LcdEnable = ((val >> 7) & 1) == 1;
                    GPU.WindowTileMapSelect = ((val >> 6) & 1) == 1;
                    GPU.WindowEnable = ((val >> 5) & 1) == 1;
                    GPU.TileDataSelect = ((val >> 4) & 1) == 1;
                    GPU.BgTileMapSelect = ((val >> 3) & 1) == 1;
                    GPU.ObjSize = ((val >> 2) & 1) == 1;
                    GPU.ObjEnable = ((val >> 1) & 1) == 1;
                    GPU.BgEnable = (val & 1) == 1;
                    break;

                // Scroll Y
                case 0xFF42:
                    GPU.yScrl = val;
                    break;

                // Scroll X
                case 0xFF43:
                    GPU.xScrl = val;
                    break;

                case 0xFF45:
                    GPU.raster = val;
                    break;

                // Background palette
                case 0xFF47:
                    for (var i = 0; i < 4; i++)
                    {
                        switch ((val >> (i * 2)) & 3)
                        {
                            case 0: GPU.pal[i] = System.Drawing.Color.White; break;
                            case 1: GPU.pal[i] = System.Drawing.Color.LightGray; break;
                            case 2: GPU.pal[i] = System.Drawing.Color.DarkGray; break;
                            case 3: GPU.pal[i] = System.Drawing.Color.Black; break;
                        }
                    }
                    break;
            }
        }

        public static void renderScan()
        {
            // VRAM offset for the tile map
            UInt16 mapOffs = (UInt16)(curLine + yScrl);

            mapOffs &= 0xFF;
            mapOffs >>= 3;
            mapOffs <<= 5;

            UInt16 lineOffs = (UInt16)((xScrl >> 3) & 31);

            UInt16 index = BgTileMapSelect ? MMU.readByte((UInt16)((mapOffs + lineOffs) + 0x8800)) : MMU.readByte((UInt16)((mapOffs + lineOffs) + 0x8800));

            // Where in the tile to start
            byte x = (byte)(xScrl & 7);
            byte y = (byte)((curLine + yScrl) & 7);

            if (!TileDataSelect)
            {
                if (index < 128) index += 128;
                else index -= 128;
            }

            for (int i = 0; i < 160; i++)
            {
                //get 16-bit tile data
                UInt16 tile = TileDataSelect ? MMU.readWord((UInt16)((index << 1) + 0x8000)) : MMU.readWord((UInt16)((index << 1) + 0x8800));

                //mask all but 0xnn where n = 'pixel number'
                //then add the 2 bits
                byte a = (byte)(((tile & 0xFF) & (1 << x)) >> x);
                byte b = (byte)(((tile >> 8) & (1 << x)) >> x);
                byte c = (byte)(a + (b >> 1));


                // Write to linebuffer
                GPU.lineBuffer[i] = c;

                // Increment x pixel within tile
                x++;

                if (x == 8)
                {
                    //Reset x pixel within tile, get new tile data
                    x = 0;
                    lineOffs = (UInt16)((lineOffs + 1) & 31);
                    index = BgTileMapSelect ? vram[(mapOffs + lineOffs) + 0x1C00] : vram[(mapOffs + lineOffs) + 0x1800];
                }
            }
        }


    }
}