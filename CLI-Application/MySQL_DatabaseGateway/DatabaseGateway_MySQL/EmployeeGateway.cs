using Entities;
using DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace DatabaseGateway
{
    public class EmployeeGateway : MySqlDatabaseGateway
    {
        // Template implementations of SQL strings
        protected override string ProcessSQL_Create { get; }
            = "INSERT INTO `employee` (`EmployeeName`) VALUES (@name);";

        protected override string ProcessSQL_Read_Find_One { get; }
            = "SELECT `EmployeeName` FROM `employee` WHERE `EmployeeName` = @name;";

        protected override string ProcessSQL_Read_All { get; }
            = "SELECT * FROM `employee`;";

        protected override string ProcessSQL_Update { get; }
            = "";


        public EmployeeGateway()
        {
        }


        // Template implementation of Do_Create method
        protected override Template_DTO Do_Create(MySqlCommand command, object objectToInsert)
        {
            Employee e = (Employee)objectToInsert;
            string employeeName = e.EmpName;
            Employee employeeReturned;
            Template_DTO return_dto;
            string status;
            string message;

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@name", employeeName);
                int numRowsAffected = command.ExecuteNonQuery();

                if (numRowsAffected != 1)
                {
                    status = "ERROR";
                    message = "Item not inserted.";
                    return_dto = new Template_DTO(status, message);
                }
                else
                {
                    // Item successfully inserted, fetch the added employees's details
                    Template_DTO checkEmployee = Do_Read_Find_One(command, employeeName);

                    if (checkEmployee.Status == "OK")
                    {
                        employeeReturned = (Employee)checkEmployee.ReturnObject;
                        status = "OK";
                        message = "Item added.";
                        return_dto = new Template_DTO(status, message, employeeReturned);
                    }
                    else
                    {
                        status = "ERROR";
                        message = "(E-01): Item insertion succeeded, but retrieval failed.";
                        return_dto = new Template_DTO(status, message);
                    }
                }
                return return_dto;
            }
            catch (Exception ex)
            {
                status = "ERROR";
                message = "Adding item failed: " + ex;
                return_dto = new Template_DTO(status, message);
                return return_dto;
            }
        }


        // Template implementation of Do_Read_Find_One method
        protected override Template_DTO Do_Read_Find_One(MySqlCommand command, object employeeToFind)
        {
            string employeeName = (string)employeeToFind;
            Employee employeeToReturn = null;
            Template_DTO return_dto = null;
            string status = "";
            string message = "";

            try
            {
                command.Prepare();
                command.Parameters.AddWithValue("@name", employeeName);
                MySqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    employeeToReturn = new Employee(dataReader.GetString(0));
                    status = "OK";
                    message = "Item found successfully.";

                }
                return_dto = new(status, message, employeeToReturn);
              
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


        // Template implementation of Do_Read_All method
        protected override Template_DTO Do_Read_All(MySqlCommand command)
        {
            Template_DTO return_dto = null;
            string status = null;
            string message = null;
            List<Employee> employees = new List<Employee>();

            try
            {
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Employee employee = new(dataReader.GetString(1));
                    employees.Add(employee);
                }
                dataReader.Close();

                status = "OK";
                message = "Items found successfully.";
                return_dto = new(status, message, employees);

            }
            catch (Exception e)
            {
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
