using Flunt.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IWantApp.Domain.Products;

public class Category: Entity
{    
    public string Name { get; set; }
 
    public bool Active { get; set; }
    public Category(string name) {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name ´e obrigatorio");
            AddNotifications(contract);

        Name = name;
        Active = true;
        
    }

}
