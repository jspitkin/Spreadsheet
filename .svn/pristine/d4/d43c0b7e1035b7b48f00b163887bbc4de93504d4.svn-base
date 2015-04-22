using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <summary>
    /// Represents a Spreadsheet using the Cell class. Spreadsheet
    /// keeps track of all the cells contained in the spreadsheet
    /// that have values as well as the cells dependency graph.
    /// A cell in the spreadsheet can contain a string, double, or formula.
    /// A formula will be evaluated (Using the Formula classes Evaluate method) 
    /// to a double or a FormulaError object.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// A collection of all the cells contained in the Spreadsheet.
        /// The key is the name of the cell and the value is the cell
        /// object itself.
        /// </summary>
        private Dictionary<string, Cell> cells;

        /// <summary>
        /// A graph showing which cells (if any) a cell depends on to calculate it's value.
        /// A cell cannot depend on itself, this is called a circular dependency.
        /// </summary>
        private DependencyGraph dependencyGraph;

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved                  
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed { get; protected set; }

        /// <summary>
        /// Contructs a Spreadsheet that imposes no extra validity
        /// conditions, normalizes every cell name to itself and has
        /// version "default".
        /// </summary>
        public Spreadsheet() :
            this(s => true, s => s, "default")
        {
            Changed = false;
        }

        /// <summary>
        /// Constructs a Spreadsheet by recording its variable validity test,
        /// its normalization method, and its version information.  The variable validity
        /// test is used throughout to determine whether a string that consists of one or
        /// more letters followed by one or more digits is a valid cell name.  The variable
        /// equality test should be used thoughout to determine whether two variables are
        /// equal.
        /// </summary>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            cells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
            Changed = false;
        }

        /// <summary>
        /// Constructs a Spreadsheet by loading a saved spreadsheet, its variable validity test,
        /// its normalization method, and its version information.  The variable validity
        /// test is used throughout to determine whether a string that consists of one or
        /// more letters followed by one or more digits is a valid cell name.  The variable
        /// equality test should be used thoughout to determine whether two variables are
        /// equal.
        /// </summary>
        public Spreadsheet(string filename, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            cells = new Dictionary<string, Cell>();
            dependencyGraph = new DependencyGraph();
            ReadFile(filename, false);
            Changed = false;
        }

        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override string GetSavedVersion(String filename)
        {
            string version = ReadFile(filename, true);
            return version;
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>
        /// cell name goes here
        /// </name>
        /// <contents>
        /// cell contents goes here
        /// </contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(String filename)
        {
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(filename);

                writer.WriteStartDocument();
                writer.WriteStartElement("spreadsheet");
                writer.WriteAttributeString("version", this.Version);
                //Get all the non-empty cells
                HashSet<string> nonemptyCells = new HashSet<string>(GetNamesOfAllNonemptyCells());
                //Write each cell to the XML file
                foreach (string cell in nonemptyCells)
                {
                    Cell currentCell = cells[cell];
                    writer.WriteStartElement("cell");
                    writer.WriteStartElement("name");
                    writer.WriteString(currentCell.name);
                    writer.WriteEndElement(); //name
                    writer.WriteStartElement("contents");
                    if (currentCell.content is String)
                    {
                        writer.WriteString((string)currentCell.content);
                    }
                    else if (currentCell.content is Double)
                    {
                        writer.WriteString(currentCell.content.ToString());
                    }
                    else //Formula
                    {
                        writer.WriteString("=" + currentCell.content.ToString());
                    }
                    writer.WriteEndElement(); //contents
                    writer.WriteEndElement(); //cell
                }
                writer.WriteEndElement(); //spreadsheet
                writer.WriteEndDocument();

                Changed = false;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The save destination could not be found.");
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
            finally
            {
                //Close the reader if it's not null
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// If the contents is an empty string, we say that the cell is empty.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            HashSet<string> nonemptyCells = new HashSet<string>();
            //Test each cell to see if it contains an empty string
            foreach (string cellName in cells.Keys)
            {
                Cell nextCell = cells[cellName];
                if (!nextCell.content.Equals(""))
                {
                    //Add the cell if it is non-empty
                    nonemptyCells.Add(cellName);
                }
            }
            return nonemptyCells;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  
        /// The return value should be either a string, a double, or a Formula.
        /// <param name="name">The name of the cell.</param>
        /// <returns>A string, double or Formula contained in the named cell.</returns>
        public override object GetCellContents(String name)
        {
            //Normalize the cell name
            name = Normalize(name);

            //Throw an exception if cell name is null or invalid
            ValidateCellName(name);

            //If the cell doesn't have content, return an empty string
            if (cells.ContainsKey(name))
            {
                return cells[name].content;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        public override object GetCellValue(String name)
        {
            //Normalize the cell name
            name = Normalize(name);

            //Throw an exception if cell name is null or invalid
            ValidateCellName(name);

            //If the cell doesn't have a value, return an empty string
            if (cells.ContainsKey(name))
            {
                return cells[name].value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetContentsOfCell(String name, String content)
        {
            if (content == null)
                throw new ArgumentNullException();

            ValidateCellName(name);

            if (content.Length < 1)
            {
                HashSet<string> empty = new HashSet<string>();
                return empty;
            }

            //Normalize the cell name
            name = Normalize(name);

            //The cells that need to be recalculated as a result of this addition
            HashSet<string> allDependents = new HashSet<string>();

            double number;
            //Check if content is a double
            if (Double.TryParse(content, out number))
            {
                allDependents = new HashSet<string>(SetContentsOfCell(name, number));
            }

            //Check if content is a formula
            else if (content.Substring(0, 1).Equals("="))
            {
                string formula = content.Substring(1, content.Length - 1);
                Formula contentFormula = new Formula(formula, Normalize, IsValid);
                allDependents = new HashSet<string>(SetCellContents(name, contentFormula));
            }

            //If it's neither a double or formula, the content becomes the string content
            else
            {
                allDependents = new HashSet<string>(SetCellContents(name, content));
            }

            //Indicate a change has been made
            Changed = true;

            //All the cells dependent on the newly changed cell
            HashSet<string> cellsToRecalculate = new HashSet<string>(allDependents);
            cellsToRecalculate.Remove(name);

            //Recalculate all the cells dependent on the newly changed cell
            foreach (string cell in cellsToRecalculate)
            {
                Cell currentCell = cells[cell];
                SetCellContents(cell, (Formula)currentCell.content);
            }

            //return all the dependents of this cell
            return allDependents;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="number">The double to store inside the cell.</param>
        protected override ISet<String> SetContentsOfCell(String name, double number)
        {
            //Throw an exception if cell name is null or invalid
            ValidateCellName(name);

            //Create a cell with the given name and double value
            Cell cell = new Cell(name, number);

            //If the cell already exists in the collection
            if (cells.ContainsKey(name))
            {
                //Remove all current dependees
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                //Add the cell to the collection
                cells[name] = cell;
            }
            else
            {
                //If the cell doesn't already exist, add it.
                cells.Add(name, cell);
            }

            //Get all the dependents of the current cell being set.
            HashSet<String> allDependents = new HashSet<String>(GetCellsToRecalculate(name));

            return allDependents;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="text">The string to be stored inside the cell.</param>
        protected override ISet<String> SetCellContents(String name, String text)
        {
            //Throw an exception if text is null
            if (text == null)
                throw new ArgumentNullException();

            //Throw an exception if cell name is null or invalid
            ValidateCellName(name);

            //Create a cell with the given name and double value
            Cell cell = new Cell(name, text);

            //If the cell already exists in the collection
            if (cells.ContainsKey(name))
            {
                //Remove all current dependees
                dependencyGraph.ReplaceDependees(name, new HashSet<string>());
                //Add the cell to the collection
                cells[name] = cell;
            }
            else
            {
                //If the cell doesn't already exist, add it.
                cells.Add(name, cell);
            }

            //Get all the dependents of the current cell being set.
            HashSet<String> allDependents = new HashSet<String>(GetCellsToRecalculate(name));

            return allDependents;
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.  (No change is made to the spreadsheet.)
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="formula">The Formula to store inside the cell.</param>
        protected override ISet<String> SetCellContents(String name, Formula formula)
        {
            //Throw an exception if formula is null
            if (formula == null)
                throw new ArgumentNullException();

            //Throw an exception if cell name is invalid
            ValidateCellName(name);

            //Save the old contents of this cell
            Object oldContents;

            //Save the old dependees
            HashSet<string> oldDependees = new HashSet<string>(dependencyGraph.GetDependees(name));

            //Create a cell with the given name and Formula object
            Cell cell = new Cell(name, formula, formula.Evaluate(VariableLookup));

            //Add the cell to the dictionary of cells
            if (cells.ContainsKey(name))
            {
                oldContents = GetCellContents(name);
                cells[name] = cell;
            }
            else
            {
                oldContents = "";
                cells.Add(name, cell);
            }

            //Get all the variables contained in the formula
            HashSet<String> variables = new HashSet<string>(formula.GetVariables());

            //Replace all of the cells dependees with the variables in the formula
            dependencyGraph.ReplaceDependees(name, variables);

            //Get all the dependents of the current cell being set and check for circular dependency
            HashSet<String> allDependents;
            try
            {
                allDependents = new HashSet<String>(GetCellsToRecalculate(name));

            }
            catch (CircularException)
            {
                //Restore old dependees
                dependencyGraph.ReplaceDependees(name, oldDependees);
                //if oldContents is a double
                if (oldContents is Double)
                {
                    //Restore old contents
                    SetContentsOfCell(name, (double)oldContents);
                }
                //if oldContents is a string
                else if (oldContents is String)
                {
                    //Restore old contents
                    SetCellContents(name, (string)oldContents);
                }
                //if oldContents is a Formula
                else
                {
                    //Restore old contents
                    SetCellContents(name, (Formula)oldContents);
                }
                throw new CircularException();
            }
            return allDependents;
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            //Throw an exception if text is null
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            //Throw an exception if cell name is null or invalid
            ValidateCellName(name);
            //Get all the direct dependents of the given cell.
            HashSet<String> directDependents = new HashSet<string>(dependencyGraph.GetDependents(name));

            return directDependents;
        }

        /// <summary>
        /// If the given cell name is null, does not consist of 1 or more letters followed
        /// by 1 or more numbers or isValid(cellName) returns false; a InvalidNameException is thrown.
        /// Nothing happens otherwise.
        /// </summary>
        /// <param name="cellName">The value to be determined to be a valid cell name.</param>
        private void ValidateCellName(String cellName)
        {
            //The cell name cannot be null
            if (cellName == null)
            {
                throw new InvalidNameException();
            }

            //Contains 1 or more letters followed by 1 or more numbers
            string validPattern = @"^[a-zA-Z]+[0-9]+$";
            if (!Regex.IsMatch(cellName, validPattern))
            {
                throw new InvalidNameException();
            }

            //Passes the isValid test
            if (!IsValid(cellName))
            {
                throw new InvalidNameException();
            }
        }

        /// <summary>
        /// Looks up value of the variable from the 
        /// dictionary of cells.
        /// </summary>
        /// <param name="variable">variable name</param>
        /// <returns>variable value</returns>
        private double VariableLookup(String variable)
        {
            try
            {
                Object variableValue = cells[variable].value;

                if (variableValue is Double)
                {
                    return (double)variableValue;
                }
                else
                {
                    throw new ArgumentException("One of your variables in your formula does not contain a double value.");
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Variable not found in variable table.");
            }
        }

        /// <summary>
        /// Creates Cell objects from an XML file and uses them to 
        /// reconstruct a formally saved spreadsheet. Alternatively,
        /// if versionOnly is true no cells are created and the version
        /// information for the saved spreadsheet is returned as a string.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>
        /// cell name goes here
        /// </name>
        /// <contents>
        /// cell contents goes here
        /// </contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// </summary>
        /// <param name="filename">The name of the saved spreadsheet XML file</param>
        /// <param name="versionOnly">Conditional to return version information only</param>
        /// <returns>If versionOnly is true, the version is returned as a string. Null returned otherwise.</returns>
        private string ReadFile(string filename, bool versionOnly)
        {
            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(filename);
                string cellName = null;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //Returns the version information if versionOnly is true
                        if (reader.Name.Equals("spreadsheet"))
                        {
                            string version = reader["version"];
                            if (versionOnly)
                            {
                                return version;
                            }

                            if (!version.Equals(this.Version))
                            {
                                throw new SpreadsheetReadWriteException("The version of this saved spreadsheet is not compatible with this spreadsheet.");
                            }
                        }
                        //Gets the name of the next cell
                        if (reader.Name.Equals("name"))
                        {
                            cellName = reader.ReadElementContentAsString();
                        }
                        //Gets the content of the next cell and creates a cell
                        if (reader.Name.Equals("contents"))
                        {
                            string content = reader.ReadElementContentAsString();
                            this.SetContentsOfCell(cellName, content);
                        }
                    }
                }
                return null;
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("Could not find file" + filename + ".");
            }
            catch (System.Xml.XmlException)
            {
                throw new SpreadsheetReadWriteException("Cannot load from an empty XML file. Root element is missing.");
            }
            catch (ArgumentNullException)
            {
                throw new SpreadsheetReadWriteException("Your file path is null; double check your file path.");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new SpreadsheetReadWriteException("The load directory could not be found.");
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                Changed = false;
            }
        }
    }
}
