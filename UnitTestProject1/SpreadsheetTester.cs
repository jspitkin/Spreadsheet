using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;

namespace SpreadsheetTester
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
        }
    }
}
