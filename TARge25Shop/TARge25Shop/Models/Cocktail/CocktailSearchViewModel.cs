namespace TARge25Shop.Models.Cocktail
{
    public class CocktailSearchViewModel
    {
        public string? SearchName { get; set; }

        public List<CocktailViewModel> Cocktails { get; set; }
            = new();
    }
}