using System;
using System.Collections.Generic;
using System.Text;

namespace TARge25Shop.Core.Domain
{
    public class RealEstate
    {
        public Guid? ID { get; set; }
        public double? Area { get; set; }
        public string Location { get; set; }
        public int RoomNumber { get; set; }
        public string BuildingType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
