using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ToDoList.Models;
using ToDoList.ViewModel;

namespace ToDoList.Services
{
    public class ToDoService
    {
        private readonly HttpClient _httpClient;

        public ToDoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ToDoAPI");
        }

        public async Task<IEnumerable<ToDoViewData>> GetAll()
        {
            var response = await _httpClient.GetAsync("api/ToDo");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ToDoViewData>>() ?? new List<ToDoViewData>();
        }

        public async Task Create(ToDoViewData todo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ToDo", todo);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id) 
        {
            var response = await _httpClient.DeleteAsync($"api/Todo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
