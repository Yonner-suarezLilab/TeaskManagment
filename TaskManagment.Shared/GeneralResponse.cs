
namespace TaskManagment.Shared
{
    public class GeneralResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GeneralResponse() { }

        public GeneralResponse(int _status, string _message, object _data)
        {
            Status = _status;
            Message = _message;
            Data = _data;
        }
    }
    public static class GenericoResponse
    {
        public static GeneralResponse Ok(int resultado = Variables.Results.OK, string message = "", object data = null)
        {
            if (!string.IsNullOrEmpty(message) && data != null)
            {
                return new GeneralResponse()
                {
                    Status = resultado,
                    Message = message,
                    Data = data
                };
            }

            if (!string.IsNullOrEmpty(message))
            {
                return new GeneralResponse()
                {
                    Status = resultado,
                    Message = message
                };
            }

            if (data != null)
            {
                return new GeneralResponse()
                {
                    Status = resultado,
                    Message = Variables.Messages.OK,
                    Data = data
                };
            }

            return new GeneralResponse()
            {
                Status = resultado,
                Message = Variables.Messages.OK
            };
        }
        public static GeneralResponse Error(int result = Variables.Results.ERROR, string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                return new GeneralResponse()
                {
                    Status = result,
                    Message = message
                };
            }
            return new GeneralResponse()
            {
                Status = result,
                Message = Variables.Messages.ERROR
            };
        }

    }

}
