using DatabaseGateway;
using Entities;
using DTOs;
using CommandLineUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    /*
    [TestClass]
    public class MySqlDatabaseGatewayTests
    {
        [TestMethod]
        public void T01_TestConnectionToDatabase_Opened()
        {
            // Create gateway references
            DataGatewayFacade DB_Facade = new();
            InitialiseDatabaseGateway DB_init = new();
            // EmployeeGateway DB_Employee = new();
            ItemGateway DB_Item = new();
            // TransactionLogEntryGateway DB_Transaction = new TransactionLogEntryGateway();

            // Setup database structure
            DB_init.InitialiseMySqlDatabase();
             
            // Setup employees
            DB_Facade.AddEmployee(new Employee("Graham"));
            DB_Facade.AddEmployee(new Employee("Phil"));
            DB_Facade.AddEmployee(new Employee("Jan"));

            // Setup items
            Item i1 = new(1, "Pencil", 10, DateTime.Now);
            // DB_Item.AddItem(i1);
            DB_Facade.AddItem(i1);
            Item i2 = new(2, "Eraser", 20, DateTime.Now);
            // DB_Item.AddItem(i2);
            DB_Facade.AddItem(i2);

            // Add transaction log entries
            TransactionLogEntry tr1 = new("Item Added", i1.ID, i1.Name, 0.25f, i1.Quantity, "Graham", DateTime.Now);
            DB_Facade.AddTransactionLogEntry(tr1);
            TransactionLogEntry tr2 = new("Item Added", i2.ID, i2.Name, 0.15f, i2.Quantity, "Phil", DateTime.Now);
            DB_Facade.AddTransactionLogEntry(tr2);

            Item_DTO dto = DB_Item.GetItem(1);
            // Employee returnObj = DB_Employee.FindEmployee("Phil");

            Assert.AreEqual(1, dto.Item.ID);
        }

        /*
        [TestMethod]
        public void T02_TestConnectionToDatabase_Closed()
        {
            MySqlDatabaseGateway DB_Gateway = new MySqlDatabaseGateway();

            MySqlConnection DB_Conn = DB_Gateway.GetMySqlConnection();

            DB_Gateway.CloseMySqlConnection(DB_Conn);

            Assert.AreEqual(true, checkLine);
        }
        



    }

    */
}
