using System;

class Program
{
    static void Main(string[] args)
    {
        GUI gui = new GUI(256, 128);

        while (!gui.IsClosed)
            gui.Main();
    }
}