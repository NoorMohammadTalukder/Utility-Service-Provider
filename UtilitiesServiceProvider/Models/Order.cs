//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilitiesServiceProvider.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string Details { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
    }
}
