﻿using System;
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
        private static Surface display = null;

        public static Bitmap fb = new Bitmap(160, 144);

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

        public static byte[][][] tileset;

        static GPU()
        {
            scanRow = new int[161];
            for (int i = 0; i < 161; i++) GPU.scanRow[i] = 0;

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
            GPU.tileset = null;
            GPU.tileset = new byte[512][][];
            for (int t = 0; t < 512; t++)
            {
                tileset[t] = new byte[8][];
                for (int y = 0; y < 8; y++)
                {
                    tileset[t][y] = new byte[8];
                    for (int x = 0; x < 8; x++)
                    {
                        tileset[t][y][x] = 0;
                    }
                }
            }
            for (int i = 0; i < 384; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        GPU.tileset[i][j][k] = 0;
                    }
                }
            }

            if (display != null)
            {
                Color[,] colorBlock = new Color[1, 1];
                colorBlock[0, 0] = Color.Green;

                // Set the pixels in the framebuffer to white and push to screen
                for (int r = 0; r < 144; r++)
                {
                    for (int c = 0; c < 160; c++)
                    {
                        
                                display.SetPixels(new Point(c, r), colorBlock);
                                //fb.SetPixel(c, r, Color.Blue);
                    }
                }
                //displa0y.BeginInvoke(new Action(() => updateDisplay() ));
                display.Update();
            }

            GPU.curLine = 0;
            GPU.curScan = 0;
            GPU.lineMode = 2;
            GPU.modeClock = 0;
            GPU.yScrl = 0;
            GPU.xScrl = 0;
            GPU.raster = 0;

        }

        public static void step()
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

        public static Bitmap Update()
        {
            using (Graphics g = Graphics.FromImage(fb))
            {
                if (LcdEnable)
                {
                    //Graphics g = Graphics.FromImage(fb);
                    if (BgEnable)
                        g.DrawImage(RenderBg(), new Point(0, 0));
                }
            }
            return fb;
        }

        // Takes a value written to VRAM and updates the internal tile data set
        public static void updateTile(UInt16 addr, byte val)
        {
            addr &= 0x1FFF;

            var saddr = addr;
            if ((addr & 1) != 0) { saddr--; addr--; }
            var tile = (addr >> 4) & 511;
            var y = (addr >> 1) & 7;
            int sx;
            for (var x = 0; x < 8; x++)
            {
                sx = 1 << (7 - x);
                GPU.tileset[tile][y][x] = (byte)(((GPU.vram[saddr] & sx) != 0 ? 1 : 0) | ((GPU.vram[saddr + 1] & sx) != 0 ? 2 : 0));
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
                            case 1: GPU.pal[i] = System.Drawing.Color.Black; break;
                            case 2: GPU.pal[i] = System.Drawing.Color.Black; break;
                            case 3: GPU.pal[i] = System.Drawing.Color.Black; break;
                        }
                    }
                    break;
            }
        }

        private static Bitmap GetTile(int addr, bool transparent = false)
        {
            using (Bitmap bmp = new Bitmap(8, 8))
            {
                int y = 0;

                for (int i = 0; i < 16; i += 2)
                {
                    byte b1, b2;
                    b1 = vram[addr + 2 * y];
                    b2 = vram[addr + 1 + 2 * y];

                    for (int j = 0; j < 8; j++)
                    {
                        int color = (((b2 >> (7 - j)) & 1) << 1) + ((b1 >> (7 - j)) & 1);
                        if (color == 0 && transparent)
                        {
                            bmp.SetPixel(j, y, pal[color]);
                        }
                        else
                        {
                            bmp.SetPixel(j, y, pal[color]);
                        }
                    }
                    y++;
                }

                return (Bitmap)bmp.Clone();
            }
        }

        private static Bitmap RenderBg()
        {
            using (Bitmap map = new Bitmap(255, 255))
            {
                using (Bitmap bg = new Bitmap(160, 144))
                {
                    using (Graphics g = Graphics.FromImage(map))
                    {
                        // Generate map
                        int tmap = BgTileMapSelect ? 0x1C00 : 0x1800;
                        int x = 0, y = 0;
                        for (int i = 0; i < 32 * 32; i++)
                        {
                            if (x == 32)
                            {
                                x = 0;
                                y++;
                            }
                            if (!TileDataSelect)
                            {
                                int value = vram[tmap + i];
                                int addr;
                                if (value > 0x7F) // It's signed!
                                {
                                    value -= 256;
                                }
                                addr = 0x1000 + (value * 16);

                                using (Bitmap tile = GetTile(addr))
                                {
                                    g.DrawImage(tile, new Point(x * 8, y * 8));
                                    //display.Blit(new Surface(GetTile(vram[tmap + i] * 16)), new Point(x * 8, y * 8));
                                }
                            }
                            else
                            {
                                using (Bitmap tile = GetTile(vram[tmap + i] * 16))
                                {
                                    g.DrawImage(tile, new Point(x * 8, y * 8));
                                    //display.Blit(new Surface(GetTile(vram[tmap + i] * 16)), new Point(x * 8, y * 8));
                                }
                            }
                            x++;
                        }

                        // Scroll
                        for (y = 0; y < 144; y++)
                        {
                            for (x = 0; x < 160; x++)
                            {
                                int srcx = xScrl + x;
                                int srcy = yScrl + y;

                                if (srcx >= 255)
                                    srcx = srcx - 255;

                                if (srcy >= 255)
                                    srcy = srcy - 255;

                                bg.SetPixel(x, y, map.GetPixel(srcx, srcy));
                            }
                        }

                        return (Bitmap)bg.Clone();
                    }
                }
            }
        }

        private Bitmap RenderWindow()
        {
            using (Bitmap map = new Bitmap(255, 255))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    // Generate map
                    int tmap = WindowTileMapSelect ? 0x1C00 : 0x1800;
                    int x = 0, y = 0;
                    for (int i = 0; i < 32 * 32; i++)
                    {
                        if (x == 32)
                        {
                            x = 0;
                            y++;
                        }
                        if (!TileDataSelect)
                        {
                            int value = vram[tmap + i];
                            int addr;
                            if (value > 0x7F) // It's signed!
                            {
                                value -= 256;
                            }
                            addr = 0x1000 + (value * 16);
                            g.DrawImage(GetTile(addr), new Point(x * 8, y * 8));
                        }
                        else
                        {
                            g.DrawImage(GetTile(vram[tmap + i] * 16), new Point(x * 8, y * 8));
                        }
                        x++;
                    }
                    return (Bitmap)map.Clone();
                }
            }
        }

        private Bitmap RenderObj()
        {
            using (Bitmap obj = new Bitmap(160, 144))
            {
                using (Graphics g = Graphics.FromImage(obj))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        int addr = i * 4;
                        int x = oam[addr + 1];
                        int y = oam[addr];
                        if (x > 0 && x < 168 && y > 0 && y < 160) // Sprite is visible
                        {
                            obj.SetPixel(x, y, Color.Red);
                            //g.DrawImage(GetTile((OAM[addr + 2] * 16) & 0xFE, OBP0), new Point(x, y));
                        }
                    }
                    return (Bitmap)obj.Clone();
                }
            }
        }


    }
}