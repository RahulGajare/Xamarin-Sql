using FundooSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FundooSQL.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateNote : ContentPage
	{
        string key = null;
		public UpdateNote (string val)
		{
			InitializeComponent ();
            key = val;
            Updatedata();


        }

        public async void Updatedata()
        {
           
            Note note = await App.SQLiteDb.GetItemAsync(key);
            Entrytitle.Text = note.Title;
            Editorinfo.Text = note.Info1;
        }

        protected override bool OnBackButtonPressed()
        {
            Note note = new Note()
            {
                Title = Entrytitle.Text,
                Info1 = Editorinfo.Text,
                NoteID = Convert.ToInt32(this.key)
        };

            App.SQLiteDb.SaveItemAsync(note);
            return base.OnBackButtonPressed();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            Note note = new Note()
            {
                Title = Entrytitle.Text,
                Info1 = Editorinfo.Text,
                NoteID = Convert.ToInt32(this.key)
            };
            App.SQLiteDb.DeleteItemAsync(note);

            Navigation.PopModalAsync();
        }
    }
}