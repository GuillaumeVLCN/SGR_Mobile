using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGR_Mobile.Vues
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccueilAdmin : ContentPage
    {
        public AccueilAdmin()
        {
            InitializeComponent();
            Button addButton = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            addButton.Clicked += OnAddButtonClicked;

            StackLayout stackLayout = new StackLayout
            {
                Children = { addButton }
            };

            Content = stackLayout;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            // Créer votre formulaire avec les champs nécessaires
            Entry nomPlatEntry = new Entry { Placeholder = "Nom du plat" };
            Entry typePlatEntry = new Entry { Placeholder = "Type de plat" };
            Entry prixUnitaireEntry = new Entry { Placeholder = "Prix unitaire" };
            Entry sousCategorieIdEntry = new Entry { Placeholder = "ID de la sous-catégorie" };
            Button submitButton = new Button { Text = "Envoyer" };
            submitButton.Clicked += async (s, args) =>
            {
                // Récupérer les valeurs des champs
                
                string nomPlat = nomPlatEntry.Text;
                string typePlat = typePlatEntry.Text;
                decimal prixUnitaire = decimal.Parse(prixUnitaireEntry.Text);
                int sousCategorieId = int.Parse(sousCategorieIdEntry.Text);

                // Créer l'objet contenant les données à envoyer à l'API
                VotreObjetDonnees donnees = new VotreObjetDonnees
                {
                    NomPlat = nomPlat,
                    TypePlat = typePlat,
                    PrixUnitaireCarte = prixUnitaire,
                    IdSousCategorie = sousCategorieId
                };

                // Appeler l'API pour envoyer les données à la base de données
                HttpClient client = new HttpClient();
                string url = "https://apisgr.alwaysdata.net/controllers/plat/create.php";
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(donnees);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Traiter la réponse de l'API
                if (response.IsSuccessStatusCode)
                {
                    // Les données ont été envoyées avec succès
                    await DisplayAlert("Succès", "Les données ont été envoyées avec succès.", "OK");
                }
                else
                {
                    // Une erreur s'est produite lors de l'envoi des données
                    await DisplayAlert("Erreur", "Une erreur s'est produite lors de l'envoi des données.", "OK");
                }
            };

            StackLayout formLayout = new StackLayout
            {
                Children = { nomPlatEntry, typePlatEntry, prixUnitaireEntry, sousCategorieIdEntry, submitButton }
            };

            Content = formLayout;
        }


        public class VotreObjetDonnees
        {
            public int IdPlat { get; set; }
            public string NomPlat { get; set; }
            public string TypePlat { get; set; }
            public decimal PrixUnitaireCarte { get; set; }
            public int IdSousCategorie { get; set; }
        }
    }
}