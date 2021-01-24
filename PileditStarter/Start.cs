using System;

namespace PileditBackendStarter
{
    class Start
    {
        public static void Main(string[] args)
        {
            for(int i = 0;i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("--"))
                {

                }
                else if (arg.StartsWith('-') && i < args.Length - 1)
                {
                    i++;
                    var value = args[i];
                }
                else continue;
                break;
            }
            PileditBackendServer.Start.ServerStart(args);
        }
    }
}
