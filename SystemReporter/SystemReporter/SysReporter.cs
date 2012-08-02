using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemReporter
{
    class SysReporter
    {
        private Object _configuration;
        private String _version;

        public SysReporter()
        {
            this._configuration = new Object();
            this._version = "1.0";
            
        }

        public String getVersion
        {
            get { return this._version; }
            set { this._version = value; }
        }
    }
}
