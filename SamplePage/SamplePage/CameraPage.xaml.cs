using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace SamplePage
{
    
    public partial class CameraPage : ContentPage
    {
        String scanedData;
        public CameraPage()
        {
            InitializeComponent();
            this.BindingContext = scanedData;
        }

        async void ScanButtonClicked(object sender, EventArgs s)
        {
            var scanPage = new ZXingScannerPage()
            {
                DefaultOverlayTopText = "バーコードを読み取ります",
                DefaultOverlayBottomText = "",
            };

            // スキャナページを表示
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                // スキャン停止
                scanPage.IsScanning = false;

                // PopAsyncで元のページに戻り、結果をダイアログで表示
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("スキャン完了", result.Text, "OK");
                });

                scanedData =(result.Text);
            };
        }

        public void InsertClicked(object sender, EventArgs e)
        {

            var InsertName = scanedData;
            //Userテーブルに適当なデータを追加する
            UserModel.insertUser(InsertName);

        }

        void SelectClicked(object sender, EventArgs e)
        {

            //Userテーブルの行データを取得
            var query = UserModel.selectUser(); //中身はSELECT * FROM [User]
            
            foreach (var user in query)
            {
                //Userテーブルの名前列をLabelに書き出す
                zx.Text = user.Name;

            }

           
        }
}