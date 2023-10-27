using System;
using System.ComponentModel;


public class InternalError
{
    private eMessage message = eMessage.MSG_ERROR_GERAL;
    public long Code { get { return (long)message; } }
    public string Description { get; set; }

    public InternalError ()
    {
        Description = string.Empty;
    }

    public InternalError (eMessage message, string description)
    {
        this.message = message;
        Description = description;
    }
    public InternalError(string description)
    {
        Description = description;
    }
}
