using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;

namespace SS
{
    /// <summary>
    /// Represents a Cell in a Spreadsheet. The Cell has
    /// a name, content and value. If the content of a cell
    /// is a double or a string, the value will be the same.
    /// If the content is a Formula, the value will either
    /// be a double (the result of the Formula) or a FormulaError
    /// if an invalid formula is entered.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The content of a cell will either be a string,
        /// double or Formula Object. If content is a string,
        /// it's value is that string. Likewise, if content is
        /// a double, it's value is that double.
        /// </summary>
        public Object content { get; private set; }

        /// <summary>
        /// The value of a cell will either be a string,
        /// double or FormulaError Object.
        /// </summary>
        public Object value { get; private set;}

        /// <summary>
        /// The name of the cell. The name must be one or more
        /// letters followed by one or more numbers and when
        /// passed to isValid has to return true.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The constructor for a cell that contains a double value.
        /// The content and value will be the same for this cell.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="number">The double value represented by this cell.</param>
        public Cell(String name, double number)
        {
            this.name = name;
            this.content = number;
            this.value = number;
        }

        /// <summary>
        /// The constructor for a cell that contains a string value.
        /// The content and value will be the same for this cell.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="number">The string value represented by this cell.</param>
        public Cell(String name, string text)
        {
            this.name = name;
            this.content = text;
            this.value = text;
        }

        /// <summary>
        /// The constructor for a cell that contains a Formula object.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="formula">The Formula object represented by this cell.</param>
        /// <param name="value">Either a double or a FormulaError from the result of Evaluate</param>
        public Cell(String name, Formula formula, Object value)
        {
            this.name = name;
            this.content = formula;
            this.value = value;
        }
    }
}

