namespace ModelLayer.Models.UserModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ResetPassModel
    {
        [Required]
        [DefaultValue("")]
        
        public string NewPassword { get; set; }

        [Required]
        [DefaultValue("")]
        public string ConfirmPassword { get; set; }
    }
}
