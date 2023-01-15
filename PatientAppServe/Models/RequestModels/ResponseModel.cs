namespace PatientAppServe.Models.ViewModels;

public class ResponseModel<T>
{
    public int TotalPages { get; set; }

    public int TotalItems { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public bool Success { get; set; }

    public string Message { get; set; }

    public T Data { get; set; }

    public ResponseModel()
    {
        TotalPages = 1;
        TotalItems = 1;
        CurrentPage = 1;
        PageSize = 1;
        Success = true;
        Message = string.Empty;
        Data = default;
    }
}