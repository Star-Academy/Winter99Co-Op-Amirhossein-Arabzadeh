﻿using System;
using Nest;
using Phase10Library;

namespace ViewRun
{
    class Program
    {
        static void Main(string[] args)
        {

            IMyElasticClient2 elasticClient = new MyElasticClient();

            View view = new View();
            view.Run();
        }
    }
}