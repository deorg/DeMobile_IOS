using System;
namespace DmobileApp.Model
{
    public class m_newOrder
    {
        public int CustomerId { get; set; }
        public string ContractNo { get; set; }
        public string DeviceId { get; set; }
        public int Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string ChannelCode { get; set; }
    }
    public class m_resOrder
    {
        public int code { get; set; }
        public string message { get; set; }
        public resOrder_data data { get; set; }
    }
    public class resOrder_data
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public string OrderNo { get; set; }
        public string CustomerId { get; set; }
        public string ChannelCode { get; set; }
        public string ReturnUrl { get; set; }
        public string PaymentUrl { get; set; }
        public string IpAddress { get; set; }
        public string Token { get; set; }
        public string CreatedDate { get; set; }
        public string ExpiredDate { get; set; }
    }
}
