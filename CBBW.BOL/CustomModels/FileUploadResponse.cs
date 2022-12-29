using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBBW.BOL.CustomModels
{
    public class FileUploadResponse
    {
        public string FileName { get; set; }
        public int ResponseStat { get; set; }
        public string ResponseMsg { get; set; }
    }
}
