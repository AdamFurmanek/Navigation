using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        for(int i = 0; i < 10; i++)
        {
            Map map = new Map(30, 40);
            map.FindPath(map.GetVertex(0, 0), map.GetVertex(29, 39));
            map.PrintMap();
            Console.WriteLine(map.searching.Count);
            map.Reset();
        }
    }
}
