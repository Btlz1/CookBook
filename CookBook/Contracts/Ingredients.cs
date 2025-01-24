using CookBook.Models;

namespace CookBook.Contracts;

public record IngredientRC(string Name);
public record IngredientVm(int IngredientId, double Quantity, string Unit);
public record IngredientsVm(List<IngredientVm> Ingredients);
public record CreateIngredientDto(int IngredientId, double Quantity, string Unit);

public record UpdateIngredientDto(int IngredientId, double Quantity, string Unit);
