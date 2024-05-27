using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WpfAccess185_2.Models
{
    public class Category
    {
        public int CategoryId { set; get; }
        public string CategoryName { set; get; }
        public Category(int id, string name)
        {
            CategoryId = id;
            CategoryName = name;
        }
    }
}