namespace TaskManagement.Infrastructure.option;
public record ApiOption
{
    public const string SectionName = "Api";

    public required OpenApiOption OpenApi { get; init; }
    public required string BarcodeScanner { get; init; }

    public record OpenApiOption
    {
        public static readonly string SectionName = $"{ApiOption.SectionName}:OpenApi";

        public required string Title { get; init; }
        public required string OpenApiRoutePattern { get; init; }
        public required string ScalarEndpointRoutePattern { get; init; }
        public required string ServerUrl { get; init; }
        public string SubPath => string.IsNullOrWhiteSpace(ServerUrl) ? string.Empty : new Uri(ServerUrl).AbsolutePath;
    }

   
}

