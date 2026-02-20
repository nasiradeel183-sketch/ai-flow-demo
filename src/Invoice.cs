using System;

namespace src
{
    public class Invoice
    {
        public object SupplierId { get; internal set; }
        public string ErpType { get; internal set; }

        internal bool IsValid()
        {
            return true;
        }
    }
}