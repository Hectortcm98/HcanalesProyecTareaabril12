using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {

        public bool Correct { get; set; } = true;
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public object Objet { get; set; }
        public List<object> ListaObjets { get; set; }
    }
}
