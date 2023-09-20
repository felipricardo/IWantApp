using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    // Propriedades da entidade
    public string Name { get; private set; }
    public bool Active { get; private set; }

    // Construtor para inicializar a entidade e validar os campos
    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    // Método para validar a entidade usando Flunt
    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");

        AddNotifications(contract);
    }

    // Método para editar informações da categoria
    public void EditInfo(string name, bool active, string editedBy)
    {
        Name = name;
        Active = active;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;

        Validate();
    }
}
