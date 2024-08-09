using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace WpfApplication1.Utils
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiKey = "adaptive"; // Replace with your actual API key

        static ApiClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", apiKey); // Add the API key to the headers
        }

        public static async Task<List<Broker>> GetAllBrokers()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://oao-be.koyeb.app/brokers");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse<Broker> apiResponse = JsonConvert.DeserializeObject<ApiResponse<Broker>>(responseBody);

                if (apiResponse.meta.code == 200)
                {
                    // print data from API (only broker name)
                    // foreach (var broker in apiResponse.data)
                    // {
                    //     Console.WriteLine(broker.nama);
                    // }
                    return apiResponse.data;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Broker>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Broker>();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Broker>();
            }
        }

        public static async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://oao-be.koyeb.app/customers");
                Console.WriteLine(response.StatusCode);
                response.EnsureSuccessStatusCode();
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Data tidak ditemukan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer>();
                }
                
                if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    MessageBox.Show("Terjadi kesalahan pada server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer>();
                }
                
                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Unauthorized", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer>();
                }
                
                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse<Customer> apiResponse = JsonConvert.DeserializeObject<ApiResponse<Customer>>(responseBody);

                if (apiResponse.meta.code == 200)
                {
                    // print data from API (only broker name)
                    // foreach (var customer in apiResponse.data)
                    // {
                    //     foreach (var broker in customer.Broker)
                    //     {
                    //         Console.WriteLine(broker.nama);
                    //     }
                    // }
                    // print count of data
                    return apiResponse.data;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Customer>();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Customer>();
            }
        }


        public static async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                string json = JsonConvert.SerializeObject(customer);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://oao-be.koyeb.app/customer", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse<object> apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);

                if (apiResponse.meta.code == 201)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }

    public class ApiResponse<T>
    {
        public Meta meta { get; set; }
        public List<T> data { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
