using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UCS.Core.Threading
{
    internal class InterfaceThread
    {
        private static Thread T { get; set; }
        private static string Command { get; set; }


        public static void Start()
        {
            T = new Thread(() =>
            {
                //InterfaceThread.T = new Thread(null);
                InterfaceThread.T.SetApartmentState(ApartmentState.STA);
                InterfaceThread.T.Start();
            });
            T.Start();
        }
           

    public static void Stop()
    {
        if (T.ThreadState == ThreadState.Running)
            T.Abort();
    }
}
}
