using System;
using DatabaseGateway;
using DTOs;
using Entities;

namespace CommandLineUI.Commands
{
    public class Command_Support_AddTransactionLogEntry : UI_Command<Command_DTO>
    {
        private readonly DataGatewayFacade DB_Facade;
        public Command_DTO ReturnData { get; private set; }
        public TransactionLogEntry Transaction { get; private set; }

        public Command_Support_AddTransactionLogEntry(TransactionLogEntry transaction)
        {
            DB_Facade = new DataGatewayFacade();
            Transaction = transaction;
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            try
            {
                Template_DTO dto = DB_Facade.Transaction_Create(Transaction);
                ReturnData = new Command_DTO(dto.Status, dto.Message);
            }
            catch (Exception e)
            {
                ReturnData = new Command_DTO("ERROR", e.Message);
            }

            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}