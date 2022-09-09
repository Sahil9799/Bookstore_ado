namespace ModelLayer.Models.UserModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class UserLoginModel
    {
        [Required]
        [DefaultValue("")]
        public string EmailId { get; set; }

        [Required]
        [DefaultValue("")]
        public string Password { get; set; }
    }
}
