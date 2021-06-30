using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xunit;

namespace Apriori.Core
{
    public class AprioriParserTests : TestsBase
    {
        private readonly IAprioriItemsParser _processor;
        public AprioriParserTests()
        {
            _processor = new AprioriItemsParser();
        }

        [Fact]
        [Trait("Apriori", "Parser")]
        public void ShouldReturnListOfItemSets()
        {
            //Arrange
            string input = ValidInput;

            //Act
            List<IItemSet> result = _processor.Process(input);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count);
        }

        [Fact]
        [Trait("Apriori", "Parser")]
        public void ShouldThrowExceptionIfInputIsNull()
        {
            //Arrange
            string input = null;

            //Act
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => _processor.Process(input));

            //Assert
            Assert.AreEqual("input", exception.ParamName);
        }
    }
}
