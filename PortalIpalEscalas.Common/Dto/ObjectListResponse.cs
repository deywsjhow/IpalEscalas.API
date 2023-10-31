using System;
using System.Collections.Generic;

public class ObjectListResponse<T>
{

    public bool Success { get; set; }
    public List<T> ResultList { get; set; }
    public List<InternalError> Errors { get; set; }

    public ObjectListResponse()
    {
        Success = false;
        Errors = new List<InternalError>();
    }


}
