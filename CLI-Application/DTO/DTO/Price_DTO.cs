namespace DTOs
{
    public class Price_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public bool Check {  get; private set; }

        public Price_DTO(string status, string message, bool check)
        {
            this.Status = status;
            this.Message = message;
            this.Check = check;
        }

    }
}
