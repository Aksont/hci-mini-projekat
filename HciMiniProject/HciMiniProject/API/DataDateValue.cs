using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciMiniProject.API
{
    public class DataDateValue
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

        public DateTime dateTime { get; private set; }

        public DataDateValue(string date, double value)
        {
            this.date = date;
            this.value = value;
            dateTime = DateTime.ParseExact(date, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
        }

        public DataDateValue(string startDate, string endDate, double value)
        {
            date = startDate + "-" + endDate;
            this.value = value;
        }
    }
}
