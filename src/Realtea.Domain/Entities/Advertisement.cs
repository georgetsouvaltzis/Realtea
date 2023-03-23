using Realtea.Domain.Enums;

namespace Realtea.Domain.Entities
{
    public class Advertisement : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public AdvertisementType AdvertisementType { get; set; } = AdvertisementType.Free;
        
        public AdvertisementDetails AdvertisementDetails { get; set; }

        public int AdvertisementDetailsId { get; set; }
    }
}
