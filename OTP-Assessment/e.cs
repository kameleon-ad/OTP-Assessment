using System;
using System.Runtime.Serialization;

[Serializable]
internal class e : Exception
{
    public e()
    {
    }

    public e(string message) : base(message)
    {
    }

    public e(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected e(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}