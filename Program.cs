using System.Net.Http.Json;
using System.Text.Json;

class Program
{
    private static readonly HttpClient _httpCliet = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:11434")
    };
    static async Task Main()
    {
        Console.Write("Digite uma pergunta: ");
        string prompt = Console.ReadLine();

        var model = new
        {
            Model = "llama3",
            Prompt = prompt,
            Stream = false
        };

        var response = await _httpCliet.PostAsJsonAsync("/api/generate", model);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();
            Console.WriteLine("\nResposta da IA: ");
            Console.WriteLine(result.GetProperty("response").ToString());
        }
        else
        {
            Console.Write("Error: {0}", response.StatusCode);
        }

    }
}