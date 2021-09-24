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

            var rows = inputString.Split(",")
                .Select(it => it.Split(" ").Select(int.Parse).ToArray())
                .ToArray();
            var tableView = GetTableView(rows);

            Console.WriteLine(tableView);
            Console.Read();
        }

        public static string GetTableView(int[][] rows)
        {
            if (rows.Length <= 0 || rows.Length > 100)
            {
                throw new ArgumentException("Wrong rows length.", nameof(rows));
            }

            for (var i = 0; i < rows.Length; i++)
            {
                if (rows[i].Length <= 0 || rows[i].Length > 100 || rows[i].Any(it => it <= 0 || it > 100))
                {
                    throw new ArgumentException("Wrong rows length.", $"row[{i}]");
                }
            }

            if (rows.Any(it => it.Sum() != rows[0].Sum()))
            {
                throw new ArgumentException("All lines must have the same width.", nameof(rows));
            }

            var tableView = new StringBuilder();

            var tableWidth = rows[0].Sum() + rows[0].Length + 1;
            var line = string.Join("", Enumerable.Repeat("*", tableWidth));
            tableView.AppendLine(line);

            foreach (var row in rows)
            {
                tableView.Append("*");
                foreach (var columnWidth in row)
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
