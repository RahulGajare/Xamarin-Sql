using FundooSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FundooSQL.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TakeaNote : ContentPage
	{
		public TakeaNote ()
		{
			InitializeComponent ();
		}


        protected override bool OnBackButtonPressed()
        {
            Note note = new Note();
            note.Title = title.Text;
            note.Info1 = info.Text;

             App.SQLiteDb.SaveItemAsync(note);

            DisplayAlert("Alert","Notes Saved","OK");

            return base.OnBackButtonPressed();
        }


    }
}