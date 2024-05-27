using System.IO;
namespace WpfAccess185_2.Models
{
    public class Good
    {
        public int GoodId { set; get; }
        public string GoodName { set; get; }
        public double Price { set; get; }
        public string Picture { set; get; }
        public string Description { set; get; }
        public int CategoryId { set; get; }
        public string GetPhoto
        {
            get
            {
                if (Picture is null) return null;
                return Directory.GetCurrentDirectory()
                + @"\Images\" + Picture;
            }
        }
        public Good()
        { }
        public Good(int id, string name, double price, string photo, string descr, int catid)
        {
            GoodId = id;
            GoodName = name; Price = price;
            Picture = photo;
            Description = descr;
            CategoryId = catid;
        }
    }
}
