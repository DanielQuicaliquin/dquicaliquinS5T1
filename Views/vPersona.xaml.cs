using dquicaliquinS5T1.Models;

namespace dquicaliquinS5T1.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";

        List<Persona> people = App.PersonRepo.GetAllPeople();
        listaPersona.ItemsSource = people;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var id = (int)((Button)sender).CommandParameter;
        App.PersonRepo.DeletePerson(id);
        // Luego puedes volver a cargar la lista de personas para actualizar la vista
        btnObtener_Clicked(sender, e);
    }



    private Persona personaEditada;

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        personaEditada = (sender as Button)?.BindingContext as Persona;
        txtNombre.Text = personaEditada.Nombre;
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        if (personaEditada == null) // Si no hay persona editada, agregamos una nueva
        {
            statusMessage.Text = "";

            Persona nuevaPersona = new Persona() { Nombre = txtNombre.Text };
            App.PersonRepo.AddNewPerson(nuevaPersona);
            statusMessage.Text = App.PersonRepo.statusMessage;
        }
        else // Si hay una persona editada, actualizamos sus datos
        {
            statusMessage.Text = "";

            personaEditada.Nombre = txtNombre.Text;
            App.PersonRepo.UpdatePerson(personaEditada);
            statusMessage.Text = App.PersonRepo.statusMessage;

            // Limpiamos la variable de edición después de guardar los cambios
            personaEditada = null;
        }

        // Limpiamos el Entry después de agregar/editar la persona
        txtNombre.Text = string.Empty;

        // Volvemos a cargar la lista de personas
        btnObtener_Clicked(sender, e);
    }











}