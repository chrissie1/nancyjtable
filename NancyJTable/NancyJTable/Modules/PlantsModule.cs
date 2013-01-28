using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using NancyJTable.Models;

namespace NancyJTable.Modules
{
    public class PlantsModule:NancyModule
    {
        public PlantsModule()
        {
            Get["/"] = parameters => View["Plants"];
            Get["/plants/{Id}"] = parameters => View[GetPlantModels().SingleOrDefault(x => x.Id == parameters.Id)];
            Post["/plants"] = parameters =>
                {
                    var paging = this.Bind<PagingParameters>();
                    var plantsModel = new PlantsModel
                        {
                            Result = "OK",
                            Records = new List<PlantModel>(),
                            TotalRecordCount = 25
                        };
                    plantsModel.Records = paging.jtSorting == "Name ASC" ? GetPlantModels().OrderBy(x => x.Name).Skip(paging.jtStartIndex).Take(paging.jtPageSize).ToList() : GetPlantModels().OrderByDescending(x => x.Name).Skip(paging.jtStartIndex).Take(paging.jtPageSize).ToList();
                    return Response.AsJson(plantsModel);
                };
        }

        private IList<PlantModel> GetPlantModels()
        {
            var plantModels = new List<PlantModel>();
            for (var i = 1; i <= 25; i++)
            {
                var j = i.ToString("000");
                plantModels.Add(new PlantModel()
                {
                    Id = i,
                    Name = "name" + j,
                    Genus = "genus" + j,
                    Species = "Species" + j,
                    DateAdded = DateTime.Now
                });
            }
            return plantModels;
        }
    }

    public class PagingParameters
    {
        public int jtStartIndex { get; set; }
        public int jtPageSize { get; set; }
        public string jtSorting { get; set; }
    }
}