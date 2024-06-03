using DatabaseGateway;
using DTOs;

namespace CommandLineUI.Commands
{
    public class Command_Support_InitialiseDatabase : UI_Command<Command_DTO>
    {
        private readonly InitialiseDatabaseGateway DB_Init;

        public Command_DTO ReturnData { get; private set; }

        public Command_Support_InitialiseDatabase()
        {
            DB_Init = new InitialiseDatabaseGateway();
        }

        // UI_Command interface implementation of Start_Command_Process method
        public Command_DTO Start_Command_Process()
        {
            // Initialise database
            DBConnection_DTO DB_dto = DB_Init.InitialiseMySqlDatabase();

            if (DB_dto != null && DB_dto.Status != "ERROR")
            {
                // Populate database
                CommandFactory cf_addEmp1 = new CommandFactoryBuilder().Build();
                Command_DTO populateStatus = cf_addEmp1.CreateCommand(UI_Command<Command_DTO>.POPULATE_TEST_DATA).Start_Command_Process();

                if (populateStatus.Status == "OK")
                {
                    ReturnData = new Command_DTO("OK", "Database initialised and populated with test data.");
                }
                else
                {
                    ReturnData = new Command_DTO(DB_dto.Status, DB_dto.Message);
                }
            }
            else
            {
                ReturnData = new Command_DTO(DB_dto.Status, DB_dto.Message);
            }
            return ReturnData;
        }

        // UI_Command interface implementation of Execute_Command_Process method
        public Command_DTO Execute_Command_Process() { return new Command_DTO(new Null_DTO()); }

        // UI_Command interface implementation of Command_Process_Validator method
        public Command_DTO Command_Process_Validator() { return new Command_DTO(new Null_DTO()); }
    }
}