using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Services.Common
{
    #region Classes
    public class Raw
    {
    }

    public class DDLModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LVL { get; set; }
    }

    public class TypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class DDLCampaigns
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentCampaignId { get; set; }

    }

    public class BatchExpiryDate
    {
        public DateTime? ExpiryDate { get; set; }

        public DateTime? ManufacturingDate { get; set; }
        public bool IsBatchNoSame { get; set; }
    }

    #endregion
}
