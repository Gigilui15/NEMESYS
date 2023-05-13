using NEMESYS.Models;

namespace NEMESYS.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Collection navigation property
        public List<Report> Reports { get; set; }
    }
}
