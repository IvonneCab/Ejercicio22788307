namespace Ejercicio22788307
{
    public partial class MainPage : ContentPage
    {

        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultado());
        }

        private async void CalcularBtn_Clicked(object sender, EventArgs e)
        {

            string A = Entryprimernumero.Text;
            string B = Entrysegundonumero.Text;

            // Convertir los valores a números
            if (!int.TryParse(A, out int a) || !int.TryParse(B, out int b))
            {
                await DisplayAlert("Error", "Los valores ingresados deben ser números enteros.", "Aceptar");
                return;
            }

            // Calcular la suma
            double Calcular = (Math.Pow(a + b, 2)) / 3;
            // Asignar el resultado a la etiqueta
            labelresultado.Text = Calcular.ToString();
            if (_editResultadoId == 0)
            {


                await _dbService.Create(new Resultado
                {
                    A = Entryprimernumero.Text,
                B = Entrysegundonumero.Text,
                    Calcular = labelresultado.Text


                });
            }
            else
            {
                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    A = Entryprimernumero.Text,
                   B = Entrysegundonumero.Text,
                    Calcular = labelresultado.Text
                });
                _editResultadoId = 0;
            }
            Entryprimernumero.Text = string.Empty;
            Entrysegundonumero.Text = string.Empty;
            labelresultado.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetResultado();

        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.A;
                    Entrysegundonumero.Text = resultado.B;
                    labelresultado.Text = resultado.Calcular;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }


    }

}
