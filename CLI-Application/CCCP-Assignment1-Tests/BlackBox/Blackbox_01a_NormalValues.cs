using DatabaseGateway;
using Entities;
using DTOs;
using CommandLineUI.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


namespace Tests.BlackBox.NormalValues
{
    [TestClass]
    public class Blackbox_01a_NormalValues
    {
        [TestMethod]
        public void T01_TestCheckEmployeeIsValid_WithNormalValues()
        {
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<object>.INITIALISE_DATABASE).Start_Command_Process();

            string employeeName = "Phil";

            Dictionary<string, string> employeeNameObject = new Dictionary<string, string>
            {
                { "name", "Phil" }
            };

            CommandFactory cf_addEmp = new CommandFactoryBuilder().WithEmployeeName(employeeName).Build();
            cf_addEmp.CreateCommand(UI_Command<Command_DTO>.ADD_EMPLOYEE).Start_Command_Process();

            CommandFactory cf_checkEmp = new CommandFactoryBuilder().WithEmployeeNameObject(employeeNameObject).Build();
            Command_DTO dto = cf_checkEmp.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();

            string name = dto.Employee.EmpName;
            Console.WriteLine(name);

            Assert.AreEqual("Phil", name);
        }



        [TestMethod]
        public void T02_TestAddItemToStock_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            // insert item
            DB_Facade.Item_Create(item);

            // check inserted item
            Template_DTO insertedItem = DB_Facade.Item_Read_One(itemId);
            Item returnItem = (Item)insertedItem.ReturnObject;

            int expected_itemId = 5;
            string expected_itemName = "Marker";
            int expected_itemQuantity = 10;

            Assert.AreEqual(expected_itemId, returnItem.ID);
            Assert.AreEqual(expected_itemName, returnItem.Name);
            Assert.AreEqual(expected_itemQuantity, returnItem.Quantity);
        }



        [TestMethod]
        public void T03_TestTransactionLogEntryCreated_ItemAdded_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            string transactionType = "Item Added";
            int itemID = 5;
            string itemName = "Marker";
            double itemPrice = 0.50;
            int itemQuantity = 10;
            string employeeName = "Phil";
            DateTime dateAdded = DateTime.Now;

            // insert transaction
            TransactionLogEntry newTransaction = new(transactionType, itemID, itemName, itemPrice, itemQuantity, employeeName, dateAdded);
            DB_Facade.Transaction_Create(newTransaction);

            // check transaciton
            Template_DTO transactionCheck = DB_Facade.Transaction_Read_One(newTransaction);
            TransactionLogEntry returnTransaction = (TransactionLogEntry)transactionCheck.ReturnObject;

            string expected_transactionType = "Item Added";

            Assert.AreEqual(expected_transactionType, returnTransaction.TypeOfTransaction);
        }



        [TestMethod]
        public void T04_TestQuantityAddedToToStockItem_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 5;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToAdd = 10;

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
        public void T05_TestTransactionLogEntryCreated_QtyAdded_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            string transactionType = "Quantity Added";
            int itemID = 5;
            string itemName = "Marker";
            double itemPrice = 0.50;
            int itemQuantity = 10;
            string employeeName = "Phil";
            DateTime dateAdded = DateTime.Now;

            // insert transaction
            TransactionLogEntry newTransaction = new(transactionType, itemID, itemName, itemPrice, itemQuantity, employeeName, dateAdded);
            DB_Facade.Transaction_Create(newTransaction);

            // check transaciton
            Template_DTO transactionCheck = DB_Facade.Transaction_Read_One(newTransaction);
            TransactionLogEntry returnTransaction = (TransactionLogEntry)transactionCheck.ReturnObject;

            string expected_transactionType = "Quantity Added";

            Assert.AreEqual(expected_transactionType, returnTransaction.TypeOfTransaction);
        }


        [TestMethod]
        public void T06_TestQuantityRemovedFromStockItem_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;
            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = 5;

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
        public void T07_TestTransactionLogEntryCreated_QtyRemoved_WithNormalValues()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            string transactionType = "Quantity Removed";
            int itemID = 5;
            string itemName = "Marker";
            double itemPrice = 0.50;
            int itemQuantity = 10;
            string employeeName = "Phil";
            DateTime dateAdded = DateTime.Now;

            // insert transaction
            TransactionLogEntry newTransaction = new(transactionType, itemID, itemName, itemPrice, itemQuantity, employeeName, dateAdded);
            DB_Facade.Transaction_Create(newTransaction);

            // check transaciton
            Template_DTO transactionCheck = DB_Facade.Transaction_Read_One(newTransaction);
            TransactionLogEntry returnTransaction = (TransactionLogEntry)transactionCheck.ReturnObject;

            string expected_transactionType = "Quantity Removed";

            Assert.AreEqual(expected_transactionType, returnTransaction.TypeOfTransaction);
        }

    }

}
