using System;
using System.Linq;
using System.Text;

namespace TableViewer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var inputString = args.Length > 0 ? args[0] : Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputString))
            {
                Console.WriteLine("Empty input.");
                return;
            }

            var inputIntArray = inputString.Split(" ").Select(int.Parse).ToArray();

            var rowCount = inputIntArray[0];
            var columnWidths = inputIntArray[1..];
            var tableView = GetTableView(rowCount, columnWidths);

            Console.WriteLine(tableView);
            Console.Read();
        }

        public static string GetTableView(int rowCount, int[] columnWidths)
        {
            if (rowCount <= 0 || rowCount > 100)
            {
                throw new ArgumentException("Wrong row count.", nameof(rowCount));
            }

            if (columnWidths.Length == 0 || columnWidths.Length > 100 || columnWidths.Any(it => it <= 0 || it > 100))
            {
                throw new ArgumentException("Wrong column widths.", nameof(columnWidths));
            }

            var tableView = new StringBuilder();

            var tableWidth = columnWidths.Sum() + columnWidths.Length + 1;
            var line = string.Join("", Enumerable.Repeat("*", tableWidth));
            tableView.AppendLine(line);

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                tableView.Append("*");
                foreach (var columnWidth in columnWidths)
                {
                    tableView.Append(string.Join("", Enumerable.Repeat(" ", columnWidth)));
                    tableView.Append("*");
                }

                tableView.AppendLine();
                tableView.AppendLine(line);
            }

            return tableView.ToString();
        }
    }
}
