using IpcimWPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csudh
{
    internal class Program
    {
        static List<DomainEntry> entries = new List<DomainEntry>();

        static void Main(string[] args)
        {
            ReadFile();
            PrintList();
        }

        
    }
}