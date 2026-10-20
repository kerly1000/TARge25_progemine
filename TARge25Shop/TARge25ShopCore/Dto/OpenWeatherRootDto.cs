using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TARge25Shop.Core.Dto
{
    public class OpenWeatherRootDto
    {
        [JsonPropertyName("weather")]
        public List<OpenWeatherDto>? Weather { get; set; }
        }
    }
}

