namespace ModelLayer.Models.CartModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CartResponseModel
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int BookQuantity { get; set; }
    }
}
