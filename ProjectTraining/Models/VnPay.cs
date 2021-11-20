using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class VnPay
    {
        private string vnp_Version { set; get; }
        private string vnp_Command { set; get; }
        private string vnp_Amount { set; get; }
        private string vnp_TmnCode { set; get; }
        private string vnp_BankCode { set; get; }
        private string vnp_BankTranNo { set; get; }
        private string vnp_CardType { set; get; }
        private string vnp_OrderInfo { set; get; }
        private string vnp_PayDate { set; get; }
        private string vnp_ResponseCode { set; get; }
        private string vnp_TransactionNo { set; get; }
        private string vnp_TxnRef { set; get; }
        private string vnp_SecureHashType { set; get; }
        private string vnp_SecureHash { set; get; }
    }
}
