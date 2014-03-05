using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBEmu
{
    class Z80
    {

        public struct Clock
        {
            public static byte m;
            public static byte t;
        }

        public struct Registers
        {
            public static byte a, b, c, d, e, h, l, f, m, t;    // 8-bit (1 byte) registers
            public static UInt16 pc, sp;                  // 16-bit (2 bytes) registers
        }

        static Z80()
        {
            Z80.reset();
        }

        public static void reset()
        {
            // Set all registers to zero
            Z80.Registers.a = 0;
            Z80.Registers.b = 0;
            Z80.Registers.c = 0;
            Z80.Registers.d = 0;
            Z80.Registers.e = 0;
            Z80.Registers.h = 0;
            Z80.Registers.l = 0;
            Z80.Registers.f = 0;
            Z80.Registers.m = 0;
            Z80.Registers.t = 0;
            Z80.Registers.pc = 0;
            Z80.Registers.sp = 0;

            // Set clock to zero
            Z80.Clock.m = 0;
            Z80.Clock.t = 0;

            Ops.ClearFlags();
        }

        public static void step()
        {
            Z80.Commands[MMU.readByte(Z80.Registers.pc++)]();
            Z80.Registers.pc &= 65535;
            Z80.Clock.m += Z80.Registers.m;
            Z80.Clock.t += Z80.Registers.t;
        }



        public static Func<bool>[] Commands = {
                                           // 0x00
                                           Z80.Ops.NOP,
                                           Z80.Ops.LDBCnn,
                                           Z80.Ops.LDBCmr_a,
                                           Z80.Ops.INCBC,
                                           Z80.Ops.INCr_b,
                                           Z80.Ops.DECr_b,
                                           Z80.Ops.LDrn_b,
                                           Z80.Ops.RLCA,
                                           Z80.Ops.LDNNmr_sp,
                                           Z80.Ops.ADDHLBC,
                                           Z80.Ops.LDrBCm_a,
                                           Z80.Ops.DECBC,
                                           Z80.Ops.INCr_c,
                                           Z80.Ops.DECr_c,
                                           Z80.Ops.LDrn_c,
                                           Z80.Ops.RRCA,

                                           // 0x10
                                           Z80.Ops.STOP,
                                           Z80.Ops.LDDEnn,
                                           Z80.Ops.LDDEmr_a,
                                           Z80.Ops.INCDE,
                                           Z80.Ops.INCr_d,
                                           Z80.Ops.DECr_d,
                                           Z80.Ops.LDrn_d,
                                           Z80.Ops.RLA,
                                           Z80.Ops.JRn,
                                           Z80.Ops.ADDHLDE,
                                           Z80.Ops.LDrDEm_a,
                                           Z80.Ops.DECDE,
                                           Z80.Ops.INCr_e,
                                           Z80.Ops.DECr_e,
                                           Z80.Ops.LDrn_e,
                                           Z80.Ops.RRA,

                                           // 0x20
                                           Z80.Ops.JRNZn,
                                           Z80.Ops.LDHLnn,
                                           Z80.Ops.LDIHLmr_a,
                                           Z80.Ops.INCHL,
                                           Z80.Ops.INCr_h,
                                           Z80.Ops.DECr_h,
                                           Z80.Ops.LDrn_h,
                                           Z80.Ops.DAA,
                                           Z80.Ops.JRZn,
                                           Z80.Ops.ADDHLHL,
                                           Z80.Ops.LDIrHLm_a,
                                           Z80.Ops.DECHL,
                                           Z80.Ops.INCr_l,
                                           Z80.Ops.DECr_l,
                                           Z80.Ops.LDrn_l,
                                           Z80.Ops.CPL,

                                           // 0x30
                                           Z80.Ops.JRNCn,
                                           Z80.Ops.LDSPnn,
                                           Z80.Ops.LDDHLmr_a,
                                           Z80.Ops.INCSP,
                                           Z80.Ops.INCHLm,
                                           Z80.Ops.DECHLm,
                                           Z80.Ops.LDHLmn,
                                           Z80.Ops.SCF,
                                           Z80.Ops.JRCn,
                                           Z80.Ops.ADDHLSP,
                                           Z80.Ops.LDDrHLm_a,
                                           Z80.Ops.DECSP,
                                           Z80.Ops.INCr_a,
                                           Z80.Ops.DECr_a,
                                           Z80.Ops.LDrn_a,
                                           Z80.Ops.CCF,

                                           // 0x40
                                           Z80.Ops.LDrr_bb,
                                           Z80.Ops.LDrr_bc,
                                           Z80.Ops.LDrr_bd,
                                           Z80.Ops.LDrr_be,
                                           Z80.Ops.LDrr_bh,
                                           Z80.Ops.LDrr_bl,
                                           Z80.Ops.LDrHLm_b,
                                           Z80.Ops.LDrr_ba,
                                           Z80.Ops.LDrr_cb,
                                           Z80.Ops.LDrr_cc,
                                           Z80.Ops.LDrr_cd,
                                           Z80.Ops.LDrr_ce,
                                           Z80.Ops.LDrr_ch,
                                           Z80.Ops.LDrr_cl,
                                           Z80.Ops.LDrHLm_c,
                                           Z80.Ops.LDrr_ca,

                                           // 0x50
                                           Z80.Ops.LDrr_db,
                                           Z80.Ops.LDrr_dc,
                                           Z80.Ops.LDrr_dd,
                                           Z80.Ops.LDrr_de,
                                           Z80.Ops.LDrr_dh,
                                           Z80.Ops.LDrr_dl,
                                           Z80.Ops.LDrHLm_d,
                                           Z80.Ops.LDrr_da,
                                           Z80.Ops.LDrr_eb,
                                           Z80.Ops.LDrr_ec,
                                           Z80.Ops.LDrr_ed,
                                           Z80.Ops.LDrr_ee,
                                           Z80.Ops.LDrr_eh,
                                           Z80.Ops.LDrr_el,
                                           Z80.Ops.LDrHLm_e,
                                           Z80.Ops.LDrr_ea,

                                           // 0x60
                                           Z80.Ops.LDrr_hb,
                                           Z80.Ops.LDrr_hc,
                                           Z80.Ops.LDrr_hd,
                                           Z80.Ops.LDrr_he,
                                           Z80.Ops.LDrr_hh,
                                           Z80.Ops.LDrr_hl,
                                           Z80.Ops.LDrHLm_h,
                                           Z80.Ops.LDrr_ha,
                                           Z80.Ops.LDrr_lb,
                                           Z80.Ops.LDrr_lc,
                                           Z80.Ops.LDrr_ld,
                                           Z80.Ops.LDrr_le,
                                           Z80.Ops.LDrr_lh,
                                           Z80.Ops.LDrr_ll,
                                           Z80.Ops.LDrHLm_l,
                                           Z80.Ops.LDrr_la,

                                           // 0x70
                                           Z80.Ops.LDHLmr_b,
                                           Z80.Ops.LDHLmr_c,
                                           Z80.Ops.LDHLmr_d,
                                           Z80.Ops.LDHLmr_e,
                                           Z80.Ops.LDHLmr_h,
                                           Z80.Ops.LDHLmr_l,
                                           Z80.Ops.HALT,
                                           Z80.Ops.LDHLmr_a,
                                           Z80.Ops.LDrr_ab,
                                           Z80.Ops.LDrr_ac,
                                           Z80.Ops.LDrr_ad,
                                           Z80.Ops.LDrr_ae,
                                           Z80.Ops.LDrr_ah,
                                           Z80.Ops.LDrr_al,
                                           Z80.Ops.LDrHLm_a,
                                           Z80.Ops.LDrr_aa,

                                           // 0x80
                                           Z80.Ops.ADDr_b,
                                           Z80.Ops.ADDr_c,
                                           Z80.Ops.ADDr_d,
                                           Z80.Ops.ADDr_e,
                                           Z80.Ops.ADDr_h,
                                           Z80.Ops.ADDr_l,
                                           Z80.Ops.ADDHLm,
                                           Z80.Ops.ADDr_a,
                                           Z80.Ops.ADCr_b,
                                           Z80.Ops.ADCr_c,
                                           Z80.Ops.ADCr_d,
                                           Z80.Ops.ADCr_e,
                                           Z80.Ops.ADCr_h,
                                           Z80.Ops.ADCr_l,
                                           Z80.Ops.ADCHLm,
                                           Z80.Ops.ADCr_a,

                                           // 0x90
                                           Z80.Ops.SUBr_b,
                                           Z80.Ops.SUBr_c,
                                           Z80.Ops.SUBr_d,
                                           Z80.Ops.SUBr_e,
                                           Z80.Ops.SUBr_h,
                                           Z80.Ops.SUBr_l,
                                           Z80.Ops.SUBHLm,
                                           Z80.Ops.SUBr_a,
                                           Z80.Ops.SBCr_b,
                                           Z80.Ops.SBCr_c,
                                           Z80.Ops.SBCr_d,
                                           Z80.Ops.SBCr_e,
                                           Z80.Ops.SBCr_h,
                                           Z80.Ops.SBCr_l,
                                           Z80.Ops.SBCHLm,
                                           Z80.Ops.SBCr_a,

                                           // 0xA0
                                           Z80.Ops.ANDr_b,
                                           Z80.Ops.ANDr_c,
                                           Z80.Ops.ANDr_d,
                                           Z80.Ops.ANDr_e,
                                           Z80.Ops.ANDr_h,
                                           Z80.Ops.ANDr_l,
                                           Z80.Ops.ANDHLm,
                                           Z80.Ops.ANDr_a,
                                           Z80.Ops.XORr_b,
                                           Z80.Ops.XORr_c,
                                           Z80.Ops.XORr_d,
                                           Z80.Ops.XORr_e,
                                           Z80.Ops.XORr_h,
                                           Z80.Ops.XORr_l,
                                           Z80.Ops.XORHLm,
                                           Z80.Ops.XORr_a,

                                           // 0xB0
                                           Z80.Ops.ORr_b,
                                           Z80.Ops.ORr_c,
                                           Z80.Ops.ORr_d,
                                           Z80.Ops.ORr_e,
                                           Z80.Ops.ORr_h,
                                           Z80.Ops.ORr_l,
                                           Z80.Ops.ORHLm,
                                           Z80.Ops.ORr_a,
                                           Z80.Ops.CPr_b,
                                           Z80.Ops.CPr_c,
                                           Z80.Ops.CPr_d,
                                           Z80.Ops.CPr_e,
                                           Z80.Ops.CPr_h,
                                           Z80.Ops.CPr_l,
                                           Z80.Ops.CPHLm,
                                           Z80.Ops.CPr_a,

                                           // 0xC0
                                           Z80.Ops.RETNZ,
                                           Z80.Ops.POPBC,
                                           Z80.Ops.JPNZnn,
                                           Z80.Ops.JPnn,
                                           Z80.Ops.CALLNZnn,
                                           Z80.Ops.PUSHBC,
                                           Z80.Ops.ADDn,
                                           Z80.Ops.RST00,
                                           Z80.Ops.RETZ,
                                           Z80.Ops.RET,
                                           Z80.Ops.JPZnn,
                                           Z80.Ops.CB,
                                           Z80.Ops.CALLZnn,
                                           Z80.Ops.CALLnn,
                                           Z80.Ops.ADCn,
                                           Z80.Ops.RST08,
                                           
                                           // 0xD0
                                           Z80.Ops.RETNC,
                                           Z80.Ops.POPDE,
                                           Z80.Ops.JPNCnn,
                                           Z80.Ops.NOP,
                                           Z80.Ops.CALLNCnn,
                                           Z80.Ops.PUSHDE,
                                           Z80.Ops.SUBn,
                                           Z80.Ops.RST10,
                                           Z80.Ops.RETC,
                                           Z80.Ops.RETI,
                                           Z80.Ops.JPCnn,
                                           Z80.Ops.NOP,
                                           Z80.Ops.CALLCnn,
                                           Z80.Ops.NOP,
                                           Z80.Ops.SBCn,
                                           Z80.Ops.RST18,

                                           // 0xE0
                                           Z80.Ops.LDIONr_a,
                                           Z80.Ops.POPHL,
                                           Z80.Ops.LDIOCr_a,
                                           Z80.Ops.NOP,
                                           Z80.Ops.NOP,
                                           Z80.Ops.PUSHHL,
                                           Z80.Ops.ANDn,
                                           Z80.Ops.RST20,
                                           Z80.Ops.ADDSPn,
                                           Z80.Ops.JPHL,
                                           Z80.Ops.LDNNmr_a,
                                           Z80.Ops.NOP,
                                           Z80.Ops.NOP,
                                           Z80.Ops.NOP,
                                           Z80.Ops.XORn,
                                           Z80.Ops.RST28,

                                           // 0xF0
                                           Z80.Ops.LDrIOC_a,
                                           Z80.Ops.POPAF,
                                           Z80.Ops.NOP,
                                           Z80.Ops.DI,
                                           Z80.Ops.NOP,
                                           Z80.Ops.PUSHAF,
                                           Z80.Ops.ORn,
                                           Z80.Ops.RST30,
                                           Z80.Ops.LDHLSPn,
                                           Z80.Ops.LDSPHL,
                                           Z80.Ops.LDrNNm_a,
                                           Z80.Ops.EI,
                                           Z80.Ops.NOP,
                                           Z80.Ops.NOP,
                                           Z80.Ops.CPn,
                                           Z80.Ops.RST38
                                       };

        public static Func<bool>[] CBCommands = {
                                           // 0x00
                                           Z80.CBOps.RLCr_b,
                                           Z80.CBOps.RLCr_c,
                                           Z80.CBOps.RLCr_d,
                                           Z80.CBOps.RLCr_e,
                                           Z80.CBOps.RLCr_h,
                                           Z80.CBOps.RLCr_l,
                                           Z80.CBOps.RLCHLm,
                                           Z80.CBOps.RLCr_a,
                                           Z80.CBOps.RRCr_b,
                                           Z80.CBOps.RRCr_c,
                                           Z80.CBOps.RRCr_d,
                                           Z80.CBOps.RRCr_e,
                                           Z80.CBOps.RRCr_h,
                                           Z80.CBOps.RRCr_l,
                                           Z80.CBOps.RRCHLm,
                                           Z80.CBOps.RRCr_a,

                                           // 0x10
                                           Z80.CBOps.RLr_b,
                                           Z80.CBOps.RLr_c,
                                           Z80.CBOps.RLr_d,
                                           Z80.CBOps.RLr_e,
                                           Z80.CBOps.RLr_h,
                                           Z80.CBOps.RLr_l,
                                           Z80.CBOps.RLHLm,
                                           Z80.CBOps.RLr_a,
                                           Z80.CBOps.RRr_b,
                                           Z80.CBOps.RRr_c,
                                           Z80.CBOps.RRr_d,
                                           Z80.CBOps.RRr_e,
                                           Z80.CBOps.RRr_h,
                                           Z80.CBOps.RRr_l,
                                           Z80.CBOps.RRHLm,
                                           Z80.CBOps.RRr_a,

                                           // 0x20
                                           Z80.CBOps.SLAr_b,
                                           Z80.CBOps.SLAr_c,
                                           Z80.CBOps.SLAr_d,
                                           Z80.CBOps.SLAr_e,
                                           Z80.CBOps.SLAr_h,
                                           Z80.CBOps.SLAr_l,
                                           Z80.CBOps.SLAHLm,
                                           Z80.CBOps.SLAr_a,
                                           Z80.CBOps.SRAr_b,
                                           Z80.CBOps.SRAr_c,
                                           Z80.CBOps.SRAr_d,
                                           Z80.CBOps.SRAr_e,
                                           Z80.CBOps.SRAr_h,
                                           Z80.CBOps.SRAr_l,
                                           Z80.CBOps.SRAHLm,
                                           Z80.CBOps.SRAr_a,

                                           // 0x30
                                           Z80.CBOps.SWAPr_b,
                                           Z80.CBOps.SWAPr_c,
                                           Z80.CBOps.SWAPr_d,
                                           Z80.CBOps.SWAPr_e,
                                           Z80.CBOps.SWAPr_h,
                                           Z80.CBOps.SWAPr_l,
                                           Z80.CBOps.SWAPHLm,
                                           Z80.CBOps.SWAPr_a,
                                           Z80.CBOps.SRLr_b,
                                           Z80.CBOps.SRLr_c,
                                           Z80.CBOps.SRLr_d,
                                           Z80.CBOps.SRLr_e,
                                           Z80.CBOps.SRLr_h,
                                           Z80.CBOps.SRLr_l,
                                           Z80.CBOps.SRLHLm,
                                           Z80.CBOps.SRLr_a,

                                           // 0x40
                                           Z80.CBOps.BIT0r_b,
                                           Z80.CBOps.BIT0r_c,
                                           Z80.CBOps.BIT0r_d,
                                           Z80.CBOps.BIT0r_e,
                                           Z80.CBOps.BIT0r_h,
                                           Z80.CBOps.BIT0r_l,
                                           Z80.CBOps.BIT0HLm,
                                           Z80.CBOps.BIT0r_a,
                                           Z80.CBOps.BIT1r_b,
                                           Z80.CBOps.BIT1r_c,
                                           Z80.CBOps.BIT1r_d,
                                           Z80.CBOps.BIT1r_e,
                                           Z80.CBOps.BIT1r_h,
                                           Z80.CBOps.BIT1r_l,
                                           Z80.CBOps.BIT1HLm,
                                           Z80.CBOps.BIT1r_a,

                                           // 0x50
                                           Z80.CBOps.BIT2r_b,
                                           Z80.CBOps.BIT2r_c,
                                           Z80.CBOps.BIT2r_d,
                                           Z80.CBOps.BIT2r_e,
                                           Z80.CBOps.BIT2r_h,
                                           Z80.CBOps.BIT2r_l,
                                           Z80.CBOps.BIT2HLm,
                                           Z80.CBOps.BIT2r_a,
                                           Z80.CBOps.BIT3r_b,
                                           Z80.CBOps.BIT3r_c,
                                           Z80.CBOps.BIT3r_d,
                                           Z80.CBOps.BIT3r_e,
                                           Z80.CBOps.BIT3r_h,
                                           Z80.CBOps.BIT3r_l,
                                           Z80.CBOps.BIT3HLm,
                                           Z80.CBOps.BIT3r_a,

                                           // 0x60
                                           Z80.CBOps.BIT0r_b,
                                           Z80.CBOps.BIT4r_c,
                                           Z80.CBOps.BIT4r_d,
                                           Z80.CBOps.BIT4r_e,
                                           Z80.CBOps.BIT4r_h,
                                           Z80.CBOps.BIT4r_l,
                                           Z80.CBOps.BIT4HLm,
                                           Z80.CBOps.BIT4r_a,
                                           Z80.CBOps.BIT5r_b,
                                           Z80.CBOps.BIT5r_c,
                                           Z80.CBOps.BIT5r_d,
                                           Z80.CBOps.BIT5r_e,
                                           Z80.CBOps.BIT5r_h,
                                           Z80.CBOps.BIT5r_l,
                                           Z80.CBOps.BIT5HLm,
                                           Z80.CBOps.BIT5r_a,

                                           // 0x70
                                           Z80.CBOps.BIT6r_b,
                                           Z80.CBOps.BIT6r_c,
                                           Z80.CBOps.BIT6r_d,
                                           Z80.CBOps.BIT6r_e,
                                           Z80.CBOps.BIT6r_h,
                                           Z80.CBOps.BIT6r_l,
                                           Z80.CBOps.BIT6HLm,
                                           Z80.CBOps.BIT6r_a,
                                           Z80.CBOps.BIT7r_b,
                                           Z80.CBOps.BIT7r_c,
                                           Z80.CBOps.BIT7r_d,
                                           Z80.CBOps.BIT7r_e,
                                           Z80.CBOps.BIT7r_h,
                                           Z80.CBOps.BIT7r_l,
                                           Z80.CBOps.BIT7HLm,
                                           Z80.CBOps.BIT7r_a,

                                           // 0x80
                                           Z80.CBOps.RES0r_b,
                                           Z80.CBOps.RES0r_c,
                                           Z80.CBOps.RES0r_d,
                                           Z80.CBOps.RES0r_e,
                                           Z80.CBOps.RES0r_h,
                                           Z80.CBOps.RES0r_l,
                                           Z80.CBOps.RES0HLm,
                                           Z80.CBOps.RES0r_a,
                                           Z80.CBOps.RES1r_b,
                                           Z80.CBOps.RES1r_c,
                                           Z80.CBOps.RES1r_d,
                                           Z80.CBOps.RES1r_e,
                                           Z80.CBOps.RES1r_h,
                                           Z80.CBOps.RES1r_l,
                                           Z80.CBOps.RES1HLm,
                                           Z80.CBOps.RES1r_a,

                                           // 0x90
                                           Z80.CBOps.RES2r_b,
                                           Z80.CBOps.RES2r_c,
                                           Z80.CBOps.RES2r_d,
                                           Z80.CBOps.RES2r_e,
                                           Z80.CBOps.RES2r_h,
                                           Z80.CBOps.RES2r_l,
                                           Z80.CBOps.RES2HLm,
                                           Z80.CBOps.RES2r_a,
                                           Z80.CBOps.RES3r_b,
                                           Z80.CBOps.RES3r_c,
                                           Z80.CBOps.RES3r_d,
                                           Z80.CBOps.RES3r_e,
                                           Z80.CBOps.RES3r_h,
                                           Z80.CBOps.RES3r_l,
                                           Z80.CBOps.RES3HLm,
                                           Z80.CBOps.RES3r_a,

                                           // 0xA0
                                           Z80.CBOps.RES0r_b,
                                           Z80.CBOps.RES4r_c,
                                           Z80.CBOps.RES4r_d,
                                           Z80.CBOps.RES4r_e,
                                           Z80.CBOps.RES4r_h,
                                           Z80.CBOps.RES4r_l,
                                           Z80.CBOps.RES4HLm,
                                           Z80.CBOps.RES4r_a,
                                           Z80.CBOps.RES5r_b,
                                           Z80.CBOps.RES5r_c,
                                           Z80.CBOps.RES5r_d,
                                           Z80.CBOps.RES5r_e,
                                           Z80.CBOps.RES5r_h,
                                           Z80.CBOps.RES5r_l,
                                           Z80.CBOps.RES5HLm,
                                           Z80.CBOps.RES5r_a,

                                           // 0xB0
                                           Z80.CBOps.RES6r_b,
                                           Z80.CBOps.RES6r_c,
                                           Z80.CBOps.RES6r_d,
                                           Z80.CBOps.RES6r_e,
                                           Z80.CBOps.RES6r_h,
                                           Z80.CBOps.RES6r_l,
                                           Z80.CBOps.RES6HLm,
                                           Z80.CBOps.RES6r_a,
                                           Z80.CBOps.RES7r_b,
                                           Z80.CBOps.RES7r_c,
                                           Z80.CBOps.RES7r_d,
                                           Z80.CBOps.RES7r_e,
                                           Z80.CBOps.RES7r_h,
                                           Z80.CBOps.RES7r_l,
                                           Z80.CBOps.RES7HLm,
                                           Z80.CBOps.RES7r_a,

                                           // 0xC0
                                           Z80.CBOps.SET0r_b,
                                           Z80.CBOps.SET0r_c,
                                           Z80.CBOps.SET0r_d,
                                           Z80.CBOps.SET0r_e,
                                           Z80.CBOps.SET0r_h,
                                           Z80.CBOps.SET0r_l,
                                           Z80.CBOps.SET0HLm,
                                           Z80.CBOps.SET0r_a,
                                           Z80.CBOps.SET1r_b,
                                           Z80.CBOps.SET1r_c,
                                           Z80.CBOps.SET1r_d,
                                           Z80.CBOps.SET1r_e,
                                           Z80.CBOps.SET1r_h,
                                           Z80.CBOps.SET1r_l,
                                           Z80.CBOps.SET1HLm,
                                           Z80.CBOps.SET1r_a,

                                           // 0xD0
                                           Z80.CBOps.SET2r_b,
                                           Z80.CBOps.SET2r_c,
                                           Z80.CBOps.SET2r_d,
                                           Z80.CBOps.SET2r_e,
                                           Z80.CBOps.SET2r_h,
                                           Z80.CBOps.SET2r_l,
                                           Z80.CBOps.SET2HLm,
                                           Z80.CBOps.SET2r_a,
                                           Z80.CBOps.SET3r_b,
                                           Z80.CBOps.SET3r_c,
                                           Z80.CBOps.SET3r_d,
                                           Z80.CBOps.SET3r_e,
                                           Z80.CBOps.SET3r_h,
                                           Z80.CBOps.SET3r_l,
                                           Z80.CBOps.SET3HLm,
                                           Z80.CBOps.SET3r_a,

                                           // 0xE0
                                           Z80.CBOps.SET0r_b,
                                           Z80.CBOps.SET4r_c,
                                           Z80.CBOps.SET4r_d,
                                           Z80.CBOps.SET4r_e,
                                           Z80.CBOps.SET4r_h,
                                           Z80.CBOps.SET4r_l,
                                           Z80.CBOps.SET4HLm,
                                           Z80.CBOps.SET4r_a,
                                           Z80.CBOps.SET5r_b,
                                           Z80.CBOps.SET5r_c,
                                           Z80.CBOps.SET5r_d,
                                           Z80.CBOps.SET5r_e,
                                           Z80.CBOps.SET5r_h,
                                           Z80.CBOps.SET5r_l,
                                           Z80.CBOps.SET5HLm,
                                           Z80.CBOps.SET5r_a,

                                           // 0xF0
                                           Z80.CBOps.SET6r_b,
                                           Z80.CBOps.SET6r_c,
                                           Z80.CBOps.SET6r_d,
                                           Z80.CBOps.SET6r_e,
                                           Z80.CBOps.SET6r_h,
                                           Z80.CBOps.SET6r_l,
                                           Z80.CBOps.SET6HLm,
                                           Z80.CBOps.SET6r_a,
                                           Z80.CBOps.SET7r_b,
                                           Z80.CBOps.SET7r_c,
                                           Z80.CBOps.SET7r_d,
                                           Z80.CBOps.SET7r_e,
                                           Z80.CBOps.SET7r_h,
                                           Z80.CBOps.SET7r_l,
                                           Z80.CBOps.SET7HLm,
                                           Z80.CBOps.SET7r_a,
                                       };

        // The instruction set, and a few helper functions
        private struct Ops
        {
            // Gets a flag at the specified bit. Can only retrieve one at a time
            private static byte GetFlag(byte flag)
            {
                return (Z80.Registers.f & flag) > 0 ? (byte)1 : (byte)0;
            }

            //Clears the flags
            public static void ClearFlags()
            {
                byte mask = 0;
                Z80.Registers.f &= (byte)~mask;
            }

            // Clears flag on f register by default
            public static void ClearFlag(byte flag)
            {
                byte mask = flag;
                Z80.Registers.f &= (byte)~flag;
            }

            public static void ClearFlag(byte flag, ref byte register)
            {
                byte mask = flag;
                register &= (byte)~flag;
            }

            // Sets flag on f register by default
            public static void SetFlag(byte flag)
            {
                Z80.Registers.f |= flag;
            }

            // Can set a bit on any byte
            public static void SetFlag(byte flag, ref byte register)
            {
                register |= flag;
            }

            // Checks for a overflow and half overflow and sets flags accordingly for 8-bit operations. Use variables as a + b
            private static void CheckOverflow(byte a, byte b, byte carryIn = 0)
            {
                // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                if (( ((int)a & 0xF) + (b & 0xF) + carryIn) > 0xF)
                {
                    SetFlag(0x20);
                }
                else
                {
                    ClearFlag(0x20);
                }

                if ((int)a + b + carryIn > 0xFF)
                {
                    SetFlag(0x10);
                }
                else
                {
                    ClearFlag(0x10);
                }
            }

            private static void CheckOverflow(byte a, sbyte b, byte carryIn = 0)
            {
                if (b < 0)
                {
                    // b is smaller than 0, check for underflow
                    b = System.Math.Abs(b);
                    CheckUnderflow(a, (byte)b, carryIn);
                }
                else
                {
                    // b is larger than 0, check for an overflow
                    CheckOverflow(a, (byte)b, carryIn);
                }
            }

            // Checks for a overflow and half overflow and sets flags accordingly for 16-bit operations. Use variables as a + b
            private static void CheckOverflow(UInt16 a, UInt16 b, byte carryIn = 0)
            {
                // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                if ((((a & 0xFFF) + (b & 0xFFF) + carryIn) & 0x1000) == 0x1000 )
                {
                    SetFlag(0x20);
                }
                else
                {
                    ClearFlag(0x20);
                }

                // Set bit 0x10 on the flag register to 1 if a carry has occurred
                if ((((int)a + b + carryIn) & 0x10000) == 0x10000)
                {
                    SetFlag(0x20);
                }
                else
                {
                    ClearFlag(0x20);
                }
                Z80.Registers.f |= (((a & 0xFFF) + (b & 0xFFF) + carryIn) & 0x1000) == 0x1000 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.f |= (((int)a + b + carryIn) & 0x10000) == 0x10000 ? (byte)0x10 : (byte)0; // Set bit 0x10 on the flag register to 1 if a carry has occurred
            }

            private static void CheckOverflow(UInt16 a, sbyte b, byte carryIn = 0)
            {
                if (b < 0)
                {
                    // b is smaller than 0, check for underflow
                    b = System.Math.Abs(b);
                    CheckUnderflow((byte)a, (byte)b, carryIn);
                }
                else
                {
                    // b is larger than 0, check for an overflow
                    CheckOverflow(a, (byte)b, carryIn);
                }
            }

            // Checks for a overflow and half overflow and sets flags accordingly. Use variables as a + b
            private static void CheckUnderflow(byte a, byte b, byte carryIn = 0)
            {
                Z80.Registers.f |= (a & 0xF) < (b + carryIn & 0xF) ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.f |= a < b + carryIn ? (byte)0x10 : (byte)0; // Set bit 0x10 on the flag register to 1 if a borrow has occurred
            }

            public static bool NOP() { Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }

            /***************
             ***************
             * 8-bit Loads *
             ***************
             ***************/

            public static bool LDrn_a() { Z80.Registers.a = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_b() { Z80.Registers.b = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_c() { Z80.Registers.c = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_d() { Z80.Registers.d = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_e() { Z80.Registers.e = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_h() { Z80.Registers.h = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrn_l() { Z80.Registers.l = MMU.readByte(Z80.Registers.pc++); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }

            public static bool LDrr_aa() { Z80.Registers.a = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ab() { Z80.Registers.a = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ac() { Z80.Registers.a = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ad() { Z80.Registers.a = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ae() { Z80.Registers.a = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ah() { Z80.Registers.a = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_al() { Z80.Registers.a = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ba() { Z80.Registers.b = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_bb() { Z80.Registers.b = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_bc() { Z80.Registers.b = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_bd() { Z80.Registers.b = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_be() { Z80.Registers.b = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_bh() { Z80.Registers.b = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_bl() { Z80.Registers.b = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ca() { Z80.Registers.c = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_cb() { Z80.Registers.c = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_cc() { Z80.Registers.c = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_cd() { Z80.Registers.c = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ce() { Z80.Registers.c = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ch() { Z80.Registers.c = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_cl() { Z80.Registers.c = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_da() { Z80.Registers.d = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_db() { Z80.Registers.d = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_dc() { Z80.Registers.d = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_dd() { Z80.Registers.d = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_de() { Z80.Registers.d = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_dh() { Z80.Registers.d = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_dl() { Z80.Registers.d = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ea() { Z80.Registers.e = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_eb() { Z80.Registers.e = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ec() { Z80.Registers.e = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ed() { Z80.Registers.e = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ee() { Z80.Registers.e = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_eh() { Z80.Registers.e = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_el() { Z80.Registers.e = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ha() { Z80.Registers.h = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_hb() { Z80.Registers.h = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_hc() { Z80.Registers.h = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_hd() { Z80.Registers.h = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_he() { Z80.Registers.h = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_hh() { Z80.Registers.h = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_hl() { Z80.Registers.h = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_la() { Z80.Registers.l = Z80.Registers.a; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_lb() { Z80.Registers.l = Z80.Registers.b; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_lc() { Z80.Registers.l = Z80.Registers.c; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ld() { Z80.Registers.l = Z80.Registers.d; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_le() { Z80.Registers.l = Z80.Registers.e; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_lh() { Z80.Registers.l = Z80.Registers.h; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }
            public static bool LDrr_ll() { Z80.Registers.l = Z80.Registers.l; Z80.Registers.m = 1; Z80.Registers.t = 4; return true; }


            public static bool LDrHLm_a() { Z80.Registers.a = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_b() { Z80.Registers.b = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_c() { Z80.Registers.c = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_d() { Z80.Registers.d = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_e() { Z80.Registers.e = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_h() { Z80.Registers.h = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrHLm_l() { Z80.Registers.l = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }


            public static bool LDrBCm_a() { Z80.Registers.a = MMU.readByte((UInt16)((Z80.Registers.b << 8) + Z80.Registers.c));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDrDEm_a() { Z80.Registers.a = MMU.readByte((UInt16)((Z80.Registers.d << 8) + Z80.Registers.e));
                Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }

            public static bool LDBCmr_a() { MMU.writeByte((UInt16)((Z80.Registers.b << 8) + Z80.Registers.c), Z80.Registers.a); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDDEmr_a() { MMU.writeByte((UInt16)((Z80.Registers.d << 8) + Z80.Registers.e), Z80.Registers.a); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            
            public static bool LDHLmr_a() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.a); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_b() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.b); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_c() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.c); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_d() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.d); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_e() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.e); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_h() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.h); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmr_l() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), Z80.Registers.l); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDHLmn() { MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), MMU.readByte(Z80.Registers.pc++)); Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }

            public static bool LDNNmr_a() { MMU.writeByte(MMU.readWord(Z80.Registers.pc), Z80.Registers.a); Z80.Registers.pc += 2; Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }
            public static bool LDrNNm_a() { Z80.Registers.a = MMU.readByte(Z80.Registers.pc); Z80.Registers.pc += 2; Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }

            public static bool LDrIOC_a() { Z80.Registers.a = MMU.readByte((UInt16)(0xFF00 + MMU.readByte(Z80.Registers.pc++))); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDIOCr_a() { MMU.writeByte((UInt16)(0xFF00 + MMU.readByte(Z80.Registers.pc++)), Z80.Registers.a); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            
            public static bool LDDrHLm_a() { LDrHLm_a(); DECHL(false); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDDHLmr_a() { LDHLmr_a(); DECHL(false); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }

            public static bool LDIrHLm_a() { LDrHLm_a(); INCHL(false); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }
            public static bool LDIHLmr_a() { LDHLmr_a(); INCHL(false); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }

            public static bool LDIONr_a() { MMU.writeByte((UInt16)(0xFF00 + MMU.readByte(Z80.Registers.pc++)), Z80.Registers.a); Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool LDrION_a() { Z80.Registers.a = MMU.readByte((UInt16)(MMU.readByte(Z80.Registers.pc++))); Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }



            /****************
             ****************
             * 16-bit Loads *
             ****************
             ****************/

            public static bool LDBCnn() { Z80.Registers.c = MMU.readByte(Z80.Registers.pc++); Z80.Registers.b = MMU.readByte(Z80.Registers.pc++); 
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool LDDEnn() { Z80.Registers.e = MMU.readByte(Z80.Registers.pc++); Z80.Registers.d = MMU.readByte(Z80.Registers.pc++); 
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool LDHLnn() { Z80.Registers.l = MMU.readByte(Z80.Registers.pc++); Z80.Registers.h = MMU.readByte(Z80.Registers.pc++); 
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool LDSPnn() { Z80.Registers.sp = MMU.readWord(Z80.Registers.pc); Z80.Registers.pc += 2; 
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }

            public static bool LDSPHL() { Z80.Registers.sp = (UInt16)(Z80.Registers.h << 8 + Z80.Registers.l); Z80.Registers.m = 2; Z80.Registers.t = 8; return true; }

            public static bool LDHLSPn()
            {
                ClearFlags(); // Reset the flags
                sbyte n = (sbyte)MMU.readByte(Z80.Registers.pc++);
                CheckOverflow(Z80.Registers.sp, n); // Check for overflow and set appropriate flags
                int sum = (byte)(Z80.Registers.sp + n); // Calculate PC+n
                Z80.Registers.l = (byte)(sum & 0xFF); // Add lower byte of SP to L
                Z80.Registers.h = (byte)((sum >> 8) & 0xFF); // Add higher byte of SP to H
                Z80.Registers.m = 3; 
                Z80.Registers.t = 12;
                return true;
            }

            public static bool LDNNmr_sp() { MMU.writeWord(MMU.readWord(Z80.Registers.pc), Z80.Registers.sp); Z80.Registers.pc += 2; Z80.Registers.m = 5; 
                Z80.Registers.t = 20; return true; }

            public static bool PUSHAF() { DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.a); DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.f);
                Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }
            public static bool PUSHBC() { DECSP(false);  MMU.writeByte(Z80.Registers.sp, Z80.Registers.b); DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.c);
                Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }
            public static bool PUSHDE() { DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.d); DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.e);
                Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }
            public static bool PUSHHL() { DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.h); DECSP(false); MMU.writeByte(Z80.Registers.sp, Z80.Registers.l);
                Z80.Registers.m = 4; Z80.Registers.t = 16; return true; }

            public static bool POPAF() { Z80.Registers.f = MMU.readByte(Z80.Registers.sp); INCSP(false); Z80.Registers.a = MMU.readByte(Z80.Registers.sp);
                INCSP(false); Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool POPBC() { Z80.Registers.c = MMU.readByte(Z80.Registers.sp); INCSP(false); Z80.Registers.b = MMU.readByte(Z80.Registers.sp); INCSP(false);
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool POPDE() { Z80.Registers.e = MMU.readByte(Z80.Registers.sp); INCSP(false); Z80.Registers.d = MMU.readByte(Z80.Registers.sp); INCSP(false);
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }
            public static bool POPHL() { Z80.Registers.l = MMU.readByte(Z80.Registers.sp); INCSP(false); Z80.Registers.h = MMU.readByte(Z80.Registers.sp); INCSP(false);
                Z80.Registers.m = 3; Z80.Registers.t = 12; return true; }


            /***************
             ***************
             *  8-bit ALU  *
             ***************
             ***************/

            public static bool ADDr_a() 
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.a);
                Z80.Registers.a += Z80.Registers.a;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_b()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.b);
                Z80.Registers.a += Z80.Registers.b;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_c()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.c);
                Z80.Registers.a += Z80.Registers.c;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_d()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.d);
                Z80.Registers.a += Z80.Registers.d;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_e()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.e);
                Z80.Registers.a += Z80.Registers.e;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_h()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.h);
                Z80.Registers.a += Z80.Registers.h;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDr_l()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, Z80.Registers.l);
                Z80.Registers.a += Z80.Registers.l;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADDHLm()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)));
                Z80.Registers.a += MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADDn()
            {
                ClearFlags(); // Clear flags
                CheckOverflow(Z80.Registers.a, MMU.readByte(Z80.Registers.pc));
                Z80.Registers.a += MMU.readByte(Z80.Registers.pc++);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADCr_a()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.a, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.a + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_b()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.b, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.b + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_c()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.c, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.c + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_d()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.d, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.d + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_e()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.e, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.e + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_h()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.h, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.h + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCr_l()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, Z80.Registers.l, carryIn);
                Z80.Registers.a += (byte)(Z80.Registers.l + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ADCHLm()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)));
                Z80.Registers.a += (byte)(MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)) + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADCn()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckOverflow(Z80.Registers.a, MMU.readByte(Z80.Registers.pc));
                Z80.Registers.a += (byte)(MMU.readByte(Z80.Registers.pc++) + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SUBr_a()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.a);
                Z80.Registers.a -= Z80.Registers.a;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_b()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.b);
                Z80.Registers.a -= Z80.Registers.b;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_c()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.c);
                Z80.Registers.a -= Z80.Registers.c;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_d()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.d);
                Z80.Registers.a -= Z80.Registers.d;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_e()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.e);
                Z80.Registers.a -= Z80.Registers.e;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_h()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.h);
                Z80.Registers.a -= Z80.Registers.h;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBr_l()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.l);
                Z80.Registers.a -= Z80.Registers.l;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SUBHLm()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)));
                Z80.Registers.a -= MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SUBn()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, MMU.readByte(Z80.Registers.pc));
                Z80.Registers.a -= MMU.readByte(MMU.readByte(Z80.Registers.pc++));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SBCr_a()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.a, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.a + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_b()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.b, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.b + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_c()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.c, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.c + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_d()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.d, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.d + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_e()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.e, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.e + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_h()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.h, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.h + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCr_l()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.l, carryIn);
                Z80.Registers.a -= (byte)(Z80.Registers.l + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SBCHLm()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)), carryIn);
                Z80.Registers.a -= (byte)(MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)) + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SBCn()
            {
                byte carryIn = GetFlag(0x10);
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, MMU.readByte(Z80.Registers.pc), carryIn);
                Z80.Registers.a -= (byte)(MMU.readByte(Z80.Registers.pc++) + carryIn);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ANDr_a()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.a;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_b()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.b;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_c()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.c;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_d()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.d;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_e()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.e;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_h()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.h;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDr_l()
            {
                ClearFlags();
                Z80.Registers.a &= Z80.Registers.l;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ANDHLm()
            {
                ClearFlags();
                Z80.Registers.a &= MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ANDn()
            {
                ClearFlags();
                Z80.Registers.a &= (byte)Z80.Registers.pc++;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x20; // Set half carry flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ORr_a()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.a;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_b()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.b;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_c()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.c;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_d()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.d;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_e()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.e;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_h()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.h;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORr_l()
            {
                ClearFlags();
                Z80.Registers.a |= Z80.Registers.l;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool ORHLm()
            {
                ClearFlags();
                Z80.Registers.a |= MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ORn()
            {
                ClearFlags();
                Z80.Registers.a |= (byte)Z80.Registers.pc++;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool XORr_a()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.a;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_b()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.b;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_c()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.c;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_d()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.d;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_e()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.e;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_h()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.h;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORr_l()
            {
                ClearFlags();
                Z80.Registers.a ^= Z80.Registers.l;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool XORHLm()
            {
                ClearFlags();
                Z80.Registers.a ^= MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool XORn()
            {
                ClearFlags();
                Z80.Registers.a ^= (byte)Z80.Registers.pc++;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool CPr_a()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.a);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_b()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.b);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_c()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.c);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_d()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.d);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_e()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.e);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_h()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.h);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPr_l()
            {
                ClearFlags();
                CheckUnderflow(Z80.Registers.a, Z80.Registers.l);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPHLm()
            {
                ClearFlags();
                byte HLm = MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l));
                CheckUnderflow(Z80.Registers.a, HLm);
                if (Z80.Registers.a - HLm  == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool CPn()
            {
                ClearFlags();
                byte n = MMU.readByte(Z80.Registers.pc++);
                CheckUnderflow(Z80.Registers.a, n);
                if (Z80.Registers.a - n == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.f |= 0x40; // Set the N flag
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool INCr_a()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.a & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.a++;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_b()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.b & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.b++;
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_c()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.c & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.c++;
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_d()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.d & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.d++;
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_e()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.e & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.e++;
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_h()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.h & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.h++;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCr_l()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.l & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                Z80.Registers.l++;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool INCHLm()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                Z80.Registers.f |= (((int)Z80.Registers.a & 0xF) + 1) > 0xF ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half carry has occurred
                MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), (byte)(MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)) + 1));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool DECr_a()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.a & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.a--;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_b()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.b & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.b--;
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_c()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.c & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.c--;
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_d()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.d & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.d--;
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_e()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.e & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.e--;
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_h()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.h & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.h--;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECr_l()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (Z80.Registers.l & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                Z80.Registers.l--;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool DECHLm()
            {
                Z80.Registers.f |= 0x40; // Set Flag N
                Z80.Registers.f |= (MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)) & 0xF) < (byte)1 ? (byte)0x20 : (byte)0; // Set bit 0x20 on the flag register to 1 if a half borrow has occurred
                MMU.writeByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l), (byte)(MMU.readByte((UInt16)((Z80.Registers.h << 8) + Z80.Registers.l)) - 1));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }


            /***********************
             ***********************
             *  16-bit Arithmetic  *
             ***********************
             ***********************/

            public static bool ADDHLBC()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                UInt16 bc = (UInt16)((Z80.Registers.b << 8) + Z80.Registers.c);
                CheckOverflow(hl, bc);
                hl += bc;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADDHLDE()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                UInt16 de = (UInt16)((Z80.Registers.d << 8) + Z80.Registers.e);
                CheckOverflow(hl, de);
                hl += de;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADDHLHL()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                CheckOverflow(hl, hl);
                hl += hl;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADDHLSP()
            {
                byte mask;
                mask = 0x40; // Set mask to clear below flag
                Z80.Registers.f &= (byte)~mask; // Clear Flag N
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                CheckOverflow(hl, Z80.Registers.sp);
                hl += Z80.Registers.sp;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool ADDSPn()
            {
                ClearFlags();
                sbyte n = (sbyte)MMU.readByte(Z80.Registers.pc++);
                CheckOverflow(Z80.Registers.sp, n);
                Z80.Registers.sp = (UInt16)(Z80.Registers.sp + n); // Calculate SP+n;
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool INCBC()
            { 
                UInt16 bc = (UInt16)((Z80.Registers.b << 8) + Z80.Registers.c);
                bc++;
                Z80.Registers.c = (byte)(bc & 0xFF);
                Z80.Registers.b = (byte)((bc & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool INCDE()
            {
                UInt16 de = (UInt16)((Z80.Registers.d << 8) + Z80.Registers.e);
                de++;
                Z80.Registers.e = (byte)(de & 0xFF);
                Z80.Registers.d = (byte)((de & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool INCHL(bool mt)
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                hl++;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                if (mt)
                {
                    Z80.Registers.m = 2; Z80.Registers.t = 8;
                }
                return true;
            }

            public static bool INCHL()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                hl++;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool INCSP(bool mt)
            {
                Z80.Registers.sp++;
                if (mt)
                {
                    Z80.Registers.m = 2; Z80.Registers.t = 8;
                }
                return true;
            }

            public static bool INCSP()
            {
                Z80.Registers.sp++;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool DECBC()
            {
                UInt16 bc = (UInt16)((Z80.Registers.b << 8) + Z80.Registers.c);
                bc--;
                Z80.Registers.c = (byte)(bc & 0xFF);
                Z80.Registers.b = (byte)((bc & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool DECDE()
            {
                UInt16 de = (UInt16)((Z80.Registers.d << 8) + Z80.Registers.e);
                de--;
                Z80.Registers.e = (byte)(de & 0xFF);
                Z80.Registers.d = (byte)((de & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool DECHL(bool mt)
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                hl--;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                if (mt)
                {
                    Z80.Registers.m = 2; Z80.Registers.t = 8;
                }
                return true;
            }

            public static bool DECHL()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                hl--;
                Z80.Registers.l = (byte)(hl & 0xFF);
                Z80.Registers.h = (byte)((hl & 0xFF00) >> 8);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool DECSP(bool mt)
            {
                Z80.Registers.sp--;
                if (mt)
                {
                    Z80.Registers.m = 2; Z80.Registers.t = 8;
                }
                return true;
            }

            public static bool DECSP()
            {
                Z80.Registers.sp--;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }


            /*********************
             *********************
             *  Misc Operations  *
             *********************
             *********************/

            public static bool DAA()
            {
                if ((Z80.Registers.f & 0x40) == 0x40)
                {
                    // Last operation was SUB/SBC/DEC/NEG
                    if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) <= 0x90) && 
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) <= 0x9))
                    {
                        Z80.Registers.a += 0;
                        ClearFlag(0x10);
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) <= 0x80) && 
                        ((Z80.Registers.f & 0x20) == 0x20) && ((Z80.Registers.a & 0x0F) >= 0x6))
                    {
                        Z80.Registers.a += 0xFA;
                        ClearFlag(0x10);
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x10) && ((Z80.Registers.a & 0xF0) >= 0x70) && 
                        ((Z80.Registers.f & 0x20) == 0x00) &&((Z80.Registers.a & 0x0F) <= 0x9))
                    {
                        Z80.Registers.a += 0xA0;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x10) && ( (Z80.Registers.a & 0xF0) == 6 || (Z80.Registers.a & 0xF0) == 7 ) && 
                        ( (Z80.Registers.f & 0x20) == 0x20) && ( (Z80.Registers.a & 0x0F) >= 0x6) )
                    {
                        Z80.Registers.a += 0x9A;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                }
                else
                {
                    // Last operation was ADD/ADC/INC
                    if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) <= 0x90) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) <= 0x9))
                    {
                        Z80.Registers.a += 0;
                        ClearFlag(0x10);
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) <= 0x80) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) >= 0xA))
                    {
                        Z80.Registers.a += 0x06;
                        ClearFlag(0x10);
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) <= 0x90) &&
                        ((Z80.Registers.f & 0x20) == 0x20) && ((Z80.Registers.a & 0x0F) <= 0x3))
                    {
                        Z80.Registers.a += 0x06;
                        ClearFlag(0x10);
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) >= 0xA0) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) <= 0x9))
                    {
                        Z80.Registers.a += 0x60;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) >= 0x90) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) >= 0xA))
                    {
                        Z80.Registers.a += 0x66;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x00) && ((Z80.Registers.a & 0xF0) >= 0xA0) &&
                        ((Z80.Registers.f & 0x20) == 0x20) && ((Z80.Registers.a & 0x0F) <= 0x3))
                    {
                        Z80.Registers.a += 0x66;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x10) && ((Z80.Registers.a & 0xF0) <= 0x20) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) <= 0x9))
                    {
                        Z80.Registers.a += 0x60;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x10) && ((Z80.Registers.a & 0xF0) <= 0x20) &&
                        ((Z80.Registers.f & 0x20) == 0x00) && ((Z80.Registers.a & 0x0F) >= 0xA))
                    {
                        Z80.Registers.a += 0x66;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                    else if (((Z80.Registers.f & 0x10) == 0x10) && ((Z80.Registers.a & 0xF0) <= 0x30) &&
                        ((Z80.Registers.f & 0x20) == 0x20) && ((Z80.Registers.a & 0x0F) <= 0x3))
                    {
                        Z80.Registers.a += 0x66;
                        Z80.Registers.f |= 0x10; // Set C flag to 1
                    }
                }

                ClearFlag(0x20);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CPL()
            {
                Z80.Registers.f |= (0x20 + 0x40); // Set H and N flags
                Z80.Registers.a = (byte)~Z80.Registers.a;
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool CCF()
            {
                ClearFlag(0x20 + 0x40);
                Z80.Registers.f |= (byte)((Z80.Registers.f & 0x10) == 0x10 ? 0x00 : 0x10);
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool SCF()
            {
                ClearFlag(0x20 + 0x40);
                Z80.Registers.f |= 0x10;
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool HALT()
            {
                // @TODO: Interrupts: Need to code interrupts first
                return true;
            }

            public static bool STOP()
            {
                // @TODO: Interrupts/IO/LCD: Need to code more stuff first
                return true;
            }

            public static bool DI()
            {
                // @TODO: Interrupts: Need to code interrupts first
                return true;
            }

            public static bool EI()
            {
                // @TODO: Interrupts: Need to code interrupts first
                return true;
            }

            /**********************
             **********************
             *  Rotates & Shifts  *
             **********************
             **********************/

            public static bool RLCA()
            {
                ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.a & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool RLA()
            {
                ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.a & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRCA()
            {
                ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.a & 0x1); // the carry from bit 1
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool RRA()
            {
                ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.a & 0x1); // the carry from bit 1
                Z80.Registers.a = (byte)(Z80.Registers.a >> 1);
                Z80.Registers.a |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }


            /**********************
             **********************
             *        Jumps       *
             **********************
             **********************/

            public static bool JPnn()
            {
                Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool JPNZnn()
            {
                if ((Z80.Registers.f & 0x80) == 0)
                {
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool JPZnn()
            {
                if ((Z80.Registers.f & 0x80) == 0x80)
                {
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool JPNCnn()
            {
                if ((Z80.Registers.f & 0x10) == 0)
                {
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool JPCnn()
            {
                if ((Z80.Registers.f & 0x10) == 0x10)
                {
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool JPHL()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Registers.pc = hl;
                Z80.Registers.m = 1; Z80.Registers.t = 4;
                return true;
            }

            public static bool JRn()
            {
                Z80.Registers.pc = (byte)((Z80.Registers.pc + 1) + (sbyte)MMU.readByte(Z80.Registers.pc));
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool JRNZn()
            {
                if ((Z80.Registers.f & 0x80) == 0)
                {
                    Z80.Registers.pc = (byte)((Z80.Registers.pc + 1) + (sbyte)MMU.readByte(Z80.Registers.pc));
                }
                else
                {
                    Z80.Registers.pc ++;
                }
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool JRZn()
            {
                if ((Z80.Registers.f & 0x80) == 0x80)
                {
                    Z80.Registers.pc = (byte)((Z80.Registers.pc + 1) + (sbyte)MMU.readByte(Z80.Registers.pc));
                }
                else
                {
                    Z80.Registers.pc++;
                }
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool JRNCn()
            {
                if ((Z80.Registers.f & 0x10) == 0)
                {
                    Z80.Registers.pc = (byte)((Z80.Registers.pc + 1) + (sbyte)MMU.readByte(Z80.Registers.pc));
                }
                else
                {
                    Z80.Registers.pc++;
                }
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool JRCn()
            {
                if ((Z80.Registers.f & 0x10) == 0x10)
                {
                    Z80.Registers.pc = (byte)((Z80.Registers.pc + 1) + (sbyte)MMU.readByte(Z80.Registers.pc));
                }
                else
                {
                    Z80.Registers.pc++;
                }
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }


            /**********************
             **********************
             *        Calls       *
             **********************
             **********************/

            public static bool CALLnn()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc + 2));
                Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool CALLNZnn()
            {
                if ((Z80.Registers.f & 0x80) == 0)
                {
                    DECSP(false); DECSP(false);
                    MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc + 2));
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool CALLZnn()
            {
                if ((Z80.Registers.f & 0x80) == 0x80)
                {
                    DECSP(false); DECSP(false);
                    MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc + 2));
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool CALLNCnn()
            {
                if ((Z80.Registers.f & 0x10) == 0)
                {
                    DECSP(false); DECSP(false);
                    MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc + 2));
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }

            public static bool CALLCnn()
            {
                if ((Z80.Registers.f & 0x10) == 0x10)
                {
                    DECSP(false); DECSP(false);
                    MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc + 2));
                    Z80.Registers.pc = MMU.readWord(Z80.Registers.pc);
                }
                else
                {
                    Z80.Registers.pc += 2;
                }
                Z80.Registers.m = 3; Z80.Registers.t = 12;
                return true;
            }


            /**********************
             **********************
             *      Restarts      *
             **********************
             **********************/

            public static bool RST00()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x00);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST08()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x08);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST10()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x10);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST18()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x18);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST20()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x20);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST28()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x28);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST30()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x30);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }

            public static bool RST38()
            {
                DECSP(false); DECSP(false);
                MMU.writeWord(Z80.Registers.sp, (UInt16)(Z80.Registers.pc));
                Z80.Registers.pc = (UInt16)((Z80.Registers.pc & 0) + 0x38);
                Z80.Registers.m = 8; Z80.Registers.t = 32;
                return true;
            }


            /*********************
             *********************
             *      Returns      *
             *********************
             *********************/


            public static bool RET(bool mt)
            {
                Z80.Registers.pc = MMU.readWord(Z80.Registers.sp);
                INCSP(); INCSP();
                if (mt)
                {
                    Z80.Registers.m = 2; Z80.Registers.t = 8;
                }
                return true;
            }

            public static bool RET()
            {
                Z80.Registers.pc = MMU.readWord(Z80.Registers.sp);
                INCSP(); INCSP();
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RETNZ()
            {
                if ((Z80.Registers.f & 0x80) == 0) RET(false);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RETZ()
            {
                if ((Z80.Registers.f & 0x80) == 0x80) RET(false);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RETNC()
            {
                if ((Z80.Registers.f & 0x10) == 0) RET(false);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RETC()
            {
                if ((Z80.Registers.f & 0x10) == 0x10) RET(false);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RETI()
            {
                RET(false);
                // @TODO: Interrupts: Enable interrupts here
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            public static bool CB()
            {
                Z80.CBCommands[MMU.readByte(Z80.Registers.pc++)]();
                return true;
            }
        }

        private struct CBOps
        {
            public static bool SWAPr_a()
            {
                Ops.ClearFlags();
                Z80.Registers.a = (byte)((Z80.Registers.a >> 4 & 0x0F) | (Z80.Registers.a << 4 & 0xF0));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SWAPr_b()
            {
                Ops.ClearFlags();
                Z80.Registers.b = (byte)((Z80.Registers.b >> 4 & 0x0F) | (Z80.Registers.b << 4 & 0xF0));
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPr_c()
            {
                Ops.ClearFlags();
                Z80.Registers.c = (byte)((Z80.Registers.c >> 4 & 0x0F) | (Z80.Registers.c << 4 & 0xF0));
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPr_d()
            {
                Ops.ClearFlags();
                Z80.Registers.d = (byte)((Z80.Registers.d >> 4 & 0x0F) | (Z80.Registers.d << 4 & 0xF0));
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPr_e()
            {
                Ops.ClearFlags();
                Z80.Registers.e = (byte)((Z80.Registers.e >> 4 & 0x0F) | (Z80.Registers.e << 4 & 0xF0));
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPr_h()
            {
                Ops.ClearFlags();
                Z80.Registers.h = (byte)((Z80.Registers.h >> 4 & 0x0F) | (Z80.Registers.h << 4 & 0xF0));
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPr_l()
            {
                Ops.ClearFlags();
                Z80.Registers.l = (byte)((Z80.Registers.l >> 4 & 0x0F) | (Z80.Registers.l << 4 & 0xF0));
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }
            
            public static bool SWAPHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte HLm = MMU.readByte(hl);
                Ops.ClearFlags();
                MMU.writeByte(hl, (byte)((HLm >> 4 & 0x0F) | (HLm << 4 & 0xF0)));
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }
            
            /*************************
             *************************
             *  CB Rotates & Shifts  *
             *************************
             *************************/

            public static bool RLCr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.a & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.b & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.b = (byte)(Z80.Registers.b << 1);
                Z80.Registers.b |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.c & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.c = (byte)(Z80.Registers.c << 1);
                Z80.Registers.c |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.d & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.d = (byte)(Z80.Registers.d << 1);
                Z80.Registers.d |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.e & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.e = (byte)(Z80.Registers.e << 1);
                Z80.Registers.e |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.h & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.h = (byte)(Z80.Registers.h << 1);
                Z80.Registers.h |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((Z80.Registers.l & 0x80) >> 7); // the carry from bit 7
                Z80.Registers.l = (byte)(Z80.Registers.l << 1);
                Z80.Registers.l |= carry;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RLCHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((MMU.readByte(hl) & 0x80) >> 7); // the carry from bit 7
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) << 1));
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | carry));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RLr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.a & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.b & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.b = (byte)(Z80.Registers.b << 1);
                Z80.Registers.b |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.c & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.c = (byte)(Z80.Registers.c << 1);
                Z80.Registers.c |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.d & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.d = (byte)(Z80.Registers.d << 1);
                Z80.Registers.d |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.e & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.e = (byte)(Z80.Registers.e << 1);
                Z80.Registers.e |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.h & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.h = (byte)(Z80.Registers.h << 1);
                Z80.Registers.h |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((Z80.Registers.l & 0x80) >> 7); // The carry from bit 7
                Z80.Registers.l = (byte)(Z80.Registers.l << 1);
                Z80.Registers.l |= (byte)(prevCarry >> 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RLHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)((MMU.readByte(hl) & 0x80) >> 7); // The carry from bit 7
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) << 1));
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | (prevCarry >> 1) ));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRCr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.a & 0x1); // the carry from bit 1
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                Z80.Registers.a |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.b & 0x1); // the carry from bit 1
                Z80.Registers.b = (byte)(Z80.Registers.b << 1);
                Z80.Registers.b |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.c & 0x1); // the carry from bit 1
                Z80.Registers.c = (byte)(Z80.Registers.c << 1);
                Z80.Registers.c |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.d & 0x1); // the carry from bit 1
                Z80.Registers.d = (byte)(Z80.Registers.d << 1);
                Z80.Registers.d |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.e & 0x1); // the carry from bit 1
                Z80.Registers.e = (byte)(Z80.Registers.e << 1);
                Z80.Registers.e |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.h & 0x1); // the carry from bit 1
                Z80.Registers.h = (byte)(Z80.Registers.h << 1);
                Z80.Registers.h |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.l & 0x1); // the carry from bit 1
                Z80.Registers.l = (byte)(Z80.Registers.l << 1);
                Z80.Registers.l |= (byte)(carry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RRCHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(MMU.readByte(hl) & 0x1); // the carry from bit 1
                MMU.writeByte(hl, (byte)(Z80.Registers.a << 1));
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | carry << 7));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RRr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.a & 0x1); // the carry from bit 1
                Z80.Registers.a = (byte)(Z80.Registers.a >> 1);
                Z80.Registers.a |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.b & 0x1); // the carry from bit 1
                Z80.Registers.b = (byte)(Z80.Registers.b >> 1);
                Z80.Registers.b |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.c & 0x1); // the carry from bit 1
                Z80.Registers.c = (byte)(Z80.Registers.c >> 1);
                Z80.Registers.c |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.d & 0x1); // the carry from bit 1
                Z80.Registers.d = (byte)(Z80.Registers.d >> 1);
                Z80.Registers.d |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.e & 0x1); // the carry from bit 1
                Z80.Registers.e = (byte)(Z80.Registers.e >> 1);
                Z80.Registers.e |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.h & 0x1); // the carry from bit 1
                Z80.Registers.h = (byte)(Z80.Registers.h >> 1);
                Z80.Registers.h |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.l & 0x1); // the carry from bit 1
                Z80.Registers.l = (byte)(Z80.Registers.l >> 1);
                Z80.Registers.l |= (byte)(prevCarry << 7);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool RRHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte prevCarry = (byte)(Z80.Registers.f & 0x10); // The previous carry
                byte carry = (byte)(Z80.Registers.a & 0x1); // the carry from bit 1
                MMU.writeByte(hl, (byte)(Z80.Registers.a >> 1));
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | prevCarry << 7));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                return true;
            }

            public static bool SLAr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.a & 0x80); // the carry from bit 7
                Z80.Registers.a = (byte)(Z80.Registers.a << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.b & 0x80); // the carry from bit 7
                Z80.Registers.b = (byte)(Z80.Registers.b << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.c & 0x80); // the carry from bit 7
                Z80.Registers.c = (byte)(Z80.Registers.c << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.d & 0x80); // the carry from bit 7
                Z80.Registers.d = (byte)(Z80.Registers.d << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.e & 0x80); // the carry from bit 7
                Z80.Registers.e = (byte)(Z80.Registers.e << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.h & 0x80); // the carry from bit 7
                Z80.Registers.h = (byte)(Z80.Registers.h << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.l & 0x80); // the carry from bit 7
                Z80.Registers.l = (byte)(Z80.Registers.l << 1);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SLAHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)((MMU.readByte(hl) & 0x80) >> 3); // the carry from bit 7
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) << 1));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SRAr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.a & 1); // the carry from bit 0
                Z80.Registers.a = (byte)(Z80.Registers.a >> 1);
                Z80.Registers.a |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.b & 1); // the carry from bit 0
                Z80.Registers.b = (byte)(Z80.Registers.b >> 1);
                Z80.Registers.b |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.c & 1); // the carry from bit 0
                Z80.Registers.c = (byte)(Z80.Registers.c >> 1);
                Z80.Registers.c |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.d & 1); // the carry from bit 0
                Z80.Registers.d = (byte)(Z80.Registers.d >> 1);
                Z80.Registers.d |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.e & 1); // the carry from bit 0
                Z80.Registers.e = (byte)(Z80.Registers.e >> 1);
                Z80.Registers.e |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.h & 1); // the carry from bit 0
                Z80.Registers.h = (byte)(Z80.Registers.h >> 1);
                Z80.Registers.h |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.l & 1); // the carry from bit 0
                Z80.Registers.l = (byte)(Z80.Registers.l >> 1);
                Z80.Registers.l |= 0x80;
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRAHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(MMU.readByte(hl) & 1); // the carry from bit 0
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) >> 1));
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 0x80));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SRLr_a()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.a & 1); // the carry from bit 0
                Z80.Registers.a = (byte)(Z80.Registers.a >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.a);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.a == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_b()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.b & 1); // the carry from bit 0
                Z80.Registers.b = (byte)(Z80.Registers.b >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.b);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.b == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_c()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.c & 1); // the carry from bit 0
                Z80.Registers.c = (byte)(Z80.Registers.c >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.c);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.c == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_d()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.d & 1); // the carry from bit 0
                Z80.Registers.d = (byte)(Z80.Registers.d >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.d);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.d == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_e()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.e & 1); // the carry from bit 0
                Z80.Registers.e = (byte)(Z80.Registers.e >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.a);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.e == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_h()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.h & 1); // the carry from bit 0
                Z80.Registers.h = (byte)(Z80.Registers.h >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.h);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLr_l()
            {
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(Z80.Registers.l & 1); // the carry from bit 0
                Z80.Registers.l = (byte)(Z80.Registers.l >> 1);
                Ops.ClearFlag(0x80, ref Z80.Registers.l);
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (Z80.Registers.h == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SRLHLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                Z80.Ops.ClearFlag(0x20 + 0x40);
                byte carry = (byte)(MMU.readByte(hl) & 1); // the carry from bit 0
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) >> 1));
                byte mask = 0x80;
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                if (carry > 0) Z80.Ops.SetFlag(0x10); else Z80.Ops.ClearFlag(0x10);
                if (MMU.readByte(hl) == 0) Z80.Ops.SetFlag(0x80); else Z80.Ops.ClearFlag(0x80); // Set/Clear zero flag depending on result
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }


            /*****************
             *****************
             *  Bit Opcodes  *
             *****************
             *****************/

            public static bool BIT0r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT0HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 1) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT1r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT1HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 2) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT2r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT2HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 4) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT3r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT3HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 8) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT4r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT4HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 16) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT5r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT5HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 32) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT6r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_h()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT6HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 64) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool BIT7r_a()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.a & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_b()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.b & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_c()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.c & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_d()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.d & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                if ((Z80.Registers.d & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset

                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_e()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.e & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_h()
            {
                byte mask;
                mask = 0x40; // Set mask to 0x40 to clear N flag
                Z80.Registers.f &= (byte)~(mask); // Clear N flag
                mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.h & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7r_l()
            {
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((Z80.Registers.l & 128) == 0) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool BIT7HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                byte mask;
				mask = 0x40; // Set mask to 0x40 to clear N flag
				Z80.Registers.f &= (byte)~(mask); // Clear N flag
				mask = 0x80; // Set mask to 0x80 to clear Z flag if 0
                Z80.Registers.f |= 0x20; // Set H flag
                if ((MMU.readByte(hl) & 128) == 0 ) Z80.Registers.f |= 0x80; else Z80.Registers.f &= (byte)~mask; // Set zero flag if result is zero, otherwise reset
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET0r_a()
            {
                Z80.Registers.a |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_b()
            {
                Z80.Registers.b |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_c()
            {
                Z80.Registers.c |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_d()
            {
                Z80.Registers.d |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_e()
            {
                Z80.Registers.e |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_h()
            {
                Z80.Registers.h |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0r_l()
            {
                Z80.Registers.l |= 1;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET0HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 1));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET1r_a()
            {
                Z80.Registers.a |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_b()
            {
                Z80.Registers.b |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_c()
            {
                Z80.Registers.c |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_d()
            {
                Z80.Registers.d |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_e()
            {
                Z80.Registers.e |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_h()
            {
                Z80.Registers.h |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1r_l()
            {
                Z80.Registers.l |= 2;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET1HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 2));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET2r_a()
            {
                Z80.Registers.a |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_b()
            {
                Z80.Registers.b |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_c()
            {
                Z80.Registers.c |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_d()
            {
                Z80.Registers.d |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_e()
            {
                Z80.Registers.e |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_h()
            {
                Z80.Registers.h |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2r_l()
            {
                Z80.Registers.l |= 4;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET2HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 4));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET3r_a()
            {
                Z80.Registers.a |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_b()
            {
                Z80.Registers.b |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_c()
            {
                Z80.Registers.c |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_d()
            {
                Z80.Registers.d |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_e()
            {
                Z80.Registers.e |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_h()
            {
                Z80.Registers.h |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3r_l()
            {
                Z80.Registers.l |= 8;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET3HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 8));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET4r_a()
            {
                Z80.Registers.a |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_b()
            {
                Z80.Registers.b |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_c()
            {
                Z80.Registers.c |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_d()
            {
                Z80.Registers.d |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_e()
            {
                Z80.Registers.e |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_h()
            {
                Z80.Registers.h |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4r_l()
            {
                Z80.Registers.l |= 16;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET4HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 16));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET5r_a()
            {
                Z80.Registers.a |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_b()
            {
                Z80.Registers.b |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_c()
            {
                Z80.Registers.c |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_d()
            {
                Z80.Registers.d |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_e()
            {
                Z80.Registers.e |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_h()
            {
                Z80.Registers.h |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5r_l()
            {
                Z80.Registers.l |= 32;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET5HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 32));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET6r_a()
            {
                Z80.Registers.a |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_b()
            {
                Z80.Registers.b |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_c()
            {
                Z80.Registers.c |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_d()
            {
                Z80.Registers.d |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_e()
            {
                Z80.Registers.e |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_h()
            {
                Z80.Registers.h |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6r_l()
            {
                Z80.Registers.l |= 64;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET6HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 64));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool SET7r_a()
            {
                Z80.Registers.a |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_b()
            {
                Z80.Registers.b |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_c()
            {
                Z80.Registers.c |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_d()
            {
                Z80.Registers.d |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_e()
            {
                Z80.Registers.e |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_h()
            {
                Z80.Registers.h |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7r_l()
            {
                Z80.Registers.l |= 128;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool SET7HLm()
            {
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) | 64));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES0r_a()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_b()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_c()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_d()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_e()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_h()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0r_l()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES0HLm()
            {
                byte mask;
                mask = 1; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES1r_a()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_b()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_c()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_d()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_e()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_h()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1r_l()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES1HLm()
            {
                byte mask;
                mask = 2; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES2r_a()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_b()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_c()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_d()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_e()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_h()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2r_l()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES2HLm()
            {
                byte mask;
                mask = 4; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES3r_a()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_b()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_c()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_d()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_e()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_h()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3r_l()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES3HLm()
            {
                byte mask;
                mask = 8; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES4r_a()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_b()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_c()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_d()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_e()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_h()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4r_l()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES4HLm()
            {
                byte mask;
                mask = 16; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES5r_a()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_b()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_c()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_d()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_e()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_h()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5r_l()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES5HLm()
            {
                byte mask;
                mask = 32; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES6r_a()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_b()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_c()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_d()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_e()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_h()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6r_l()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES6HLm()
            {
                byte mask;
                mask = 64; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & (byte)~mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }

            public static bool RES7r_a()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.a &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_b()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.b &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_c()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.c &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_d()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.d &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_e()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.e &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_h()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.h &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7r_l()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                Z80.Registers.l &= (byte)~mask;
                Z80.Registers.m = 2; Z80.Registers.t = 8;
                return true;
            }

            public static bool RES7HLm()
            {
                byte mask;
                mask = 128; // Set mask to 128 to clear the bit
                UInt16 hl = (UInt16)((Z80.Registers.h << 8) + Z80.Registers.l);
                MMU.writeByte(hl, (byte)(MMU.readByte(hl) & mask));
                Z80.Registers.m = 4; Z80.Registers.t = 16;
                return true;
            }
        }
    }
}
