﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class XMLTagsConstants
    {

        public const string DatabasePropertiesRootElementTag_WR = "database";
        public const string DatabasePropertiesTableElementTag_WR = "table";
        public const string DatabasePropertiesTableElementNameTag_WR = "name";

        public const string TableStructureRootElementTag_WR = "table";
        public const string TableStructureColumnElementTag_WR = "column";
        public const string TableStructureColumnNameTag_WR = "columnName";
        public const string TableStructureColumnDataTypeTag_WR = "columnDataType";
        public const string TableDataRootElementTag_WR = "data";
        public const string TableDataRowElementTag_WR = "row";
        public const string TableDataCellElementTag_WR = "cell";
        public const string TableDataCellColumnNameAtributeTag_WR = "columnName";

        public const string PrimaryKeyElementTag_WR = "primaryKey";
        public const string ForeignKeyElementTag_WR = "foreignKey";
        public const string ForeignKeyPairElementTag_WR = "pair";
        public const string ReferedColumnElementTag_WR = "referedColumn";


    }
}
