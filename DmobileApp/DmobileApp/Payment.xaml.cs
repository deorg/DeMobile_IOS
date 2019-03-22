using System;
using System.Collections.Generic;
using DmobileApp.Model;
using Xamarin.Forms;
using DmobileApp.Services;
using System.Diagnostics;

namespace DmobileApp
{
    public partial class Payment : ContentPage
    {
        private contract_data _contract;
        private profile_data _profile;
        private m_banks _banks;
        private string _deviceId;
        public Payment(profile_data profile, contract_data contract, string deviceId)
        {
            InitializeComponent();
            //BindingContext = contract;
            _contract = contract;
            _profile = profile;
            _deviceId = deviceId;
            Title = "สัญญา " + contract.con_no;
            txtBanlance.Text = String.Format("{0:#,##0}", contract.bal_amt);//contract.bal_amt.ToString();
            txtPay.Text = String.Format("{0:#,##0}", contract.pay_amt);//contract.pay_amt.ToString();
            txtDiscount.Text = String.Format("{0:#,##0}", contract.disc_amt);//contract.disc_amt.ToString();
            _banks = Payments.getBanks();
            //img0.Source = _banks.data[0].channel_img;
            btnBank0.Source = _banks.data[0].channel_img;
            btnBank1.Source = _banks.data[1].channel_img;
            btnBank2.Source = _banks.data[2].channel_img;
            btnBank3.Source = _banks.data[3].channel_img;
            btnBank4.Source = _banks.data[4].channel_img;
            btnBank5.Source = _banks.data[5].channel_img;
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            txtBanlance.Unfocus();
        }

        void Handle_Focused_1(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            txtDiscount.Unfocus();
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            txtPay.Text = txtPay.Text.Replace(".", "");
        }

        void Period_Clicked(object sender, System.EventArgs e)
        {
            txtPay.Text = _contract.pay_amt.ToString();
        }

        void Total_Clicked(object sender, System.EventArgs e)
        {
            var abs_bal = _contract.bal_amt - _contract.disc_amt;
            txtPay.Text = abs_bal.ToString();
        }

        async void PayBank_ClickedAsync(object sender, System.EventArgs e)
        {
            var item = sender as ImageButton;
            var pay = double.Parse(txtPay.Text);
            if (pay > _contract.bal_amt - _contract.disc_amt)
                await DisplayAlert("ยืนยันการชำระเงิน", "ไม่สามารถชำระเงินได้ เนื่องจากจำนวนเงินที่ต้องการชำระเกินจำนวนยอดคงเหลือหลังจากหักส่วนลดแล้ว!", "ตกลง");
            else
            {
                
                var payTransaction = pay.ToString("0.00");
                var index2 = item.StyleId.Replace("btnBank", "");
                var index = Int32.Parse(index2);
                var bank = _banks.data[index];
                if (bank.channel_id != "internetbank_scb" && bank.channel_id != "payplus_kbank")
                {
                    var answer = await DisplayAlert("ยืนยันการชำระเงิน", $"คุณต้องการชำระเงินจำนวน {pay} บาท ผ่านธนาคาร {bank.channel_name} ใช่หรือไม่", "ตกลง", "ยกเลิก");
                    if (answer)
                    {
                        payTransaction = payTransaction.Replace(".", "");
                        var payOrder = Int32.Parse(payTransaction);
                        var order = new m_newOrder
                        {
                            CustomerId = _profile.CUST_NO,
                            ContractNo = _contract.con_no,
                            DeviceId = _deviceId,
                            Amount = payOrder,
                            PhoneNumber = _profile.TEL,
                            Description = "Test API",
                            ChannelCode = bank.channel_id
                        };
                        var result = Payments.newOrder(order);
                        if(result.code == 200)
                        {
                            if (result.data.Code == 200)
                            {
                                Application.Current.MainPage = new NavigationPage(new PaymentView(result.data.PaymentUrl, _profile, _deviceId));
                                //  NavigationPage(new Mainpage(resIdentify.data, deviceId));
                                //await Navigation.PushAsync(new PaymentView(result.data.PaymentUrl));
                                //this.Navigation.RemovePage(this);
                            }
                            else
                                await DisplayAlert("ไม่สามารถทำรายการชำระได้", result.data.Message, "ตกลง");
                        }
                        else
                        {

                            await DisplayAlert("สามารถทำรายการชำระได้", result.message, "ตกลง");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("ไม่สามารถทำรายการชำระได้", $"บริการชำระเงินผ่านธนาคาร {bank.channel_name} ปิดชั่วคราว", "ตกลง");
                }
            }
        }
    }
}
