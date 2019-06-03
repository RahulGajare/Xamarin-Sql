using FundooSQL.DataHandler;
using FundooSQL.Model;
using FundooSQL.Views;
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
	public partial class GridPage : ContentPage
	{
		public GridPage ()
		{
			InitializeComponent ();
		}

        private void TakeaNote_Clicked(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new TakeaNote());
        }

        public void GridView(IList<Note> list)
        {
            try
            {
                ///// Creates column defination of width 170
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                //// Creates number of columns as lables are added
                for (int row = 0; row < list.Count; row++)
                {
                    //// Adds new row after 2 labelss
                    if (row % 2 == 0)
                    {
                        GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    }
                }

                var index = -1;

                //// Adds label to row and columns
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        Note data = null;

                        index++;
                        if (index < list.Count)
                        {
                            data = list[index];
                        }

                        //// Creates Labels
                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Title,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        //// Created label key
                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.NoteID.ToString(),
                            IsVisible = false
                        };

                        //// Content view
                        var content = new Xamarin.Forms.Label
                        {
                            Text = data.Info1,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        //// Creates stack layout for each label
                        StackLayout layout = new StackLayout()
                        {
                            Spacing = 2,
                            Margin = 2,
                            BackgroundColor = Color.White,
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);

                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.Content = layout;




                        //// Tap gesture recognizer
                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout stacklayout = (StackLayout)sender;
                            IList<Xamarin.Forms.View> item = stacklayout.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;

                            Navigation.PushModalAsync(new UpdateNote(Keyval));
                        };
                        GridLayout.Children.Add(frame, columnIndex, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            try 
            {
                
           
                var personList = await App.SQLiteDb.GetItemsAsync();

                //// Gets all the notes
                // IList<NotesData> notes = await this.notesDatabase.GetNotesAsync();

                //// Gets all labels


                //// Checks if notes is not null
                if (personList != null)
                {

                    this.GridView(personList);
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}