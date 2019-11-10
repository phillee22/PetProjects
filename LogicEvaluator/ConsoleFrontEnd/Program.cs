using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicEvalLib;

namespace ConsoleFrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adding a change..
            const string MARKER = "-------------------";
            LogicTree tree = new LogicTree();

            Console.WriteLine(MARKER);
            LogicNode root = tree.Parse("!p^q");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("p^qVr");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("p^(qVr)");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("p^!(qVr)");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("(p^q)Vr");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("p^(qVr)^s");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("(p^(qVr))");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("((p^q)Vr)");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("(!(p^(qVr)))");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");

            Console.WriteLine(MARKER);
            root = tree.Parse("(p^(!(qVr)))");
            Console.WriteLine("Node.ToString() == " + root.ToString() + "\r\n");
        }
    }
}
