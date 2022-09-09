namespace ModelLayer.Models.AdminModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminResponseModel
    {
        public int AdminId {get; set;}

        public string AdminName { get; set; }

        public string AdminEmailId { get; set; }

        public string AdminAddress { get; set; }

        public long AdminMobile { get; set; }

        public string AdminPassword { get; set; }
    }
}
