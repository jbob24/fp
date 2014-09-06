using FinanceProjector.Enums;

namespace FinanceProjector.Models
{
    public abstract class ResponseBase
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public void SetStatus(ResponseStatus status)
        {
            this.Status = status;
        }

        public void SetStatus(ResponseStatus status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
