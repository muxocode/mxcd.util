using System;
using System.Collections.Generic;
using System.Text;

namespace mxcd.util.test.classes
{
    public class User : IUser
    {
        public int Id { get; set; }
        public DateTime birth;
        public TimeSpan Edad
        {
            get
            {
                return DateTime.Now - this.birth;
            }
            set
            {
                this.birth = DateTime.Now - value;
            }
        }
        public string nombre;
        public string apelido;
        public string NombreCompleto { get => $"{nombre} {apelido}"; }
    }
}
