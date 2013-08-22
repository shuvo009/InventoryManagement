using System;

namespace InventoryManagement.Dreamer.Domain.Metadata
{
    public class SearchMetadata
    {
        public SearchMetadata()
        {
            To =
                From = DateTime.Today;
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}