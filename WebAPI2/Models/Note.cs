using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public int Code { get; set; }
        [Required]
        public string Value { get; set; }

        public Note(int code, string value)
        {
            Code = code;
            Value = value;
        }
    }
}
