using System;
using System.ComponentModel.DataAnnotations;

namespace PRSCapstone.Models {
    public class RequestLines {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)] public int Qty { get; set; } = 0;

        //public virtual Request Request { get; set; }
        public virtual Product Product { get; set; }

        public RequestLines() { }

    }
}
