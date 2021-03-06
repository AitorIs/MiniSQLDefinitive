﻿using MiniSQL.Clases;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using MiniSQL.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class ParserBuilder
    {
        private AbstractParser parser;

        public void SetDataFormatManager(string dataFormatManagerVersion) 
        {
            IntType.GetIntType().SetDataFormatManager(DataFormatFactory.GetDataFormatFactory().GetSaveDataFormatManager(SaveDataFormatVersions.NothingToDoDataFormatManagerVersion));
            DoubleType.GetDoubleType().SetDataFormatManager(DataFormatFactory.GetDataFormatFactory().GetSaveDataFormatManager(SaveDataFormatVersions.NothingToDoDataFormatManagerVersion));
            StringType.GetStringType().SetDataFormatManager(DataFormatFactory.GetDataFormatFactory().GetSaveDataFormatManager(dataFormatManagerVersion));
        }

        public void SetUbicationManager(string ubicationManager) 
        {
            IUbicationManager manager = UbicationManagerFactory.GetUbicationManagerFactory().GetUbicationManager(ubicationManager);
            if (!Directory.Exists(manager.GetRootDirectoryPath())) Directory.CreateDirectory(manager.GetRootDirectoryPath());
            this.parser.SetUbicationManager(manager);           
        }

        protected void SetParser(AbstractParser parser) 
        {
            this.parser = parser;
        }

        public AbstractParser GetParser() 
        {
            return this.parser;
        }


    }
}
