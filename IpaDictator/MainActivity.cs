using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Speech;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Threading;

using IpaTranscriber;

namespace IpaDictator
{
    [Activity(Label = "The IPA Dictator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private bool isRecording;
        private readonly int VOICE = 10;
        private TextView textBoxOrthography;
        private TextView textBoxIPA;
        private ImageButton recButton;
        private Button buttonTranscribe;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set the isRecording flag to false (not recording)
            isRecording = false;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            buttonTranscribe = FindViewById<Button>(Resource.Id.buttonTranslate);
            buttonTranscribe.Click += buttonTranscribe_OnClick;

            // get the resources from the layout
            recButton = FindViewById<ImageButton>(Resource.Id.btnRecord);
            //textBox = FindViewById<TextView>(Resource.Id.textYourText);
            textBoxOrthography = FindViewById<TextView>(Resource.Id.textViewOrthography);
            textBoxIPA = FindViewById<TextView>(Resource.Id.textViewIPA);
            // check to see if we can actually record - if we can, assign the event to the button
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                // no microphone, no recording. Disable the button and output an alert
                var alert = new AlertDialog.Builder(recButton.Context);
                alert.SetTitle("You don't seem to have a microphone to record with");
                alert.SetPositiveButton("OK", (sender, e) =>
                {
                    textBoxOrthography.Text = "No microphone present";
                    recButton.Enabled = false;
                    return;
                });

                alert.Show();
            }
            else
                recButton.Click += delegate
                {
                    // change the text on the button
                    //recButton.Text = "End Recording";
                    isRecording = !isRecording;
                    if (isRecording)
                    {
                        // create the intent and start the activity
                        var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                        // put a message on the modal dialog
                        voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));

                        // if there is more then 1.5s of silence, consider the speech over
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                        // you can specify other languages recognised here, for example
                        // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
                        // if you wish it to recognise the default Locale language and German
                        // if you do use another locale, regional dialects may not be recognised very well

                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                        StartActivityForResult(voiceIntent, VOICE);
                    }
                };
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = "";
                        textBoxOrthography.Text = "";
                        textBoxIPA.Text = "";
                        textInput = textBoxOrthography.Text + matches[0];

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);

                        textBoxOrthography.Text = textInput;
                        textBoxIPA.Text = "";
                    }
                    else
                        textBoxOrthography.Text = "No speech was recognised";
                    // change the text back on the button
                    //recButton.Text = "Start Recording";
                }
            }

            base.OnActivityResult(requestCode, resultVal, data);
        }

        public void buttonTranscribe_OnClick(object sender, System.EventArgs e)
        {
            textBoxIPA.Text = "";
            string phrase = textBoxOrthography.Text;
            IpaTranscriber.IpaTranscriber ipa = new IpaTranscriber.IpaTranscriber();
            var regex = new Regex(@"\b[\s,\.-:;]*");
            //var phrase = "I am a student";
            var words = regex.Split(phrase.ToLower()).Where(x => !string.IsNullOrEmpty(x));
            string phrase_ipa = "";

            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetCancelable(true);
            progressDialog.SetMessage("Transcribing to IPA...");
            progressDialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            progressDialog.Progress = 0;
            progressDialog.Max = 100;
            progressDialog.Show();

            // define and run background thread
            new Thread(new ThreadStart(delegate
            {
                int wordcount = words.Count();
                int wordweight = (int)Math.Floor(100.0 / wordcount);

                foreach (var word in words)
                {
                    progressDialog.Progress += wordweight;
                    //string result = ipa.Transcribe("effect", "noun");
                    phrase_ipa += ipa.Transcribe(word) + " ";
                }

                progressDialog.Progress = 100;
                progressDialog.SetMessage("Transcription complete.");

                RunOnUiThread(() =>
                {
                    phrase_ipa = "/" + phrase_ipa.Trim() + "/";
                    textBoxIPA.Text = phrase_ipa;
                    progressDialog.Hide();
                });
            })).Start();
        }
    }
}
