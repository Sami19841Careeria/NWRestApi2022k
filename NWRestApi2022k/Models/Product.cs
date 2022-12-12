using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace NWRestApi2022k.Models
{
    public partial class Product
    {
        //public Product()
        //{
        //    OrderDetails = new HashSet<OrderDetail>();
        //}

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }

        [JsonConverter(typeof(BooleanConverter))]
        public bool? Discontinued { get; set; }
        //public string? Rpaprocessed { get; set; }

        //public virtual Category? Category { get; set; }
        //public virtual Supplier? Supplier { get; set; }
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
