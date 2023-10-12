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
                Console.Write("\f\u001bc\x1b[3J");
                p.draw();
            }
        }
    }
}
