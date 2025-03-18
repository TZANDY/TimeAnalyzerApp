namespace TimeAnalyzerApp.Models.ViewModel
{
    public class Response<T>:BaseResponse
    {
        public T Result { get; set; }
        public List<T> Data { get; set; }
        public Response()
        {
            Success = false;
            Message = "No data found";
        }
        public Response(T result)
        {
            if (result == null)
            {
                Success = false;
                Message = "No data found";
                return;
            }

            Result = result;
            Success = true;
            Message = "Success";
        }

        // Constructor para una lista de resultados
        public Response(List<T> data)
        {
            if (data == null || data.Count == 0)
            {
                Success = false;
                Message = "No data found";
                return;
            }

            Data = data;
            Success = true;
            Message = "Success";
        }
    }
}
