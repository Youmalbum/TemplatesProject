using System;
using System.Collections.Generic;
using System.Text;

namespace NR.Core.ModelsView
{
    public class ResultView
    {
        public int LastId { get; set; }

        public int RowsAfter { get; set; }

        public string ErrorMessage { get; set; }

        public string InfoMessage { get; set; }

        public string ErrorCode { get; set; }

        public bool ResultAfterExe { get; set; }

        public bool isOk { get; set; }

        public bool isSavedVentas { get; set; }

        public bool isSentEmailInvoice { get; set; }

        public bool isSentPrint { get; set; }
    }
}
