using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intentio
{
    
      public sealed class LabelClassifier : ISocket <string>
        {
            private const int LC_CL_Port = 65434;

            public LabelClassifier() : base("localhost", LC_CL_Port) { }

            protected override string Parse(string input) => input;

            
        }
    
}
