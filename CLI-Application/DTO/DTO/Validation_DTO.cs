using System.Collections.Generic;

namespace DTOs
{
    public class Validation_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public List<string> Feedback { get; private set; }

        public Validation_DTO(string status, string message, List<string> feedback)
        {
            this.Status = status;
            this.Message = message;
            this.Feedback = feedback;
        }
    }
}
