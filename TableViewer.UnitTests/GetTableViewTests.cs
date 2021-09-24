using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TableViewer.UnitTests
{
    [TestFixture]
    public class GetTableViewTests
    {
        [Test]
        public void CorrectTable_WhenCorrectInput()
        {
            var rowCount = 2;
            var columnWidths = new[] { 1, 3, 2 };
            var expected = "**********\r\n" +
                           "* *   *  *\r\n" +
                           "**********\r\n" +
                           "* *   *  *\r\n" +
                           "**********\r\n";

            var actual = Program.GetTableView(rowCount, columnWidths);

            Assert.AreEqual(expected, actual);
            Assert.That(actual, Is.EqualTo(expected));
            actual.Should().Be(expected);
        }

        [TestCase(1)]
        [TestCase(100)]
        public void CorrectTable_WhenCorrectRowCount(int rowCount)
        {
            var columnWidths = new[] { 1, 3, 2 };

            var actual = Program.GetTableView(rowCount, columnWidths);

            var linesCount = actual.Sum(it => it == '\n' ? 1 : 0);
            linesCount.Should().Be(rowCount * 2 + 1);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        public void ThrowArgumentException_WhenWrongRowCount(int rowCount)
        {
            var columnWidths = new[] { 1, 3, 2 };

            Action action = () => Program.GetTableView(rowCount, columnWidths);

            action.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("rowCount");
        }

        [TestCase(1)]
        [TestCase(100)]
        public void CorrectTable_WhenCorrectColumnsCount(int columnCount)
        {
            var rowCount = 2;
            var columnWidths = Enumerable.Repeat(1, columnCount).ToArray();

            var actual = Program.GetTableView(rowCount, columnWidths);

            var firstLineLength = actual.TakeWhile(it => it != '\r').Count();
            firstLineLength.Should().Be(columnCount * 2 + 1);
        }

        [Test]
        public void ThrowArgumentException_WhenEmptyColumnWidths()
        {
            var rowCount = 2;
            var columnWidths = Array.Empty<int>();

            Action action = () => Program.GetTableView(rowCount, columnWidths);

            action.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("columnWidths");
        }

        [Test]
        public void ThrowArgumentException_WhenTooManyColumns()
        {
            var rowCount = 2;
            var columnWidths = Enumerable.Repeat(1, 101).ToArray();

            Action action = () => Program.GetTableView(rowCount, columnWidths);

            action.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("columnWidths");
        }

        [TestCase(1)]
        [TestCase(100)]
        public void CorrectTable_WhenCorrectColumnWidth(int columnWidth)
        {
            var rowCount = 2;
            var columnWidths = new[] { columnWidth };

            var actual = Program.GetTableView(rowCount, columnWidths);

            var firstLineLength = actual.TakeWhile(it => it != '\r').Count();
            firstLineLength.Should().Be(columnWidth + 2);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        public void ThrowArgumentException_WhenWrongColumnWidth(int columnWidth)
        {
            var rowCount = 2;
            var columnWidths = new[] { 1, columnWidth, 2 };

            Action action = () => Program.GetTableView(rowCount, columnWidths);

            action.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("columnWidths");
        }

        [Test]
        public void EveryEvenLineIsFullFilled_WhenDifferentRowCount()
        {
            var random = new Random(0);
            var columnWidths = new[] { 1, 3, 2 };

            for (var i = 0; i < 10; i++)
            {
                var rowCount = random.Next(1, 100);

                var actual = Program.GetTableView(rowCount, columnWidths);

                var evenLines = actual.Split("\r\n").Where((_, index) => index % 2 == 0);
                evenLines.Should().AllBe("**********");
            }
        }
    }
}
