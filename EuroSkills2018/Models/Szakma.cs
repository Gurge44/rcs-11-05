using System.ComponentModel.DataAnnotations;

namespace EuroSkills2018.Models
{
    public class Szakma
    {
        [Key]
        public string Id { get; set; }
        public string Nev { get; set; }
        public ICollection<Versenyzo> Versenyzok { get; set; }
    }
}
