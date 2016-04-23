using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RentMe.Model;

namespace RentMe.DAL.Interfaces
{
    public interface IAdminInterface
    {
        List<CustomDataGridView> RunSqlQuery(string query);
    }
}
