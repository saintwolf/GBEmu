using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmu
{
    static  class MMU
    {
        // Flag to indicate the bios is mapped
        // BIOS is unmapped with the first instruction above 0x00FF
        private static bool inBios = true;

        // Memory regions
        private static byte[] bios;
        private static byte[] rom;
        private static byte[] wram;
        private static byte[] eram;
        private static byte[] zram;

        public enum HardwareRegisters
        {
            // Name, Address, Usage
            P1 = 0xFF00,    // Joypad information
            SB = 0xFF01,    // Serial Transfer Data
            SC = 0xFF02,    // Serial I/O Control
            DIV = 0xFF04,    // Timer Divider
            TIMA = 0xFF05,    // Timer Counter
            TMA = 0xFF06,    // Timer Modulo
            TAC = 0xFF07,    // Timer Control
            IF = 0xFF0F,    // Interrupt Flag

            NR10 = 0xFF10,    // Sound Mode 1, Sweep
            NR11 = 0xFF11,    // Sound Mode 1, Sound length/Wave pattern duty
            NR12 = 0xFF12,    // Sound Mode 1, Envelope
            NR13 = 0xFF13,    // Sound Mode 1, Frequency Low
            NR14 = 0xFF14,    // Sound Mode 1, Frequency High


            NR21 = 0xFF16,    // Sound Mode 2, Sound length/Wave pattern duty
            NR22 = 0xFF17,    // Sound Mode 2, Envelope
            NR23 = 0xFF18,    // Sound Mode 2, Frequency Low
            NR24 = 0xFF19,    // Sound Mode 2, Frequency High

            NR30 = 0xFF1A,    // Sound Mode 3, Sound on/off
            NR31 = 0xFF1B,    // Sound Mode 3, Sound length
            NR32 = 0xFF1C,    // Sound Mode 3, Select output level
            NR33 = 0xFF1D,    // Sound Mode 3, Frequency Low
            NR34 = 0xFF1E,    // Sound Mode 3, Frequency High

            NR41 = 0xFF20,    // Sound Mode 4, Sound length
            NR42 = 0xFF21,    // Sound Mode 4, Envelope
            NR43 = 0xFF22,    // Sound Mode 4, Polynomial counter
            NR44 = 0xFF23,    // Sound Mode 4, Counter/Consecutive, Initial

            NR50 = 0xFF24,    // Sound Mode 5, Channel Control, On/Off, Volume
            NR51 = 0xFF25,    // Sound Mode 5, Select of sound output terminal
            NR52 = 0xFF26,    // Sound Mode 5, Sound On/Off

            WAV00 = 0xFF30,    // Wave pattern RAM  16bytes
            WAV01 = 0xFF31,
            WAV02 = 0xFF32,
            WAV03 = 0xFF33,
            WAV04 = 0xFF34,
            WAV05 = 0xFF35,
            WAV06 = 0xFF36,
            WAV07 = 0xFF37,
            WAV08 = 0xFF38,
            WAV09 = 0xFF39,
            WAV10 = 0xFF3A,
            WAV11 = 0xFF3B,
            WAV12 = 0xFF3C,
            WAV13 = 0xFF3D,
            WAV14 = 0xFF3E,
            WAV15 = 0xFF3F,

            LCDC = 0xFF40,    // LCD Control
            STAT = 0xFF41,    // LCD Status
            SCY = 0xFF42,    // Scroll Screen Y
            SCX = 0xFF43,    // Scroll Screen X
            LY = 0xFF44,     // LCDC Y-Coord
            LYC = 0xFF45,     // LY Compare
            DMA = 0xFF46,     // DMA Transfer
            BGP = 0xFF47,     // Background Palette Data
            OBP0 = 0xFF48,    // Object Palette 0 Data
            OBP1 = 0xFF49,    // Object Palette 1 Data
            WY = 0xFF4A,    // Window Y Position
            WX = 0xFF4B,    // Window X Position

            // Some GBC only registers, ignored here (at least now...)

            IE = 0xFFFF      // Interrupt enable 
        }

        static MMU()
        {
            MMU.bios = new byte[] {
    0x31, 0xFE, 0xFF, 0xAF, 0x21, 0xFF, 0x9F, 0x32, 0xCB, 0x7C, 0x20, 0xFB, 0x21, 0x26, 0xFF, 0x0E,
    0x11, 0x3E, 0x80, 0x32, 0xE2, 0x0C, 0x3E, 0xF3, 0xE2, 0x32, 0x3E, 0x77, 0x77, 0x3E, 0xFC, 0xE0,
    0x47, 0x11, 0x04, 0x01, 0x21, 0x10, 0x80, 0x1A, 0xCD, 0x95, 0x00, 0xCD, 0x96, 0x00, 0x13, 0x7B,
    0xFE, 0x34, 0x20, 0xF3, 0x11, 0xD8, 0x00, 0x06, 0x08, 0x1A, 0x13, 0x22, 0x23, 0x05, 0x20, 0xF9,
    0x3E, 0x19, 0xEA, 0x10, 0x99, 0x21, 0x2F, 0x99, 0x0E, 0x0C, 0x3D, 0x28, 0x08, 0x32, 0x0D, 0x20,
    0xF9, 0x2E, 0x0F, 0x18, 0xF3, 0x67, 0x3E, 0x64, 0x57, 0xE0, 0x42, 0x3E, 0x91, 0xE0, 0x40, 0x04,
    0x1E, 0x02, 0x0E, 0x0C, 0xF0, 0x44, 0xFE, 0x90, 0x20, 0xFA, 0x0D, 0x20, 0xF7, 0x1D, 0x20, 0xF2,
    0x0E, 0x13, 0x24, 0x7C, 0x1E, 0x83, 0xFE, 0x62, 0x28, 0x06, 0x1E, 0xC1, 0xFE, 0x64, 0x20, 0x06,
    0x7B, 0xE2, 0x0C, 0x3E, 0x87, 0xF2, 0xF0, 0x42, 0x90, 0xE0, 0x42, 0x15, 0x20, 0xD2, 0x05, 0x20,
    0x4F, 0x16, 0x20, 0x18, 0xCB, 0x4F, 0x06, 0x04, 0xC5, 0xCB, 0x11, 0x17, 0xC1, 0xCB, 0x11, 0x17,
    0x05, 0x20, 0xF5, 0x22, 0x23, 0x22, 0x23, 0xC9, 0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B,
    0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D, 0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E,
    0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99, 0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC,
    0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E, 0x3c, 0x42, 0xB9, 0xA5, 0xB9, 0xA5, 0x42, 0x4C,
    0x21, 0x04, 0x01, 0x11, 0xA8, 0x00, 0x1A, 0x13, 0xBE, 0x20, 0xFE, 0x23, 0x7D, 0xFE, 0x34, 0x20,
    0xF5, 0x06, 0x19, 0x78, 0x86, 0x23, 0x05, 0x20, 0xFB, 0x86, 0x20, 0xFE, 0x3E, 0x01, 0xE0, 0x50
            };
            MMU.rom = new byte[0x8000];
            MMU.wram = new byte[0x2000];
            MMU.eram = new byte[0x2000];
            MMU.zram = new byte[0xFF];

            MMU.reset();
        }

        public static void load(String pathToRom)
        {
            MMU.rom = System.IO.File.ReadAllBytes(pathToRom);
        }

        public static void reset()
        {
            // CLEAR ALL THE RAMS
            if (MMU.rom == null)
            {
            }
            for (int i=0; i < 0x2000; i++)
            {
                
                MMU.wram[i] = 0;
                MMU.eram[i] = 0;
            }
            for (int i = 0; i < 127; i++)
            {
                MMU.zram[i] = 0;
            }
            MMU.inBios = true;
        }

        public static byte readByte(UInt16 addr)
        {
            switch (addr & 0xF000)
            {
                // BIOS (256b)/ROM0
                case 0x0000:
                    if (MMU.inBios)
                    {
                        if (addr < 0x0100)
                        {
                            return MMU.bios[addr];
                        }
                        else if (addr == 0x0100)
                        {
                            MMU.inBios = false;
                        }
                    }
                    return MMU.rom[addr];

                // ROM 0 (16k)
                case 0x1000: goto case 0x3000;
                case 0x2000: goto case 0x3000;
                case 0x3000:
                    return MMU.rom[addr];
               
                // ROM 1 (switchable) (16k)
                case 0x4000: goto case 0x7000;
                case 0x5000: goto case 0x7000;
                case 0x6000: goto case 0x7000;
                case 0x7000:
                    return MMU.rom[addr];

                // Video/Graphics RAM
                case 0x8000: goto case 0x9000;
                case 0x9000:
                    return GPU.vram[addr & 0x1FFF];

                // External RAM
                case 0xA000: goto case 0xB000;
                case 0xB000:
                    return eram[addr & 0x1FFF];

                // Working RAM
                case 0xC000: goto case 0xD000;
                case 0xD000:
                    return wram[addr & 0x1FFF];

                // Working RAM shadow
                case 0xE000:
                    return wram[addr & 0x1FFF];

                // Working RAM shadow, graphics sprite info, memory mapped IO and Zero-page RAM
                case 0xF000:
                    switch (addr & 0x0F00)
                    {
                        // Working RAM shadow
                        case 0x100: goto case 0xD00;
                        case 0x200: goto case 0xD00;
                        case 0x300: goto case 0xD00;
                        case 0x400: goto case 0xD00;
                        case 0x500: goto case 0xD00;
                        case 0x600: goto case 0xD00;
                        case 0x700: goto case 0xD00;
                        case 0x800: goto case 0xD00;
                        case 0x900: goto case 0xD00;
                        case 0xA00: goto case 0xD00;
                        case 0xB00: goto case 0xD00;
                        case 0xC00: goto case 0xD00;
                        case 0xD00:
                            return wram[addr & 0x1FFF];

                        // Graphics sprite information, object attribute memory
                        // 160 bytes long, remaining bytes read as 0
                        case 0xE00:
                            if (addr < 0xFEA0)
                            {
                                return GPU.oam[addr - 0xFE00];
                            }
                            else
                            {
                                return 0;
                            }

                        // Zero page / Memory mapped IO
                        case 0xF00:
                            if (addr >= 0xFF80)
                            {
                                return MMU.zram[addr & 0x7F];
                            }
                            else 
                            {
                                // Memory Mapped IO
                                switch (addr & 0x00F0)
                                {
                                    // GPU (64 registers)
                                    case 0x40: goto case 0x70;
                                    case 0x50: goto case 0x70;
                                    case 0x60: goto case 0x70;
                                    case 0x70:
                                        return GPU.readByte(addr);
                                }
                                return 0;
                            }
                        
                        // No match found, return 0
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }
        }

        public static UInt16 readWord(UInt16 addr)
        {
            return (UInt16)(MMU.readByte(addr) + (MMU.readByte((UInt16)(addr + 1)) << 8));
        }

        public static void writeByte(UInt16 addr, byte val)
        {
            switch (addr & 0xF000)
            {
                // BIOS (256b)/ROM0
                case 0x0000:
                    if (MMU.inBios && addr <= 0x0100) return;
                    break;
                // Fall through, you don't write to the ROM... YET
                case 0x1000: goto case 0x3000;
                case 0x2000: goto case 0x3000;
                case 0x3000:
                    break;

                // ROM 1 (switchable) (16k)
                case 0x4000: goto case 0x7000;
                case 0x5000: goto case 0x7000;
                case 0x6000: goto case 0x7000;
                case 0x7000:
                    break;

                // Video/Graphics RAM
                case 0x8000: goto case 0x9000;
                case 0x9000:
                    GPU.vram[addr & 0x1FFF] = val;
                    break;

                // External RAM
                case 0xA000: goto case 0xB000;
                case 0xB000:
                    MMU.eram[addr & 0x1FFF] = val;
                    break;

                // Working RAM
                case 0xC000: goto case 0xD000;
                case 0xD000:
                    MMU.wram[addr & 0x1FFF] = val;
                    break;

                // Working RAM shadow
                case 0xE000:
                    MMU.wram[addr & 0x1FFF] = val;
                    break;

                // Working RAM shadow, graphics sprite info, memory mapped IO and Zero-page RAM
                case 0xF000:
                    switch (addr & 0x0F00)
                    {
                        // Working RAM shadow
                        case 0x100: goto case 0xD00;
                        case 0x200: goto case 0xD00;
                        case 0x300: goto case 0xD00;
                        case 0x400: goto case 0xD00;
                        case 0x500: goto case 0xD00;
                        case 0x600: goto case 0xD00;
                        case 0x700: goto case 0xD00;
                        case 0x800: goto case 0xD00;
                        case 0x900: goto case 0xD00;
                        case 0xA00: goto case 0xD00;
                        case 0xB00: goto case 0xD00;
                        case 0xC00: goto case 0xD00;
                        case 0xD00:
                            MMU.wram[addr & 0x1FFF] = val;
                            break;

                        // Graphics sprite information, object attribute memory
                        // 160 bytes long, remaining bytes read as 0
                        case 0xE00:
                            if (addr < 0xFEA0)
                            {
                                GPU.oam[addr - 0xFE00] = val;
                            }
                            break;

                        // Zero page / Memory mapped IO
                        case 0xF00:
                            if (addr >= 0xFF80)
                            {
                                MMU.zram[addr & 0x7F] = val;
                                break;
                            }
                            else
                            {
                                // Memory Mapped IO
                                switch (addr & 0x00F0)
                                {
                                    // GPU (64 registers)
                                    case 0x40: goto case 0x70;
                                    case 0x50: goto case 0x70; 
                                    case 0x60: goto case 0x70;
                                    case 0x70:
                                        GPU.writeByte(addr, val);
                                        break;
                                }
                                break;
                            }

                        // No match found, return 0
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        public static void writeWord(UInt16 addr, UInt16 val)
        {
            MMU.writeByte(addr, (byte)(val & 0x00FF));
            MMU.writeByte((UInt16)(addr + 1), (byte)(val >> 8));
        }
    }
}
