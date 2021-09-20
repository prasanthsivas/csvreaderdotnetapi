using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csvreaderdotnetapi.Models
{
    public class Caption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long captionId { get; set; }
        public string imageName { get; set; }
        public int commentNumber  { get; set; }
        public string comment { get; set; }

    }
}
