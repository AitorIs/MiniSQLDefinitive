﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Clases;
using MiniSQL.Classes;
using MiniSQL.Constants;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test
{
    [TestClass]
    public class TestColumn
    {

        [TestMethod]
        public void ExistCells_Exist_ReturnTrue_StrType()
        {
            List<string> dataToWork = TestColumn.CreateStringCellData();
            Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaa");
            Assert.IsTrue(dataToWork.Count > 0);
            Assert.IsTrue(column.ExistCells(dataToWork[0]));
        }

        [TestMethod]
        public void ExistCells_NoExist_ReturnFalse_StrType()
        {
            List<string> dataToWork = TestColumn.CreateStringCellData();
            Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
            string randomStr = "uwu";
            while (dataToWork.Contains(randomStr)) {
                randomStr = VariousFunctions.GenerateRandomString(8);
            }
            Assert.IsFalse(dataToWork.Contains(randomStr));            
            Assert.IsTrue(dataToWork.Count > 0);
            Assert.IsFalse(column.ExistCells(randomStr));
        }

        [TestMethod]
        public void AddCell_NoExistsCellsWithSameData_StrType() 
        {
            List<string> dataToWork = TestColumn.CreateStringCellData();
            Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
            string randomStr = "uwu";
            while (dataToWork.Contains(randomStr))
            {
                randomStr = VariousFunctions.GenerateRandomString(8);
            }
            Assert.IsFalse(dataToWork.Contains(randomStr));
            Assert.IsFalse(column.ExistCells(randomStr));
            column.AddCell(ObjectConstructor.CreateCell(column, randomStr, null));
            Assert.IsTrue(column.ExistCells(randomStr));
        }

        [TestMethod]
        public void AddCell_ExistCellsWithSameData_StrType()
        {
            List<string> dataToWork = TestColumn.CreateStringCellData();
            Column column = ObjectConstructor.CreateColumn(dataToWork, TypesKeyConstants.StringTypeKey, "aaaa");
            int index = 0;
            Assert.IsTrue(dataToWork.Count > 0);
            Assert.IsTrue(column.ExistCells(dataToWork[index]));
            int numberOfCellWithSameDataBeforeAddition = column.GetCells(dataToWork[index]).Count;
            column.AddCell(ObjectConstructor.CreateCell(column, dataToWork[index], null));
            Assert.AreEqual(numberOfCellWithSameDataBeforeAddition + 1, column.GetCells(dataToWork[index]).Count);
        }

        public static List<string> CreateStringCellData() 
        {
            List<string> cellData = new List<string>();
            cellData.Add("P1");
            cellData.Add("P2");
            cellData.Add("P3");
            cellData.Add("P4");
            cellData.Add("P5");
            cellData.Add("P6");
            cellData.Add("P7");
            return cellData;
        }



    }
}