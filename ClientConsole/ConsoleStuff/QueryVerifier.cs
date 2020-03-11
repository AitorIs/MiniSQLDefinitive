﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class QueryVerifier
    {
        private static QueryVerifier queryVerifier;
        public List<Tuple<string, Func<string, string>>> protocolesAndTheirConsolePatterns;
        //[^\* ,<=>\(\)]
        public static string tableGroup = "?<table>";
        public static string selectedColumnGroup = "?<selectedColumn>";
        public static string toEvaluateColumnGroup = "?<toEvalColumn>";
        public static string valueGroup = "?<value>";
        public static string operatorGroup = "?<operator>";
        public static string evalValue = "?<evalValue>";

        public static string NAINCG = "[^\\* ,<=>\\(\\)]";
        public static string wherePatern = "WHERE (" + toEvaluateColumnGroup + NAINCG + "+)(" + operatorGroup + "[<=>])(" + evalValue + NAINCG + "+)";
        public static string fromPattern = "FROM (?:(" + tableGroup + NAINCG + "+)|(?:(" + tableGroup + NAINCG + "+) "+ wherePatern+"))";
        //^SELECT (?:(?<selectedColumn>\*)|(?<selectedColumn>[^\* ,<=>\(\)]+)(?:,(?<selectedColumn>[^\* ,<=>\(\)]+))*) FROM (?:(?<table>[^\* ,<=>\(\)]+)|(?:(?<table>[^\* ,<=>\(\)]+) WHERE (?<toEvalColumn>[^\* ,<=>\(\)]+)(?<operator>[<=>])(?<evalValue>[^\* ,<=>\(\)]+));)$
        public static string selectPattern = "^SELECT (?:(\\*)|(" + selectedColumnGroup + NAINCG + "+)(?:,(" + selectedColumnGroup + NAINCG + "+))*) " + fromPattern + ";$";
        //^INSERT INTO (?<table>[^\* ,<=>\(\)]+)(?:\((?<selectedColumn>[^\* ,<=>\(\)]+)(?:,(?<selectedColumn>[^\* ,<=>\(\)]+))*\))? VALUES\((?<value>[^\* ,<=>\(\)]+)(?:,(?<value>[^\* ,<=>\(\)]+))*\);$                                                                                        
        public static string insertPattern = "^INSERT INTO (" + tableGroup + NAINCG + "+)(?:\\((" + selectedColumnGroup + NAINCG +"+)(?:,(" + selectedColumnGroup + NAINCG + "+))*\\))? VALUES\\((" + valueGroup + NAINCG + "+)(?:,(" + valueGroup + NAINCG + "+))*\\);$";

        private QueryVerifier() 
        {
            this.protocolesAndTheirConsolePatterns = new List<Tuple<string, Func<string, string>>>();        
        }

        private void AddTheProtocolesFunctions() 
        { 
        
        
        }

       


        public static QueryVerifier GetQueryVerifier() 
        {
            if (queryVerifier == null) queryVerifier = new QueryVerifier();
            return queryVerifier;
        }

    }
}
