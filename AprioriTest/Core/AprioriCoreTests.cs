using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Apriori.Core
{
    public class AprioriCoreTests : TestsBase
    {
        [Fact]
        [Trait("Apriori", "Core")]
        public void ShouldReturnListOfDistinctItems()
        {
            //Arrange
            string input = ValidInput;

            //Act
            List<IItemSet> itemSets = GetItemSet(input);
            List<IItem> result = itemSets.ToDistinctValues().ToList();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Count == 5);

            int countForLabel5 = GetCountOfLabel(result, "5");
            Assert.AreEqual(countOfLabel5, countForLabel5);
        }

        [Fact]
        [Trait("Apriori", "Core")]
        public void ShouldThrowItemSetNotFoundException()
        {
            //Arrange
            ushort minsup = MinSup;

            //Act
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => GetItemSet(EmptyInput));

            //Assert
            Assert.AreEqual("input", exception.ParamName);
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnRelations(int groupItemsCount)
        {
            //Arrange
            List<IItemSet> itemSets = GetItemSet(ValidInput);

            //Act
            IEnumerable<IEnumerable<IItem>> relations = _relationsProcessor.GetRelations(itemSets.ToDistinctValues(), groupItemsCount);

            //Assert
            Assert.IsTrue(relations.Any());
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(2)]
        public void ShouldThrowExceptionForEmptyOrNullList(int groupItemsCount)
        {
            //Arrange
            List<IItem> emptyList = new List<IItem>();
            List<IItem> nullList = null;

            //Act
            Assert.ThrowsException<Exception>(() => _relationsProcessor.GetRelations(emptyList, groupItemsCount));
            Assert.ThrowsException<Exception>(() => _relationsProcessor.GetRelations(nullList, groupItemsCount));
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldThrowExceptionIfGroupItemsCountLessThenOne(int groupItemsCount)
        {
            //Arrange
            List<IItemSet> itemSets = GetItemSet(ValidInput);

            //Act
            Assert.ThrowsException<Exception>(() => _relationsProcessor.GetRelations(itemSets.ToDistinctValues(), groupItemsCount));
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(2, 2)]
        public void ShouldReturnRelationsWithCount(int groupItemsCount, int minSup)
        {
            //Arrange
            ItemSetList itemSets = GetItemSet(ValidInput);
            IEnumerable<IEnumerable<IItem>> relations = _relationsProcessor.GetRelations(itemSets.ToDistinctValues(), groupItemsCount);

            //Act
            var result = itemSets.CalculateCount(relations, minSup);

            //Assert
            Assert.IsTrue(result.Any());
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(6, 2)]
        [InlineData(2, 6)]
        public void ShouldReturnEmptyResult(int groupItemsCount, int minSup)
        {
            //Arrange
            ItemSetList itemSets = GetItemSet(ValidInput);
            IEnumerable<IEnumerable<IItem>> relations = _relationsProcessor.GetRelations(itemSets.ToDistinctValues(), groupItemsCount);

            //Act
            ItemSetList result = itemSets.CalculateCount(relations, minSup);

            //Assert
            Assert.IsTrue(!result.Any());
        }


        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnAssociationRules(int minSup)
        {
            //Arrange
            ItemSetList itemSets = GetItemSet(ValidInput);
            AssociationRuleModel model = _aprioriCoreProcessor.PrepareAssociationModel(itemSets, minSup);

            //Act
            List<AssociationRule> rules = _associationProcessor.GetRules(model);

            //Assert
            Assert.IsTrue(rules.Any());
        }


        [Fact]
        [Trait("Apriori", "Core")]
        public void ShouldThrowExceptionIfAssociationRuleModelIsNull()
        {
            //Arrange
            AssociationRuleModel model = null;

            //Act
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => _associationProcessor.GetRules(model));

            //Assert
            Assert.AreEqual("associationRuleModel", exception.ParamName);
        }

        [Theory]
        [Trait("Apriori", "Core")]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldThrowExceptionIfMinSupNotValid(int minSup)
        {
            //Arrange
            ItemSetList itemSets = GetItemSet(ValidInput);

            //Act
            Assert.ThrowsException<Exception>(() => _aprioriCoreProcessor.PrepareAssociationModel(itemSets, minSup));
        }

        [Fact]
        [Trait("Apriori", "Core")]
        public void ShouldThrowExceptionIfItemSetListOfAssociationRuleModelIsNull()
        {
            //Arrange
            AssociationRuleModel model = new AssociationRuleModel { ItemSetList = null };

            //Act
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(() => _associationProcessor.GetRules(model));

            //Assert
            Assert.AreEqual("ItemSetList", exception.ParamName);
        }

    }
}
