using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CBBW.BOL.ETS;

namespace CBBW.Areas.Security.ViewModel.ETS
{
    public class ETSTravellingDetailsVM
    {
        public ETSTravellingDetailsVM()
        {
            travDetails = new ETSTravellingDetails();
        }
        public string NoteNumber { get; set; }
        public string CenterCodenName { get; set; }
        public string AttachFile { get; set; }
        public int PersonType { get; set; }
        public string FromdateStr { get; set; }
        public string TodateStr { get; set; }
        public int btnSubmit { get; set; }
        public ETSTravellingDetails travDetails { get; set; }
        public List<ETSTravellingDetails> TravellingDetails { get; set; }

        public List<ETSDateWiseTour> dateTour { get; set; }

    }
}