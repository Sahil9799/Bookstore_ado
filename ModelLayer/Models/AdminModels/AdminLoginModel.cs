namespace ModelLayer.Models.AdminModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AdminLoginModel
    {
        [Required]
        [DefaultValue("")]
        public string AdminEmailId { get; set; }

        [Required]
        [DefaultValue("")]
        public string AdminPassword { get; set; }
    }
}
