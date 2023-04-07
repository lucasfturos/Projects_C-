namespace PenduloDuploASCII
{
    using System;
    using PenduloDuplo;

    public class Program : PenduloDuplo
    {
        public static void Main()
        {
            PenduloDuplo p = new PenduloDuplo();
            while (true)
            {
                Console.Clear();
                p.draw();
            }
        }
    }
}
