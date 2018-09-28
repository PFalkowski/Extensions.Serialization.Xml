using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace Extensions.Serialization.Xml.Test
{
    public class UtilitiesTests
    {
        [Fact]
        public void SerializeToXDocTest()
        {
            var tested = PersonsList.SerializeToXDoc();

            var firstNames = new HashSet<string>(from e in tested.Descendants("FirstName") select e.Value);
            var ages = new HashSet<int>(from e in tested.Descendants("Age") select int.Parse(e.Value));

            Assert.Equal(5, firstNames.Count);
            Assert.Equal(5, ages.Count);
            Assert.Contains("Alex", firstNames);
            Assert.Contains("Cloe", firstNames);
            Assert.Contains("Jack", firstNames);
            Assert.Contains("John", firstNames);
            Assert.Contains("Grace", firstNames);
            Assert.Contains(27, ages);
            Assert.Contains(35, ages);
            Assert.Contains(45, ages);
            Assert.Contains(30, ages);
        }

        [Fact]
        public void DeserializeXDoc()
        {
            var input = XDocument.Parse(Properties.Resources.ArrayOfPerson);
            var received = input.Deserialize<List<Person>>();

            var ages = new HashSet<int>(received.Select(p => p.Age));
            Assert.Contains(27, ages);
            Assert.Contains(35, ages);
            Assert.Contains(45, ages);
            Assert.Contains(30, ages);
            Assert.Contains(18, ages);
        }

        [Fact]
        public void SerializeToXmlDoc()
        {
            var tested = PersonsList.SerializeToXmlDoc();

            var navigator = tested.CreateNavigator();

            Assert.Equal(5, (double)navigator.Evaluate("count(//FirstName)"));
            Assert.Equal(5, (double)navigator.Evaluate("count(//Age)"));
            Assert.Equal(27, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Alex\"]/Age/text())")));
            Assert.Equal(35, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Cloe\"]/Age/text())")));
            Assert.Equal(45, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Jack\"]/Age/text())")));
            Assert.Equal(30, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"John\"]/Age/text())")));
        }

        [Fact]
        public void DeserializeXmlDoc()
        {
            var input = new XmlDocument();
            input.LoadXml(Properties.Resources.ArrayOfPerson);

            var received = input.Deserialize<List<Person>>();

            var ages = new HashSet<int>(received.Select(p => p.Age));
            Assert.Contains(27, ages);
            Assert.Contains(35, ages);
            Assert.Contains(45, ages);
            Assert.Contains(30, ages);
            Assert.Contains(18, ages);
        }

        [Fact]
        public void ToXDocumentConverts()
        {
            var input = new XmlDocument();
            input.LoadXml(Properties.Resources.ArrayOfPerson);

            var received = input.ToXDocument();

            var firstNames = new HashSet<string>(from e in received.Descendants("FirstName") select e.Value);
            var ages = new HashSet<int>(from e in received.Descendants("Age") select int.Parse(e.Value));

            Assert.Equal(5, firstNames.Count);
            Assert.Equal(5, ages.Count);
            Assert.Contains("Alex", firstNames);
            Assert.Contains("Cloe", firstNames);
            Assert.Contains("Jack", firstNames);
            Assert.Contains("John", firstNames);
            Assert.Contains("Grace", firstNames);
            Assert.Contains(27, ages);
            Assert.Contains(35, ages);
            Assert.Contains(45, ages);
            Assert.Contains(30, ages);
            Assert.Contains(18, ages);
        }

        [Fact]
        public void ToXmlDocumentConverts()
        {
            var input = XDocument.Parse(Properties.Resources.ArrayOfPerson);

            var received = input.ToXmlDocument();


            var navigator = received.CreateNavigator();

            Assert.Equal(5, (double)navigator.Evaluate("count(//FirstName)"));
            Assert.Equal(5, (double)navigator.Evaluate("count(//Age)"));
            Assert.Equal(27, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Alex\"]/Age/text())")));
            Assert.Equal(35, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Cloe\"]/Age/text())")));
            Assert.Equal(45, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Jack\"]/Age/text())")));
            Assert.Equal(30, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"John\"]/Age/text())")));
            Assert.Equal(18, (double)(navigator.Evaluate("sum(/ArrayOfPerson/Person[FirstName=\"Grace\"]/Age/text())")));
        }

        [Fact]
        public void ToXmlDocCulture()
        {
            var quote = JustAQuote;
            var result = quote.SerializeToXmlDoc();

            var navigator = result.CreateNavigator();

            Assert.Equal(1, (double)navigator.Evaluate("count(//Open)"));
            Assert.Equal(1, (double)navigator.Evaluate("count(//High)"));
            Assert.Equal(quote.Open, (double)(navigator.Evaluate("sum(/StockQuote/Open/text())")));
            Assert.Equal(quote.High, (double)(navigator.Evaluate("sum(/StockQuote/High/text())")));
            Assert.Equal(quote.Low, (double)(navigator.Evaluate("sum(/StockQuote/Low/text())")));
            Assert.Equal(quote.Close, (double)(navigator.Evaluate("sum(/StockQuote/Close/text())")));
        }

        #region Stubs

        public sealed class StockQuote
        {
            public string Ticker { get; set; }
            public long Date { get; set; }
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Close { get; set; }
            public double Volume { get; set; }
        }

        public sealed class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }


        private static List<Person> PersonsList { get; } = new List<Person>
        {
            new Person
            {
                FirstName = "Alex",
                LastName = "Friedman",
                Age = 27
            },
            new Person
            {
                FirstName = "Jack",
                LastName = "Bauer",
                Age = 45
            },
            new Person
            {
                FirstName = "Cloe",
                LastName = "O'Brien",
                Age = 35
            },
            new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            },
            new Person
            {
                FirstName = "Grace",
                LastName = "Hooper",
                Age = 18
            }
        };

        private static StockQuote JustAQuote => new StockQuote
        {
            Ticker = "TEST",
            Open = 1.0,
            High = 1.8,
            Low = .9,
            Close = 1.2,
            Volume = 11.0
        };
        #endregion
    }
}
