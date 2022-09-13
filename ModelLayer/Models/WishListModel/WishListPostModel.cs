namespace ModelLayer.Models.WishListModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class WishListPostModel
    {
        [Required]
        public int BookId { get; set; }
    }
}