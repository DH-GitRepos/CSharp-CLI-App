using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.EntityTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void TestNewItemHasCorrectId()
        {
            Item item = new Item(1, "a", 1, DateTime.Now);
            Assert.AreEqual(1, item.ID);
        }

        [TestMethod]
        public void TestNewItemHasCorrectName()
        {
            Item item = new Item(1, "a", 1, DateTime.Now);
            Assert.AreEqual("a", item.Name);
        }

        [TestMethod]
        public void TestNewItemHasCorrectQuantity()
        {
            Item item = new Item(2, "a", 1, DateTime.Now);
            Assert.AreEqual(1, item.Quantity);
        }

        [TestMethod]
        public void TestNewItemHasCorrectCreationDate()
        {
            DateTime now = DateTime.Now;
            Item item = new Item(2, "a", 1, now);
            Assert.AreEqual(now, item.DateCreated);
        }

        [TestMethod]
        public void TestInvalidValuesForNewItemProducesException()
        {
            Item item;
            Assert.ThrowsException<Exception>(
                () => item = new Item(0, "", 0, DateTime.Now));
        }

        [TestMethod]
        public void TestInvalidValuesForNewItemProducesCorrectErrorMessage()
        {
            try
            {
                Item item = new Item(1, "", 0, DateTime.Now);
            }
            catch (Exception e)
            {
                string expectedErrorMsg =
                    "ITEM_ENTITY_ERROR -> ITEM_NAME_EMPTY";
                Assert.AreEqual(expectedErrorMsg, e.Message);
            }
        }
    }
}
