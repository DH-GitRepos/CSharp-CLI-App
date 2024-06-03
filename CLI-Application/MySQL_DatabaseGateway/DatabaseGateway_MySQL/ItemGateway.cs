using Entities;
using DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace DatabaseGateway
{
    public class ItemGateway : MySqlDatabaseGateway
    {
        // Template implementations of SQL strings
        protected override string ProcessSQL_Create { get; }
            = "INSERT INTO `item` (`ItemID`, `ItemQty`, `ItemName`, `DateCreated`) " +
                "VALUES (@id, @qty, @name, @date);";

        protected override string ProcessSQL_Read_Find_One { get; }
            = "SELECT `ItemID`,`ItemName`,`ItemQty`,`DateCreated` FROM `item` WHERE `ItemID` = @id;";

        protected override string ProcessSQL_Read_All { get; }
            = "SELECT * FROM `item`;";

        protected override string ProcessSQL_Update { get; }
            = "UPDATE `item` SET `ItemQty` = @qty WHERE `ItemID` = @id;";


        public ItemGateway()
        {
        }


        // Template implementation of Do_Create method
        protected override Template_DTO Do_Create(MySqlCommand command, object objectToInsert)
        {
            Item i = (Item)objectToInsert;
            Template_DTO return_dto;
            string status;
            string message;

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@id", i.ID);
                command.Parameters.AddWithValue("@qty", i.Quantity);
                command.Parameters.AddWithValue("@name", i.Name);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                int numRowsAffected = command.ExecuteNonQuery();

                if (numRowsAffected == 1)
                {
                    status = "OK";
                    message = "Item inserted sucessfully.";
                    return_dto = new Template_DTO(status, message);
                }
                else
                {
                    status = "ERROR";
                    message = "Item not inserted.";
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
        protected override Template_DTO Do_Read_Find_One(MySqlCommand command, object itemIdToFind)
        {
            int i = (int)itemIdToFind;
            Item itemToReturn = null;
            Template_DTO return_dto = null;
            string status = "";
            string message = "";

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@id", i);
                MySqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    itemToReturn = new Item(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetDateTime(3));
                    status = "OK";
                    message = "Item found successfully.";
                    
                }
                return_dto = new(status, message, itemToReturn);
                dataReader.Close();
            }
            catch (Exception e)
            {
                status = "ERROR";
                message = "Retrieval of item failed: " + e.ToString();
                return_dto = new(status, message);
            }

           return return_dto;
        }


        protected override Template_DTO Do_Read_All(MySqlCommand command) 
        {
            Template_DTO return_dto = null;
            string status = null;
            string message = null;
            Dictionary<int, Item> items = new Dictionary<int, Item>();

            try
            {
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Item item = new Item(dataReader.GetInt32(0), dataReader.GetString(2), dataReader.GetInt32(1), dataReader.GetDateTime(3));
                    items.Add(item.ID, item);
                }
                dataReader.Close();

                status = "OK";
                message = "Items found successfully.";
                return_dto = new(status, message, items);

            }
            catch (Exception e)
            {
                status = "ERROR";
                message = "Get items failed: " + e;
                return_dto = new(status, message);                
            }

            return return_dto;
        }


        // Template implementation of Do_Read_All method
        protected override Template_DTO Do_Update_Add(MySqlCommand command, object itemToUpdate, int qtyToAdd)
        {
            Template_DTO return_dto = null;
            Item updateItem = (Item)itemToUpdate;
            int newQuantity = updateItem.Quantity + qtyToAdd;
            string status = null;
            string message = null;

            if (qtyToAdd < 0) 
            {
                status = "ERROR";
                message = "Adding quantity to item failed, quantity less than zero.";
                return_dto = new(status, message);
            }
            else
            {
                try
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@id", updateItem.ID);
                    command.Parameters.AddWithValue("@qty", newQuantity);

                    int rowsAffected = command.ExecuteNonQuery(); // Execute UPDATE query

                    if (rowsAffected > 0)
                    {
                        status = "OK";
                        message = $"Item {updateItem.ID}:{updateItem.Name} updated. {qtyToAdd} added, new total {newQuantity}.";
                        return_dto = new(status, message);
                    }
                    else
                    {
                        status = "ERROR";
                        message = "(E-04): Adding quantity to item failed.";
                        return_dto = new(status, message);
                    }

                }
                catch (Exception e)
                {
                    // Handle exceptions, return error DTO
                    status = "ERROR";
                    message = "(E-05): Adding quantity to item failed: " + e.ToString();
                    return_dto = new(status, message);
                }
            }            

            return return_dto;
        }


        // Template implementation of Do_Update_Add method
        protected override Template_DTO Do_Update_Remove(MySqlCommand command, object itemToUpdate, int qtyToRemove)
        {
            Template_DTO return_dto = null;
            Item updateItem = (Item) itemToUpdate;
            int newQuantity = updateItem.Quantity - qtyToRemove;
            string status = null;
            string message = null;


            if (qtyToRemove < 0 || newQuantity < 0)
            {
                status = "ERROR";
                message = "Removing quantity from item failed, quantity less than zero or greater than stock.";
                return_dto = new(status, message);
            }
            else
            {
                try
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@id", updateItem.ID);
                    command.Parameters.AddWithValue("@qty", newQuantity);

                    int rowsAffected = command.ExecuteNonQuery(); // Execute UPDATE query

                    if (rowsAffected > 0)
                    {
                        status = "OK";
                        message = $"Item {updateItem.ID}:{updateItem.Name} updated. {qtyToRemove} removed, new total {newQuantity}.";
                        return_dto = new(status, message);
                    }
                    else
                    {
                        status = "ERROR";
                        message = "(E-04): Adding quantity to item failed.";
                        return_dto = new(status, message);
                    }

                }
                catch (Exception e)
                {
                    // Handle exceptions, return error DTO
                    status = "ERROR";
                    message = "(E-05): Adding quantity to item failed: " + e.ToString();
                    return_dto = new(status, message);
                }
            }

            return return_dto;

        }

    }
}
