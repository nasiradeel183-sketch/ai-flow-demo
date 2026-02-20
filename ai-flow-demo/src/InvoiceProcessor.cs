using System;

namespace src
{
    public class InvoiceProcessor
    {
        public void Process(Invoice invoice)
        {
            if (!invoice.IsValid())
            {
                Reject("Invalid payload");
                return;
            }

            if (!SupplierExists(invoice.SupplierId))
            {
                Reject("Supplier does not exist");
                return;
            }

            switch (invoice.ErpType)
            {
                case "D365":
                    PostToD365(invoice);
                    break;

                case "SAP":
                    PostToSap(invoice);
                    break;

                default:
                    Reject("Unsupported ERP");
                    break;
            }

            MarkAsPosted(invoice);
        }

        private bool SupplierExists(object supplierId)
        {
            return true;
        }

        void Reject(string reason) { }
        bool SupplierExists(string id) => true;
        void PostToD365(Invoice invoice) { }
        void PostToSap(Invoice invoice) { }
        void MarkAsPosted(Invoice invoice) { }
    }
    public class Invoice
    {
        public string Source { get; set; }
        public string ErpType { get; internal set; }
        public object SupplierId { get; internal set; }

        internal bool IsValid()
        {
            return true;
        }
    }

}
