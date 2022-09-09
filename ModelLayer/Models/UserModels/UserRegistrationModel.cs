namespace ModelLayer.Models.UserModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class UserRegistrationModel
    {
        [Required]
        [DefaultValue("")]
  
        public string FullName { get; set; }

        [Required]
        [DefaultValue("")]
        
        public string EmailId { get; set; }

        [Required]
        [DefaultValue("")]
        
        public string Password { get; set; }

        [Required]
        [DefaultValue("91")]
        
        public long MobileNo { get; set; }
    }
}
