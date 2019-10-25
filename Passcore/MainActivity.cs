using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using Core;

namespace Passcore
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
            string pass;
            int passLength = 16;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            

            EditText pKey_0 = FindViewById<EditText>(Resource.Id.pKey_0);
            EditText pKey_1 = FindViewById<EditText>(Resource.Id.pKey_1);
            EditText pKey_2 = FindViewById<EditText>(Resource.Id.pKey_2);
            CheckBox isHard = FindViewById<CheckBox>(Resource.Id.isHard);
            Button generate = FindViewById<Button>(Resource.Id.Generate);
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);

            seekBar.Max = 8;
            seekBar.Min = 0;
            
            seekBar.ProgressChanged += (sender, e) =>
            {
                if (seekBar.Progress <= 1)
                {
                    seekBar.Progress = 0; //0,5 -> 16bit
                    passLength = 16;
                }

                if (seekBar.Progress > 1 && seekBar.Progress <= 3)
                {
                    seekBar.Progress = 2; //1 -> 32bit
                    passLength = 32;
                }
                if (seekBar.Progress > 3 && seekBar.Progress <= 5)
                {
                    seekBar.Progress = 4; //1 -> 64bit
                    passLength = 64;
                }
                if (seekBar.Progress > 5 && seekBar.Progress <= 7)
                {
                    seekBar.Progress = 6; //1 -> 128bit
                    passLength = 128;
                }
                if (seekBar.Progress > 7 && seekBar.Progress <= 8)
                {
                    seekBar.Progress = 8; //0.5 -> 256bit
                    passLength = 256;
                }
            };

            generate.Click += (sender, e) =>
            {
                Core.Core core = new Core.Core();
                pass = core.EncryptMyPass(pKey_0.Text, pKey_1.Text, pKey_2.Text, isHard.Checked);
                //Toast.MakeText(this, pass, ToastLength.Long);
                if (pass != null && pass != string.Empty && pass != "")
                {
                    var alertDialog = new Android.App.AlertDialog.Builder(this).Create();
                    alertDialog.SetTitle(Resources.GetString(Resource.String.alert_title));
                    alertDialog.SetMessage(Resources.GetString(Resource.String.pass_front) + " " + pass.Substring(0, passLength));
                    alertDialog.SetButton(Resources.GetString(Resource.String.ok), (s, a) => { });
                    alertDialog.SetButton2(Resources.GetString(Resource.String.copy_to_clipboard), async (s, a) =>
                    {

                        await Clipboard.SetTextAsync(pass.Substring(0, passLength));
                        var alertDialog2 = new Android.App.AlertDialog.Builder(this).Create();
                        alertDialog2.SetTitle(Resources.GetString(Resource.String.alert_title));
                        alertDialog2.SetMessage(Resources.GetString(Resource.String.copy_success));
                        alertDialog2.SetButton(Resources.GetString(Resource.String.ok), (n, m) => { });
                        alertDialog2.Show();

                    });
                    alertDialog.Show();
                }
                else
                {
                    var alertDialog = new Android.App.AlertDialog.Builder(this).Create();
                    alertDialog.SetTitle(Resources.GetString(Resource.String.alert_title));
                    alertDialog.SetMessage(Resources.GetString(Resource.String.null_return));
                    alertDialog.SetButton(Resources.GetString(Resource.String.ok), (s, a) => { });
                    alertDialog.SetButton2(Resources.GetString(Resource.String.detail), (s, a) =>
                    {

                        var alertDialog2 = new Android.App.AlertDialog.Builder(this).Create();
                        alertDialog2.SetTitle(Resources.GetString(Resource.String.error_detail));
                        alertDialog2.SetMessage(Resources.GetString(Resource.String.null_return_code)+"\n"+ Resources.GetString(Resource.String.null_return_disc));
                        alertDialog2.SetButton(Resources.GetString(Resource.String.ok), (n, m) => { });
                        alertDialog2.Show();

                    });
                    alertDialog.Show();
                }
            };
        }

    }
}

