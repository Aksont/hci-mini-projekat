using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciMiniProject.API
{
    class DataDateValue
    {
        public string date
        {
            get;
            set;
        }
        public double value
        {
            get;
            set;
        }

        public DataDateValue(string date, double value)
        {
            this.date = date;
            this.value = value;
        }
    }
}
