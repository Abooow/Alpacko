using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.Client.Web.Models
{
    public class PackageInfoModel
    {
        public string Size { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
    }
}