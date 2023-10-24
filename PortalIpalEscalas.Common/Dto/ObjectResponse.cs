using System;
using System.Collections.Generic;

public class ObjectResponse<T>
{
    
    public bool Success { get; set; }
    public T Result { get; set; }
    public List<InternalError> Errors { get; set; }

    public ObjectResponse()
    {
        Success = false;
        Errors = new List<InternalError>();
    }

   
}
