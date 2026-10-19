using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TARge25Shop.Core.Dto
{
    public class CocktailRootDto
    {
        [JsonPropertyName("drinks")]
        public List<CocktailDto>? Drinks { get; set; }
    }
}
