using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Returns true of the cell is exactly 1 capital letter followed by exactly 1 number.
        /// </summary>
        public bool IsValid(string cell)
        {
            if (cell == null)
            {
                return false;
            }

            string validPattern = @"^[A-Z][0-9]$";
            if (Regex.IsMatch(cell, validPattern))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method used to convert a cell name to its standard form.  For example,
        /// Normalize might convert names to upper case.
        /// </summary>
        public string Normalize(string cell)
        {
            return cell;
        }

        /// <summary>
        /// Converts all characters in a string that are letters to upper case.
        /// </summary>
        /// <param name="s">The string to be converted</param>
        /// <returns>Upper case string</returns>
        public string toUpper(String s)
        {
            return s.ToUpper();
        }

        ///#################### SETTING CELLS TESTS #######################################

        /// <summary>
        /// Constructor test
        /// </summary>
        [TestMethod]
        public void Constructors()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            AbstractSpreadsheet sheet2 = new Spreadsheet(IsValid, Normalize, "default");
            AbstractSpreadsheet sheet3 = new Spreadsheet("SS.xml", IsValid, Normalize, "default");
        }

        /// <summary>
        /// Setting content as a double with simple constructor
        /// </summary>
        [TestMethod]
        public void SetContentDouble()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "33.0"));
            Assert.AreEqual(33.0 , sheet.GetCellContents("A1"));
            Assert.AreEqual(33.0, sheet.GetCellValue("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
        }

        /// <summary>
        /// Setting content as a double with simple constructor
        /// </summary>
        [TestMethod]
        public void SetContentDouble2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "33.0"));
            dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "100.0"));
            Assert.AreEqual(100.0, sheet.GetCellContents("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
        }

        /// <summary>
        /// Setting content as a string with simple constructor
        /// </summary>
        [TestMethod]
        public void SetContentString()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "Hello World!"));
            Assert.AreEqual("Hello World!", sheet.GetCellContents("A1"));
            Assert.AreEqual("Hello World!", sheet.GetCellValue("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
        }

        /// <summary>
        /// Setting content as a string with simple constructor
        /// </summary>
        [TestMethod]
        public void SetContentString2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "Hello World!"));
            dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "Goodbye World!"));
            Assert.AreEqual("Goodbye World!", sheet.GetCellContents("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
        }

        /// <summary>
        /// Setting content as a formula with simple constructor
        /// </summary>
        [TestMethod]
        public void SetContentFormula()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "=1+2"));
            dependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "=2+2"));
            Formula actual = new Formula("2+2");
            Assert.AreEqual(actual, sheet.GetCellContents("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
        }

        /// <summary>
        /// Testing for valid cell and variable names
        /// </summary>
        [TestMethod]
        public void validCellNames()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "33.0");
            sheet.SetContentsOfCell("b1", "Hello World!");
            sheet.SetContentsOfCell("myDogsNaMaeisMaE6584", "=4+3");
            
        }

        /// <summary>
        /// Get the value of a cell dependent on many cells.
        /// </summary>
        [TestMethod]
        public void GetValue()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "33.0");
            sheet.SetContentsOfCell("A2", "20.1");
            sheet.SetContentsOfCell("A3", "3.5");
            sheet.SetContentsOfCell("A4", "=A1 + A2");
            sheet.SetContentsOfCell("A5", "=A2 + A2 * 4");
            sheet.SetContentsOfCell("B1", "=A1 - A2 * A3 / A5 + A4");
            double value = (double) sheet.GetCellValue("B1");
            Assert.AreEqual(85.4, value, 1e-9);
            sheet.SetContentsOfCell("A2", "2");
            value = (double)sheet.GetCellValue("B1");
            Assert.AreEqual(67.3, value, 1e-9);
        }

        ///####################### FORMULA TESTS ##########################################

        /// <summary>
        /// Setting the content and value of a formula
        /// </summary>
        [TestMethod]
        public void SetFormula()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=1+2");
            Assert.AreEqual(3.0, sheet.GetCellValue("A1"));
        }

        /// <summary>
        /// Setting the content and value of a formula
        /// </summary>
        [TestMethod]
        public void SetFormula2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "30");
            sheet.SetContentsOfCell("A1", "=B1+2");    
            Assert.AreEqual(32.0, sheet.GetCellValue("A1"));
        }

        /// <summary>
        /// Setting the content and value of a formula
        /// </summary>
        [TestMethod]
        public void SetFormula3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1+2");
            Object test = sheet.GetCellValue("A1");
            Assert.IsInstanceOfType(test, typeof(FormulaError));
        }

        /// <summary>
        /// Updating the content of a cell that held a formula with a formula.
        /// </summary>
        [TestMethod]
        public void SetFormula4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "5");
            sheet.SetContentsOfCell("A1", "=B1+2");
            sheet.SetContentsOfCell("A1", "=B1+3");
            Object test = sheet.GetCellValue("A1");
            Assert.AreNotEqual(7.0, test);
            Assert.AreEqual(8.0, test);
        }

        /// <summary>
        /// Updating the content of a cell that held a formula with a double.
        /// </summary>
        [TestMethod]
        public void SetFormula5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "5");
            sheet.SetContentsOfCell("A1", "=B1+2");
            sheet.SetContentsOfCell("A1", "10");
            Object test = sheet.GetCellValue("A1");
            Assert.AreNotEqual(7.0, test);
            Assert.AreEqual(10.0, test);
        }

        /// <summary>
        /// Updating the content of a cell that held a formula with a string.
        /// </summary>
        [TestMethod]
        public void SetFormula6()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "5");
            sheet.SetContentsOfCell("A1", "=B1+2");
            sheet.SetContentsOfCell("A1", "Hello World!");
            Object test = sheet.GetCellValue("A1");
            Assert.AreNotEqual(7.0, test);
            Assert.AreEqual("Hello World!", test);
        }

        /// <summary>
        /// Updating the content of a cell that held a formula with a string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void SetFormula7()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "=4 + *");
        }

        /// <summary>
        /// Recalculating the value of cells that hold formulas
        /// </summary>
        [TestMethod]
        public void CellRecalculation()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("B1", "5");
            sheet.SetContentsOfCell("A1", "=B1+2");
            Assert.AreEqual(7.0, sheet.GetCellValue("A1"));
            sheet.SetContentsOfCell("B1", "10.1");
            Assert.AreEqual(12.1, sheet.GetCellValue("A1"));
            sheet.SetContentsOfCell("B1", "Hello Error!");
            Assert.IsTrue(sheet.GetCellValue("A1") is FormulaError);
        }

        ///####################### DEPENDENCIES TESTS #####################################

        /// <summary>
        /// Sets a circular dependency
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependency()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=A1 + 1");
        }

        /// <summary>
        /// Sets a circular dependency
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependency2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1 + 1");
            sheet.SetContentsOfCell("B1", "=C1 + 2");
            sheet.SetContentsOfCell("C1", "=A1 + 3");
        }

        /// <summary>
        /// Sets a circular dependency
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependency3()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1 + 1");
            sheet.SetContentsOfCell("B1", "33");
            sheet.SetContentsOfCell("C1", "=4 + 3");
            sheet.SetContentsOfCell("B1", "=A1 + 3");
        }

        /// <summary>
        /// Sets a circular dependency
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependency4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "=B1 + 1");
            sheet.SetContentsOfCell("A2", "33");
            sheet.SetContentsOfCell("B1", "=A2 + 1");
            sheet.SetContentsOfCell("B1", "=A1 + 3");
        }

        /// <summary>
        /// All dependencies of cell are correct
        /// </summary>
        [TestMethod]
        public void AllDependents()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            HashSet<string> allDependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "15"));
            Assert.IsTrue(allDependents.Count == 1);
            sheet.SetContentsOfCell("A2", "=A1 + 1");
            sheet.SetContentsOfCell("B1", "=A1 + 1");
            sheet.SetContentsOfCell("B2", "=A1 + 1");
            sheet.SetContentsOfCell("B3", "=A1 + 1");
            sheet.SetContentsOfCell("B3", "4 + 1");
            allDependents = new HashSet<string>(sheet.SetContentsOfCell("A1", "16"));
            Assert.IsTrue(allDependents.Count == 4);
            Assert.IsTrue(allDependents.Contains("A1"));
            Assert.IsTrue(allDependents.Contains("A2"));
            Assert.IsTrue(allDependents.Contains("B1"));
            Assert.IsTrue(allDependents.Contains("B2"));
            Assert.IsFalse(allDependents.Contains("B3"));
        }

        ///####################### NULL/EMPTY TESTS #######################################

        /// <summary>
        /// Setting a cell with a null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellNameNull()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            string name = null;
            sheet.SetContentsOfCell(name, "33.0");
        }

        /// <summary>
        /// Set a cells contents to null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellStringNull()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            string text = null;
            sheet.SetContentsOfCell("A1", text);
        }

        /// <summary>
        /// Testing GetCellContent with a null value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsNull()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellContents(null);
        }

        /// <summary>
        /// Testing GetCellValue with a null name value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueNull()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue(null);
        }

        ///####################### INVALID NAME TESTS #####################################

        /// <summary>
        /// Setting a cell with an invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellNameInvalid()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("%1", "33.0");
        }


        /// <summary>
        /// Getting the cell value with an invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueInvalid()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue("%1");
        }

        /// <summary>
        /// isValid fails for one of the variables
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void IsValidFails()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => false, s => s, "test");
            sheet.SetContentsOfCell("A1", "3 + 4");
        }


        ///####################### OTHER METHOD TESTS #####################################

        /// <summary>
        /// Testing GetDirectDependents with a null cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDirectDependentsTest()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            PrivateObject sheetPrivate = new PrivateObject(sheet);
            string test = null;
            sheetPrivate.Invoke("GetDirectDependents", test);
        }

        /// <summary>
        /// Testing GetDirectDependents with an invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetDirectDependentsTest2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            PrivateObject sheetPrivate = new PrivateObject(sheet);
            string test = "*";
            sheetPrivate.Invoke("GetDirectDependents", test);
        }

        /// <summary>
        /// Get the contents of a cell that hasn't been assigned yet.
        /// </summary>
        [TestMethod]
        public void GetCellContents()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Object contents = sheet.GetCellContents("A1");
            Assert.AreEqual("", contents);
        }

        /// <summary>
        /// Get the value of a cell that hasn't been assigned yet.
        /// </summary>
        [TestMethod]
        public void GetCellValue()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Object contents = sheet.GetCellValue("A1");
            Assert.AreEqual("", contents);
        }

        /// <summary>
        /// Tests if Changed is set properly
        /// </summary>
        [TestMethod]
        public void Changed()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            Assert.IsTrue(sheet.Changed == false);
            sheet.SetContentsOfCell("A1", "Hello World!");
            Assert.IsTrue(sheet.Changed == true);
            sheet.Save("SS.xml");
            Assert.IsTrue(sheet.Changed == false);
            sheet.SetContentsOfCell("A1", "33.0");
            Assert.IsTrue(sheet.Changed == true);            
        }

        ///####################### NORMALIZED/ISVALID TESTS ####################################

        /// <summary>
        /// Setting content as a string and normalizing the cell name
        /// </summary>
        [TestMethod]
        public void SetContentStringNormalized()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, toUpper, "testing");
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("a1", "Hello World!"));
            Assert.AreEqual("Hello World!", sheet.GetCellContents("A1"));
            Assert.AreEqual("Hello World!", sheet.GetCellValue("A1"));
            Assert.IsTrue(dependents.Contains("A1"));
            HashSet<string> cells = new HashSet<string>(sheet.GetNamesOfAllNonemptyCells());
            Assert.IsTrue(cells.Contains("A1"));
            Assert.IsFalse(cells.Contains("a1"));
        }

        /// <summary>
        /// Setting content as a formula and normalizing the cell name and formula
        /// </summary>
        [TestMethod]
        public void SetContentFormulaNormalized()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, toUpper, "testing");
            HashSet<string> dependents = new HashSet<string>(sheet.SetContentsOfCell("a1", "=b1 + 4"));
            HashSet<string> cells = new HashSet<string>(sheet.GetNamesOfAllNonemptyCells());
            Formula test = (Formula) sheet.GetCellContents("A1");
            Assert.AreEqual("B1+4", test.ToString());
            Assert.IsTrue(cells.Contains("A1"));
            Assert.IsFalse(cells.Contains("a1"));
        }

        /// <summary>
        /// Using a cell name that IsValid would return false on.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void IsValidTest()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(IsValid, s => s, "testing");
            sheet.SetContentsOfCell("a1", "Hello Error!");
        }

        ///####################### SAVE/LOAD TESTS ########################################

        /// <summary>
        /// Saving and loading a spreadsheet
        /// </summary>
        [TestMethod]
        public void SaveAndLoad()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "Goodbye World!");
            sheet.SetContentsOfCell("A2", "33.2");
            sheet.SetContentsOfCell("A3", "");
            sheet.SetContentsOfCell("A4", "33");
            sheet.SetContentsOfCell("A5", "Hello World!");
            sheet.SetContentsOfCell("A6", "=33 + 12");
            sheet.Save("SS.xml");
            string version = sheet.GetSavedVersion("SS.xml");
            Assert.AreEqual("default", version);
            AbstractSpreadsheet loadedSheet = new Spreadsheet("SS.xml", IsValid, Normalize, "default");
            HashSet<string> allCells = new HashSet<string>(loadedSheet.GetNamesOfAllNonemptyCells());
            Assert.IsTrue(allCells.Contains("A1"));
            Assert.IsTrue(allCells.Contains("A2"));
            Assert.IsTrue(allCells.Contains("A4"));
            Assert.IsTrue(allCells.Contains("A5"));
            Assert.IsTrue(allCells.Contains("A6"));
        }

        /// <summary>
        /// Saving and loading a spreadsheet
        /// </summary>
        [TestMethod]
        public void SaveAndLoad2()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, Normalize, "SaveAndLoad2");
            //Add 100 cells
            for (int i = 0; i < 100; i++)
            {
                string cellName = "A" + i;
                string content = i.ToString();
                sheet.SetContentsOfCell(cellName, content);
            }
            //Save the SS
            sheet.Save("SS2.xml");
            string version = sheet.GetSavedVersion("SS2.xml");
            Assert.AreEqual("SaveAndLoad2", version);
            //Load the SS
            AbstractSpreadsheet loadedSheet = new Spreadsheet("SS2.xml", s => true, Normalize, "SaveAndLoad2");
            HashSet<string> allCells = new HashSet<string>(loadedSheet.GetNamesOfAllNonemptyCells());
            //See if all 100 cells are in new SS
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(allCells.Contains("A" + i));
            }
        }

        /// <summary>
        /// Loading the demo spreadsheet from PS6
        /// </summary>
        [TestMethod]
        public void SaveAndLoad3()
        {
            AbstractSpreadsheet loadedSheet = new Spreadsheet("demo.sprd", IsValid, Normalize, "ps6");
            HashSet<string> allCells = new HashSet<string>(loadedSheet.GetNamesOfAllNonemptyCells());
            Assert.IsTrue(allCells.Contains("A1"));
            Assert.AreEqual(2.0, loadedSheet.GetCellContents("A1"));
            Assert.IsTrue(allCells.Contains("A2"));
            Assert.AreEqual(2.0, loadedSheet.GetCellContents("A2"));
            Assert.IsTrue(allCells.Contains("A3"));
            Assert.AreEqual(3.0, loadedSheet.GetCellContents("A3"));
            Assert.IsTrue(allCells.Contains("A4"));
            Assert.AreEqual(4.0, loadedSheet.GetCellContents("A4"));
            Assert.IsTrue(allCells.Contains("B1"));
            Formula formula1 = new Formula("A1/A2");
            Assert.AreEqual(formula1 , loadedSheet.GetCellContents("B1"));
            Assert.AreEqual(1.0, loadedSheet.GetCellValue("B1"));
            Assert.IsTrue(allCells.Contains("B2"));
            Formula formula2 = new Formula("A3*A4");
            Assert.AreEqual(formula2 , loadedSheet.GetCellContents("B2"));
            Assert.AreEqual(12.0, loadedSheet.GetCellValue("B2"));
            Assert.IsTrue(allCells.Contains("C1"));
            Formula formula3 = new Formula("B1+B2");
            Assert.AreEqual(formula3 , loadedSheet.GetCellContents("C1"));
        }

        /// <summary>
        /// Loading a spreadsheet with a bad filename
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveAndLoadError()
        {
            AbstractSpreadsheet loadedSheet = new Spreadsheet("BadPath.xml", IsValid, Normalize, "version 2");
        }

        /// <summary>
        /// Loading a spreadsheet that is an empty file
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveAndLoadError2()
        {
            AbstractSpreadsheet loadedSheet = new Spreadsheet("empty.xml", IsValid, Normalize, "version 2");
        }

        /// <summary>
        /// Loading a spreadsheet with a null file
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveAndLoadError3()
        {
            AbstractSpreadsheet loadedSheet = new Spreadsheet(null , IsValid, Normalize, "version 2");
        }

        /// <summary>
        /// Save a file with an invalid destination
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveAndLoadError4()
        {
            AbstractSpreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("A1", "Goodbye World!");
            sheet.SetContentsOfCell("A2", "33.2");
            sheet.SetContentsOfCell("A3", "");
            sheet.SetContentsOfCell("A4", "33");
            sheet.SetContentsOfCell("A5", "Hello World!");
            sheet.SetContentsOfCell("A6", "=33 + 12");
            sheet.Save("W:\\Bad.xml");
        }

        /// <summary>
        /// Load a spreadsheet with a different version
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SaveAndLoadError5()
        {
            AbstractSpreadsheet sheet = new Spreadsheet(s => true, s => s, "Version 1.0");
            sheet.SetContentsOfCell("A1", "Hello World!");
            sheet.Save("SS3.xml");
            AbstractSpreadsheet loadedSheet = new Spreadsheet("SS3.xml", IsValid, Normalize, "Version 1.1");
        }
    }
}
