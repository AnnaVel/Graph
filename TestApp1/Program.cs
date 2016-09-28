using GraphCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            int numberOfVertices = 1000000;

            for (int i = 0; i < numberOfVertices; i++)
            {
                graph.AddVertex(i).SetProperty("prop", i);
            }

            Thread.Sleep(5000);
        }
    }
}
