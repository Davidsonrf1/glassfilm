using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorView
{
    public interface ICrypto
    {
        string Encrypt(string src);
        string Decrypt(string src);
    }
}
