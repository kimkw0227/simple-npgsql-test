using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;

namespace simple_npgsql_test
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            SimpleCypherTest sct = new SimpleCypherTest();

            sct.createTest();
            sct.matchTest();
            sct.deleteTest();
        }
    }
}