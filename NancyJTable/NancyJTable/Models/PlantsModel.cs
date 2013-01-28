using System.Collections.Generic;

namespace NancyJTable.Models
{
    public class PlantsModel
    {
        public string Result { get; set; }
        public IList<PlantModel> Records { get; set; }
        public int TotalRecordCount { get; set; }
    }
}