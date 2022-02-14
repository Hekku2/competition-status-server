using System.Collections.Generic;

namespace Api.Util;

public class CorsSettings
{
    public List<string> AllowedOrigins { get; set; } = new List<string>();
}
