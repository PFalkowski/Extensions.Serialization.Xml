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

            Assert.Equal(4, received.Count);
            Assert.Equal(27, received[0].Age);
            Assert.Equal(45, received[1].Age);
            Assert.Equal(35, received[2].Age);
            Assert.Equal(30, received[3].Age);
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

            Assert.Equal(4, received.Count);
            Assert.Equal(27, received[0].Age);
            Assert.Equal(45, received[1].Age);
            Assert.Equal(35, received[2].Age);
            Assert.Equal(30, received[3].Age);
        }


        #region Stubs
        public sealed class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }


        private List<Person> PersonsList { get; } = new List<Person>
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
                Age = (int) ((DateTime.Now - new DateTime(1906, 12, 9)).TotalDays / 365.25)
            }
        };
        #endregion
    }
}
