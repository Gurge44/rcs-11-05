using System.ComponentModel.DataAnnotations;

namespace EuroSkills2018.Models
{
    public class Orszag
    {
        [Key]
        public string Id { get; set; }
        public string Nev { get; set; }
        public ICollection<Versenyzo> Versenyzok { get; set; }
    }
}
