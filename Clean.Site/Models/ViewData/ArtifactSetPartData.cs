using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;

namespace Clean.Site.Models.ViewData
{
    public class ArtifactSetPartData
    {
        public string PartName { get; set; }
        public MediaWithCrops Image { get; set; }
        public BlockListModel Description { get; set; }
    }
}
