using Newtonsoft.Json;
using Sainsburys.DepotManagement.Shared.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.Depot.Tests.Models
{
    public class ODataSingleResponse : DepotODataViewModel
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }

    }
}
