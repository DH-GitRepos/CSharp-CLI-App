using DatabaseGateway;
using Entities;
using DTOs;
using CommandLineUI.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Tests.BlackBox.BoundaryValues
{

    [TestClass]
    public class Blackbox_01c_BoundaryValues
    {
        [TestMethod]
        public void T16_TestAddStockItem_BoundaryTest_ItemId_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            // initialise without populate - call init gateway directly
            InitialiseDatabaseGateway DB_Init = new InitialiseDatabaseGateway();
            DBConnection_DTO DB_dto = DB_Init.InitialiseMySqlDatabase();

            int itemId = 1;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            // insert item
            DB_Facade.Item_Create(item);

            // check inserted item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemId = 1;
            string expected_itemName = "Marker";
            int expected_itemQuantity = 10;

            Assert.AreEqual(expected_itemId, returnItem.ID);
            Assert.AreEqual(expected_itemName, returnItem.Name);
            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T17_TestAddStockItem_BoundaryTest_ItemId_Invalid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 0;
            string itemName = "Marker";
            int itemQty = 10;

            Action act = () =>
            {
                Item item = new Item(itemId, itemName, itemQty, DateTime.Now);
            };

            Assert.ThrowsException<Exception>(act);
        }


        [TestMethod]
        public void T18_TestAddStockItem_BoundaryTest_ItemQty_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            // initialise without populate - call init gateway directly
            InitialiseDatabaseGateway DB_Init = new InitialiseDatabaseGateway();
            DBConnection_DTO DB_dto = DB_Init.InitialiseMySqlDatabase();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 0;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            // insert item
            DB_Facade.Item_Create(item);

            // check inserted item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemId = 5;
            string expected_itemName = "Marker";
            int expected_itemQuantity = 0;

            Assert.AreEqual(expected_itemId, returnItem.ID);
            Assert.AreEqual(expected_itemName, returnItem.Name);
            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T19_TestAddStockItem_BoundaryTest_ItemQty_Invalid()
        {

            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            // initialise without populate - call init gateway directly
            InitialiseDatabaseGateway DB_Init = new InitialiseDatabaseGateway();
            DBConnection_DTO DB_dto = DB_Init.InitialiseMySqlDatabase();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = -1;

            Action act = () =>
            {
                Item item = new Item(itemId, itemName, itemQty, DateTime.Now);
            };

            Assert.ThrowsException<Exception>(act);
        }


        [TestMethod]
        public void T20_TestAddQtyToStockItem_BoundaryTest_ItemID_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 1;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToAdd = 1;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            DB_Facade.Item_Update_Add(item, qtyToAdd);

            // check updated item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemQuantity = itemQty + qtyToAdd;

            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T21_TestAddQtyToStockItem_BoundaryTest_ItemID_Invalid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            // initialise without populate - call init gateway directly
            InitialiseDatabaseGateway DB_Init = new InitialiseDatabaseGateway();
            DBConnection_DTO DB_dto = DB_Init.InitialiseMySqlDatabase();

            int itemId = 1;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);
            Item invalidItem = new Item(itemId + 1, itemName, itemQty, DateTime.Now);

            int qtyToAdd = 1;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            Template_DTO update_response = DB_Facade.Item_Update_Add(invalidItem, qtyToAdd);

            string expected_response = "ERROR";

            Assert.AreEqual(expected_response, update_response.Status);
        }


        [TestMethod]
        public void T22_TestAddQtyToStockItem_BoundaryTest_ItemQty_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 5;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToAdd = 0;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            DB_Facade.Item_Update_Add(item, qtyToAdd);

            // check updated item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemQuantity = itemQty + qtyToAdd;

            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T23_TestAddQtyToStockItem_BoundaryTest_ItemQty_Invalid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 5;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToAdd = -1;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            Template_DTO add_response = DB_Facade.Item_Update_Add(item, qtyToAdd);

            Assert.AreEqual("ERROR", add_response.Status);
        }


        [TestMethod]
        public void T24_TestRemoveQtyFromStockItem_BoundaryTest_ItemQty_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = 0;

            // insert item
            DB_Facade.Item_Create(item);
            // remove qty from item
            DB_Facade.Item_Update_Remove(item, qtyToRemove);

            // check updated item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemQuantity = itemQty - qtyToRemove;

            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T25_TestRemoveQtyFromStockItem_BoundaryTest_ItemQty_Invalid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = -1;

            // insert item
            DB_Facade.Item_Create(item);
            // remove qty from item
            Template_DTO remove_response = DB_Facade.Item_Update_Remove(item, qtyToRemove);

            Assert.AreEqual("ERROR", remove_response.Status);
        }



        [TestMethod]
        public void T26_TestRemoveQtyFromStockItem_BoundaryTest_ItemStockQty_AllStockQty_Valid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = 10;

            // insert item
            DB_Facade.Item_Create(item);
            // remove qty from item
            DB_Facade.Item_Update_Remove(item, qtyToRemove);

            // check updated item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemQuantity = itemQty - qtyToRemove;

            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }


        [TestMethod]
        public void T27_TestRemoveQtyFromStockItem_BoundaryTest_ItemStockQty_OverStockQty_Invalid()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = 11;

            // insert item
            DB_Facade.Item_Create(item);
            // remove qty from item
            Template_DTO remove_response = DB_Facade.Item_Update_Remove(item, qtyToRemove);

            Assert.AreEqual("ERROR", remove_response.Status);
        }

    }

}
