using DTOs;
using System;
using System.Data;
using MySqlConnector;

namespace DatabaseGateway
{
    // This class defines the template pattern
    public abstract class MySqlDatabaseGateway
    {
        private MySqlDatabaseConnectionPool ConnectionPool;

        // Abstract SQL strings to be implemented in child classes
        protected abstract string ProcessSQL_Create { get; }
        protected abstract string ProcessSQL_Read_Find_One { get; }
        protected abstract string ProcessSQL_Read_All { get; }
        protected abstract string ProcessSQL_Update { get; }
        

        public MySqlDatabaseGateway()
        {
            this.ConnectionPool = MySqlDatabaseConnectionPool.GetInstance();
        }

        protected void CloseMySqlConnection(MySqlConnection conn)
        {
            this.ConnectionPool.ReleaseConnection(conn);
        }

        protected MySqlConnection GetMySqlConnection()
        {
            return this.ConnectionPool.AcquireConnection();
        }


        // This implements the Template Method design pattern usind CRUD functions
        public Template_DTO Create(object objectToCreate)
        {
            // define static parts of the template in this method
            Template_DTO insert_check;
            Template_DTO return_dto;
            MySqlConnection conn = GetMySqlConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = ProcessSQL_Create,
                CommandType = CommandType.Text
            };

            try
            {
                // call template method (gateway specific portion depending on context
                // - implemented in the specific Gateway class with custom query and logic) 
                insert_check = Do_Create(command, objectToCreate);
                return_dto = new(insert_check.Status, insert_check.Message);
            }
            catch (Exception e)
            {
                // throw new Exception(e.Message, e);
                return_dto = new("ERROR", e.Message);
            }

            CloseMySqlConnection(conn);

            return return_dto;
        }


        public Template_DTO Read_Find_One(object itemIdToFind)
        {
            Template_DTO find_one_check;
            Template_DTO return_dto;
            MySqlConnection conn = GetMySqlConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = ProcessSQL_Read_Find_One,
                CommandType = CommandType.Text
            };

            try
            {
                find_one_check = Do_Read_Find_One(command, itemIdToFind);
                if (find_one_check != null && find_one_check.Status == "OK")
                {
                    return_dto = new(find_one_check.Status, find_one_check.Message, find_one_check.ReturnObject);
                }
                else
                {
                    return_dto = new(find_one_check.Status, find_one_check.Message);
                }

            }
            catch (Exception e)
            {
                return_dto = new("ERROR", e.Message);
            }

            CloseMySqlConnection(conn);

            return return_dto;
        }


        public Template_DTO Read_All()
        {
            Template_DTO read_all_check;
            Template_DTO return_dto;
            MySqlConnection conn = GetMySqlConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = ProcessSQL_Read_All,
                CommandType = CommandType.Text
            };

            try
            {
                read_all_check = Do_Read_All(command);
                if (read_all_check != null && read_all_check.Status == "OK")
                {
                    return_dto = new(read_all_check.Status, read_all_check.Message, read_all_check.ReturnObject);
                }
                else
                {
                    return_dto = new(read_all_check.Status, read_all_check.Message);
                }
            }
            catch (Exception e)
            {
                return_dto = new("ERROR", e.Message);
            }

            CloseMySqlConnection(conn);

            return return_dto;
        }


        public Template_DTO Update_Add(object itemIdToUpdate, int qtyToAdd)
        {
            Template_DTO qty_add_check;
            Template_DTO return_dto;
            MySqlConnection conn = GetMySqlConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = ProcessSQL_Update,
                CommandType = CommandType.Text
            };

            try
            {
                qty_add_check = Do_Update_Add(command, itemIdToUpdate, qtyToAdd);
                return_dto = new(qty_add_check.Status, qty_add_check.Message);
            }
            catch (Exception e)
            {
                return_dto = new("ERROR", e.Message);
            }

            CloseMySqlConnection(conn);

            return return_dto;
        }


        public Template_DTO Update_Remove(object itemIdToUpdate, int qtyToRemove)
        {
            Template_DTO qty_add_check;
            Template_DTO return_dto;
            MySqlConnection conn = GetMySqlConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = ProcessSQL_Update,
                CommandType = CommandType.Text
            };

            try
            {
                qty_add_check = Do_Update_Remove(command, itemIdToUpdate, qtyToRemove);
                return_dto = new(qty_add_check.Status, qty_add_check.Message);
            }
            catch (Exception e)
            {
                return_dto = new("ERROR", e.Message);
            }

            CloseMySqlConnection(conn);

            return return_dto;
        }

        // // Abstract operation methods to be implemented in child classes
        protected abstract Template_DTO Do_Create(MySqlCommand command, object objectToInsert);
        protected abstract Template_DTO Do_Read_Find_One(MySqlCommand command, object itemIdToFind);
        protected abstract Template_DTO Do_Read_All(MySqlCommand command);
        protected abstract Template_DTO Do_Update_Add(MySqlCommand command, object itemIdToUpdate, int qtyToAdd);
        protected abstract Template_DTO Do_Update_Remove(MySqlCommand command, object itemIdToUpdate, int qtyToRemove);

    }
}
