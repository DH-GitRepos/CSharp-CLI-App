using Entities;
using DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace DatabaseGateway
{
    public class InitialiseDatabaseGateway : MySqlDatabaseGateway
    {
        // template implementation of InsertionSQL
        protected override string ProcessSQL_Create { get; } = "";
        protected override string ProcessSQL_Read_Find_One { get; } = "";
        protected override string ProcessSQL_Read_All { get; } = "";
        protected override string ProcessSQL_Update { get; } = "";

        private MySqlCommand dropTable_Employee = new MySqlCommand
        {
            CommandText = "DROP TABLE `employee`;",
            CommandType = CommandType.Text
        };

        private MySqlCommand dropTable_Item = new MySqlCommand
        {
            CommandText = "DROP TABLE `item`;",            
            CommandType = CommandType.Text
        };

        private MySqlCommand dropTable_TransactionLogEntry = new MySqlCommand
        {
            CommandText = "DROP TABLE `transaction_log_entry`;",
            CommandType = CommandType.Text
        };

        private MySqlCommand createTable_Employee = new MySqlCommand
        {
            CommandText = "CREATE TABLE `cccp_a1`.`employee` (`EmployeeID` INT NOT NULL AUTO_INCREMENT , `EmployeeName` VARCHAR(255) NULL DEFAULT NULL , PRIMARY KEY (`EmployeeID`)) ENGINE = InnoDB;",
            CommandType = CommandType.Text
        };

        private MySqlCommand createTable_Item = new MySqlCommand
        {
            CommandText = "CREATE TABLE `cccp_a1`.`item` (`ItemID` INT NOT NULL AUTO_INCREMENT , `ItemQty` INT NULL DEFAULT NULL , `ItemName` VARCHAR(255) NULL DEFAULT NULL , `DateCreated` DATETIME NULL DEFAULT NULL , PRIMARY KEY (`ItemID`)) ENGINE = InnoDB;",
            CommandType = CommandType.Text
        };

        private MySqlCommand createTable_TransactionLogEntry = new MySqlCommand
        {
            CommandText = "CREATE TABLE `cccp_a1`.`transaction_log_entry` (`TransactionID` INT NOT NULL AUTO_INCREMENT , `TransactionType` VARCHAR(255) NOT NULL , `ItemID` INT NOT NULL , `ItemName` VARCHAR(255) NOT NULL , `ItemPrice` DOUBLE NOT NULL , `TransactionQty` INT NOT NULL , `EmployeeName` VARCHAR(255) NOT NULL , `TransactionDate` DATETIME NOT NULL , PRIMARY KEY (`TransactionID`)) ENGINE = InnoDB;",
            CommandType = CommandType.Text
        };

        private List<MySqlCommand> commandSequence;

        public InitialiseDatabaseGateway()
        {
            commandSequence = new List<MySqlCommand>()
            {
                dropTable_Employee,
                createTable_Employee,

                dropTable_Item,
                createTable_Item,

                dropTable_TransactionLogEntry,
                createTable_TransactionLogEntry,
            };
        }


        public DBConnection_DTO InitialiseMySqlDatabase()
        {
            DBConnection_DTO connection_dto;

            try
            {
                MySqlConnection conn = GetMySqlConnection();

                foreach (MySqlCommand c in commandSequence)
                {   
                    try
                    {
                        c.Connection = conn;
                        c.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        connection_dto = new("ERROR", e.Message);
                        break;
                    }
                }

                CloseMySqlConnection(conn);
                connection_dto = new("OK", "Connection established.");

            }
            catch(Exception e) 
            {
                connection_dto = new("ERROR", e.Message);
            }

            return connection_dto;            
        }


        // IMPLEMENTED TEMPLATE METHODS

        protected override Template_DTO Do_Create(MySqlCommand command, object objectToInsert)
        {
            return new Template_DTO(new NullEntity());
        }

        protected override Template_DTO Do_Read_Find_One(MySqlCommand command, object itemIdToFind)
        {
            return new Template_DTO(new NullEntity());
        }

        protected override Template_DTO Do_Read_All(MySqlCommand command)
        {
            return new Template_DTO(new NullEntity());
        }

        protected override Template_DTO Do_Update_Add(MySqlCommand command, object itemIdToUpdate, int qtyToAdd)
        {
            return new Template_DTO(new NullEntity());
        }

        protected override Template_DTO Do_Update_Remove(MySqlCommand command, object itemIdToUpdate, int qtyToRemove)
        {
            return new Template_DTO(new NullEntity());
        }

    }
}
