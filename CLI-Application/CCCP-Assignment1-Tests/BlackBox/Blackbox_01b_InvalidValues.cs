using DatabaseGateway;
using Entities;
using DTOs;
using CommandLineUI.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


namespace Tests.BlackBox.InvalidValues
{
    [TestClass]
    public class Blackbox_01b_InvalidValues
    {
        [TestMethod]
        public void T08_TestCheckEmployeeIsValid_WithInvalidValue()
        {
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<object>.INITIALISE_DATABASE).Start_Command_Process();

            string employeeName = "Phil";

            CommandFactory cf_addEmp = new CommandFactoryBuilder().WithEmployeeName(employeeName).Build();
            cf_addEmp.CreateCommand(UI_Command<Command_DTO>.ADD_EMPLOYEE).Start_Command_Process();

            Dictionary<string, string> employeeNameObject = new Dictionary<string, string>
            {
                { "name", "Fred" }
            };

            CommandFactory cf_checkEmp = new CommandFactoryBuilder().WithEmployeeNameObject(employeeNameObject).Build();
            Command_DTO return_dto = cf_checkEmp.CreateCommand(UI_Command<Command_DTO>.CHECK_EMPLOYEE_EXISTS).Start_Command_Process();

            Console.WriteLine("STATUS: " + return_dto.Status);
            Console.WriteLine("MSG: " + return_dto.Message);

            string expected_response = "ERROR";

            Assert.AreEqual(expected_response, return_dto.Status);
        }


        [TestMethod]
        public void T09_TestAddItemToStock_WithInvalidIdValue()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = -1;
            string itemName = "Marker";
            int itemQty = 10;

            Action act = () =>
            {
                Item item = new Item(itemId, itemName, itemQty, DateTime.Now);
            };

            Assert.ThrowsException<Exception>(act);
        }


        [TestMethod]
        public void T10_TestAddItemToStock_WithInvalidQuantityValue()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

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
        public void T11_TestAddItemToStock_WithInvalidItemNameValue()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "";
            int itemQty = 10;

            Action act = () =>
            {
                Item item = new Item(itemId, itemName, itemQty, DateTime.Now);
            };

            Assert.ThrowsException<Exception>(act);
        }


        [TestMethod]
        public void T12_TestAddQtyToStock_WithInvalidIdValue()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = -5;
            string itemName = "Marker";
            int itemQty = 5;
            Action act = () =>
            {
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
            };

            Assert.ThrowsException<Exception>(act);
        }


        [TestMethod]
        public void T13_TestAddQtyToStock_WithInvalidQty()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 5;

            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToAdd = -5;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            Template_DTO add_resonse = DB_Facade.Item_Update_Add(item, qtyToAdd);

            string expected_respose = "ERROR";

            Assert.AreEqual(expected_respose, add_resonse.Status);
        }



        [TestMethod]
        public void T14_TestRemoveQtyFromStock_WithInvalidQtyLessThanZero()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 5;

            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = -5;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            Template_DTO add_resonse = DB_Facade.Item_Update_Remove(item, qtyToRemove);

            string expected_respose = "ERROR";

            Assert.AreEqual(expected_respose, add_resonse.Status);
        }


        [TestMethod]
        public void T15_TestRemoveQtyFromStock_WithInvalidQtyMoreThanInStock()
        {
            DataGatewayFacade DB_Facade = new DataGatewayFacade();
            CommandFactory cf_init = new CommandFactoryBuilder().Build();
            cf_init.CreateCommand(UI_Command<Command_DTO>.INITIALISE_DATABASE).Start_Command_Process();

            int itemId = 5;
            string itemName = "Marker";
            int itemQty = 10;

            Item item = new Item(itemId, itemName, itemQty, DateTime.Now);

            int qtyToRemove = 15;

            // insert item
            DB_Facade.Item_Create(item);
            // add qty to item
            Template_DTO add_resonse = DB_Facade.Item_Update_Remove(item, qtyToRemove);

            string expected_respose = "ERROR";

            Assert.AreEqual(expected_respose, add_resonse.Status);
        }

    }

}