using System;

class Program
{
    static void Main(string[] args)
    {
        Map map = new Map(10, 10);
        map.PrintMap();
        map.FindPath(map.GetVertex(0,0), map.GetVertex(9,9));
        Console.WriteLine();
        map.PrintMap();
    }
}
