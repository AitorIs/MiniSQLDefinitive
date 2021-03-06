﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestInsert
    {

        [TestMethod]
        public void Insert_BadArguments_ConcretelyTableDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestInsert1");
            databaseContainer.AddDatabase(database);
            string doenstExistTableNames = VariousFunctions.GenerateRandomString(6);
            while (database.ExistTable(doenstExistTableNames))
            {
                doenstExistTableNames = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(database.ExistTable(doenstExistTableNames));
            Insert insert = CreateInsert(databaseContainer, database.databaseName, doenstExistTableNames);
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
        }

        [TestMethod]
        public void Insert_BadArguments_TheDataTypesDontMatch_NoticeInValidateParameters()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestInsert2");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            string[] rowData = new string[] { "aaa", "aaa" };
            Assert.IsFalse(column1.dataType.IsAValidDataType(rowData[0]) && column2.dataType.IsAValidDataType(rowData[1]));
            int rowCount = table.GetRowCount();
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            insert.AddValue(rowData[0]);
            insert.AddValue(rowData[1]);
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
            Assert.AreEqual(rowCount, table.GetRowCount());
        }

        [TestMethod]
        public void InsertBadArguments_ConcretelyTooMuchParameters_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestInsert2");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            int rowCount = table.GetRowCount();
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            string[] rowData = { "aaa", "1", "aaaa" };            
            insert.AddValue(rowData[0]);
            insert.AddValue(rowData[1]);
            insert.AddValue(rowData[2]);
            Assert.IsTrue(table.GetColumnCount() < rowData.Length);
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
            Assert.AreEqual(rowCount, table.GetRowCount());
        }

        [TestMethod]
        public void InsertBadArguments_ConcretelyNotEnougthParameters_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("TestInsert2");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            int rowCount = table.GetRowCount();
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            string[] rowData = { "aaa" };
            insert.AddValue(rowData[0]);
            Assert.IsTrue(table.GetColumnCount() > rowData.Length);
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
            Assert.AreEqual(rowCount, table.GetRowCount());
        }

        [TestMethod]
        public void Insert_BadArguments_ConcretelyDatabaseDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string doenstExistDatabaseNames = VariousFunctions.GenerateRandomString(6);
            while (databaseContainer.ExistDatabase(doenstExistDatabaseNames))
            {
                doenstExistDatabaseNames = VariousFunctions.GenerateRandomString(6);
            }
            Assert.IsFalse(databaseContainer.ExistDatabase(doenstExistDatabaseNames));
            Insert insert = CreateInsert(databaseContainer, doenstExistDatabaseNames, "aa");
            insert.AddValue("zz");
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
        }

        [TestMethod]
        public void Insert_GodArguments_TheQueryInsertTheNewsRows()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string c1FirstRowData = "aaa";
            string c2FirstRowData = "1";
            Database database = new Database("TestInsert3");
            ITable table = new Table("table1");
            Column column1 = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2 = new Column("c2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            Assert.IsTrue(column1.dataType.IsAValidDataType(c1FirstRowData));
            Assert.IsTrue(column2.dataType.IsAValidDataType(c2FirstRowData));
            table.AddColumn(column1);
            table.AddColumn(column2);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            int rowCount = table.GetRowCount();
            Select firstSelect = TestSelect.CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            firstSelect.ValidateParameters();
            firstSelect.Execute();
            Assert.AreEqual(0, firstSelect.GetAfectedRowCount());
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            insert.AddValue(c1FirstRowData);
            insert.AddValue(c2FirstRowData);
            Assert.IsTrue(insert.ValidateParameters());
            insert.Execute();
            Select secondSelect = TestSelect.CreateSelect(databaseContainer, database.databaseName, table.tableName, true);
            secondSelect.ValidateParameters();
            secondSelect.Execute();
            Assert.IsTrue(firstSelect.GetAfectedRowCount() < secondSelect.GetAfectedRowCount());
            Assert.IsTrue(column1.GetCells(c1FirstRowData).Count > 0);
            Assert.IsTrue(column2.GetCells(c2FirstRowData).Count > 0);
            Assert.AreEqual(rowCount + 1, table.GetRowCount());
        }

        [TestMethod]
        public void Insert_BadArguments_PKViolated_NoticeInValidate()
        {
            //Construct
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("database");
            ITable table = new Table("table");
            Column column = new Column("c1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            table.primaryKey.AddKey(column);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            Row row = table.CreateRowDefinition();
            row.GetCell(column.columnName).data = "aaaa";
            table.AddRow(row);
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
            insert.AddValue(row.GetCell(column.columnName).data);
            //Test
            Assert.IsFalse(insert.ValidateParameters());
            insert.Execute();
        }

        [TestMethod]
        public void InsertInTableWithPK_GoodArgument_TheQueryInsertsTheNewRows()
        {
            //Construct
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("database");
            ITable table = new Table("table1");
            Column column = new Column("c1t1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table.AddColumn(column);
            table.primaryKey.AddKey(column);
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            //Test
            bool b = true;
            int limit = 100;
            Insert insert;
            for (int i = 0; i < limit && b; i++)
            {
                insert = CreateInsert(databaseContainer, database.databaseName, table.tableName);
                insert.AddValue(i + "");
                b = insert.ValidateParameters();
                insert.Execute();
            }
            Assert.IsTrue(b);
            Assert.AreEqual(limit, table.GetRowCount());
        }

        [TestMethod]
        public void InsertInTableWithFK_BadArguments_FKViolated_NoticeInValidate()
        {
            //Construct
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("database");
            ITable table1 = new Table("table1");
            Column columnt1 = new Column("c1t1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table1.AddColumn(columnt1);
            table1.primaryKey.AddKey(columnt1);
            database.AddTable(table1);
            ITable table2 = new Table("table2");
            Column column1t2 = new Column("c1t2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2t2 = new Column("c2t2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table2.AddColumn(column1t2);
            table2.AddColumn(column2t2);
            table2.primaryKey.AddKey(column1t2);
            table2.foreignKey.AddForeignKey(column2t2, columnt1);
            database.AddTable(table2);
            databaseContainer.AddDatabase(database);
            //Add some data
            Row row = table1.CreateRowDefinition();
            row.GetCell(columnt1.columnName).data = "aa";
            table1.AddRow(row);
            //Test
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table2.tableName);
            insert.AddValue(row.GetCell(columnt1.columnName).data + "a"); //it is not the fk column
            insert.AddValue(row.GetCell(columnt1.columnName).data + "a"); //it is the fk column
            Assert.IsFalse(insert.ValidateParameters());
        }

        [TestMethod]
        public void InsertInTableWithFK_GoodArgument_TheQueryInsertTheNewsRows()
        {
            //like the other fk test, but now not violating the FK
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("database");
            ITable table1 = new Table("table1");
            Column columnt1 = new Column("c1t1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table1.AddColumn(columnt1);
            table1.primaryKey.AddKey(columnt1);
            database.AddTable(table1);
            ITable table2 = new Table("table2");
            Column column1t2 = new Column("c1t2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            Column column2t2 = new Column("c2t2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey));
            table2.AddColumn(column1t2);
            table2.AddColumn(column2t2);
            table2.primaryKey.AddKey(column1t2);
            table2.foreignKey.AddForeignKey(column2t2, columnt1);
            database.AddTable(table2);
            databaseContainer.AddDatabase(database);
            //Add some data
            Row row = table1.CreateRowDefinition();
            row.GetCell(columnt1.columnName).data = "aa";
            table1.AddRow(row);
            //Test
            Insert insert = CreateInsert(databaseContainer, database.databaseName, table2.tableName);
            insert.AddValue(row.GetCell(columnt1.columnName).data); //it is not the fk column
            insert.AddValue(row.GetCell(columnt1.columnName).data); //it is the fk column
            int numberOfColumn = table2.GetRowCount();
            Assert.IsTrue(insert.ValidateParameters());
            insert.Execute();
            Assert.AreEqual(numberOfColumn + 1, table2.GetRowCount());
        }
        public Insert CreateInsert(IDatabaseContainer databaseContainer, string databaseName, string tableName)
        {
            Insert insert = new Insert(databaseContainer);
            insert.targetDatabase = databaseName;
            insert.targetTableName = tableName;
            return insert;
        }
    }
}