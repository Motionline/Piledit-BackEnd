using System;
using System.ComponentModel;
using System.IO;
using PileditBackend.IO;
using static PileditBackend.PileditSystem;

namespace PileditBackend
{
    public static class Start
    {
        public static void ProgramStart(string[] args)
        {
            if (args == null) args = Array.Empty<string>();
            Starting(args);
            Ending();
        }

        private static void Starting(string[] args)
        {
            /*for (int i = 0;i < args.Length;i++)
            {
                var arg = args[i];
                if (arg == "--outinfo") ConsoleInfo = true;
            }*/
        }

        private static void Ending(int code = 0)
        {
            Log.Info("プログラム終了");
            Environment.Exit(code);
        }

        private static void Main() { }
    }
}
