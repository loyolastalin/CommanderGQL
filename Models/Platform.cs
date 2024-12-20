using System.ComponentModel.DataAnnotations;

namespace CommanderGQL.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 

         public string LicenseKey { get; set; }
    }

      public record AddPlatformInput(string Name, string LicenseKey);  
       public record AddPlatformPayload(Platform platform);
}