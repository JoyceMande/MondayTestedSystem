using System.ComponentModel.DataAnnotations;

namespace SCRIPTERS.Core
{
    public class SmsModel
    {
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber), Display(Name = "Phone Number")]
        [Required]
        public string ToNo { get; set; }

        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string SmsBody { get; set; }
        
    }
}