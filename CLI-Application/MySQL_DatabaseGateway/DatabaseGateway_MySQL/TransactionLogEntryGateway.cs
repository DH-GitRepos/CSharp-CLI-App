using Entities;
using DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace DatabaseGateway
{
    public class TransactionLogEntryGateway : MySqlDatabaseGateway
    {
        // Template implementations of SQL strings
        protected override string ProcessSQL_Create { get; }
            = "INSERT INTO `transaction_log_entry`"
                + " (`TransactionID`, `TransactionType`, `ItemID`, `ItemName`, `ItemPrice`, `TransactionQty`, `EmployeeName`, `TransactionDate`)"
                + " VALUES (NULL, @type, @itemID, @itemName, @itemPrice, @TrQty, @EmpName, @TrDate);";

        protected override string ProcessSQL_Read_Find_One { get; }
            = "SELECT * FROM `transaction_log_entry` " +
                "WHERE `TransactionType` = @type AND `ItemID` = @itemID AND `ItemName` = @itemName AND `ItemPrice` = @itemPrice " +
                "AND `TransactionQty` = @TrQty AND `EmployeeName` = @EmpName;";


        protected override string ProcessSQL_Read_All { get; }
            = "SELECT * FROM `transaction_log_entry`;";

        protected override string ProcessSQL_Update { get; }
            = "";


        public TransactionLogEntryGateway()
        {
        }


        // Template implementation of Do_Create method
        protected override Template_DTO Do_Create(MySqlCommand command, object objectToInsert)
        {
            TransactionLogEntry t = (TransactionLogEntry)objectToInsert;
            TransactionLogEntry transactionReturned;
            Template_DTO return_dto;
            string status;
            string message;

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@type", t.TypeOfTransaction);
                command.Parameters.AddWithValue("@itemID", t.ItemID);
                command.Parameters.AddWithValue("@itemName", t.ItemName);
                command.Parameters.AddWithValue("@itemPrice", t.ItemPrice);
                command.Parameters.AddWithValue("@TrQty", t.Quantity);
                command.Parameters.AddWithValue("@EmpName", t.EmployeeName);
                command.Parameters.AddWithValue("@TrDate", t.DateAdded);
                int numRowsAffected = command.ExecuteNonQuery();

                if (numRowsAffected == 1)
                {
                    // Item successfully inserted, fetch the added item's details
                    Template_DTO checkItem = Do_Read_Find_One(command, t);

                    if (checkItem.Status == "OK")
                    {
                        transactionReturned = (TransactionLogEntry)checkItem.ReturnObject;
                        status = "OK";
                        message = "Transaction added.";
                        return_dto = new Template_DTO(status, message, transactionReturned);
                    }
                    else
                    {
                        status = "ERROR";
                        message = "(E-01): Item insertion succeeded, but retrieval failed.";
                        return_dto = new Template_DTO(status, message);
                    }
                }
                else
                {
                    status = "ERROR";
                    message = "Transaction not inserted.";
                    return_dto = new Template_DTO(status, message);
                }

                return return_dto;

            }
            catch (Exception e)
            {
                status = "ERROR";
                message = "Adding item failed: " + e;
                return_dto = new Template_DTO(status, message);
                return return_dto;
            }
        }

        // Template implementation of Do_Read_Find_One method
        protected override Template_DTO Do_Read_Find_One(MySqlCommand command, object transactionToFind)
        {
            TransactionLogEntry t = (TransactionLogEntry)transactionToFind;
            TransactionLogEntry returnTransaction = null;
            Template_DTO return_dto = null;
            string status = "";
            string message = "";

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@type", t.TypeOfTransaction);
                command.Parameters.AddWithValue("@itemID", t.ItemID);
                command.Parameters.AddWithValue("@itemName", t.ItemName);
                command.Parameters.AddWithValue("@itemPrice", t.ItemPrice);
                command.Parameters.AddWithValue("@TrQty", t.Quantity);
                command.Parameters.AddWithValue("@EmpName", t.EmployeeName);
                command.Parameters.AddWithValue("@TrDate", t.DateAdded);
                MySqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    returnTransaction = new(dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetString(3), dataReader.GetDouble(4), dataReader.GetInt32(5), dataReader.GetString(6), dataReader.GetDateTime(7));
                    status = "OK";
                    message = "Transaction found successfully.";
                    return_dto = new(status, message, returnTransaction);
                }
                
                dataReader.Close();
            }
            catch (Exception e)
            {
                status = "ERROR";
                message = "Retrieval of transaction failed: " + e.ToString();
                return_dto = new(status, message);
            }

            return return_dto;
        }


        // Template implementation of Do_Read_All method
        protected override Template_DTO Do_Read_All(MySqlCommand command)
        {
            Template_DTO return_dto = null;
            string status = null;
            string message = null;
            List<TransactionLogEntry> transactions = new List<TransactionLogEntry>();

            try
            {
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    TransactionLogEntry transaction = new TransactionLogEntry(dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetString(3), dataReader.GetDouble(4), dataReader.GetInt32(5), dataReader.GetString(6), dataReader.GetDateTime(7));
                    transactions.Add(transaction);
                }
                dataReader.Close();

                status = "OK";
                message = "Items found successfully.";
                return_dto = new(status, message, transactions);

            }
            catch (Exception e)
            {
                // throw new Exception(e.Message, e);
                status = "ERROR";
                message = "Get items failed: " + e;
                return_dto = new(status, message);

            }

            return return_dto;
        }


        // Template implementation of Do_Update_Add method
        protected override Template_DTO Do_Update_Add(MySqlCommand command, object itemIdToUpdate, int qtyToAdd)
        {
            return new Template_DTO("NULL", "Not implemented", new NullEntity());
        }


        // Template implementation of Do_Update_Remove method
        protected override Template_DTO Do_Update_Remove(MySqlCommand command, object itemIdToUpdate, int qtyToRemove)
        {
            return new Template_DTO("NULL", "Not implemented", new NullEntity());
        }

    }
}
