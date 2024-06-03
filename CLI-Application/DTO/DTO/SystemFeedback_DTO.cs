using System.Collections.Generic;

namespace DTOs
{
    public class SystemFeedback_DTO
    {
        public string ContentTypeDescription { get; private set; }
        
        public List<string> Output { get; private set; }

        public double Cost { get; private set; }

        public SystemFeedback_DTO(string contentTypeDescription, List<string> output)
        {
            this.ContentTypeDescription = contentTypeDescription;
            this.Output = output;
        }

        public SystemFeedback_DTO(string contentTypeDescription, List<string> output, double cost)
        {
            this.ContentTypeDescription = contentTypeDescription;
            this.Output = output;
            this.Cost = cost;
        }
    }
}
