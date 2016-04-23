using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMe.Model
{
    public class CustomDataGridView
    {
        public List<string> Columns { get; set; }
        public List<string> Items { get; set; }

       public CustomDataGridView()
        {
            this.Columns = new List<string>();
            this.Items = new List<string>();
        }
    }
}
