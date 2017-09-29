using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashReflection.Benchmark
{
    public class TestUri
    {
        public TestUri(Uri uri)
        {
            Host = uri.Host;
        }

        public TestUri(string host)
        {
            Host = host;
        }

        private string host;
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        public string PublicHost
        {
            get { return host; }
            set { host = value; }
        }
    }
}
