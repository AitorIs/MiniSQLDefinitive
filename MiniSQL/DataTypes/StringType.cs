﻿using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Clases
{
    class StringType : IDataType
    {

        private static StringType stringType;

        private StringType() 
        { 
        
        }

        public static StringType GetStringType() 
        {
            if (stringType == null) {
                stringType = new StringType();
            }
            return stringType;        
        }


        public bool IsAValidDataType(string value)
        {
            throw new NotImplementedException();
        }
    }
}
