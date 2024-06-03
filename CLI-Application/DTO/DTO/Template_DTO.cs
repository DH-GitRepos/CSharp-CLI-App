namespace DTOs
{
    public class Template_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public object ReturnObject { get; private set; }

        public Template_DTO(object returnObject)
        {
            this.ReturnObject = returnObject;
        }

        public Template_DTO(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
        public Template_DTO(string status, string message, object returnObject)
        {
            this.Status = status;
            this.Message = message;
            this.ReturnObject = returnObject;
        }

    }
}
