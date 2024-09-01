using TrelloConverter;
using TrelloConverter.Models;

namespace TrelloConverterTests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void RemoveUsPrefix_ValidCards_RemovesPrefixFromCardNames()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { Name = "US-001 Test card 1" },
                new Card { Name = "US-002 Test card 2" }
            };

            // Act
            Converter.RemoveUsPrefix(cards);

            // Assert
            Assert.AreEqual("Test card 1", cards[0].Name);
            Assert.AreEqual("Test card 2", cards[1].Name);
        }

        [TestMethod]
        public void AddUsPrefix_ValidCards_AddsPrefixToCardNames()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { Name = "Test card 1", ReorderPosition = 1 },
                new Card { Name = "Test card 2", ReorderPosition = 2 }
            };

            // Act
            Converter.AddUsPrefix(cards);

            // Assert
            Assert.AreEqual("US-001 Test card 1", cards[0].Name);
            Assert.AreEqual("US-002 Test card 2", cards[1].Name);
        }

        [TestMethod]
        public void ScourString_ValidString_ReturnsCleanedString()
        {
            // Arrange
            string input = "This *is* a _test_ \"string\"\nwith new lines.";
            string expected = "This is a test 'string'with new lines.";

            // Act
            string result = Converter.ScourString(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PrepForConversion_ValidCards_ReturnsProcessedCards()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { Name = "Test *Card 1*", Desc = "Description _1_" },
                new Card { Name = "Test *Card 2*", Desc = "Description _2_" }
            };

            // Act
            var result = Converter.PrepForConversion(cards);

            // Assert
            Assert.AreEqual("Test Card 1", result[0].Name);
            Assert.AreEqual("Description 1", result[0].Desc);
            Assert.AreEqual("Test Card 2", result[1].Name);
            Assert.AreEqual("Description 2", result[1].Desc);
        }

        [TestMethod]
        public void ReverseOrder_ValidCards_ReturnsCardsInReversedOrder()
        {
            // Arrange
            var cards = new List<Card>
            {
                new Card { Name = "Card 1", ListName = "List 1", ReorderPosition = 1 },
                new Card { Name = "Card 2", ListName = "List 1", ReorderPosition = 2 }
            };

            // Act
            var result = Converter.ReverseOrder(cards);

            // Assert
            Assert.AreEqual(2, result[0].ReorderPosition);
            Assert.AreEqual(1, result[1].ReorderPosition);
        }
    }
}
