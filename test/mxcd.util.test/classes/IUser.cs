using System;

namespace mxcd.util.test.classes
{
    public interface IUser
    {
        TimeSpan Edad { get; set; }
        int Id { get; set; }
        string NombreCompleto { get; }
    }
}