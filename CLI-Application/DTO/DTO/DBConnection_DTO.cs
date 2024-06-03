namespace DTOs
{
    public class DBConnection_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public DBConnection_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
